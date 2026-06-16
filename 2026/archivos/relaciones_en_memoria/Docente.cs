namespace RelacionesEnMemoria;

public class Docente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Especialidad { get; set; } = "";

    // Relación 1-N: un docente dicta muchos cursos
    public List<Curso> Cursos { get; set; } = new();
}
