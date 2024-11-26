using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Repositories
{
    public interface IOrdenRepository
    {
        Task AddOrdenAsync(OrdenDeServicio orden);
        Task<IEnumerable<OrdenDeServicio>> GetAllOrdenAsync();
        Task<IEnumerable<PolizaAsegurado>> GetAllPolizaAseguradoAsync();
        Task<IEnumerable<Asegurado>> GetAllAseguradoAsync();
        Task<IEnumerable<Poliza>> GetAllPolizaAsync();
    }
}
