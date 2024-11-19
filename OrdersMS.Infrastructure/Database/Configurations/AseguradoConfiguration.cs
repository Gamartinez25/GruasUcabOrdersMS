using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class AseguradoConfiguration : IEntityTypeConfiguration<Asegurado>
    {
        public void Configure(EntityTypeBuilder<Asegurado> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Nombres).IsRequired().HasMaxLength(60);
            builder.Property(s => s.Apellidos).IsRequired().HasMaxLength(60);
            builder.Property(s => s.FechaNacimiento).IsRequired().HasMaxLength(10);
            builder.Property(s => s.TipoDocumento).IsRequired().HasMaxLength(1);
            builder.Property(s => s.NumeroDocumento).IsRequired().HasMaxLength(8);
            builder.HasMany(a => a.PolizasAsegurados)  // Un Asegurado tiene muchas Polizas
               .WithOne(p => p.Asegurado)       // Una Poliza tiene un Asegurado
               .HasForeignKey(p => p.AseguradoId);
        }
    }
}
