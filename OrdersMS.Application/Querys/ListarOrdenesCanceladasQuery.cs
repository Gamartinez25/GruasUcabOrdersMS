
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarOrdenesCanceladasQuery:IRequest<List<OrdenCanceladaDto>>
    {
        public ListarOrdenesCanceladasQuery( Guid idVehiculo)
        {
            IdVehiculo = idVehiculo;
        }

        public Guid IdVehiculo { get;  private set; }
    }
}
