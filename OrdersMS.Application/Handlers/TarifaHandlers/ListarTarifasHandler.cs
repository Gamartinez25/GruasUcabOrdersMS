using AutoMapper;
using MediatR;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Application.Querys;
using OrdersMS.Core.Repositories;
using OrdersMS.Core.Services.IGoogleServices;
using OrdersMS.Core.Services.MsProviders;


namespace OrdersMS.Application.Handlers.TarifaHandlers
{
    public class ListarTarifasHandler : IRequestHandler<ListarTarifasQuery, IEnumerable<ListarTarifaDto>>
    {
        private readonly IMapper Mapper;
        private readonly ITarifaRepository TarifaRepository;
        private readonly IGoogleService GoogleService;
       
        public ListarTarifasHandler(IMapper mapper, ITarifaRepository tarifaRepository,IGoogleService googleService)
        {
            Mapper = mapper;
            TarifaRepository = tarifaRepository;
            GoogleService = googleService;
            
            
        }
        public async Task<IEnumerable<ListarTarifaDto>> Handle(ListarTarifasQuery request, CancellationToken cancellationToken)
        {
            GoogleService.GetDistanceToOriginAccidentDestination(10.486646170684534, -66.87469950180656, 10.464257917422497, -66.97641877353811);
            var tarifas = await TarifaRepository.GetAllTarifaAsync();
            var tarifasActivas = tarifas.Where(x => x.Estatus == "Activo");
            var tarifasDto = Mapper.Map<IEnumerable<ListarTarifaDto>>(tarifasActivas);
            
            return tarifasDto;
        }
        
    }
}
