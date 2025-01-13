

using AutoMapper;
using FluentValidation;
using Moq;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Handlers.TarifaHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestCrearTarifaCommandHandler
    {
        private readonly Mock<ITarifaRepository> MockTarifaRepository;
        private readonly Mock<IMapper> MockMapper;
        private readonly CrearTarifaHandler Handler;

        public TestCrearTarifaCommandHandler()
        {
            MockMapper = new Mock<IMapper>();
            MockTarifaRepository= new Mock<ITarifaRepository>();
            Handler=new CrearTarifaHandler(MockMapper.Object, MockTarifaRepository.Object);

        }
        [Fact]
        public async Task Handle_ValidInput_SavesTarifaToRepository()
        {
            // Arrange

            var crearTarifaDto = new CrearTarifaDto("Tarifa 1", 100, 50, 2); // Inicializa con datos válidos
            var tarifa =new Tarifa(Guid.NewGuid(), "Tarifa 1", 100, 50, 2, "Activo"); // Instancia una tarifa válida

            MockMapper.Setup(m => m.Map<Tarifa>(crearTarifaDto)).Returns(tarifa);
            MockTarifaRepository.Setup(m=>m.AddTarifaAsync(tarifa)).Returns(Task.CompletedTask);    
            var command = new CrearTarifaCommand(crearTarifaDto) ;

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            MockTarifaRepository.Verify(r => r.AddTarifaAsync(tarifa), Times.Once);
        }

    }
}
