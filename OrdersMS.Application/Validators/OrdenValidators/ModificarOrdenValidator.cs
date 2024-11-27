using FluentValidation;
using OrdersMS.Application.Dtos.OrdenDtos;


namespace OrdersMS.Application.Validators.OrdenValidators
{
    public class ModificarOrdenValidator : AbstractValidator<ModificarOrdenDto>
    {
        public ModificarOrdenValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("La propiedad id es requerida");
            RuleFor(x => x.Vehiculo).NotEmpty().NotNull().WithMessage("La propiedad vehiculo es requerida");
            RuleFor(x => x.CantidadKmExtra).GreaterThan(-1).WithMessage("La cantidad de km extra no puede ser negativa");
            RuleFor(x => x.CostoTotal).GreaterThan(-1).WithMessage("El costo total no puede ser negativo ");
            RuleFor(x => x.CostoTotalKmExtra).GreaterThan(-1).WithMessage("El costo total no puede ser negativo ");

        }
    }
}
