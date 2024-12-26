using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Commands.AsignarConductorCommand
{
    public class AsignarConductorCommand:IRequest<VehiculosDisponiblesAsignarOrdenDto>
    {
        public AsignarConductorCommand(Guid ordenId)
        {
            OrdenId = ordenId;
        }

        public Guid OrdenId { get; set; }
    }
}
