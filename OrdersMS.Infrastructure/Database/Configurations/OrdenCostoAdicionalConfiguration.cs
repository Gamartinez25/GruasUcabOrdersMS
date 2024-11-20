using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class OrdenCostoAdicionalConfiguration : IEntityTypeConfiguration<OrdenCostoAdicional>
    {
        public void Configure(EntityTypeBuilder<OrdenCostoAdicional> builder)
        {
            builder.HasKey(oca => new { oca.OrdenDeServicioId, oca.CostoAdicionalId }); // Clave compuesta

            builder.Property(s=> s.Costo).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(s=> s.Estatus).IsRequired().HasMaxLength(50);
            builder.Property(s => s.CreadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.ActualizadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.FechaCreacion).HasDefaultValue(null);
            builder.Property(s => s.FechaActualizacion).HasDefaultValue(null);

            builder.HasOne(oca => oca.OrdenDeServicio) // OrdenCostoAdicional tiene una OrdenDeServicio
                   .WithMany(o => o.OrdenCostosAdicionales)  // OrdenDeServicio tiene muchas relaciones con CostoAdicional
                   .HasForeignKey(oca => oca.OrdenDeServicioId); // Clave foránea en la tabla intermedia

            builder.HasOne(oca => oca.CostoAdicional) // OrdenCostoAdicional tiene un CostoAdicional
                   .WithMany(c => c.OrdenCostosAdicionales)  // CostoAdicional tiene muchas relaciones con OrdenDeServicio
                   .HasForeignKey(oca => oca.CostoAdicionalId); // Clave foránea en la tabla intermedia
        }
    }
}
