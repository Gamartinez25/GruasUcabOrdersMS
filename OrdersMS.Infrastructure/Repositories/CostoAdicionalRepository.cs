using Microsoft.EntityFrameworkCore;
using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Infrastructure.Repositories
{
    public class CostoAdicionalRepository : ICostoAdicionalRepository
    {
        private readonly IOrderMsDbContext OrderMsDbContext;
        public CostoAdicionalRepository(IOrderMsDbContext orderMsDbContext)
        {
            OrderMsDbContext = orderMsDbContext;

        }
        public async Task AddCostoAdicionalAsync(OrdenCostoAdicional costoAdicional)
        {
            await OrderMsDbContext.OrdenCostoAdicional.AddAsync(costoAdicional);
            await OrderMsDbContext.SaveChangesAsync();
        }

        public  async Task<IEnumerable<OrdenCostoAdicional>> GetAllCostoAdicionalAsync(Guid idOrden)
        {
            return await OrderMsDbContext.OrdenCostoAdicional.Where(x=>x.OrdenDeServicioId == idOrden).ToListAsync();

        }
        public async Task<IEnumerable<Tuple<Guid, string>>> GetAllNombresCostosAdicionalesByIdAsync(Guid idOrden)
        {
                var nombresCostosAdicionales = await OrderMsDbContext.OrdenCostoAdicional
                    .Where(oca => oca.OrdenDeServicioId == idOrden) // Filtrar por idOrden
                    .Select(oca => new Tuple<Guid, string>(
                        oca.CostoAdicional.Id,    // Obtener el ID del CostoAdicional
                        oca.CostoAdicional.Nombre // Obtener el Nombre del CostoAdicional
                    ))
                    .ToListAsync(); // Convertir a lista

                return nombresCostosAdicionales;
            
        }

        public async Task<OrdenCostoAdicional> GetCostoAdicionalByIdAsync(Guid id)
        {
           var  existingCostoAdicional= await OrderMsDbContext.OrdenCostoAdicional.FirstOrDefaultAsync(x=>x.IdCostoOrden==id);
            if (existingCostoAdicional is null) throw new InvalidOperationException("Orden costo adicional no encontrado");

            return existingCostoAdicional;
        }

        public async Task UpdateCostoAdicional(OrdenCostoAdicional costoAdicional)
        {
            var existingCostoAdicional = GetCostoAdicionalByIdAsync(costoAdicional.IdCostoOrden);
            OrderMsDbContext.OrdenCostoAdicional.Entry(existingCostoAdicional.Result).CurrentValues.SetValues(costoAdicional);
            await OrderMsDbContext.SaveChangesAsync();
        }
    }
}
