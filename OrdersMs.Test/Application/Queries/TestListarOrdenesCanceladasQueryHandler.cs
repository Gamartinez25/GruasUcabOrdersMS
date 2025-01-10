
using Moq;
using OrdersMS.Application.Handlers.OrdenHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarOrdenesCanceladasQueryHandler
    {
        private readonly ListarOrdenesCanceladasHandler Handler;
        private readonly Mock<IOrdenRepository> OrdenRepositoryMock;
        private readonly Mock<IGoogleService> GoogleServiceMock;

        public TestListarOrdenesCanceladasQueryHandler()
        {
            OrdenRepositoryMock = new Mock<IOrdenRepository>();
            GoogleServiceMock = new Mock<IGoogleService>();
            Handler = new ListarOrdenesCanceladasHandler(OrdenRepositoryMock.Object, GoogleServiceMock.Object);
        }
        [Fact]
        public async Task Handle_WithCancelledOrders_ReturnsListOfOrdenCanceladaDto()
        {
            var listaOrdenes = new List<OrdenDeServicio>
    {
        new OrdenDeServicio(
            Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"),
            "#001",
            new DateTime(2025, 1, 8),
            "Colisión leve en avenida principal.",
            "10.5050,-66.9188", "10.4999,-66.9170",
            30.00, "Juan Pérez", "V", "12345678",
            Guid.Parse("1168c9e7-ad7f-4a13-a3a5-a9437052cda5"),
            null,
            Guid.Parse("40bc0cea-518e-46fc-a38c-43e234bcf9a6"),
            Guid.Parse("ebee3e87-a5b7-4690-84bb-2aedd5d18095"),
            5.0, 20.00, 10.00
        )
    };

            var ordenEstatus = new EstadoOrden(Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"), "Cancelado", new DateTime(2025, 1, 8));

            // Configura la respuesta para la orden con el estado "Cancelado"
            OrdenRepositoryMock.Setup(s => s.GetEstadoOrdenByIdOrdenAsync(Guid.Parse("82130627-1ff9-46a6-9347-4358c988d7e8"))).ReturnsAsync(ordenEstatus);

            // Configura la llamada para obtener todas las órdenes
            OrdenRepositoryMock.Setup(s => s.GetAllOrdenAsync()).ReturnsAsync(listaOrdenes);
            GoogleServiceMock.Setup(s => s.GetDirecction(It.IsAny<double>(), It.IsAny<double>())).ReturnsAsync("Farmatodo,Av Ventuari");


            // Actuar
            var result = await Handler.Handle(new ListarOrdenesCanceladasQuery(Guid.Parse("ebee3e87-a5b7-4690-84bb-2aedd5d18095")), new System.Threading.CancellationToken());

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(1,result.Count);
        }


    }
}
