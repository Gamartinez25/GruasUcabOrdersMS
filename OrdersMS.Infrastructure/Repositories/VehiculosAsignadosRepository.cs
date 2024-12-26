
using OrdersMS.Core.Repositories;

namespace OrdersMS.Infrastructure.Repositories
{
    public class VehiculosAsignadosRepository:IVehiculosAsignadosRepository
    {
      public static  Dictionary<Guid, List<Guid>> GruasAsignadasDB = new Dictionary<Guid, List<Guid>>();

        public  void AddAsignacionGrua(Guid IdOrden, Guid IdVehiculo)
        {
            if (!GruasAsignadasDB.ContainsKey(IdOrden))
            {
                GruasAsignadasDB[IdOrden] = new List<Guid>();

            }
            GruasAsignadasDB[IdOrden].Add(IdVehiculo);
        }

        public void DeleteAsignacionGrua(Guid IdOrden)
        {
            GruasAsignadasDB.Remove(IdOrden);
        }

        public List<Guid> GetGruasAsignadasPreviamente(Guid IdOrden)
        {
            if (GruasAsignadasDB.ContainsKey(IdOrden))
                return GruasAsignadasDB[IdOrden];
            else 
                return null;
        }
    }
}
