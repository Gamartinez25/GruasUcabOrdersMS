using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Repositories
{
    public interface ITarifaRepository
    {
       Task AddTarifaAsync(Tarifa tarifa);
       Task<IEnumerable<Tarifa>> GetAllTarifaAsync();
       Task UptadeTarifaAsync(Tarifa tarifa);
        Task DeleteTarifaAsync(Guid id);
    }

}
