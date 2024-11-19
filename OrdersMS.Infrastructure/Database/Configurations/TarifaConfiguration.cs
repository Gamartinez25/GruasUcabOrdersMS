using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class TarifaConfiguration : IEntityTypeConfiguration<Tarifa>
    {
        public void Configure(EntityTypeBuilder<Tarifa> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(s => s.CostoBase).IsRequired().HasPrecision(10,2);
            builder.Property(s => s.DistanciaKm).IsRequired().HasPrecision(10,2);
            builder.Property(s => s.CostoPorKm).IsRequired().HasPrecision(10, 2);
            builder.Property(s => s.Estatus).IsRequired().HasMaxLength(20);
            builder.HasOne(t => t.Poliza)  // Tarifa tiene una Poliza
              .WithOne(p => p.Tarifa); // Poliza tiene una Tarifa

        }
    }
}
