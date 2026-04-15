using Dapper;
using FluentValidation;
using SeguimientoTramites.Common;
using SeguimientoTramites.Features.Carreras.Dominio.Entidad;
using SeguimientoTramites.Features.Carreras.Dominio.Dto;

namespace SeguimientoTramites.Features.Carreras;

public class CarreraService
{
    private readonly Data.DbContext _db;
    private readonly IValidator<CrearCarreraDTO> _crearValidator;
    private readonly IValidator<ActualizarCarreraDTO> _actualizarValidator;

    public CarreraService(
        Data.DbContext db,
        IValidator<CrearCarreraDTO> crearValidator,
        IValidator<ActualizarCarreraDTO> actualizarValidator)
    {
        _db = db;
        _crearValidator = crearValidator;
        _actualizarValidator = actualizarValidator;
    }

    public async Task<ApiResponse<IEnumerable<Carrera>>> ObtenerTodas()
    {
        using var connection = _db.CreateConnection();
        var sql = "SELECT IdCarrera, Descrip FROM CARRERA";
        var carreras = await connection.QueryAsync<Carrera>(sql);
        return ApiResponse<IEnumerable<Carrera>>.Ok(carreras);
    }

    public async Task<ApiResponse<Carrera>> ObtenerPorId(int id)
    {
        using var connection = _db.CreateConnection();
        var sql = "SELECT IdCarrera, Descrip FROM CARRERA WHERE IdCarrera = @Id";
        var carrera = await connection.QueryFirstOrDefaultAsync<Carrera>(sql, new { Id = id });

        if (carrera == null)
            return ApiResponse<Carrera>.Error("Carrera no encontrada");

        return ApiResponse<Carrera>.Ok(carrera);
    }

    public async Task<ApiResponse<Carrera>> Crear(CrearCarreraDTO dto)
    {
        dto.Descrip = StringNormalizer.Normalize(dto.Descrip);
        var validacion = await _crearValidator.ValidateAsync(dto);
        if (!validacion.IsValid)
            return ApiResponse<Carrera>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = @"INSERT INTO CARRERA (Descrip)
                    VALUES (@Descrip);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
        var id = await connection.QuerySingleAsync<int>(sql, new { dto.Descrip });

        return ApiResponse<Carrera>.Ok(new Carrera { IdCarrera = id, Descrip = dto.Descrip });
    }

    public async Task<ApiResponse<string>> Actualizar(int id, ActualizarCarreraDTO dto)
    {
        dto.Descrip = StringNormalizer.Normalize(dto.Descrip);
        var context = new ValidationContext<ActualizarCarreraDTO>(dto);
        context.RootContextData["Id"] = id;
        var validacion = await _actualizarValidator.ValidateAsync(context);
        if (!validacion.IsValid)
            return ApiResponse<string>.Error(validacion.Errors.First().ErrorMessage);

        using var connection = _db.CreateConnection();
        var sql = "UPDATE CARRERA SET Descrip = @Descrip WHERE IdCarrera = @Id";
        var rows = await connection.ExecuteAsync(sql, new { dto.Descrip, Id = id });

        if (rows == 0)
            return ApiResponse<string>.Error("Carrera no encontrada");

        return ApiResponse<string>.Ok("");
    }

    public async Task<ApiResponse<string>> Eliminar(int id)
    {
        using var connection = _db.CreateConnection();
        var sql = "DELETE FROM CARRERA WHERE IdCarrera = @Id";
        var rows = await connection.ExecuteAsync(sql, new { Id = id });

        if (rows == 0)
            return ApiResponse<string>.Error("Carrera no encontrada");

        return ApiResponse<string>.Ok("");
    }
}
