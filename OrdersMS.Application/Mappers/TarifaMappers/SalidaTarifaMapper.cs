using AutoMapper;
using OrdersMS.Application.Dtos.TarifaDtos;
using OrdersMS.Domain.Entities;


namespace OrdersMS.Application.Mappers.TarifaMappers
{
    public class SalidaTarifaMapper : Profile
    {
        public SalidaTarifaMapper()
        {
            CreateMap<Tarifa, ListarTarifaDto>();
        }
    }
}
