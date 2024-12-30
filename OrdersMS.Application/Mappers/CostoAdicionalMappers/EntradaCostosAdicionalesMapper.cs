using AutoMapper;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Application.Mappers.CostoAdicionalMappers
{
    public class EntradaCostosAdicionalesMapper : Profile
    {
        public EntradaCostosAdicionalesMapper()
        {
            CreateMap<RegistrarCostoAdicionalDto, OrdenCostoAdicional>()
            .ForMember(dest => dest.IdCostoOrden, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CostoAdicionalId, opt => opt.MapFrom(src => src.IdCostoAdicional))
            .ForMember(dest => dest.OrdenDeServicioId, opt => opt.MapFrom(src => src.IdOrden))
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => "Por Aprobar"));

             CreateMap<(OrdenCostoAdicional, ModificarCostoAdicionalDto), OrdenCostoAdicional>()
            .ConvertUsing<OrdenCostoAdicionalConverter>();

        }

    }
}
