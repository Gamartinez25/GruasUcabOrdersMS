using OrdersMS.Core.Database;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Infrastructure.Repositories
{
    public class CostoAdicionalRepository : ICostoAdicionalRepository
    {
        private readonly IOrderMsDbContext OrderMsDbContext;
        public CostoAdicionalRepository(IOrderMsDbContext orderMsDbContext)
        {
            OrderMsDbContext = orderMsDbContext;

        }
        public async Task AddCostoAdicionalAsync(OrdenCostoAdicional costoAdicional)
        {
            await OrderMsDbContext.OrdenCostoAdicional.AddAsync(costoAdicional);
            await OrderMsDbContext.SaveChangesAsync();
        }
    }
}
