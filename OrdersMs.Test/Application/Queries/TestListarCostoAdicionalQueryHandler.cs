
using System.Reflection.Metadata;
using Moq;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Application.Handlers.CostoAdicionalHandlers;
using OrdersMS.Application.Mappers.CostoAdicionalMappers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarCostoAdicionalQueryHandler
    {
        private readonly Mock<ICostoAdicionalRepository> CostoAdicionalRepositoryMock;
        private readonly Mock<ISalidaCostoAdicionalMapper> CostoAdicionalMapperMock;
        private readonly ListarCostoAdicionalPorOrdenHandler Handler;

        public TestListarCostoAdicionalQueryHandler()
        {
            CostoAdicionalMapperMock=new Mock<ISalidaCostoAdicionalMapper> ();
            CostoAdicionalRepositoryMock=new Mock<ICostoAdicionalRepository> ();
            Handler=new ListarCostoAdicionalPorOrdenHandler(CostoAdicionalMapperMock.Object,CostoAdicionalRepositoryMock.Object);   
        }
        [Fact]
        public async Task Handle_ShouldCallRepositoriesWithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var listaOrdenCostoAdicional = new List<OrdenCostoAdicional>
                                            {
                                                new OrdenCostoAdicional(
                                                    idCostoOrden: Guid.Parse("b3e1c9d8-5b9d-4e7a-a1cb-f9d9a640d9a1"),
                                                    ordenDeServicioId:id,
                                                    costoAdicionalId: Guid.Parse("a87f19e6-5c33-41a5-bb4d-d0b41b4f3721"),
                                                    costo: 150.75,
                                                    estatus: "PorAceptar",
                                                    descripcion: "Costo adicional por uso de grúa en horario nocturno",
                                                    Id: 1,
                                                    fechaCreacion: DateTime.Parse("2025-01-01T10:00:00"),
                                                    creadoPor: "Admin",
                                                    fechaActualizacion: null,
                                                    actualizadoPor: null
                                                ),
                                                new OrdenCostoAdicional(
                                                    idCostoOrden: Guid.Parse("d9f5c3b8-6c54-49e1-90c8-5f3d14d9b8c3"),
                                                    ordenDeServicioId: id,
                                                    costoAdicionalId: Guid.Parse("b46e18c7-9f82-4c2d-85d8-6173b29a4f73"),
                                                    costo: 85.50,
                                                    estatus: "Rechazado",
                                                    descripcion: "Costo adicional por kilómetros extra",
                                                    Id: 2,
                                                    fechaCreacion: DateTime.Parse("2024-12-25T08:30:00"),
                                                    creadoPor: "Supervisor",
                                                    fechaActualizacion: null,
                                                    actualizadoPor: null
                                                )
                                            };

            var query = new ListarCostoAdicionalPorOrdenQuery(id);   
            CostoAdicionalRepositoryMock.Setup(repo => repo.GetAllCostoAdicionalAsync(id))
                .ReturnsAsync(listaOrdenCostoAdicional);
            CostoAdicionalRepositoryMock.Setup(repo=>repo.GetAllNombresCostosAdicionalesByIdAsync(id))
                .ReturnsAsync(new List<Tuple<Guid,string>>());


            
            // Act
           var result= await Handler.Handle(query, CancellationToken.None);

            // Assert
            CostoAdicionalRepositoryMock.Verify(repo => repo.GetAllCostoAdicionalAsync(id), Times.Once);
            CostoAdicionalRepositoryMock.Verify(repo => repo.GetAllNombresCostosAdicionalesByIdAsync(id), Times.Once);
            Assert.Equivalent(result.ToList(), new List<ListarCostosAdicionalesPorOrdenDto>());
        }
    }
}
