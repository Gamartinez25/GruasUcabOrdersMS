using MediatR;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Handlers.OrdenHandlers
{
    public class ListarEstatusOrdenPorEstatusHandler : IRequestHandler<ListarEstatusOrdenPorEstatusQuery, List<EstadoOrden>>
    {
        private readonly IOrdenRepository OrdenRepository;

        public ListarEstatusOrdenPorEstatusHandler(IOrdenRepository ordenRepository)
        {
            OrdenRepository= ordenRepository;
        }

        public async  Task<List<EstadoOrden>> Handle(ListarEstatusOrdenPorEstatusQuery request, CancellationToken cancellationToken)
        {
            var estatusOrdenes = await OrdenRepository.GetAllEstadoOrden();
            var ordenesFiltradas = new List<EstadoOrden>();
            foreach(var orden in estatusOrdenes) 
            { 
                if (orden.EstadoActual==request.Estatus)
                {
                    ordenesFiltradas.Add(orden);
                }
            }
            return ordenesFiltradas;
        }
    }
}
