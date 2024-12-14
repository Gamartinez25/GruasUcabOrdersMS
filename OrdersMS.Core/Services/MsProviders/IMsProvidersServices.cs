using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Services.MsProviders
{
    public interface IMsProvidersServices
    {
        public Task<IEnumerable<Vehiculo>> GetAllVehiculosAsync();
    }
}
