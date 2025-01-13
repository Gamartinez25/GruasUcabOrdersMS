using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Application.Handlers.CostoAdicionalHandlers;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Commands
{
    public class TestEliminarCostoAdicionalHandler
    {
        private readonly Mock<ICostoAdicionalRepository> CostoAdicionalRepositoryMock;
        private readonly EliminarCostoAdicionalHandler Handler;

        public TestEliminarCostoAdicionalHandler()
        {
            CostoAdicionalRepositoryMock = new Mock<ICostoAdicionalRepository>();
            Handler= new EliminarCostoAdicionalHandler(CostoAdicionalRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenEstadoIsNotPorAprobar()
        {
            // Arrange
            var id=Guid.NewGuid();
            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(), 200, "Rechazado", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);
            var request = new EliminarCostoAdicionalCommand(id);
            
            CostoAdicionalRepositoryMock.Setup(x=>x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidAdditionalCostStateException>(() => Handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallDeleteCostoAdicional_WhenEstadoIsPorAprobar()
        {
            // Arrange
            var id = Guid.NewGuid();
            var costoAdicional = new OrdenCostoAdicional(id, Guid.NewGuid(), Guid.NewGuid(), 200, "Por Aprobar", "Descripcion", null, DateTime.UtcNow, "Defauld", null, null);
            var request = new EliminarCostoAdicionalCommand(id);

            CostoAdicionalRepositoryMock.Setup(x => x.GetCostoAdicionalByIdAsync(id)).ReturnsAsync(costoAdicional);
            CostoAdicionalRepositoryMock.Setup(s=>s.DeleteCostoAdicional(id)).Returns(Task.CompletedTask);
            // Act
            await Handler.Handle(request, CancellationToken.None);

            // Assert
            CostoAdicionalRepositoryMock.Verify(repo => repo.DeleteCostoAdicional(request.Id), Times.Once);
        }

    }
}
