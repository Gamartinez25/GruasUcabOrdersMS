using AutoMapper;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;
using OrdersMS.Domain.Entities;

namespace OrdersMS.Application.Mappers.CostoAdicionalMappers
{
    public class OrdenCostoAdicionalConverter : ITypeConverter<(OrdenCostoAdicional, ModificarCostoAdicionalDto), OrdenCostoAdicional>
    {
        public OrdenCostoAdicional Convert((OrdenCostoAdicional, ModificarCostoAdicionalDto) source, OrdenCostoAdicional destination, ResolutionContext context)
        {
            var (existingCostoAdicional, costoAdicionalDto) = source;

            return new OrdenCostoAdicional(
                costoAdicionalDto.Id,
                existingCostoAdicional.OrdenDeServicioId,
                existingCostoAdicional.CostoAdicionalId,
                costoAdicionalDto.Monto,
                existingCostoAdicional.Estatus,
                costoAdicionalDto.Descripcion,
                existingCostoAdicional.Id,
                existingCostoAdicional.FechaCreacion,
                existingCostoAdicional.CreadoPor,
                existingCostoAdicional.FechaActualizacion,
                existingCostoAdicional.ActualizadoPor
            );
        }
    }

}
