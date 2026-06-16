namespace RelacionesEnMemoria;

public class Curso
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int Cupo { get; set; }

    // Referencia inversa al padre (navegación de hijo a padre)
    public Docente? Docente { get; set; }

    // Relación 1-N: un curso tiene muchas inscripciones
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
