using Microsoft.AspNetCore.Mvc;
using SeguimientoTramites.Features.Tramites;
using SeguimientoTramites.Features.Tramites.Dominio.Dto;

namespace SeguimientoTramites.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TramiteController(TramiteService service) : ControllerBase
{
    private readonly TramiteService _service = service;

    [HttpGet("ObtenerTodos")]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _service.ObtenerTodos());
    }

    [HttpGet("ObtenerPorId/{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        return Ok(await _service.ObtenerPorId(id));
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] CrearTramiteDTO dto)
    {
        return Ok(await _service.Crear(dto));
    }

    [HttpPut("Actualizar/{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTramiteDTO dto)
    {
        return Ok(await _service.Actualizar(id, dto));
    }

    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        return Ok(await _service.Eliminar(id));
    }
}
