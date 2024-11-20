using FluentValidation;
using OrdersMS.Application.Dtos.TarifaDtos;


namespace OrdersMS.Application.Validators.TarifaValidators
{
    public class ModificarTarifaValidator :AbstractValidator<ListarTarifaDto>
    {
        public ModificarTarifaValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().NotNull().WithMessage("La propiedad Id es querida");
            RuleFor(x => x.Nombre).NotEmpty().NotNull().WithMessage("La propiedad nombre es reuerida");
            RuleFor(x => x.CostoBase).NotEmpty().NotNull().WithMessage("La propiedad costo base es reuerida");
            RuleFor(x => x.DistanciaKm).NotEmpty().NotNull().WithMessage("La propiedad distancia por km es reuerida");
            RuleFor(x => x.CostoPorKm).NotEmpty().NotNull().WithMessage("La propiedad costo por km es reuerida");
            RuleFor(x => x.CostoBase).InclusiveBetween(0.01, 9999999999.99).WithMessage("El valor de costo base  debe estar entre 0.01 y 9,999,999,999.99.");
            RuleFor(x => x.DistanciaKm).InclusiveBetween(0.01, 9999999999.99).WithMessage("El valor de distancia por km debe estar entre 0.01 y 9,999,999,999.99.");
            RuleFor(x => x.CostoPorKm).InclusiveBetween(0.01, 9999999999.99).WithMessage("El valor de  costo por km debe estar entre 0.01 y 9,999,999,999.99.");
        }
    }
}
