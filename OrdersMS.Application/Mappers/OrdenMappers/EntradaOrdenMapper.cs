using AutoMapper;
using OrdersMS.Application.Dtos.OrdenDtos;
using OrdersMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Mappers.OrdenMappers
{
    public class EntradaOrdenMapper : Profile
    {
        public EntradaOrdenMapper()
        {
            CreateMap<CrearOrdenDto, OrdenDeServicio>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                 .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => "Por Asignar"))
                 .ForMember(dest => dest.CostoServiciosAdicionales, opt => opt.MapFrom(src => 0));
                 
        }
    }
}
