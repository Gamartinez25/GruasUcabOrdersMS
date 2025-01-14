using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Infrastructure.Database;
using OrdersMS.Infrastructure.Repositories;
using System.Reflection;
using OrdersMS.Application.Validators.TarifaValidators;
using OrdersMS.Application.Mappers.TarifaMappers;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Mappers.CostoAdicionalMappers;
using OrdersMS.Application.Validators.CostoAdicionalValidators;
using MassTransit;
using OrdersMS.Application.Saga;
using OrdersMS.Domain.Entities;
using OrdersMS.Infrastructure.Services;
using OrdersMS.Core.Services.MsProviders;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Infrastructure.Mappers;
using OrdersMS.Core.Services.MsUsers;

var builder = WebApplication.CreateBuilder(args);
var applicationAssembly = Assembly.Load("OrdersMS.Application");
// Add services to the container.

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5101); // Puerto HTTP
    options.ListenAnyIP(7057); // Puerto HTTPS
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderMsDbContext, OrderMsContext>();
builder.Services.AddTransient<ITarifaRepository, TarifaRepository>();
builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
builder.Services.AddTransient<ICostoAdicionalRepository, CostoAdicionalRepository>();
builder.Services.AddTransient<IOrdenMapper, OrdenMapper>();
builder.Services.AddTransient<ISalidaCostoAdicionalMapper, SalidaCostoAdicionalMapper>();
builder.Services.AddTransient<IVehiculosAsignadosRepository, VehiculosAsignadosRepository>();


var dbConnectionString = builder.Configuration.GetValue<string>("DBConnectionString");
builder.Services.AddDbContext<OrderMsContext>(options => options.UseNpgsql(dbConnectionString));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddAutoMapper(typeof(EntradaTarifaMapper));
builder.Services.AddAutoMapper(typeof(SalidaTarifaMapper));
builder.Services.AddAutoMapper(typeof(EntradaOrdenMapper));
builder.Services.AddAutoMapper(typeof(EntradaCostosAdicionalesMapper));
builder.Services.AddAutoMapper(typeof(VehiculoMapper));




builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CrearTarifaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CostoAdicionalValidator>();

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddSagaStateMachine<MaquinaEstadoOrden, EstadoOrden>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Optimistic;
            r.AddDbContext<DbContext, OrderMsContext>((provider, options) =>
            {
                options.UseNpgsql(dbConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(OrderMsContext).Assembly.GetName().Name);
                });
            });
        });

    cfg.UsingRabbitMq((context, rabbitCfg) =>
    {
        var rabbitConfig = builder.Configuration.GetSection("Rabbit");
        var host = rabbitConfig["Host"];
        var username = rabbitConfig["Username"];
        var password = rabbitConfig["Password"];
        rabbitCfg.Host(host, h =>
        {
            h.Username(username);
            h.Password(password);
        });

        rabbitCfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHttpClient<IMsProvidersServices, MsProvidersServices>(client =>
{
    var providerConfig = builder.Configuration.GetSection("ServiosUrl");
    client.BaseAddress = new Uri(providerConfig["MsProvidersBase"]);//poner el url del microservicio provider
});
builder.Services.AddHttpClient<IUserMsService, UserMsService>(client =>
{
    var userConfig = builder.Configuration.GetSection("ServiosUrl");
    client.BaseAddress = new Uri(userConfig["MsUserBase"]);//poner el url del microservicio provider
});
builder.Services.AddHttpClient<IGoogleService, GoogleService>(client =>
{
    var googleConfig = builder.Configuration.GetSection("ServiosUrl");

    client.BaseAddress = new Uri(googleConfig["Google"]);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins() // Solo permite solicitudes desde este origen
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
