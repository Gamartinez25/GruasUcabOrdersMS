

using System.Reflection.Metadata;
using Moq;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarEstatusOrdenPorEstatusQueryHandler
    {
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly ListarEstatusOrdenPorEstatusHandler Handler;

        public TestListarEstatusOrdenPorEstatusQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            Handler = new ListarEstatusOrdenPorEstatusHandler(OrdenRepositoryMock.Object);
        }
        [Theory]
        [InlineData("PorAsignar")]
        [InlineData("PorAceptar")]
        [InlineData("Aceptado")]
        [InlineData("EnProceso")]
        [InlineData("Localizado")]
        [InlineData("Finalizado")]
        [InlineData("Pagado")]
        public async Task Handle_FiltersOrdersByEstatus_ReturnsFilteredOrders(string estatus)
        {
            //Arrange
            List<EstadoOrden> estadosOrden = new List<EstadoOrden>
            {
                  new EstadoOrden(Guid.NewGuid(), "PorAsignar", DateTime.Now),
                  new EstadoOrden(Guid.NewGuid(), "PorAceptar", DateTime.Now.AddHours(1)),
                  new EstadoOrden(Guid.NewGuid(), "Aceptado", DateTime.Now.AddHours(2)),
                  new EstadoOrden(Guid.NewGuid(), "EnProceso", DateTime.Now.AddHours(3)),
                  new EstadoOrden(Guid.NewGuid(), "Localizado", DateTime.Now.AddHours(4)),
                  new EstadoOrden(Guid.NewGuid(), "Finalizado", DateTime.Now.AddHours(5)),
                  new EstadoOrden(Guid.NewGuid(), "Pagado", DateTime.Now.AddHours(6))
            };
            OrdenRepositoryMock.Setup(r => r.GetAllEstadoOrden()).ReturnsAsync(estadosOrden);

            var request = new ListarEstatusOrdenPorEstatusQuery(estatus);

            // Act
            var result = await Handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.All(result, item => Assert.Equal(estatus, item.EstadoActual));

        }
    }
}
