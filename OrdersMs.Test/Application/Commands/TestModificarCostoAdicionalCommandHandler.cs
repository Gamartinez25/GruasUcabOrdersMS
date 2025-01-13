

using AutoMapper;
using FluentValidation;
using Moq;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.CostoAdicionalHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestModificarCostoAdicionalCommandHandler
    {
        private readonly Mock<ICostoAdicionalRepository> CostoAdicionalRepositoryMock;
        private readonly Mock<IMapper> MapperMock;
        private readonly ModificarCostoAdicionalHandler Handler;

        public TestModificarCostoAdicionalCommandHandler()
        {
            CostoAdicionalRepositoryMock = new Mock<ICostoAdicionalRepository>();
            MapperMock = new Mock<IMapper>();
            Handler = new ModificarCostoAdicionalHandler(MapperMock.Object, CostoAdicionalRepositoryMock.Object);
        }
        [Fact]
        public async Task Should_ThrowValidationException_When_CostoAdicionalDtoIsInvalid()
        {
            // Arrange
            var command = new ModificarCostoAdicionalCommand(Guid.NewGuid(),new ModificarCostoAdicionalDto(Guid.Empty,0, null));
            
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Should_ThrowIdsCostoAdicionalNoCoincidenException_When_IdsDoNotMatch()
        {
            var id=Guid.NewGuid();
            var idCosto = Guid.NewGuid();
            // Arrange
            var command = new ModificarCostoAdicionalCommand(id,new ModificarCostoAdicionalDto(idCosto, 10, "Costo Prueba"));
            
            // Act & Assert
            await Assert.ThrowsAsync<IdsCostoAdicionalNoCoincidenException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Should_ThrowInvalidAdditionalCostStateException_When_CostoAdicionalStateIsNotPorAprobar()
        {
            // Arrange
            var idCosto = Guid.NewGuid();
            var command = new ModificarCostoAdicionalCommand(idCosto, new ModificarCostoAdicionalDto(idCosto, 10, "Costo Prueba"));

            CostoAdicionalRepositoryMock.Setup(r => r.GetCostoAdicionalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new OrdenCostoAdicional(idCosto,Guid.NewGuid(),Guid.NewGuid(),10,"Aceptado","Descripcion",null,DateTime.UtcNow,"Defaul",null,null)); // Estatus distinto de "Por Aprobar"


            // Act & Assert
            await Assert.ThrowsAsync<InvalidAdditionalCostStateException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Should_CallUpdateCostoAdicional_When_ValidRequest()
        {
            // Arrange
            var idCosto = Guid.NewGuid();
            var command = new ModificarCostoAdicionalCommand(idCosto, new ModificarCostoAdicionalDto(idCosto, 15, "Costo Prueba"));
            CostoAdicionalRepositoryMock.Setup(r => r.GetCostoAdicionalByIdAsync(It.IsAny<Guid>()))
              .ReturnsAsync(new OrdenCostoAdicional(idCosto, Guid.NewGuid(), Guid.NewGuid(), 10, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defaul", null, null)); // Estatus distinto de "Por Aprobar"
            MapperMock.Setup(m => m.Map<OrdenCostoAdicional>(It.IsAny<object>()))
                .Returns(new OrdenCostoAdicional(idCosto, Guid.NewGuid(), Guid.NewGuid(), 15, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defaul", null, null));
            CostoAdicionalRepositoryMock.Setup(s=>s.UpdateCostoAdicional(It.IsAny<OrdenCostoAdicional>())).Returns(Task.CompletedTask);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            CostoAdicionalRepositoryMock.Verify(r => r.UpdateCostoAdicional(It.IsAny<OrdenCostoAdicional>()), Times.Once);
            MapperMock.Verify(m => m.Map<OrdenCostoAdicional>(It.IsAny<object>()), Times.Once);
        }



    }
}
