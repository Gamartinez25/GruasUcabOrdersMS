

using System.Reflection.Metadata;
using Moq;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;
using OrdersMS.Infrastructure.Repositories;

namespace OrdersMs.Test.Application.Queries
{
    public  class TestInformacionPolizaQueryHandler
    {
        private readonly InformacionPolizaHandler Handler;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<ITarifaRepository> TarifaRepositoryMock;
        private readonly Mock<IOrdenMapper> OrdenMapperMock;

        public TestInformacionPolizaQueryHandler()
        {
            OrdenMapperMock = new Mock<IOrdenMapper>();
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            TarifaRepositoryMock=new Mock<ITarifaRepository>();
            Handler = new InformacionPolizaHandler(OrdenMapperMock.Object, OrdenRepositoryMock.Object, TarifaRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ActivePolizaAsegurado_ReturnsInformacionPolizaDto()
        {
            //Arrange
            var polizaAsegurado = new PolizaAsegurado(Guid.NewGuid(), "12-02-2024", "12-02-2025", "Ford", "Aveo", "2004", "AA09DS", "Carro", "Azul", "Activo");
            var polizas = new List<Poliza>{new Poliza(   Guid.Parse("a1b2c3d4-e5f6-7890-abcd-1234567890ef"),   "Poliza Básica",   500.0,"Cobertura básica contra accidentes menores")};
            var asegurados = new List<Asegurado>{new Asegurado( Guid.NewGuid(), "Juan", "Pérez", "1990-05-15", "Cédula", "12345678", "Activo")};
            var tarifas = new List<Tarifa>{new Tarifa (Guid.NewGuid(), "Tarifa 1",100, 50, 2,"Activo" ),};
            OrdenRepositoryMock.Setup(s=>s.GetPolizaAseguradoById(Guid.NewGuid())).ReturnsAsync(new PolizaAsegurado());
            OrdenRepositoryMock.Setup(s => s.GetAllPolizaAsync()).ReturnsAsync(polizas);
            OrdenRepositoryMock.Setup(s => s.GetAllAseguradoAsync()).ReturnsAsync(asegurados);
            TarifaRepositoryMock.Setup(s=>s.GetAllTarifaAsync()).ReturnsAsync(tarifas);
            OrdenMapperMock.Setup(s => s.ConsultarInformacionPoliza(It.IsAny<Guid>(),
                                                                    It.IsAny<IEnumerable<PolizaAsegurado>>(),
                                                                    It.IsAny<IEnumerable<Poliza>>(),
                                                                    It.IsAny<IEnumerable<Asegurado>>(),
                                                                    It.IsAny<IEnumerable<Tarifa>>())).Returns(new InformacionPolizaDto());
            //Act
            var result = Handler.Handle(new InformacionPolizaQuery(Guid.NewGuid()), new System.Threading.CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Task<InformacionPolizaDto>>(result);


        }
        [Fact]
        public async Task Handle_InactivePolizaAsegurado_ThrowsInvalidOperationException()
        {
            //Arrange
            var polizaAsegurado = new PolizaAsegurado(Guid.NewGuid(), "12-02-2024", "12-02-2025", "Ford", "Aveo", "2004", "AA09DS", "Carro", "Azul", "Inactivo");
            //Act y Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => Handler.Handle(new InformacionPolizaQuery(Guid.NewGuid()), new System.Threading.CancellationToken()));
            Assert.Equal("La póliza asegurada debe estar activa para realizar esta operación", exception.Message);

        }
    }
}
