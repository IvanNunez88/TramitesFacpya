using Microsoft.AspNetCore.Mvc;

namespace SeguimientoTramites.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacionesController : ControllerBase
{
    [HttpGet("Mensaje")]
    public IActionResult Mensaje()
    {
        return Ok("Hola desde el controlador de operaciones");
    }

    [HttpGet("Sumar/{Numero1:int}/{Numero2:int}")]
    public IActionResult Sumar(int Numero1, int Numero2)
    {
        return Ok($"La suma de {Numero1} y {Numero2} es: {Numero1 + Numero2}");
    }

    [HttpPost("SumaColeccion")]
    public IActionResult SumaColeccion([FromBody] List<int> Numeros)
    {
        int suma = Numeros.Sum();
        return Ok($"La suma de los números es: {suma}");
    }
}