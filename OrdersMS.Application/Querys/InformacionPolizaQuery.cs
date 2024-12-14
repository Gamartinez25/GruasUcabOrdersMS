
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class InformacionPolizaQuery : IRequest<InformacionPolizaDto>
    {
        public InformacionPolizaQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
}
