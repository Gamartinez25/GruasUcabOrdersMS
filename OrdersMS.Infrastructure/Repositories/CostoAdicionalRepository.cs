using Microsoft.EntityFrameworkCore;
using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Tuple<Guid, string>>> ObtenerNombresCostosAdicionalesPorId(Guid idOrden)
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
    }
}
