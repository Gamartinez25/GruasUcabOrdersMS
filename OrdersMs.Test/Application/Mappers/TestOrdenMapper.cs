using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Application.Mappers.OrdenMappers;
using OrdersMS.Domain.Entities;
using static MassTransit.ValidationResultExtensions;

namespace OrdersMs.Test.Application.Mappers
{
    public class TestOrdenMapper
    {
        private readonly OrdenMapper Handler;

        public TestOrdenMapper()
        {
            Handler = new OrdenMapper();
        }
        [Fact]
        public void ConsultarInformacionPoliza_ShouldReturnCorrectDto()
        {
            var id = Guid.NewGuid(); // ID único para la póliza asegurada
            var tarifaId = Guid.NewGuid(); // ID único para la tarifa
            var polizaId = Guid.NewGuid(); // ID único para la póliza
            var aseguradoId = Guid.NewGuid(); // ID único para el asegurado

            List<Tarifa> tarifas = new List<Tarifa> { new Tarifa(tarifaId, "Tarifa Normal", 50.00, 10.0, 5.00, "Activa") };
            List<Poliza> polizas = new List<Poliza> { new Poliza(polizaId, "Póliza Básica", 500.00, "Cobertura básica para vehículos.", tarifaId) };
            List<Asegurado> asegurados = new List<Asegurado> { new Asegurado(aseguradoId, "Juan", "Pérez", "1990-05-15", "V", "12345678", "Activo") };
            List<PolizaAsegurado> polizasAsegurado = new List<PolizaAsegurado> { new PolizaAsegurado(id, "2023-01-01", "2024-01-01", "Toyota", "Corolla", "2020", "ABC123", "Sedán", "Rojo", "Activo", polizaId, aseguradoId) };
            var result = Handler.ConsultarInformacionPoliza(id, polizasAsegurado, polizas, asegurados, tarifas);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Juan Pérez", result.NombreAsegurado);
            Assert.IsType<InformacionPolizaDto>(result);
        }

        [Fact]
        public void ListarOrdenesDtos_ShouldReturnCorrectDtos()
        {
            var id = Guid.NewGuid(); // ID único para la póliza asegurada
            var tarifaId = Guid.NewGuid(); // ID único para la tarifa
            var polizaId = Guid.NewGuid(); // ID único para la póliza
            var aseguradoId = Guid.NewGuid(); // ID único para el asegurado
            var idOrden=Guid.NewGuid();//ID unico para la orden
            var orden = new OrdenDeServicio(idOrden,"#001",DateTime.Now,"Accidente en la autopista","Calle Falsa 123, Ciudad X","Avenida Siempre Viva 456, Ciudad Y",200.50, "Juan Pérez","V","12345678", id,  null,  Guid.NewGuid(),null,15.0,50.0,30.0);
            List<OrdenDeServicio> ordenes= new List<OrdenDeServicio> { orden};
            List<EstadoOrden> estados = new List<EstadoOrden> { new EstadoOrden(idOrden, "EnProceso", DateTime.Now) };
            List<Tarifa> tarifas = new List<Tarifa> { new Tarifa(tarifaId, "Tarifa Normal", 50.00, 10.0, 5.00, "Activa") };
            List<Poliza> polizas = new List<Poliza> { new Poliza(polizaId, "Póliza Básica", 500.00, "Cobertura básica para vehículos.", tarifaId) };
            List<Asegurado> asegurados = new List<Asegurado> { new Asegurado(aseguradoId, "Juan", "Pérez", "1990-05-15", "V", "12345678", "Activo") };
            List<PolizaAsegurado> polizasAsegurado = new List<PolizaAsegurado> { new PolizaAsegurado(id, "2023-01-01", "2024-01-01", "Toyota", "Corolla", "2020", "ABC123", "Sedán", "Rojo", "Activo", polizaId, aseguradoId) };
            var result= Handler.ListarOrdenesDtos(ordenes,estados,polizasAsegurado, polizas,asegurados,tarifas);
            Assert.NotNull(result);
            var dto = result.First();
            Assert.Equal(idOrden, dto.Id);
        }
        [Fact]
        public void ModificarOrden_ShouldReturnOrdenDeServicio()
        {
            var orden = new OrdenDeServicio(Guid.NewGuid(), "#001", DateTime.Now, "Accidente en la autopista", "Calle Falsa 123, Ciudad X", "Avenida Siempre Viva 456, Ciudad Y", 200.50, "Juan Pérez", "V", "12345678", Guid.NewGuid(), null, Guid.NewGuid(), null, 15.0, 50.0, 30.0);
            var tarifa = new Tarifa(Guid.NewGuid(), "Tarifa Normal", 50.00, 10.0, 5.00, "Activa");
            var ordenDto = new ModificarOrdenDto(orden.Id, Guid.NewGuid(), 2, 4);
            var result = Handler.ModificarOrden(tarifa,orden,ordenDto);
            Assert.NotNull(result);
            Assert.Equal(4, result.CostoTotalKmExtra);
            Assert.IsType<OrdenDeServicio>(result);

        }
    }
    }
