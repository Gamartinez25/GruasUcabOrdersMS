using Microsoft.EntityFrameworkCore;
using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Infrastructure.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly IOrderMsDbContext OrderMsDbContext;
        public OrdenRepository(IOrderMsDbContext orderMsDbContext)
        {
            OrderMsDbContext = orderMsDbContext;

        }
        public async  Task AddOrdenAsync(OrdenDeServicio orden)
        {
            await OrderMsDbContext.OrdenDeServicio.AddAsync(orden);

            await OrderMsDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asegurado>> GetAllAseguradoAsync()
        {
            return await OrderMsDbContext.Asegurado.ToListAsync();
        }

        public async Task<IEnumerable<EstadoOrden>> GetAllEstadoOrden()
        {
            return await OrderMsDbContext.EstadoOrden.ToListAsync();
        }

        public  async Task<IEnumerable<OrdenDeServicio>> GetAllOrdenAsync()
        {
            return await OrderMsDbContext.OrdenDeServicio.ToListAsync();
        }

        public async Task<IEnumerable<PolizaAsegurado>> GetAllPolizaAseguradoAsync()
        {
            return await OrderMsDbContext.PolizaAsegurado.ToListAsync();
        }

        public  async Task<IEnumerable<Poliza>> GetAllPolizaAsync()
        {
            return await OrderMsDbContext.Poliza.ToListAsync();
        }

        public async Task<OrdenDeServicio> GetOrdenDeServicioByIdAsync(Guid id)
        {
            var existingOrden = await OrderMsDbContext.OrdenDeServicio.FindAsync(id);
            if (existingOrden is null) throw new InvalidOperationException("Orden no encontrada");
            return existingOrden;
        }

        public async Task<PolizaAsegurado> GetPolizaAseguradoById(Guid id)
        { 
            var existingPoliza =await OrderMsDbContext.PolizaAsegurado.FindAsync(id);
            return existingPoliza;

        }

        public async Task<Tarifa> GetTarifaByIdOrdenAsync(Guid id)
        {
            var tarifa = await OrderMsDbContext.OrdenDeServicio
           .Include(o => o.PolizaAsegurado) // Incluye la relación con PolizaAsegurado
           .ThenInclude(pa => pa.Poliza) // Incluye la relación con Poliza
           .ThenInclude(p => p.Tarifa) // Incluye la relación con Tarifa
           .Where(o => o.Id == id) // Filtro por el ID de la Orden
           .Select(o => o.PolizaAsegurado.Poliza.Tarifa) // Selecciona la póliza asociada
           .FirstOrDefaultAsync();
            return tarifa;
        }

        public async Task UpdateOrdenAsync(OrdenDeServicio orden)
        {
           var existingOrden = await OrderMsDbContext.OrdenDeServicio.FindAsync(orden.Id);
           OrderMsDbContext.OrdenDeServicio.Entry(existingOrden).CurrentValues.SetValues(orden);
           await OrderMsDbContext.SaveChangesAsync();
        }
    }
}
