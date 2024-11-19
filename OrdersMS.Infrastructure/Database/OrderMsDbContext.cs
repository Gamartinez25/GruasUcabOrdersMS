using Microsoft.EntityFrameworkCore;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
