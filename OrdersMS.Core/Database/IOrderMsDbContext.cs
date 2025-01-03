using Microsoft.EntityFrameworkCore;
using OrdersMS.Domain.Entities;


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
        DbSet<EstadoOrden> EstadoOrden { get; set; }   
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
