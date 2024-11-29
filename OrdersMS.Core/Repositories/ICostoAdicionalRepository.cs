using OrdersMS.Domain.Entities;


namespace OrdersMS.Core.Repositories
{
    public interface ICostoAdicionalRepository
    {
        Task AddCostoAdicionalAsync(OrdenCostoAdicional costoAdicional);
    }
}
