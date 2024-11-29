using AutoMapper;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Mappers.CostoAdicionalMappers
{
    public class SalidaCostoAdicionalMapper : ISalidaCostoAdicionalMapper
    {
        public IEnumerable<ListarCostosAdicionalesPorOrdenDto> Map(IEnumerable<OrdenCostoAdicional> ordenCostoAdicionales, IEnumerable<Tuple<Guid, string>> nombreCostos)
        {
            var nombreCostoSinRepeticion=nombreCostos.Distinct().ToList();
            var costoNombreDiccionario = nombreCostoSinRepeticion.ToDictionary(tupla => tupla.Item1, tupla => tupla.Item2);
            var listaCostos=new List<ListarCostosAdicionalesPorOrdenDto>();
            foreach (var orden in ordenCostoAdicionales)
            {
                if (orden.Estatus != "Inactivo")
                {
                    var nombre = costoNombreDiccionario[orden.CostoAdicionalId].Trim();
                    var ordenCosto = new ListarCostosAdicionalesPorOrdenDto(orden.IdCostoOrden, nombre, orden.Costo, orden.Descripcion, orden.Estatus);
                    listaCostos.Add(ordenCosto);
                }
            }
            return listaCostos;
        }
    }
}
