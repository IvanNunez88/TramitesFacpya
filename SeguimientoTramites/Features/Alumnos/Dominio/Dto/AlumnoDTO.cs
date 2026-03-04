namespace SeguimientoTramites.Features.Alumnos.Dominio.Dto;

public class CrearAlumnoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contra { get; set; } = string.Empty;
    public int IdCarrera { get; set; }
}

public class ActualizarAlumnoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public int IdCarrera { get; set; }
    public bool IsActivo { get; set; } = true;
}
