namespace RelacionesEnMemoria;

public class Departamento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    // Relación 1-N: un departamento tiene muchos docentes
    public List<Docente> Docentes { get; set; } = new();
}

// Diagrama Mermaid
/*
```mermaid
classDiagram
    class Departamento {
        int Id
        string Nombre
    }
    class Docente {
        int Id
        string Nombre
    }
    Departamento "1" --> "0..*" Docente : tiene
}
*/