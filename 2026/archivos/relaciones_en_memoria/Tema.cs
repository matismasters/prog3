
namespace RelacionesEnMemoria;

public class Tema
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    // Relación N-N: un tema tiene muchos cursos
    public List<TemaCurso> TemaCursos { get; set; } = new();
}