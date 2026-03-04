using Microsoft.AspNetCore.Mvc;
using SeguimientoTramites.Features.Carreras;
using SeguimientoTramites.Features.Carreras.Dominio.Dto;

namespace SeguimientoTramites.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarreraController(CarreraService service) : ControllerBase
{
    private readonly CarreraService _service = service;

    [HttpGet("ObtenerTodas")]
    public async Task<IActionResult> ObtenerTodas()
    {
        return Ok(await _service.ObtenerTodas());
    }

    [HttpGet("ObtenerPorId/{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        return Ok(await _service.ObtenerPorId(id));
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] CrearCarreraDTO dto)
    {
        return Ok(await _service.Crear(dto));
    }

    [HttpPut("Actualizar/{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCarreraDTO dto)
    {
        return Ok(await _service.Actualizar(id, dto));
    }

    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return Ok(await _service.Eliminar(id));
    }
}
