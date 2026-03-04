using FluentValidation;
using SeguimientoTramites.Features.Alumnos.Dominio.Dto;

namespace SeguimientoTramites.Features.Alumnos;

public class CrearAlumnoValidator : AbstractValidator<CrearAlumnoDTO>
{
    public CrearAlumnoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede tener mas de 100 caracteres");

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es requerido")
            .EmailAddress().WithMessage("El correo no es valido")
            .MaximumLength(100).WithMessage("El correo no puede tener mas de 100 caracteres");

        RuleFor(x => x.Contra)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(x => x.IdCarrera)
            .GreaterThan(0).WithMessage("La carrera es requerida");
    }
}

public class ActualizarAlumnoValidator : AbstractValidator<ActualizarAlumnoDTO>
{
    public ActualizarAlumnoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede tener mas de 100 caracteres");

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es requerido")
            .EmailAddress().WithMessage("El correo no es valido")
            .MaximumLength(100).WithMessage("El correo no puede tener mas de 100 caracteres");

        RuleFor(x => x.IdCarrera)
            .GreaterThan(0).WithMessage("La carrera es requerida");
    }
}
