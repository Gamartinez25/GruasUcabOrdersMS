using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class CostoAdicionalConfiguration : IEntityTypeConfiguration<CostoAdicional>
    {
        public void Configure(EntityTypeBuilder<CostoAdicional> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Nombre).IsRequired().HasMaxLength(60);
            builder.HasMany(c => c.OrdenCostosAdicionales) // CostoAdicional tiene muchas relaciones con OrdenDeServicio
              .WithOne(oca => oca.CostoAdicional)    // OrdenCostoAdicional tiene un CostoAdicional
              .HasForeignKey(oca => oca.CostoAdicionalId);
        }
    }
}
