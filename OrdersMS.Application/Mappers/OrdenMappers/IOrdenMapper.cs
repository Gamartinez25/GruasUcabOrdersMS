using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Mappers.OrdenMappers
{
    public interface IOrdenMapper
    {
        public IEnumerable<ListarOrdenesDto> ListarOrdenesDtos(IEnumerable<OrdenDeServicio> ordenes,IEnumerable<PolizaAsegurado> polizaAsegurados,IEnumerable<Poliza> polizas,IEnumerable<Asegurado>asegurados,IEnumerable<Tarifa> tarifas);
        public OrdenDeServicio ModificarOrden(OrdenDeServicio orden,ModificarOrdenDto ordenDto);

        public InformacionPolizaDto ConsultarInformacionPoliza(Guid id, IEnumerable<PolizaAsegurado> polizaAsegurados, IEnumerable<Poliza> polizas, IEnumerable<Asegurado> asegurados, IEnumerable<Tarifa> tarifas);

    }
}
