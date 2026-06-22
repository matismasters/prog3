namespace RelacionesEnMemoria;

public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    public abstract string ObtenerRol();

    public string Saludo() => $"Hola, soy {Nombre} ({ObtenerRol()})";
}
