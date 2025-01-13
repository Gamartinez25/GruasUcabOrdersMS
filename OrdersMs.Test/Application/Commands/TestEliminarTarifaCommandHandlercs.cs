
using Moq;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.TarifaHandlers;
using OrdersMS.Core.Repositories;

namespace OrdersMs.Test.Application.Commands
{
    public class TestEliminarTarifaCommandHandlercs
    {
        private readonly Mock<ITarifaRepository> MockTarifaRepository;

        private readonly EliminarTarifaHandler Handler;

        public TestEliminarTarifaCommandHandlercs()
        {
            MockTarifaRepository=new Mock<ITarifaRepository>();
            Handler=new EliminarTarifaHandler(MockTarifaRepository.Object);
        }
        [Fact]
        public async Task Should_ThrowInvalidIdException_When_IdIsInvalid()
        {
            // Arrange
            var command = new EliminarTarifaCommand( Guid.Empty);
          
            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Should_CallDeleteTarifaAsync_When_ValidId()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var command = new EliminarTarifaCommand(validId);
            MockTarifaRepository.Setup(x=>x.DeleteTarifaAsync(validId)).Returns(Task.CompletedTask);    
            
            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            MockTarifaRepository.Verify(r => r.DeleteTarifaAsync(validId), Times.Once); // Verifica que el repositorio haya sido llamado con el Id correcto
        }
        [Fact]
        public async Task Should_ThrowInvalidIdException_When_RequestIsNull()
        {
            // Arrange
            EliminarTarifaCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => Handler.Handle(command, CancellationToken.None));
        }

    }
}

