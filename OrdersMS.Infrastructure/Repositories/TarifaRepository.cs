using Microsoft.EntityFrameworkCore;
using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Repositories
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly IOrderMsDbContext  OrderMsDbContext;
        public TarifaRepository(IOrderMsDbContext orderMsDbContext)
        {
            OrderMsDbContext = orderMsDbContext;

        }

        public async Task AddTarifaAsync(Tarifa tarifa)
        {
           await  OrderMsDbContext.Tarifa.AddAsync(tarifa);
           
           await OrderMsDbContext.SaveChangesAsync();
        }

        public async Task DeleteTarifaAsync(Guid id)
        {
            var existingTarifa = await OrderMsDbContext.Tarifa.FindAsync(id);
            if (existingTarifa is null) throw new InvalidOperationException("Tarifa no encontrado");

            existingTarifa.ActualizarEstatus("Inactivo");
            OrderMsDbContext.Tarifa.Entry(existingTarifa).Property(o => o.Estatus).IsModified = true;
            await OrderMsDbContext.SaveChangesAsync(); ;
        }

        public async Task<IEnumerable<Tarifa>> GetAllTarifaAsync()
        {
         return await OrderMsDbContext.Tarifa.ToListAsync();
        }

        public  async Task UptadeTarifaAsync(Tarifa tarifa)
        {
            var existingTarifa = await OrderMsDbContext.Tarifa.FindAsync(tarifa.Id);
            if (existingTarifa == null)
            {
                throw new KeyNotFoundException("La tarifa no se encontró.");
            }
            OrderMsDbContext.Tarifa.Entry(existingTarifa).CurrentValues.SetValues(tarifa);
            OrderMsDbContext.SaveChangesAsync();
        }
    }
}
