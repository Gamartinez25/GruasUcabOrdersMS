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
            builder.Property(s => s.CostoBase).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(s => s.DistanciaKm).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(s => s.CostoPorKm).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(s => s.Estatus).IsRequired().HasMaxLength(20);
            builder.Property(s => s.CreadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.ActualizadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.FechaCreacion).HasDefaultValue(null);
            builder.Property(s => s.FechaActualizacion).HasDefaultValue(null);
            builder.HasOne(t => t.Poliza)  // Tarifa tiene una Poliza
              .WithOne(p => p.Tarifa); // Poliza tiene una Tarifa

        }
    }
}
