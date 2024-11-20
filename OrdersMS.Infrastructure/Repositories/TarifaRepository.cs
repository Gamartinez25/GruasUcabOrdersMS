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

        public async Task<IEnumerable<Tarifa>> GetAllTarifaAsync()
        {
         return await OrderMsDbContext.Tarifa
                .Where(t => t.Estatus == "Activo")
                .ToListAsync();
        }
    }
}
