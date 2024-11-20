
using MediatR;
using OrdersMS.Application.Commands.TarifaCommands;
using OrdersMS.Application.Exceptions;
using OrdersMS.Core.Repositories;


namespace OrdersMS.Application.Handlers.TarifaHandlers
{
    public class EliminarTarifaHandler : IRequestHandler<EliminarTarifaCommand>
    {
        private readonly ITarifaRepository TarifaRepository;

        public EliminarTarifaHandler(ITarifaRepository tarifaRepository)
        {
            
            TarifaRepository = tarifaRepository;
           
        }
        public  async Task Handle(EliminarTarifaCommand request, CancellationToken cancellationToken)
        {
            
            if (request == null||request.Id==Guid.Empty) 
            {
                throw new InvalidIdException("El Id proporcionado es invalido");
            }
            await TarifaRepository.DeleteTarifaAsync(request.Id);
        }
    }
}
