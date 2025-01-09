using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarOrdenByIdQuery:IRequest<OrdenByIdDto>
    {
        public ListarOrdenByIdQuery( Guid idOrden)
        {
            IdOrden=idOrden;
        }

        public Guid IdOrden { get; private set; }

    }
}
