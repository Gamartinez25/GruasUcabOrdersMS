using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrdersMS.Application.Commands.AsignarConductorCommand
{
    public class AsignarConductorCommand:IRequest
    {
        public AsignarConductorCommand(Guid ordenId)
        {
            OrdenId = ordenId;
        }

        public Guid OrdenId { get; set; }
    }
}
