using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Application.Mappers.CostoAdicionalMappers;
using OrdersMS.Domain.Entities;

namespace OrdersMs.Test.Application.Mappers
{
    public class TestSalidaCostoAdicionalMapper
    {
        private readonly SalidaCostoAdicionalMapper Mapper;

        public TestSalidaCostoAdicionalMapper()
        {
            Mapper=new SalidaCostoAdicionalMapper();
        }
        [Fact]
        public void Map_ShouldReturnFilteredAndMappedCostos()
        {
            // Arrange

            var ordenCostoAdicionales = new List<OrdenCostoAdicional>
        {
            new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(), 150.50, "Por Aprobar","Costo 1",null,DateTime.UtcNow,"Defaul",null,null),
            new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(), 200.75,"Por Aprobar", "Costo 2",null,DateTime.UtcNow,"Defaul",null,null),
            new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(), 300.00,"Por Aprobar","Costo 3",null,DateTime.UtcNow,"Defaul",null,null)
        };

            var nombreCostos = new List<Tuple<Guid, string>>
        {
            Tuple.Create(ordenCostoAdicionales[0].CostoAdicionalId, "Nombre Costo 1"),
            Tuple.Create(ordenCostoAdicionales[1].CostoAdicionalId, "Nombre Costo 2"),
            Tuple.Create(ordenCostoAdicionales[2].CostoAdicionalId, "Nombre Costo 3")
        };

            // Act
            var result = Mapper.Map(ordenCostoAdicionales, nombreCostos);

            // Assert
            Assert.NotNull(result);
            var firstCosto = result.First();
            Assert.Equal(ordenCostoAdicionales[0].IdCostoOrden, firstCosto.Id);
            Assert.Equal("Nombre Costo 1", firstCosto.Nombre);
            Assert.Equal("Costo 1", firstCosto.Descripcion);
        }
        [Fact]
        public void Map_ShouldThrowExceptionForMissingCostoAdicionalId()
        {
            // Arrange
            var ordenCostoAdicionales = new List<OrdenCostoAdicional>{new OrdenCostoAdicional(Guid.NewGuid(), Guid.NewGuid(),Guid.NewGuid(), 150.50, "Por Aprobar","Costo 1",null,DateTime.UtcNow,"Defaul",null,null) }; 
            var nombreCostos = new List<Tuple<Guid, string>>(); // Lista vacía para simular un ID no encontrado

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => Mapper.Map(ordenCostoAdicionales, nombreCostos));
        }
    }
}
