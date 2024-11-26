using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class PolizaConfiguration : IEntityTypeConfiguration<Poliza>
    {
        public void Configure(EntityTypeBuilder<Poliza> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Nombre).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Costo).IsRequired().HasPrecision(10, 2);
            builder.Property(s => s.Descripcion).IsRequired().HasMaxLength(50);
            builder.HasOne(p => p.Tarifa)  // Poliza tiene una Tarifa
               .WithOne(t => t.Poliza) // Tarifa tiene una Poliza
               .HasForeignKey<Poliza>(p => p.TarifaId);

            builder.HasOne(p => p.PolizaAsegurado)
             .WithOne(pa => pa.Poliza)
             .HasForeignKey<PolizaAsegurado>(pa => pa.PolizaId);  // La clave foránea está en PolizaAsegurado



        }
    }
}
