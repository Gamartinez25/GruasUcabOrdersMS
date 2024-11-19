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
            builder.Property(o => o.Direccion).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Estatus).IsRequired().HasMaxLength(30);
            builder.Property(o => o.CantidadKmExtra).IsRequired();
            builder.Property(o => o.CostoServiciosAdicionales).IsRequired().HasPrecision(12, 2);
            builder.Property(o => o.CostoTotalKm).IsRequired().HasPrecision(12, 2);
            builder.Property(o => o.CostoTotal).IsRequired().HasPrecision(12, 2);
            builder.Property(o => o.NombreDenunciante).IsRequired().HasMaxLength(100);
            builder.Property(o => o.TipoDocumentoDenunciante).IsRequired().HasMaxLength(1);
            builder.Property(o => o.NumeroDocumentoDenunciante).IsRequired().HasMaxLength(8);

            
            builder.HasMany(o => o.OrdenCostosAdicionales) // OrdenDeServicio tiene muchas relaciones con CostoAdicional
                   .WithOne(oca => oca.OrdenDeServicio)     // OrdenCostoAdicional tiene una OrdenDeServicio
                   .HasForeignKey(oca => oca.OrdenDeServicioId); // La clave foránea está en la tabla intermedia
            
            builder.HasOne(o => o.PolizaAsegurado)  // OrdenDeServicio tiene una PolizaAsegurado
                  .WithMany(p => p.OrdenesDeServicio) // PolizaAsegurado tiene muchas OrdenDeServicio
                  .HasForeignKey(o => o.PolizaAseguradoId); // La clave foránea está en OrdenDeServicio

        }
    }
}
