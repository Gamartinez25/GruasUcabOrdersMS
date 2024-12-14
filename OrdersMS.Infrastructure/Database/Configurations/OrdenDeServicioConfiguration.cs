using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class OrdenDeServicioConfiguration : IEntityTypeConfiguration<OrdenDeServicio>
    {
        public void Configure(EntityTypeBuilder<OrdenDeServicio> builder)
        {
            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.Fecha).IsRequired();
            builder.Property(o => o.DetallesIncidente).IsRequired().HasMaxLength(300);
            builder.Property(o => o.DireccionOrigen).IsRequired().HasMaxLength(200);
            builder.Property(o => o.DireccionDestino).IsRequired().HasMaxLength(200);
            builder.Property(o => o.CantidadKmExtra).IsRequired();
            builder.Property(o => o.CostoServiciosAdicionales).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(o => o.CostoTotalKmExtra).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(o => o.CostoTotal).IsRequired().HasColumnType("numeric(12,2)");
            builder.Property(o => o.NombreDenunciante).IsRequired().HasMaxLength(100);
            builder.Property(o => o.TipoDocumentoDenunciante).IsRequired().HasMaxLength(1);
            builder.Property(o => o.NumeroDocumentoDenunciante).IsRequired().HasMaxLength(8);
            builder.Property(s => s.CreadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.ActualizadoPor).HasDefaultValue(null).HasMaxLength(100);
            builder.Property(s => s.FechaCreacion).HasDefaultValue(null);
            builder.Property(s => s.FechaActualizacion).HasDefaultValue(null);

            builder.HasMany(o => o.OrdenCostosAdicionales) // OrdenDeServicio tiene muchas relaciones con CostoAdicional
                   .WithOne(oca => oca.OrdenDeServicio)     // OrdenCostoAdicional tiene una OrdenDeServicio
                   .HasForeignKey(oca => oca.OrdenDeServicioId); // La clave foránea está en la tabla intermedia
            
            builder.HasOne(o => o.PolizaAsegurado)  // OrdenDeServicio tiene una PolizaAsegurado
                  .WithMany(p => p.OrdenesDeServicio) // PolizaAsegurado tiene muchas OrdenDeServicio
                  .HasForeignKey(o => o.PolizaAseguradoId); // La clave foránea está en OrdenDeServicio

        }
    }
}
