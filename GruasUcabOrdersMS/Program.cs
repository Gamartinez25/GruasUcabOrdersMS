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

var builder = WebApplication.CreateBuilder(args);
var applicationAssembly = Assembly.Load("OrdersMS.Application");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderMsDbContext, OrderMsContext>();
builder.Services.AddTransient<ITarifaRepository, TarifaRepository>();
builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
var dbConnectionString = builder.Configuration.GetValue<string>("DBConnectionString");
builder.Services.AddDbContext<OrderMsContext>(options => options.UseNpgsql(dbConnectionString));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddAutoMapper(typeof(EntradaTarifaMapper));
builder.Services.AddAutoMapper(typeof(SalidaTarifaMapper));
builder.Services.AddAutoMapper(typeof(EntradaOrdenMapper));
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CrearTarifaValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
