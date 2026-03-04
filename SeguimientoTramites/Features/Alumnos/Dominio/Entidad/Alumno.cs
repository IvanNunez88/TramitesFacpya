namespace SeguimientoTramites.Features.Alumnos.Dominio.Entidad;

public class Alumno
{
    public int Matricula { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contra { get; set; } = string.Empty;
    public int IdCarrera { get; set; }
    public bool IsActivo { get; set; }

    // Para JOINs - datos de la carrera
    public string? CarreraDescrip { get; set; }
}
