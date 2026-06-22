namespace RelacionesEnMemoria;

public class Docente : Persona
{
    public string Especialidad { get; set; } = "";

    // Relación 1-N: un docente dicta muchos cursos
    public List<Curso> Cursos { get; set; } = new();

    public override string ObtenerRol() => $"Docente de {Especialidad}";
}
