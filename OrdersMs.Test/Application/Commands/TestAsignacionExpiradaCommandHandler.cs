
using System.Reflection.Metadata;
using MassTransit;
using Moq;
using OrdersMS.Application.Commands.AsignarConductorCommand;
using OrdersMS.Application.Handlers.AsignarConductorHandler;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestAsignacionExpiradaCommandHandler
    {
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IPublishEndpoint> PublishEndpointMock;
        private readonly AsignacionExpiradaHandler Handler;

        public TestAsignacionExpiradaCommandHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            PublishEndpointMock= new Mock<IPublishEndpoint>();
            Handler=new AsignacionExpiradaHandler(OrdenRepositoryMock.Object,PublishEndpointMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldPublishEvent_WhenTimeIsGreaterThan6Minutes()
        {
            // Arrange
            var estadoOrden = new EstadoOrden(Guid.NewGuid(), "PorAceptar", DateTime.UtcNow.AddMinutes(-7));
            
            OrdenRepositoryMock.Setup(repo => repo.GetAllEstadoOrden()).ReturnsAsync(new List<EstadoOrden> { estadoOrden});

            var command = new AsignacionExpiradaCommand();

            // Act
            await Handler.Handle(command,  CancellationToken.None);

            // Assert
            PublishEndpointMock.Verify(p => p.Publish(It.IsAny<ReasignarOrdenEvent>(), CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldNotPublishEvent_WhenTimeIsLessThanOrEqualTo6Minutes()
        {
            // Arrange
            var estadoOrden = new EstadoOrden(Guid.NewGuid(), "PorAceptar", DateTime.UtcNow.AddMinutes(-5));
            OrdenRepositoryMock.Setup(repo => repo.GetAllEstadoOrden()).ReturnsAsync(new List<EstadoOrden> { estadoOrden });

            var command = new AsignacionExpiradaCommand();

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            PublishEndpointMock.Verify(p => p.Publish(It.IsAny<ReasignarOrdenEvent>(), CancellationToken.None), Times.Never);
        }

    }

}
