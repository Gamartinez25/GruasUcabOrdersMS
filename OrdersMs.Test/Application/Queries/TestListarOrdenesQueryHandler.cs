
using Moq;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using OrdersMS.Infrastructure.Repositories;
using static MassTransit.ValidationResultExtensions;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarOrdenesQueryHandler
    {
        private readonly ListarOrdenesHandler Handler;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<ITarifaRepository> TarifaRepositoryMock;
        private readonly Mock<IOrdenMapper> OrdenMapperMock;
        public TestListarOrdenesQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            TarifaRepositoryMock=new Mock<ITarifaRepository>();
            OrdenMapperMock = new Mock<IOrdenMapper>();
            Handler=new ListarOrdenesHandler(OrdenMapperMock.Object,OrdenRepositoryMock.Object,TarifaRepositoryMock.Object);
        }
        [Fact]
       public async Task Handle_ReturnsCorrectDtos_WhenCalled()
        {
            //Arrange
            IEnumerable<ListarOrdenesDto> dtos =new List<ListarOrdenesDto>();
            OrdenRepositoryMock.Setup(s => s.GetAllOrdenAsync()).ReturnsAsync(new List<OrdenDeServicio>());
            OrdenRepositoryMock.Setup(s => s.GetAllEstadoOrden()).ReturnsAsync(new List<EstadoOrden>());
            OrdenRepositoryMock.Setup(s => s.GetAllPolizaAseguradoAsync()).ReturnsAsync(new List<PolizaAsegurado>());
            OrdenRepositoryMock.Setup(s => s.GetAllPolizaAsync()).ReturnsAsync(new List<Poliza>());
            OrdenRepositoryMock.Setup(s => s.GetAllAseguradoAsync()).ReturnsAsync(new List<Asegurado>());
            TarifaRepositoryMock.Setup(s=>s.GetAllTarifaAsync()).ReturnsAsync(new List<Tarifa>());
            OrdenMapperMock.Setup(s => s.ListarOrdenesDtos(It.IsAny<IEnumerable<OrdenDeServicio>>(),
                                                                    It.IsAny<IEnumerable<EstadoOrden>>(),
                                                                    It.IsAny<IEnumerable<PolizaAsegurado>>(),
                                                                    It.IsAny<IEnumerable<Poliza>>(),
                                                                    It.IsAny<IEnumerable<Asegurado>>(),
                                                                    It.IsAny<IEnumerable<Tarifa>>())).Returns(dtos);
            //Act
            var result = Handler.Handle(new ListarOrdenesQuery(), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.Equivalent(dtos, result.Result.ToList());
        }
    }
}

