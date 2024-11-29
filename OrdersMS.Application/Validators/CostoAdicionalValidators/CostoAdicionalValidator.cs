using FluentValidation;
using OrdersMS.Application.Dtos.CostoAdicionalDtos;


namespace OrdersMS.Application.Validators.CostoAdicionalValidators
{
    public class CostoAdicionalValidator : AbstractValidator<RegistrarCostoAdicionalDto>
    {
        public CostoAdicionalValidator()
        {
            RuleFor(x => x.IdCostoAdicional).NotEmpty().NotNull().WithMessage("El id del costo adicional es requerido");
            RuleFor(x => x.IdOrden).NotEmpty().NotNull().WithMessage("El id de la orden es requerido");
            RuleFor(x => x.Costo).NotEmpty().NotNull().WithMessage("El costo asociado es requerido");
            RuleFor(x => x.Costo).GreaterThan(0).WithMessage("El costo tiene que ser mayor que 0");
            RuleFor(x => x.Descripcion)
            .MaximumLength(100)
            .WithMessage("La descripción no puede exceder los 100 caracteres.");

        }
    }
}
