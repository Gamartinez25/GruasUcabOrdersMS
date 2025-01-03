using OrdersMS.Domain.Entities;


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
