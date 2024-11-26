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
    public class OrdenRepository : IOrdenRepository
    {
        private readonly IOrderMsDbContext OrderMsDbContext;
        public OrdenRepository(IOrderMsDbContext orderMsDbContext)
        {
            OrderMsDbContext = orderMsDbContext;

        }
        public async  Task AddOrdenAsync(OrdenDeServicio orden)
        {
            await OrderMsDbContext.OrdenDeServicio.AddAsync(orden);

            await OrderMsDbContext.SaveChangesAsync();
        }
    }
}
