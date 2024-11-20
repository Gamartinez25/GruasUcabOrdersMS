using Microsoft.EntityFrameworkCore;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Database
{
    public interface IOrderMsDbContext
    {
        DbContext DbContext { get; }
        DbSet<Asegurado> Asegurado { get; set; }
        DbSet<CostoAdicional> CostoAdicional { get; set; }
        DbSet<OrdenCostoAdicional> OrdenCostoAdicional { get; set; }
        DbSet<OrdenDeServicio> OrdenDeServicio { get; set; }
        DbSet<Poliza> Poliza { get; set; }
        DbSet<PolizaAsegurado> PolizaAsegurado { get; set; }
        DbSet<Tarifa> Tarifa { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
