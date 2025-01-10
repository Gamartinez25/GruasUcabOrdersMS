

using Moq;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Core.Repositories;

namespace OrdersMs.Tests.Tes.Applications.Querys.Orders
{
    public class InformacionPolizaQueryHandler
    {
        private readonly InformacionPolizaHandler Handler;
        private readonly Mock<IOrdenRepository> OrderRepositoryMock;
        private readonly Mock<ITarifaRepository> TarifaRepositoryMock;
        private readonly Mock<IOrdenMapper> OrdenMapperMock;

        public InformacionPolizaQueryHandler()
        {

            //Handler=new InformacionPolizaHandler()
        }
    }
}
