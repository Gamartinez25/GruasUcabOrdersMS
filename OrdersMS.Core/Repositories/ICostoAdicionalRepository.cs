using OrdersMS.Domain.Entities;


namespace OrdersMS.Core.Repositories
{
    public interface ICostoAdicionalRepository
    {
        Task AddCostoAdicionalAsync(OrdenCostoAdicional costoAdicional);
        Task<IEnumerable<OrdenCostoAdicional>> GetAllCostoAdicionalAsync( Guid idOrden);
        Task<IEnumerable<Tuple<Guid, string>>> ObtenerNombresCostosAdicionalesPorId(Guid idOrden);
    }
}
