using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using AutoMapper;
using MassTransit;
using Moq;
using OrdersMS.Application.Commands.OrdenCommands;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrdersMs.Test.Application.Commands
{
    public class TestCrearOrdenCommandHandler
    {
        private readonly Mock<IMapper> MapperMock;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IPublishEndpoint> PublishEndpointMock;
        private readonly CrearOrdenHandler Handler;

        public TestCrearOrdenCommandHandler()
        {
            MapperMock = new Mock<IMapper>();
            OrdenRepositoryMock= new Mock<IOrdenRepository>();
            PublishEndpointMock = new Mock<IPublishEndpoint>();
            Handler=new CrearOrdenHandler(MapperMock.Object, OrdenRepositoryMock.Object, PublishEndpointMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenAdministradorAndOperadorAreSet()
        {
            var crearOrdenDto = new CrearOrdenDto(
                                     "Colisión leve en una intersección",
                                     "Av. Principal Los Jardines, Caracas",
                                     "Calle Bolívar, Maracay",
                                     "Juan Pérez",
                                     "V",
                                     "12345678",
                                      Guid.NewGuid(),
                                      Guid.NewGuid(),
                                     Guid.NewGuid());
            var command =new CrearOrdenCommand(crearOrdenDto);
            var exception = await Assert.ThrowsAsync<InvalidIdException>(() => Handler.Handle(command, CancellationToken.None));
            Assert.Equal("El responsable del registro de la orden es único", exception.Message);


        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenAdministradorAndOperadorAreNull()
        {
            var crearOrdenDto = new CrearOrdenDto(
                                     "Colisión leve en una intersección",
                                     "Av. Principal Los Jardines, Caracas",
                                     "Calle Bolívar, Maracay",
                                     "Juan Pérez",
                                     "V",
                                     "12345678",
                                     Guid.NewGuid(),
                                     null,
                                     null);
            var command = new CrearOrdenCommand(crearOrdenDto);
            var exception = await Assert.ThrowsAsync<InvalidIdException>(() => Handler.Handle(command, CancellationToken.None));
            Assert.Equal("El id del responsable de crear la orden no puede ser nulo", exception.Message);


        }
        [Fact]
        public async Task Handle_successful()
        {
            var crearOrdenDto = new CrearOrdenDto(
                                    "Colisión leve en una intersección",
                                    "Av. Principal Los Jardines, Caracas",
                                    "Calle Bolívar, Maracay",
                                    "Juan Pérez",
                                    "V",
                                    "12345678",
                                    Guid.NewGuid(),
                                    Guid.NewGuid(),
                                    null);
            var command = new CrearOrdenCommand(crearOrdenDto);
            var orden = new OrdenDeServicio(Guid.NewGuid(),
                                                    "#001",
                                                    DateTime.Now,
                                                    "Accidente en la autopista",
                                                    "Calle Falsa 123, Ciudad X",
                                                    "Avenida Siempre Viva 456, Ciudad Y",
                                                    200.50,
                                                    "Juan Pérez",
                                                    "V",
                                                    "12345678",
                                                    Guid.NewGuid(),
                                                    null,
                                                    Guid.NewGuid(),
                                                    null,
                                                    15.0,
                                                    50.0,
                                                    30.0);
            MapperMock.Setup(m => m.Map<OrdenDeServicio>(command.CrearOrdenDto)).Returns(orden);
            OrdenRepositoryMock.Setup(s=>s.AddOrdenAsync(orden)).Returns(Task.CompletedTask);
            // Act
            await Handler.Handle(command, CancellationToken.None);

            // Assert
            PublishEndpointMock.Verify(p => p.Publish(It.Is<OrdenCreadaEvent>(e =>
                e.OrdenId == orden.Id),
                It.IsAny<CancellationToken>()),
                Times.Once);
            OrdenRepositoryMock.Verify(x=>x.AddOrdenAsync(orden), Times.Once);  

        }


    }

}
