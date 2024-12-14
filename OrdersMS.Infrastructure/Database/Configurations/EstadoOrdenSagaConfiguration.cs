using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Database.Configurations
{
    public class EstadoOrdenSagaConfiguration : IEntityTypeConfiguration<EstadoOrden>
    {
        public void Configure(EntityTypeBuilder<EstadoOrden> builder)
        {
            builder.ToTable("EstadoOrden");
            builder.HasKey(x => x.CorrelationId); 

            builder.Property(x => x.EstadoActual)
                .IsRequired()
                .HasMaxLength(64); 

            builder.Property(x => x.CorrelationId).ValueGeneratedNever(); 
        }
    }
}
