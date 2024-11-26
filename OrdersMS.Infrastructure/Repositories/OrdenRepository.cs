using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Asegurado>> GetAllAseguradoAsync()
        {
            return await OrderMsDbContext.Asegurado.ToListAsync();
        }

        public  async Task<IEnumerable<OrdenDeServicio>> GetAllOrdenAsync()
        {
            return await OrderMsDbContext.OrdenDeServicio.ToListAsync();
        }

        public async Task<IEnumerable<PolizaAsegurado>> GetAllPolizaAseguradoAsync()
        {
            return await OrderMsDbContext.PolizaAsegurado.ToListAsync();
        }

        public  async Task<IEnumerable<Poliza>> GetAllPolizaAsync()
        {
            return await OrderMsDbContext.Poliza.ToListAsync();
        }
    }
}
