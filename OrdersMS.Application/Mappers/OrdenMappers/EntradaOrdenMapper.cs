using AutoMapper;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Mappers.OrdenMappers
{
    public class EntradaOrdenMapper : Profile
    {
        public EntradaOrdenMapper()
        {
            CreateMap<CrearOrdenDto, OrdenDeServicio>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.CostoServiciosAdicionales, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.CostoTotal, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.CantidadKmExtra, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.CostoTotalKmExtra, opt => opt.MapFrom(src => 0));


            CreateMap<ModificarOrdenDto, OrdenDeServicio>()
                .ForMember(dest => dest.CostoServiciosAdicionales, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.CostoTotal, opt => opt.MapFrom(src => 0));

        }
    }
}
