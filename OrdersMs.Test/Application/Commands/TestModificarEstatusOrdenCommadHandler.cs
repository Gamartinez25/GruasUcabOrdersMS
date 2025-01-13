

using FluentValidation;
using MassTransit;
using Moq;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Application.Validators.OrdenValidators;

namespace OrdersMs.Test.Application.Commands
{
    public class TestModificarEstatusOrdenCommadHandler
    {
        private readonly Mock<IPublishEndpoint> PublishEndpointMock;

        private readonly ModificarEstatusOrdenHandler Handler;

        public TestModificarEstatusOrdenCommadHandler()
        {
            PublishEndpointMock = new Mock<IPublishEndpoint>();

            Handler=new ModificarEstatusOrdenHandler(PublishEndpointMock.Object);  
            
        }

        [Fact]
            public async Task Handle_ShouldPublishCorrectEventActualizar()
            {
                // Arrange
                var estatusDto = new ModificarEstatusDto(Guid.NewGuid(), "Actualizar");
                var command = new ModificarEstatusOrdenCommand(estatusDto);

                // Act
                await Handler.Handle(command, CancellationToken.None);

                PublishEndpointMock.Verify(p => p.Publish(It.Is<ActualizarOrdenEvent>(e =>
                e.OrdenId == estatusDto.Id),
                It.IsAny<CancellationToken>()),
                Times.Once);
              }
        [Fact]
        public async Task Handle_ShouldPublishCorrectEventCancelar()
        {
            // Arrange
            var estatusDto = new ModificarEstatusDto(Guid.NewGuid(), "Cancelar");
            var command = new ModificarEstatusOrdenCommand(estatusDto);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            PublishEndpointMock.Verify(p => p.Publish(It.Is<OrdenCanceladaEvent>(e =>
            e.OrdenId == estatusDto.Id),
            It.IsAny<CancellationToken>()),
            Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldPublishCorrectEventReasignar()
        {
            // Arrange
            var estatusDto = new ModificarEstatusDto(Guid.NewGuid(), "Reasignar");
            var command = new ModificarEstatusOrdenCommand(estatusDto);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            PublishEndpointMock.Verify(p => p.Publish(It.Is<ReasignarOrdenEvent>(e =>
            e.OrdenId == estatusDto.Id),
            It.IsAny<CancellationToken>()),
            Times.Once);
        }

        [Fact]
            public void Handle_ShouldThrowValidationException_WhenEstatusDtoIsInvalid()
            {
            // Arrange
            var estatusDto = new ModificarEstatusDto(Guid.NewGuid(), "Otra");
            var command = new ModificarEstatusOrdenCommand(estatusDto);
            var publishEndpointMock = new Mock<IPublishEndpoint>();
            var handler = new ModificarEstatusOrdenHandler(publishEndpointMock.Object);

               


                // Act & Assert
                Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
            }
        

    }
}
