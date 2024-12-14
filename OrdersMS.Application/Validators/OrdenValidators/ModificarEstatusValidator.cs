
using System;
using FluentValidation;
using OrdersMS.Application.Dtos.OrdenDtos;

namespace OrdersMS.Application.Validators.OrdenValidators
{
    public class ModificarEstatusValidator: AbstractValidator<ModificarEstatusDto>
    {
        public ModificarEstatusValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("La propiedad id es requerida");
            RuleFor(x => x.TipoActualizacion).NotEmpty().NotNull().WithMessage("La propiedad Tipo de actualizacion es requerida");
            RuleFor(action => action)
           .Must(BeValidAction)
           .WithMessage("La acción debe ser 'cancelar' o 'actualizar'.");
        }

        private bool BeValidAction(ModificarEstatusDto dto)
        {
            return dto.TipoActualizacion == "Cancelar" || dto.TipoActualizacion== "Actualizar";
        }

      
    }
}
