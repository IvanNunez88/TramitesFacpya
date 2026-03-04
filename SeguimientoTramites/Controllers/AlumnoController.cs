using Microsoft.AspNetCore.Mvc;
using SeguimientoTramites.Features.Alumnos;
using SeguimientoTramites.Features.Alumnos.Dominio.Dto;

namespace SeguimientoTramites.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController(AlumnoService service) : ControllerBase
{
    private readonly AlumnoService _service = service;

    [HttpGet("ObtenerTodos")]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _service.ObtenerTodos());
    }

    [HttpGet("ObtenerPorMatricula/{matricula}")]
    public async Task<IActionResult> ObtenerPorMatricula(int matricula)
    {
        return Ok(await _service.ObtenerPorMatricula(matricula));
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] CrearAlumnoDTO dto)
    {
        return Ok(await _service.Crear(dto));
    }

    [HttpPut("Actualizar/{matricula}")]
    public async Task<IActionResult> Actualizar(int matricula, [FromBody] ActualizarAlumnoDTO dto)
    {
        return Ok(await _service.Actualizar(matricula, dto));
    }

    [HttpDelete("Eliminar/{matricula}")]
    public async Task<IActionResult> Eliminar(int matricula)
    {
        return Ok(await _service.Eliminar(matricula));
    }
}
