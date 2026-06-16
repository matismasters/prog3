namespace RelacionesEnMemoria;

// Clase pivote que materializa la relación N-N entre Estudiante y Curso.
// Permite guardar datos propios de la relación (Fecha, Estado) y
// navegar en ambas direcciones sin duplicar listas.
public class Inscripcion
{
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = "Activa";

    // Referencias a los dos extremos del N-N
    public Estudiante Estudiante { get; set; } = null!;
    public Curso Curso { get; set; } = null!;
}
