using FluentValidation;
using SeguimientoTramites.Features.Carreras.Dominio.Dto;

namespace SeguimientoTramites.Features.Carreras;

public class CrearCarreraValidator : AbstractValidator<CrearCarreraDTO>
{
    public CrearCarreraValidator()
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres");
    }
}

public class ActualizarCarreraValidator : AbstractValidator<ActualizarCarreraDTO>
{
    public ActualizarCarreraValidator()
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres");
    }
}
