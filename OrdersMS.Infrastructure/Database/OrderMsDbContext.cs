using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Core.Database;
using OrdersMS.Domain.Entities;
using System.Reflection;


namespace OrdersMS.Infrastructure.Database
{
    public class OrderMsContext : DbContext, IOrderMsDbContext
    {
        public OrderMsContext(
        DbContextOptions<OrderMsContext> options
    )
        : base(options) { }
        public DbContext DbContext
        {
            get { return this; }
        }
        

        public DbSet<Asegurado> Asegurado { get; set; } = null!;
        public DbSet<CostoAdicional> CostoAdicional { get; set; } = null!;
        public DbSet<OrdenCostoAdicional> OrdenCostoAdicional { get; set; } = null!;
        public DbSet<OrdenDeServicio> OrdenDeServicio { get; set; } = null!;
        public DbSet<Poliza> Poliza { get; set; } = null!;
        public DbSet<PolizaAsegurado> PolizaAsegurado { get; set; } = null!;
        public DbSet<Tarifa> Tarifa { get; set; } = null!;
        public DbSet<EstadoOrden> EstadoOrden { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Obtener la fecha y hora actual
            var currentDateTime = DateTime.UtcNow;

            // Iterar a través de las entidades rastreadas por el ChangeTracker
            foreach (var entry in ChangeTracker.Entries<Base>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FechaCreacion = currentDateTime; // Fecha de creación
                        entry.Entity.CreadoPor = "DefaultUser";   // Aquí puedes obtener el usuario autenticado
                        break;

                    case EntityState.Modified:
                        entry.Property(e => e.FechaCreacion).IsModified = false;
                        entry.Property(e => e.CreadoPor).IsModified = false;
                        entry.Entity.FechaActualizacion = currentDateTime; // Fecha de actualización
                        entry.Entity.ActualizadoPor = "DefaultUser";   // Aquí también puedes obtener el usuario autenticado
                        break;
                }
            }

            
            return await base.SaveChangesAsync(cancellationToken);
        }
       
    }
}
