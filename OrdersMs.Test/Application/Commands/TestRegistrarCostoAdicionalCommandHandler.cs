using AutoMapper;
using FluentValidation;
using Moq;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Handlers.CostoAdicionalHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestRegistrarCostoAdicionalCommandHandler
    {
        private readonly Mock<ICostoAdicionalRepository> CostoAdicionalRepositoryMock;
        private readonly Mock<IMapper> MapperMock;
        private readonly RegistrarCostoAdicionalHandler Handler;

        public TestRegistrarCostoAdicionalCommandHandler()
        {
            CostoAdicionalRepositoryMock=new Mock<ICostoAdicionalRepository> ();
            MapperMock = new Mock<IMapper> ();
            Handler = new RegistrarCostoAdicionalHandler(MapperMock.Object, CostoAdicionalRepositoryMock.Object);

        }
        [Fact]
        public async Task Should_CallAddCostoAdicionalAsync_When_ValidCostoAdicionalDto()
        {
            // Arrange
            var costoAdicionalDto = new RegistrarCostoAdicionalDto(Guid.NewGuid(), Guid.NewGuid(), 200, "Descripcion");
            var command = new RegistrarCostoAdicionalCommand(costoAdicionalDto);
            var costoAdicional = new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 200, "PorAceptar", "Descripcion", null, DateTime.UtcNow, "Defaul", null, null);
 
            MapperMock.Setup(m => m.Map<OrdenCostoAdicional>(It.IsAny<RegistrarCostoAdicionalDto>()))
                .Returns(costoAdicional);
            CostoAdicionalRepositoryMock.Setup(x => x.AddCostoAdicionalAsync(costoAdicional)).Returns(Task.CompletedTask);

            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
           CostoAdicionalRepositoryMock.Verify(r => r.AddCostoAdicionalAsync(It.IsAny<OrdenCostoAdicional>()), Times.Once);
           MapperMock.Verify(m => m.Map<OrdenCostoAdicional>(It.IsAny<RegistrarCostoAdicionalDto>()), Times.Once);
        }
        [Fact]
        public async Task Should_ThrowValidationException_When_CostoAdicionalDtoIsInvalid()
        {
            // Arrange
            var command = new RegistrarCostoAdicionalCommand(new RegistrarCostoAdicionalDto(Guid.Empty, Guid.Empty, 0, null));
                
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command, CancellationToken.None));
        }

    }
}
