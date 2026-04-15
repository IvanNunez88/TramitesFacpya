using FluentValidation;
using SeguimientoTramites.Features.Tramites.Dominio.Dto;

namespace SeguimientoTramites.Features.Tramites;

public class CrearTramiteValidator : AbstractValidator<CrearTramiteDTO>
{
    public CrearTramiteValidator()
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres");
    }
}

public class ActualizarTramiteValidator : AbstractValidator<ActualizarTramiteDTO>
{
    public ActualizarTramiteValidator()
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres");
    }
}
