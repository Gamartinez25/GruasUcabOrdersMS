using MediatR;
using OrdersMS.Application.Commands.CostoAdicionalCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Core.Repositories;

namespace OrdersMS.Application.Handlers.CostoAdicionalHandlers
{
    public class EliminarCostoAdicionalHandler : IRequestHandler<EliminarCostoAdicionalCommand>
    {
        private readonly ICostoAdicionalRepository CostoAdicionalRepository;

        public EliminarCostoAdicionalHandler(ICostoAdicionalRepository costoAdicionalRepository)
        {
            CostoAdicionalRepository = costoAdicionalRepository;
        }

        public async Task Handle(EliminarCostoAdicionalCommand request, CancellationToken cancellationToken)
        {
            var existingCostoAdicional = await CostoAdicionalRepository.GetCostoAdicionalByIdAsync(request.Id);
            if (existingCostoAdicional.Estatus != "Por Abrobar")
            {
                throw new InvalidAdditionalCostStateException("No se puede eliminar un costo adicional que no está en estado Por Aprobar");
            }
            await CostoAdicionalRepository.DeleteCostoAdicional(request.Id);
        }
    }
}
