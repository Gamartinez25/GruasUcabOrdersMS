using AutoMapper;
using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;


namespace OrdersMS.Application.Handlers.TarifaHandlers
{
    public class ListarTarifasHandler : IRequestHandler<ListarTarifasQuery, IEnumerable<ListarTarifaDto>>
    {
        private readonly IMapper Mapper;
        private readonly ITarifaRepository TarifaRepository;
        public ListarTarifasHandler(IMapper mapper, ITarifaRepository tarifaRepository)
        {
            Mapper = mapper;
            TarifaRepository = tarifaRepository;
            
        }
        public async Task<IEnumerable<ListarTarifaDto>> Handle(ListarTarifasQuery request, CancellationToken cancellationToken)
        {

            var tarifas = await TarifaRepository.GetAllTarifaAsync();
            var tarifasDto = Mapper.Map<IEnumerable<ListarTarifaDto>>(tarifas);
            return tarifasDto;
        }
        
    }
}
