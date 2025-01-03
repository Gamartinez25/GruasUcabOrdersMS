
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Querys
{
    public class ListarOrdenVigentePorGruaQuery:IRequest<OrdenVigentePorGruaDto>
    {
        public ListarOrdenVigentePorGruaQuery(Guid idGrua)
        {
            IdGrua = idGrua;
        }

        public Guid IdGrua { get; private  set; }

    }
}
