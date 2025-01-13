using AutoMapper;
using Moq;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.TarifaHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestModificarTarifaCommandHandler
    {
        private readonly Mock<ITarifaRepository> MockTarifaRepository;
        private readonly Mock<IMapper> MockMapper;
        private readonly ModificarTarifaHandler Handler;

        public TestModificarTarifaCommandHandler()
        {
            MockMapper = new Mock<IMapper>();
            MockTarifaRepository = new Mock<ITarifaRepository>();
            Handler = new ModificarTarifaHandler(MockMapper.Object, MockTarifaRepository.Object);
        }
        [Fact]
        public async Task Should_ThrowInvalidIdException_When_IdIsInvalid()
        {
            // Arrange
            var command = new ModificarTarifaCommand(new ListarTarifaDto (Guid.NewGuid(),"Tarifa 1",100,10,10 ),Guid.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidIdException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Should_ProcessSuccessfully_When_ValidIdAndDto()
        {
            // Arrange
            var validId = Guid.NewGuid();
            var command = new ModificarTarifaCommand(new ListarTarifaDto(validId, "Tarifa 1", 100, 10, 10), validId);
            var tarifa = new Tarifa(validId, "Tarifa 1", 100, 50, 2, "Activo"); // Instancia una tarifa válida

           
            MockMapper.Setup(m => m.Map<Tarifa>(It.IsAny<ListarTarifaDto>())).Returns(tarifa);
            MockTarifaRepository.Setup(m=>m.UptadeTarifaAsync(tarifa)).Returns(Task.CompletedTask); 

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            MockTarifaRepository.Verify(r => r.UptadeTarifaAsync(It.IsAny<Tarifa>()), Times.Once); // Verifica que el repositorio haya sido llamado
        }

    }
}
