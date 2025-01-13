using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Moq;
using OrdersMS.Application.Commands.AsignarConductorCommand;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.AsignarConductorHandler;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Saga.Events;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Core.Services.MsUsers;
using OrdersMS.Domain.Entities;
using OrdersMS.Infrastructure.Mappers;

namespace OrdersMs.Test.Application.Commands
{
    public class TestAsignarConductorCommandHandler
    {
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IGoogleService> GoogleServiceMock;
        private readonly Mock<IOrdenMapper> OrdenMapperMock;
        private readonly Mock<IPublishEndpoint> PublishEndpointMock;
        private readonly Mock<IVehiculosAsignadosRepository> VehiculosAsignadosRepositoryMock;
        private readonly Mock<IUserMsService> UserMsServiceMock;
        private readonly AsignarConductorHandler Handler;

        public TestAsignarConductorCommandHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            GoogleServiceMock = new Mock<IGoogleService>();
            OrdenRepositoryMock= new Mock<IOrdenRepository>();
            PublishEndpointMock= new Mock<IPublishEndpoint>();
            VehiculosAsignadosRepositoryMock = new Mock<IVehiculosAsignadosRepository>();
            UserMsServiceMock= new Mock<IUserMsService>();
            OrdenMapperMock= new Mock<IOrdenMapper>();
            Handler=new AsignarConductorHandler(OrdenRepositoryMock.Object,GoogleServiceMock.Object,OrdenMapperMock.Object,PublishEndpointMock.Object,VehiculosAsignadosRepositoryMock.Object,UserMsServiceMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldThrowValidatorException_WhenOrderStateIsNotPorAsignar()
        {

            // Arrange
            var idOrden=Guid.NewGuid();
            var estatusOrdenes = new List<EstadoOrden> { new EstadoOrden(idOrden, "OtroEstado", DateTime.UtcNow) };

            var orden = new OrdenDeServicio(idOrden,
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
            var tarifa = new Tarifa(Guid.NewGuid(), "Tarifa 1", 200, 200, 2, "Activo");
            OrdenRepositoryMock.Setup(repo => repo.GetAllEstadoOrden()).ReturnsAsync(estatusOrdenes);
            OrdenRepositoryMock.Setup(repo => repo.GetOrdenDeServicioByIdAsync(It.IsAny<Guid>())).ReturnsAsync(orden);

            var command = new AsignarConductorCommand(idOrden);
            // Act & Assert
            await Assert.ThrowsAsync<ValidatorException>(() => Handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_ShouldThrowInvalidOperationException_WhenNoAvailableVehicles()
        {
            // Arrange
            var idOrdenNueva = Guid.NewGuid();
            var idOrdenVieja= Guid.NewGuid();
            var estatusOrdenes = new List<EstadoOrden> { new EstadoOrden(idOrdenNueva, "PorAsignar", DateTime.UtcNow), new EstadoOrden(idOrdenVieja, "EnProceso", DateTime.UtcNow) };

            var orden1 = new OrdenDeServicio(idOrdenNueva,
                                                   "#001",
                                                   DateTime.Now,
                                                   "Accidente en la autopista",
                                                   "34.0522, -118.2437 ",
                                                   "34.0520, -118.2480",
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
            var orden2 = new OrdenDeServicio(idOrdenVieja,
                                                  "#002",
                                                  DateTime.Now,
                                                  "Accidente en la autopista",
                                                  "34.0522, -118.2437 ",
                                                  "34.0520, -118.2480",
                                                  200.50,
                                                  "Juan Pérez",
                                                  "V",
                                                  "12345678",
                                                  Guid.NewGuid(),
                                                  null,
                                                  Guid.NewGuid(),
                                                  Guid.Parse("34ac5fae-e321-46ce-b105-24cedb311119"),
                                                  15.0,
                                                  50.0,
                                                  30.0);
            var tarifa = new Tarifa(Guid.NewGuid(), "Tarifa 1", 200, 200, 2, "Activo");
            RutaDto ruta = new RutaDto
            {
                DistanciaValor = 15, // Distancia en kilómetros
                DistanciaTexto = "15 km",
                DuracionValor = 30, // Duración en minutos
                DuracionTexto = "30 minutos"
            };
            List<VehiculoDto> vehiculos = new List<VehiculoDto>
        {
            new VehiculoDto
            {
                Id = Guid.Parse("34ac5fae-e321-46ce-b105-24cedb311119"),
                Tipo = "Coche",
                Marca = "Toyota",
                Modelo = "Corolla",
                Placa = "ABC123",
                Color = "Rojo",
                Latitud = 34.0522,
                Longitud = -118.2437,
                DistanciaValor = 15,
                DistanciaTexto = "15 km",
                DuracionValor = 30,
                DuracionTexto = "30 minutos"
            },
            
        };
            OrdenRepositoryMock.Setup(repo => repo.GetAllEstadoOrden()).ReturnsAsync(estatusOrdenes);
            OrdenRepositoryMock.Setup(repo => repo.GetOrdenDeServicioByIdAsync(It.IsAny<Guid>())).ReturnsAsync(orden1);

            var command = new AsignarConductorCommand(idOrdenNueva);
            GoogleServiceMock.Setup(service => service.GetDistanceToOriginAccidentDestination(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(ruta);
            OrdenRepositoryMock.Setup(s => s.GetAllOrdenAsync()).ReturnsAsync(new List<OrdenDeServicio> { orden1,orden2 });
            VehiculosAsignadosRepositoryMock.Setup(repo => repo.GetGruasAsignadasPreviamente(It.IsAny<Guid>())).Returns((List<Guid>)null);
            GoogleServiceMock.Setup(service => service.GetDistanceAvailableVehiclesToOrigin(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(vehiculos);

            var cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => Handler.Handle(command, cancellationToken));
        }
        [Fact]
        public async Task Handle_ShouldUpdateOrder_WhenGruaIsAssigned()
        {
            // Arrange
            var idOrden = Guid.NewGuid();
            var estatusOrdenes = new List<EstadoOrden> { new EstadoOrden(idOrden, "PorAsignar", DateTime.UtcNow) };
            var orden = new OrdenDeServicio(idOrden,
                                                  "#001",
                                                  DateTime.Now,
                                                  "Accidente en la autopista",
                                                  "34.0522, -118.2437 ",
                                                  "34.0520, -118.2480",
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
            RutaDto ruta = new RutaDto
            {
                DistanciaValor = 15, // Distancia en kilómetros
                DistanciaTexto = "15 km",
                DuracionValor = 30, // Duración en minutos
                DuracionTexto = "30 minutos"
            };
            List<VehiculoDto> vehiculos = new List<VehiculoDto>
            {
                new VehiculoDto
                {
                    Id = Guid.Parse("34ac5fae-e321-46ce-b105-24cedb311119"),
                    Tipo = "Coche",
                    Marca = "Toyota",
                    Modelo = "Corolla",
                    Placa = "ABC123",
                    Color = "Rojo",
                    Latitud = 34.0522,
                    Longitud = -118.2437,
                    DistanciaValor = 15,
                    DistanciaTexto = "15 km",
                    DuracionValor = 30,
                    DuracionTexto = "30 minutos"
                },

            };
            var tarifa = new Tarifa(Guid.NewGuid(), "Tarifa 1", 200, 200, 2, "Activo");

            var command = new AsignarConductorCommand(idOrden);
            OrdenRepositoryMock.Setup(s => s.GetTarifaByIdOrdenAsync(idOrden)).ReturnsAsync(tarifa);
            OrdenRepositoryMock.Setup(repo => repo.GetAllEstadoOrden()).ReturnsAsync(estatusOrdenes);
            OrdenRepositoryMock.Setup(repo => repo.GetOrdenDeServicioByIdAsync(It.IsAny<Guid>())).ReturnsAsync(orden);
            GoogleServiceMock.Setup(service => service.GetDistanceToOriginAccidentDestination(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(ruta);
            OrdenRepositoryMock.Setup(s => s.GetAllOrdenAsync()).ReturnsAsync(new List<OrdenDeServicio> { orden });
            VehiculosAsignadosRepositoryMock.Setup(repo => repo.GetGruasAsignadasPreviamente(It.IsAny<Guid>())).Returns((List<Guid>)null);
            GoogleServiceMock.Setup(service => service.GetDistanceAvailableVehiclesToOrigin(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync(vehiculos);
            OrdenMapperMock.Setup(s => s.ModificarOrden(It.IsAny<Tarifa>(), It.IsAny<OrdenDeServicio>(), It.IsAny<ModificarOrdenDto>())).Returns(orden);
            OrdenRepositoryMock.Setup(s=>s.UpdateOrdenAsync(orden)).Returns(Task.CompletedTask);
            VehiculosAsignadosRepositoryMock.Setup(s=>s.AddAsignacionGrua(idOrden, Guid.Parse("34ac5fae-e321-46ce-b105-24cedb311119")));    
            UserMsServiceMock.Setup(s=>s.SendNotification(It.IsAny<Guid>())).Returns(Task.CompletedTask);
           
            // Act
           var result = await Handler.Handle(command, CancellationToken.None);

            // Assert
            OrdenRepositoryMock.Verify(repo => repo.UpdateOrdenAsync(It.IsAny<OrdenDeServicio>()), Times.Once);
            PublishEndpointMock.Verify(p => p.Publish(It.IsAny<ActualizarOrdenEvent>(), CancellationToken.None), Times.Once);

        }


    }
}
