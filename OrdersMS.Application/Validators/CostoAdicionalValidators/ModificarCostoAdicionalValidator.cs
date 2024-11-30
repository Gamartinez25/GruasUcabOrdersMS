using FluentValidation;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;

namespace OrdersMS.Application.Validators.CostoAdicionalValidators
{
    public class ModificarCostoAdicionalValidator : AbstractValidator<ModificarCostoAdicionalDto>
    {
        public ModificarCostoAdicionalValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("El id es requerido");
            RuleFor(x => x.Monto).NotEmpty().NotNull().WithMessage("El monto asociado es requerido");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto tiene que ser mayor que 0");
            RuleFor(x => x.Descripcion).NotEmpty().NotNull().WithMessage("La descripcion es requerida");
            RuleFor(x => x.Descripcion)
            .MaximumLength(100)
            .WithMessage("La descripción no puede exceder los 100 caracteres.");

        }
    }
}
