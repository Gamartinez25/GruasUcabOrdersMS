

using System.Reflection.Metadata;
using AutoMapper;
using Moq;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Handlers.TarifaHandlers;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Queries
{
    public class TestListarTarifasQueryHandler
    {
        private readonly Mock<ITarifaRepository> MockTarifaRepository;
        private readonly Mock<IMapper> MockMapper;
        private readonly ListarTarifasHandler Handler;

        public TestListarTarifasQueryHandler()
        {
            MockTarifaRepository = new Mock<ITarifaRepository>();
            MockMapper = new Mock<IMapper>();
            Handler = new ListarTarifasHandler(MockMapper.Object, MockTarifaRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnActiveTarifasMappedToDto()
        {
            // Arrange
            var tarifas = new List<Tarifa>
            {
                new Tarifa (Guid.NewGuid(), "Tarifa 1",100, 50, 2,"Activo" ),
                new Tarifa (Guid.NewGuid(), "Tarifa 2",100, 50, 2,"Inactivo" ),

            };
            var tarifasDto = new List<ListarTarifaDto>
            {
                new ListarTarifaDto(Guid.NewGuid(), "Tarifa 1",100, 50, 2)
            };
            MockTarifaRepository
            .Setup(repo => repo.GetAllTarifaAsync())
            .ReturnsAsync(tarifas);

            MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<ListarTarifaDto>>(It.IsAny<IEnumerable<Tarifa>>()))
                .Returns(tarifasDto);

            var query = new ListarTarifasQuery();
            // Act
            var result = await Handler.Handle(query, CancellationToken.None);

            // Assert
            MockTarifaRepository.Verify(repo => repo.GetAllTarifaAsync(), Times.Once);
            MockMapper.Verify(mapper => mapper.Map<IEnumerable<ListarTarifaDto>>(It.Is<IEnumerable<Tarifa>>(t => t.All(x => x.Estatus == "Activo"))), Times.Once);

            Assert.NotNull(result);
            Assert.Single(result); // Solo una tarifa activa.
            Assert.Equal("Tarifa 1", result.First().Nombre);
        }
    }

    }


