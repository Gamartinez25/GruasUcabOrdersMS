
namespace OrdersMS.Core.Repositories
{
    public interface IVehiculosAsignadosRepository
    {
        void AddAsignacionGrua(Guid IdOrden, Guid IdVehiculo);
        List<Guid>? GetGruasAsignadasPreviamente(Guid IdOrden);
        void DeleteAsignacionGrua(Guid IdOrden);


    }
}
