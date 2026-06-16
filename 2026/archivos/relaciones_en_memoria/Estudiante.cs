namespace RelacionesEnMemoria;

public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    // Relación 1-N: un estudiante tiene muchas inscripciones
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
