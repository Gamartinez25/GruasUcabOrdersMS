using Moq;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarOrdenVigenteQueryHandler
    {
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IGoogleService> GoogleServiceMock;
        private readonly ListarOrdenVigentePorGruaHandler Handler;
        public TestListarOrdenVigenteQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            GoogleServiceMock = new Mock<IGoogleService>();
            Handler=new ListarOrdenVigentePorGruaHandler(OrdenRepositoryMock.Object,GoogleServiceMock.Object);

        }
        [Fact]
        public async Task Handle_NoOrdersForGrua_ReturnsNull()
        {
            // Arrange
            OrdenRepositoryMock.Setup(repo => repo.GetAllOrdenAsync())
                .ReturnsAsync(new List<OrdenDeServicio>());

            var query = new ListarOrdenVigentePorGruaQuery(Guid.NewGuid());

            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
        [Theory]
        [InlineData("PorAceptar")]
        [InlineData("EnProceso")]
        [InlineData("Localizado")]
        public async Task Handle_OrderWithValidStatus_ReturnsOrder( string estatus)
        {
            // Arrange
            var ordenId = Guid.NewGuid();
            var gruaId = Guid.NewGuid();
            var ordenes = new List<OrdenDeServicio>
        {
            new OrdenDeServicio(ordenId, "#001", DateTime.Now, "Detalles del incidente",
                                "10.5050,-66.9188", "10.4999,-66.9170", 30.00, "Juan Pérez", "V", "12345678",
                                Guid.NewGuid(), null, Guid.NewGuid(), gruaId, 5.0, 20.00, 10.00)
        };
            var estadoOrden = new EstadoOrden(ordenId, estatus, DateTime.Now);

            OrdenRepositoryMock.Setup(repo => repo.GetAllOrdenAsync())
                .ReturnsAsync(ordenes);
            OrdenRepositoryMock.Setup(repo => repo.GetEstadoOrdenByIdOrdenAsync(ordenId))
                .ReturnsAsync(estadoOrden);
            GoogleServiceMock.Setup(service => service.GetDirecction(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync("Dirección simulada");

            var query = new ListarOrdenVigentePorGruaQuery(gruaId);

            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ordenId, result.Id);
            
        }
        [Theory]
        [InlineData("PorAsignar")]
        [InlineData("Finalizado")]
        [InlineData("Pagado")]
        [InlineData("Cancelado")]

        public async Task Handle_OrderWithInvalidStatus_ReturnsNull(string estatus)
        {
            // Arrange
            var ordenId = Guid.NewGuid();
            var gruaId = Guid.NewGuid();
            var ordenes = new List<OrdenDeServicio>
        {
            new OrdenDeServicio(ordenId, "#001", DateTime.Now, "Detalles del incidente",
                                "10.5050,-66.9188", "10.4999,-66.9170", 30.00, "Juan Pérez", "V", "12345678",
                                Guid.NewGuid(), null, Guid.NewGuid(), gruaId, 5.0, 20.00, 10.00)
        };
            var estadoOrden = new EstadoOrden(ordenId, estatus, DateTime.Now);

            OrdenRepositoryMock.Setup(repo => repo.GetAllOrdenAsync())
                .ReturnsAsync(ordenes);
            OrdenRepositoryMock.Setup(repo => repo.GetEstadoOrdenByIdOrdenAsync(ordenId))
                .ReturnsAsync(estadoOrden);

            var query = new ListarOrdenVigentePorGruaQuery(gruaId);

            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }

}
