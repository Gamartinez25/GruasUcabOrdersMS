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
    public class PolizaAseguradoConfiguration : IEntityTypeConfiguration<PolizaAsegurado>
    {
        public void Configure(EntityTypeBuilder<PolizaAsegurado> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.FechaInicioCobertura).IsRequired().HasMaxLength(10);
            builder.Property(s => s.FechaVencimientoCobertura).IsRequired().HasMaxLength(10);
            builder.Property(s => s.Marca).IsRequired().HasMaxLength(40);
            builder.Property(s => s.Modelo).IsRequired().HasMaxLength(40);
            builder.Property(s => s.Anio).IsRequired().HasMaxLength(4);
            builder.Property(s => s.Placa).IsRequired().HasMaxLength(6);
            builder.Property(s => s.TipoVehiculo).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Color).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Estatus).IsRequired().HasMaxLength(20);

            builder.HasOne(p => p.Asegurado)       // PolizaAsegurado tiene un Asegurado
              .WithMany(a => a.PolizasAsegurados)  // Asegurado tiene muchas Polizas
              .HasForeignKey(p => p.AseguradoId);
          
            builder.HasMany(p => p.OrdenesDeServicio) // PolizaAsegurado tiene muchas OrdenDeServicio
                   .WithOne(o => o.PolizaAsegurado)  // OrdenDeServicio tiene una PolizaAsegurado
                   .HasForeignKey(o => o.PolizaAseguradoId); // La clave foránea está en la entidad OrdenDeServicio

            builder.HasOne(pa => pa.Poliza)
               .WithOne(p => p.PolizaAsegurado)
               .HasForeignKey<PolizaAsegurado>(pa => pa.PolizaId);  // Clave foránea en PolizaAsegurado

        }
    }
}
