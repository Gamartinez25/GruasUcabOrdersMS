using AutoMapper;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => "Por Abrobar"));

        }
    }
}
