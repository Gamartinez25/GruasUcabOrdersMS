using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarOrdenesFinalizadasQuery:IRequest<List<OrdenesFinalizadasDto>>
    {
        public ListarOrdenesFinalizadasQuery( Guid idVehiculo)
        {
            IdVehiculo=idVehiculo;
        }

        public Guid IdVehiculo { get; private set; }

    }
}
