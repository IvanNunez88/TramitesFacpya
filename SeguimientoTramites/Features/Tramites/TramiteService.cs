using Dapper;
using FluentValidation;
using SeguimientoTramites.Common;
using SeguimientoTramites.Features.Tramites.Dominio.Entidad;
using SeguimientoTramites.Features.Tramites.Dominio.Dto;

namespace SeguimientoTramites.Features.Tramites;

public class TramiteService
{
    private readonly Data.DbContext _db;
    private readonly IValidator<CrearTramiteDTO> _crearValidator;
    private readonly IValidator<ActualizarTramiteDTO> _actualizarValidator;

    public TramiteService(
        Data.DbContext db,
        IValidator<CrearTramiteDTO> crearValidator,
        IValidator<ActualizarTramiteDTO> actualizarValidator)
    {
        _db = db;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    public async Task<ApiResponse<IEnumerable<Tramite>>> ObtenerTodos()
    {
        using var connection = _db.CreateConnection();
        var sql = "SELECT IdTramite, Descrip FROM TRAMITE";
        var tramites = await connection.QueryAsync<Tramite>(sql);
        return ApiResponse<IEnumerable<Tramite>>.Ok(tramites);
    }

    public async Task<ApiResponse<Tramite>> ObtenerPorId(int id)
    {
        using var connection = _db.CreateConnection();
        var sql = "SELECT IdTramite, Descrip FROM TRAMITE WHERE IdTramite = @Id";
        var tramite = await connection.QueryFirstOrDefaultAsync<Tramite>(sql, new { Id = id });

        if (tramite == null)
            return ApiResponse<Tramite>.Error("Tramite no encontrado");

        return ApiResponse<Tramite>.Ok(tramite);
    }

    public async Task<ApiResponse<Tramite>> Crear(CrearTramiteDTO dto)
    {
        var validacion = await _crearValidator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<Tramite>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = @"INSERT INTO TRAMITE (Descrip)
                    VALUES (@Descrip);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var id = await connection.QuerySingleAsync<int>(sql, new { dto.Descrip });

        return ApiResponse<Tramite>.Ok(new Tramite { IdTramite = id, Descrip = dto.Descrip });
    }

    public async Task<ApiResponse<string>> Actualizar(int id, ActualizarTramiteDTO dto)
    {
        var validacion = await _actualizarValidator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<string>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = "UPDATE TRAMITE SET Descrip = @Descrip WHERE IdTramite = @Id";
        var rows = await connection.ExecuteAsync(sql, new { dto.Descrip, Id = id });

        if (rows == 0)
            return ApiResponse<string>.Error("Tramite no encontrado");

        return ApiResponse<string>.Ok("");
    }

    public async Task<ApiResponse<string>> Eliminar(int id)
    {
        using var connection = _db.CreateConnection();
        var sql = "DELETE FROM TRAMITE WHERE IdTramite = @Id";
        var rows = await connection.ExecuteAsync(sql, new { Id = id });

        if (rows == 0)
            return ApiResponse<string>.Error("Tramite no encontrado");

        return ApiResponse<string>.Ok("");
    }
}
