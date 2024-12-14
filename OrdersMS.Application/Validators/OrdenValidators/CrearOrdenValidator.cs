using FluentValidation;
using OrdersMS.Application.Dtos.OrdenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersMS.Application.Validators.OrdenValidators
{
    public class CrearOrdenValidator : AbstractValidator<CrearOrdenDto>
    {
        public CrearOrdenValidator()
        {
            RuleFor(x => x.DetallesIncidente).NotEmpty().NotNull().WithMessage("La propiedad detalles del incidente es requerida");
            RuleFor(x => x.DireccionOrigen).NotEmpty().NotNull().WithMessage("La propiedad direccion origen es requerida");
            RuleFor(x => x.DireccionDestino).NotEmpty().NotNull().WithMessage("La propiedad direccion destino es requerida");
            RuleFor(x => x.NombreDenunciante).NotEmpty().NotNull().WithMessage("La propiedad nombre del denunciante es requerida");
            RuleFor(x => x.TipoDocumentoDenunciante).NotEmpty().NotNull().WithMessage("La propiedad tipo de documento del denunciante es requerida");
            RuleFor(x => x.NumeroDocumentoDenunciante).NotEmpty().NotNull().WithMessage("La propiedad numero documento del denunciante es reuerida");
            RuleFor(x => x.PolizaAseguradoId).NotEmpty().NotNull().WithMessage("La propiedad poliza es requerida");
            RuleFor(s => s.TipoDocumentoDenunciante).Matches(@"^[VE]{1}$").WithMessage("El tipo de documento de identidad solo puede ser V o E");
            RuleFor(s => s.NumeroDocumentoDenunciante).Matches(@"^\d{6,8}$").WithMessage("El número del documento de identidad debe tener máximo 8 dígitos numéricos");
        }
    }
}
