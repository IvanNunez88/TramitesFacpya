using Dapper;
using FluentValidation;
using SeguimientoTramites.Features.Carreras.Dominio.Dto;

namespace SeguimientoTramites.Features.Carreras;

public class CrearCarreraValidator : AbstractValidator<CrearCarreraDTO>
{
    public CrearCarreraValidator(Data.DbContext db)
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres")
            .MustAsync(async (descrip, _) =>
            {
                using var connection = db.CreateConnection();
                var existe = await connection.ExecuteScalarAsync<int>(
                    @"SELECT COUNT(1) FROM CARRERA
                      WHERE REPLACE(Descrip, ' ', '') COLLATE SQL_Latin1_General_CP1_CI_AI
                          = REPLACE(@Descrip, ' ', '') COLLATE SQL_Latin1_General_CP1_CI_AI",
                    new { Descrip = descrip });
                return existe == 0;
            }).WithMessage("Ya existe una carrera con esa descripcion");
    }
}

public class ActualizarCarreraValidator : AbstractValidator<ActualizarCarreraDTO>
{
    public ActualizarCarreraValidator(Data.DbContext db)
    {
        RuleFor(x => x.Descrip)
            .NotEmpty().WithMessage("La descripcion es requerida")
            .MaximumLength(100).WithMessage("La descripcion no puede tener mas de 100 caracteres")
            .MustAsync(async (_, descrip, context, __) =>
            {
                var id = context.RootContextData.TryGetValue("Id", out var raw) && raw is int i ? i : 0;
                using var connection = db.CreateConnection();
                var existe = await connection.ExecuteScalarAsync<int>(
                    @"SELECT COUNT(1) FROM CARRERA
                      WHERE REPLACE(Descrip, ' ', '') COLLATE SQL_Latin1_General_CP1_CI_AI
                          = REPLACE(@Descrip, ' ', '') COLLATE SQL_Latin1_General_CP1_CI_AI
                        AND IdCarrera <> @Id",
                    new { Descrip = descrip, Id = id });
                return existe == 0;
            }).WithMessage("Ya existe otra carrera con esa descripcion");
    }
}
