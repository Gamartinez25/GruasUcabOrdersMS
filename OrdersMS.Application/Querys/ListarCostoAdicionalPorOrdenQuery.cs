using MediatR;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarCostoAdicionalPorOrdenQuery:IRequest<IEnumerable<ListarCostosAdicionalesPorOrdenDto>>
    {
        public ListarCostoAdicionalPorOrdenQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
}
