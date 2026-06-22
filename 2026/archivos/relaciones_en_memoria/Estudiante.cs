namespace RelacionesEnMemoria;

public class Estudiante : Persona
{
    // Relación 1-N: un estudiante tiene muchas inscripciones
    public List<Inscripcion> Inscripciones { get; set; } = new();

    public override string ObtenerRol() => "Estudiante";
}
