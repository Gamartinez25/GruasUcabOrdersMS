using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Mappers.CostoAdicionalMappers
{
    public interface ISalidaCostoAdicionalMapper
    {
        public IEnumerable<ListarCostosAdicionalesPorOrdenDto> Map(IEnumerable<OrdenCostoAdicional> ordenCostoAdicionales, IEnumerable<Tuple<Guid, string>> nombreCostos);
       // public OrdenCostoAdicional MapEntrada(OrdenCostoAdicional existingCostoAdicional, ModificarCostoAdicionalDto costoAdicionalDto);
    }
}
