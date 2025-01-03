using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarResumenOrdenesQuery:IRequest<List<ResumenOrdenDto>>
    {
        public ListarResumenOrdenesQuery(Guid idVehiculo)
        {
            IdVehiculo=idVehiculo;

        }

        public Guid IdVehiculo { get; set; }
    }
}
