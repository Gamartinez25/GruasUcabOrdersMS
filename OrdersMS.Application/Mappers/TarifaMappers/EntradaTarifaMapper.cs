using AutoMapper;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Application.Mappers.TarifaMappers
{
    public class EntradaTarifaMapper : Profile
    {
        public EntradaTarifaMapper()
        {
            CreateMap<CrearTarifaDto, Tarifa>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => "Activo")); // Asigna "Activo" a Estatus
            CreateMap<ListarTarifaDto, Tarifa>()
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => "Activo")); // Asigna "Activo" a Estatus;
        }
    }
}
