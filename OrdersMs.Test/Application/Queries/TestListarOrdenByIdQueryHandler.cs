using Moq;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarOrdenByIdQueryHandler
    {
        private readonly ListarOrdenByIdHandler Handler;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IGoogleService> GoogleServiceMock;

        public TestListarOrdenByIdQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            GoogleServiceMock = new Mock<IGoogleService>();
            Handler=new ListarOrdenByIdHandler(OrdenRepositoryMock.Object,GoogleServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOrdenByIdDto_WhenIdOrdenExists()
        {
            //Arrange
            var listaOrdenes = new List<OrdenDeServicio>();
            var orden1 = new OrdenDeServicio(Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"),"#001",new DateTime(2025, 1, 8),
                                 "Colisión leve en avenida principal.","10.5050,-66.9188","10.4999,-66.9170", 30.00,"Juan Pérez",
                                 "V","12345678",Guid.Parse("1168c9e7-ad7f-4a13-a3a5-a9437052cda5"), null,
                                  Guid.Parse("40bc0cea-518e-46fc-a38c-43e234bcf9a6"),Guid.Parse("ebee3e87-a5b7-4690-84bb-2aedd5d18095"),
                                  5.0, 20.00,10.00);
            var orden2 = new OrdenDeServicio(Guid.Parse("730b4bd5-8877-4309-9b0b-5ef9b32a8f33"), "#002", new DateTime(2024, 3, 8),
                                 "Colisión leve en avenida principal.", "10.5050,-66.9188", "10.4999,-66.9170", 30.00, "Juan Pérez",
                                 "V", "12345678", Guid.Parse("1168c9e7-ad7f-4a13-a3a5-a9437052cda5"), null,
                                  Guid.Parse("40bc0cea-518e-46fc-a38c-43e234bcf9a6"), Guid.Parse("ebee3e87-a5b7-4690-84bb-2aedd5d18095"),
                                  5.0, 20.00, 10.00);
            listaOrdenes.Add(orden1);
            listaOrdenes.Add(orden2 );
            OrdenRepositoryMock.Setup(s=>s.GetAllOrdenAsync()).ReturnsAsync(listaOrdenes);
            var estadoOrden = new EstadoOrden(Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"), "EnProceso", new DateTime(2024, 3, 8));
            OrdenRepositoryMock.Setup(s => s.GetEstadoOrdenByIdOrdenAsync(Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"))).ReturnsAsync(estadoOrden);
            var poliza = new PolizaAsegurado(Guid.NewGuid(),"12-02-2024","12-02-2025","Ford","Aveo","2004","AA09DS","Carro","Azul","Activo");
            OrdenRepositoryMock.Setup(s => s.GetPolizaAseguradoById(Guid.Parse("1168c9e7-ad7f-4a13-a3a5-a9437052cda5"))).ReturnsAsync(poliza);
            GoogleServiceMock.Setup(s => s.GetDirecction(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync("Farmatodo,Av Ventuari");
            //Act
            var result = Handler.Handle(new ListarOrdenByIdQuery(Guid.Parse("730b4bd5-8877-4309-9b0b-5ef9b32a8f33")), new System.Threading.CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<Task<OrdenByIdDto>>(result);
            
        }
    }
}
