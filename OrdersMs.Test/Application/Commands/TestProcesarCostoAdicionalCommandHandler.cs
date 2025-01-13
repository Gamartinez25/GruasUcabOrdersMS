

using Moq;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Handlers.CostoAdicionalHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestProcesarCostoAdicionalCommandHandler
    {
        private readonly Mock<ICostoAdicionalRepository> CostoAdicionalRepositoryMock;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly ProcesarRespuestaSolicitudCostoAdicionalHandler Handler;

        public TestProcesarCostoAdicionalCommandHandler()
        {       
            CostoAdicionalRepositoryMock = new Mock<ICostoAdicionalRepository>();
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            Handler=new ProcesarRespuestaSolicitudCostoAdicionalHandler(CostoAdicionalRepositoryMock.Object,OrdenRepositoryMock.Object);

        }
        [Fact]
        public async Task Should_UpdateCostoAdicionalAndRecalculateOrder_When_RespuestaAprobado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new ProcesarRespuestaSolicitudCostoAdicionalCommand(id, "Aprobado");



            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(),200, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);
            var costoAdicional2 = new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 200, "Aprobado", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);

            var ordenDeServicio = new OrdenDeServicio(costoAdicional.OrdenDeServicioId,
                                                      "#001",
                                                      DateTime.Now,
                                                      "Accidente en la autopista",
                                                      "Calle Falsa 123, Ciudad X",
                                                      "Avenida Siempre Viva 456, Ciudad Y",
                                                      200.50,
                                                      "Juan Pérez",
                                                      "DNI",
                                                      "12345678",
                                                      Guid.NewGuid(),
                                                      null,
                                                      Guid.NewGuid(),
                                                      Guid.NewGuid(),
                                                      15.0,
                                                      50.0,
                                                      30.0);
            var estadoOrden = new EstadoOrden(ordenDeServicio.Id, "Localizado", DateTime.UtcNow);
            CostoAdicionalRepositoryMock.Setup(x => x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);
            OrdenRepositoryMock.Setup(x => x.GetOrdenDeServicioByIdAsync(costoAdicional.OrdenDeServicioId)).ReturnsAsync(ordenDeServicio);
            OrdenRepositoryMock.Setup(x=>x.GetEstadoOrdenByIdOrdenAsync(ordenDeServicio.Id)).ReturnsAsync(estadoOrden);
            CostoAdicionalRepositoryMock.Setup(x => x.UpdateCostoAdicional(costoAdicional)).Returns(Task.CompletedTask);
            CostoAdicionalRepositoryMock.Setup(r => r.GetAllCostoAdicionalAsync(It.IsAny<Guid>())).ReturnsAsync(new List<OrdenCostoAdicional> { costoAdicional,costoAdicional2 });
            OrdenRepositoryMock.Setup(r => r.GetTarifaByIdOrdenAsync(It.IsAny<Guid>())).ReturnsAsync(new Tarifa(Guid.NewGuid(),"Tarifa 1",12,21,11,"Activa"));
            OrdenRepositoryMock.Setup(x => x.UpdateOrdenAsync(ordenDeServicio)).Returns(Task.CompletedTask);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            CostoAdicionalRepositoryMock.Verify(r => r.UpdateCostoAdicional(It.IsAny<OrdenCostoAdicional>()), Times.Once);
            OrdenRepositoryMock.Verify(r => r.UpdateOrdenAsync(It.IsAny<OrdenDeServicio>()), Times.Once);
        }
        [Fact]
        public async Task Should_UpdateCostoAdicionalToRejected_When_RespuestaRechazada()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new ProcesarRespuestaSolicitudCostoAdicionalCommand(id, "Rechazado");
            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(), 200, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);

            var ordenDeServicio = new OrdenDeServicio(costoAdicional.OrdenDeServicioId,
                                                      "#001",
                                                      DateTime.Now,
                                                      "Accidente en la autopista",
                                                      "Calle Falsa 123, Ciudad X",
                                                      "Avenida Siempre Viva 456, Ciudad Y",
                                                      200.50,
                                                      "Juan Pérez",
                                                      "DNI",
                                                      "12345678",
                                                      Guid.NewGuid(),
                                                      null,
                                                      Guid.NewGuid(),
                                                      Guid.NewGuid(),
                                                      15.0,
                                                      50.0,
                                                      30.0);
            var estadoOrden = new EstadoOrden(ordenDeServicio.Id, "Localizado", DateTime.UtcNow);
            CostoAdicionalRepositoryMock.Setup(x => x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);
            OrdenRepositoryMock.Setup(x => x.GetOrdenDeServicioByIdAsync(costoAdicional.OrdenDeServicioId)).ReturnsAsync(ordenDeServicio);
            OrdenRepositoryMock.Setup(x => x.GetEstadoOrdenByIdOrdenAsync(ordenDeServicio.Id)).ReturnsAsync(estadoOrden);
            CostoAdicionalRepositoryMock.Setup(x => x.UpdateCostoAdicional(costoAdicional)).Returns(Task.CompletedTask);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            CostoAdicionalRepositoryMock.Verify(r => r.UpdateCostoAdicional(costoAdicional), Times.Once);
        }
        [Fact]
        public async Task Should_ThrowInvalidOperationException_When_RespuestaSolicitudIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new ProcesarRespuestaSolicitudCostoAdicionalCommand(id, "Otro");
            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(), 200, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);

            var ordenDeServicio = new OrdenDeServicio(costoAdicional.OrdenDeServicioId,
                                                      "#001",
                                                      DateTime.Now,
                                                      "Accidente en la autopista",
                                                      "Calle Falsa 123, Ciudad X",
                                                      "Avenida Siempre Viva 456, Ciudad Y",
                                                      200.50,
                                                      "Juan Pérez",
                                                      "DNI",
                                                      "12345678",
                                                      Guid.NewGuid(),
                                                      null,
                                                      Guid.NewGuid(),
                                                      Guid.NewGuid(),
                                                      15.0,
                                                      50.0,
                                                      30.0);
            CostoAdicionalRepositoryMock.Setup(x => x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);
            OrdenRepositoryMock.Setup(x => x.GetOrdenDeServicioByIdAsync(costoAdicional.OrdenDeServicioId)).ReturnsAsync(ordenDeServicio);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowInvalidOperationException_When_OrderIsFinalized()
        {
            // Arrange
            // Arrange
            var id = Guid.NewGuid();
            var command = new ProcesarRespuestaSolicitudCostoAdicionalCommand(id, "Aprobado");
            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(), 200, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);

            var ordenDeServicio = new OrdenDeServicio(costoAdicional.OrdenDeServicioId,
                                                      "#001",
                                                      DateTime.Now,
                                                      "Accidente en la autopista",
                                                      "Calle Falsa 123, Ciudad X",
                                                      "Avenida Siempre Viva 456, Ciudad Y",
                                                      200.50,
                                                      "Juan Pérez",
                                                      "DNI",
                                                      "12345678",
                                                      Guid.NewGuid(),
                                                      null,
                                                      Guid.NewGuid(),
                                                      Guid.NewGuid(),
                                                      15.0,
                                                      50.0,
                                                      30.0);
            var estadoOrden = new EstadoOrden(ordenDeServicio.Id, "Finalizado", DateTime.UtcNow);
            CostoAdicionalRepositoryMock.Setup(x => x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);
            OrdenRepositoryMock.Setup(x => x.GetOrdenDeServicioByIdAsync(costoAdicional.OrdenDeServicioId)).ReturnsAsync(ordenDeServicio);
            OrdenRepositoryMock.Setup(x => x.GetEstadoOrdenByIdOrdenAsync(ordenDeServicio.Id)).ReturnsAsync(estadoOrden);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Handler.Handle(command, CancellationToken.None));
        }



    }
}
