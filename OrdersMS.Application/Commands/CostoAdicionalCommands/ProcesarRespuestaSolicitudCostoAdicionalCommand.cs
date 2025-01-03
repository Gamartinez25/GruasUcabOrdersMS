using MediatR;

namespace OrdersMS.Application.Commands.CostoAdicionalCommands
{
    public class ProcesarRespuestaSolicitudCostoAdicionalCommand:IRequest
    {
        public ProcesarRespuestaSolicitudCostoAdicionalCommand(Guid idCostAdicional, string respuestaSolicitudCostAdicional)
        {
            IdCostAdicional = idCostAdicional;
            RespuestaSolicitudCostAdicional = respuestaSolicitudCostAdicional;
        }

        public Guid IdCostAdicional { get; private set; }
        public string RespuestaSolicitudCostAdicional { get; private set; }

    }
}
