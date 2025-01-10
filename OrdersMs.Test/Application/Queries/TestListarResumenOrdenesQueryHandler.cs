

using System.Reflection.Metadata;
using Moq;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public  class TestListarResumenOrdenesQueryHandler
    {
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IGoogleService> GoogleServiceMock;
        private readonly ListarResumenOrdenesHandler Handler;

        public TestListarResumenOrdenesQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            GoogleServiceMock = new Mock<IGoogleService>();
            Handler = new ListarResumenOrdenesHandler(OrdenRepositoryMock.Object, GoogleServiceMock.Object);
        }
        [Fact]
        public async Task Handle_NoOrdersForVehicle_ReturnsEmptyList()
        {
            // Arrange
            OrdenRepositoryMock.Setup(repo => repo.GetAllOrdenAsync())
                .ReturnsAsync(new List<OrdenDeServicio>());

            var query = new ListarResumenOrdenesQuery(Guid.NewGuid());

            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }
        [Theory]
        [InlineData("PorAsignar")]
        [InlineData("PorAceptar")]
        [InlineData("EnProceso")]
        [InlineData("Localizado")]
        [InlineData("Finalizado")]
        [InlineData("Pagado")]
        public async Task Handle_OrderWithValidStatus_ReturnsResumenOrdenDto(string estatus)
        {
            // Arrange
            var ordenId = Guid.NewGuid();
            var vehiculoId = Guid.NewGuid();
            var ordenes = new List<OrdenDeServicio>
        {
            new OrdenDeServicio(ordenId, "#001", DateTime.Now, "Detalles del incidente",
                                "10.5050,-66.9188", "10.4999,-66.9170", 30.00, "Juan Pérez", "V", "12345678",
                                Guid.NewGuid(), null, Guid.NewGuid(), vehiculoId, 5.0, 20.00, 10.00)
        };
            var estadoOrden = new EstadoOrden(ordenId, estatus, DateTime.Now);

            OrdenRepositoryMock.Setup(repo => repo.GetAllOrdenAsync())
                .ReturnsAsync(ordenes);
            OrdenRepositoryMock.Setup(repo => repo.GetEstadoOrdenByIdOrdenAsync(ordenId))
                .ReturnsAsync(estadoOrden);
            GoogleServiceMock.Setup(service => service.GetDirecction(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync("Dirección simulada");

            var query = new ListarResumenOrdenesQuery(vehiculoId);

            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(),1);
        }
    }
}
