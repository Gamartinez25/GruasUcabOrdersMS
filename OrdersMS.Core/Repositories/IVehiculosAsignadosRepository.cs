using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Core.Repositories
{
    public interface IVehiculosAsignadosRepository
    {
        void AddAsignacionGrua(Guid IdOrden, Guid IdVehiculo);
        List<Guid>? GetGruasAsignadasPreviamente(Guid IdOrden);
        void DeleteAsignacionGrua(Guid IdOrden);


    }
}
