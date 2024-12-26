
using AutoMapper;
using OrdersMS.Core.Services.MsProviders;

namespace OrdersMS.Infrastructure.Mappers
{
    public class VehiculoMapper : Profile
    {
        public VehiculoMapper()
        {
            CreateMap<Vehiculo, VehiculoDto>()
                .ForMember(dest => dest.DistanciaTexto, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.DistanciaValor, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.DuracionTexto, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.DuracionValor, opt => opt.MapFrom(src => 0));

        }
    }
}
