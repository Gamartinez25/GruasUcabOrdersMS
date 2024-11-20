using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Commands.TarifaCommands
{
    public class EliminarTarifaCommand : IRequest
    {
        public EliminarTarifaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}
