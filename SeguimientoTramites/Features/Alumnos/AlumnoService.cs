using Dapper;
using FluentValidation;
using SeguimientoTramites.Common;
using SeguimientoTramites.Features.Alumnos.Dominio.Entidad;
using SeguimientoTramites.Features.Alumnos.Dominio.Dto;

namespace SeguimientoTramites.Features.Alumnos;

public class AlumnoService
{
    private readonly Data.DbContext _db;
    private readonly IValidator<CrearAlumnoDTO> _crearValidator;
    private readonly IValidator<ActualizarAlumnoDTO> _actualizarValidator;

    public AlumnoService(
        Data.DbContext db,
        IValidator<CrearAlumnoDTO> crearValidator,
        IValidator<ActualizarAlumnoDTO> actualizarValidator)
    {
        _db = db;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    public async Task<ApiResponse<IEnumerable<Alumno>>> ObtenerTodos()
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT a.Matricula, a.Nombre, a.Correo, a.IdCarrera, a.IsActivo,
                           c.Descrip AS CarreraDescrip
                    FROM ALUMNO a
                    INNER JOIN CARRERA c ON a.IdCarrera = c.IdCarrera";
        var alumnos = await connection.QueryAsync<Alumno>(sql);
        return ApiResponse<IEnumerable<Alumno>>.Ok(alumnos);
    }

    public async Task<ApiResponse<Alumno>> ObtenerPorMatricula(int matricula)
    {
        using var connection = _db.CreateConnection();
        var sql = @"SELECT a.Matricula, 
                           a.Nombre, 
                           a.Correo, 
                           a.IdCarrera, 
                           a.IsActivo,
                           c.Descrip AS CarreraDescrip
                    FROM ALUMNO a
                    INNER JOIN CARRERA c ON a.IdCarrera = c.IdCarrera
                    WHERE a.Matricula = @Matricula";

        var alumno = await connection.QueryFirstOrDefaultAsync<Alumno>(sql, new { Matricula = matricula });

        if (alumno == null)
            return ApiResponse<Alumno>.Error("Alumno no encontrado");

        return ApiResponse<Alumno>.Ok(alumno);
    }

    public async Task<ApiResponse<Alumno>> Crear(CrearAlumnoDTO dto)
    {
        var validacion = await _crearValidator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<Alumno>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = @"INSERT INTO ALUMNO (Nombre, Correo, Contra, IdCarrera, IsActivo)
                    VALUES (@Nombre, @Correo, @Contra, @IdCarrera, 1);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var matricula = await connection.QuerySingleAsync<int>(sql, new
        {
            dto.Nombre,
            dto.Correo,
            dto.Contra,
            dto.IdCarrera
        });

        return ApiResponse<Alumno>.Ok(new Alumno
        {
            Matricula = matricula,
            Nombre = dto.Nombre,
            Correo = dto.Correo,
            IdCarrera = dto.IdCarrera,
            IsActivo = true
        });
    }

    public async Task<ApiResponse<string>> Actualizar(int matricula, ActualizarAlumnoDTO dto)
    {
        var validacion = await _actualizarValidator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<string>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = @"UPDATE ALUMNO
                    SET Nombre = @Nombre, Correo = @Correo,
                        IdCarrera = @IdCarrera, IsActivo = @IsActivo
                    WHERE Matricula = @Matricula";
        var rows = await connection.ExecuteAsync(sql, new
        {
            dto.Nombre,
            dto.Correo,
            dto.IdCarrera,
            dto.IsActivo,
            Matricula = matricula
        });

        if (rows == 0)
            return ApiResponse<string>.Error("Alumno no encontrado");

        return ApiResponse<string>.Ok("");
    }

    public async Task<ApiResponse<string>> Eliminar(int matricula)
    {
        using var connection = _db.CreateConnection();
        var sql = "DELETE FROM ALUMNO WHERE Matricula = @Matricula";
        var rows = await connection.ExecuteAsync(sql, new { Matricula = matricula });

        if (rows == 0)
            return ApiResponse<string>.Error("Alumno no encontrado");

        return ApiResponse<string>.Ok("");
    }
}
