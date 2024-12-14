using OrdersMS.Domain.Entities;


namespace OrdersMS.Core.Repositories
{
    public interface IOrdenRepository
    {
        Task AddOrdenAsync(OrdenDeServicio orden);
        Task<IEnumerable<OrdenDeServicio>> GetAllOrdenAsync();
        Task<IEnumerable<PolizaAsegurado>> GetAllPolizaAseguradoAsync();
        Task<IEnumerable<Asegurado>> GetAllAseguradoAsync();
        Task<IEnumerable<Poliza>> GetAllPolizaAsync();
        Task<OrdenDeServicio> GetOrdenDeServicioByIdAsync(Guid id);
        Task UpdateOrdenAsync(OrdenDeServicio orden);
        Task<PolizaAsegurado> GetPolizaAseguradoById(Guid id);
        Task<IEnumerable<EstadoOrden>> GetAllEstadoOrden();
      //  Task<IEnumerable<T>> GetAllInformacionOrdenAsync();

    }
}
