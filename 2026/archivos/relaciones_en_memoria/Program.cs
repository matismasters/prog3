using RelacionesEnMemoria;

// ================================================================
// RELACIÓN 1 A N: Docente dicta muchos Cursos
// ================================================================

Docente lucia = new Docente { Id = 1, Nombre = "Lucía Rodríguez", Especialidad = "Bases de datos" };

Curso db1 = new Curso { Id = 102, Nombre = "Base de datos 1", Cupo = 25 };
Curso db2 = new Curso { Id = 103, Nombre = "Base de datos 2", Cupo = 20 };

// Establecemos la relación en ambas direcciones
db1.Docente = lucia;
lucia.Cursos.Add(db1);

db2.Docente = lucia;
lucia.Cursos.Add(db2);

Console.WriteLine("=== 1 a N: Docente → Cursos ===");
Console.WriteLine($"Cursos de {lucia.Nombre}:");
foreach (Curso c in lucia.Cursos)
{
    Console.WriteLine($"  - {c.Nombre} (cupo: {c.Cupo})");
}

// Navegación inversa: desde el hijo al padre
Console.WriteLine($"\nEl curso '{db1.Nombre}' lo dicta: {db1.Docente?.Nombre}");

// ================================================================
// RELACIÓN N A N: Estudiante ↔ Curso via Inscripcion (clase pivote)
// ================================================================

Estudiante ana  = new Estudiante { Id = 1, Nombre = "Ana García" };
Estudiante luis = new Estudiante { Id = 2, Nombre = "Luis Martínez" };

// Ana se inscribe en db1
Inscripcion ins1 = new Inscripcion
{
    Fecha      = new DateTime(2026, 3, 1),
    Estado     = "Activa",
    Estudiante = ana,
    Curso      = db1
};
ana.Inscripciones.Add(ins1);
db1.Inscripciones.Add(ins1);

// Luis se inscribe en db1
Inscripcion ins2 = new Inscripcion
{
    Fecha      = new DateTime(2026, 3, 2),
    Estado     = "Activa",
    Estudiante = luis,
    Curso      = db1
};
luis.Inscripciones.Add(ins2);
db1.Inscripciones.Add(ins2);

// Ana se inscribe en db2
Inscripcion ins3 = new Inscripcion
{
    Fecha      = new DateTime(2026, 3, 5),
    Estado     = "Activa",
    Estudiante = ana,
    Curso      = db2
};
ana.Inscripciones.Add(ins3);
db2.Inscripciones.Add(ins3);

// ================================================================
// NAVEGACIÓN
// ================================================================

Console.WriteLine("\n=== N a N: navegación desde el Estudiante ===");
Console.WriteLine($"Cursos de {ana.Nombre}:");
foreach (Inscripcion ins in ana.Inscripciones)
{
    Console.WriteLine($"  - {ins.Curso.Nombre} (docente: {ins.Curso.Docente?.Nombre}, inscripta el {ins.Fecha:dd/MM/yyyy})");
}

Console.WriteLine("\n=== N a N: navegación desde el Curso ===");
Console.WriteLine($"Estudiantes en '{db1.Nombre}':");
foreach (Inscripcion ins in db1.Inscripciones)
{
    Console.WriteLine($"  - {ins.Estudiante.Nombre} ({ins.Estado})");
}

Console.WriteLine("\n=== Combinado: cursos del docente con cantidad de inscriptos ===");
Console.WriteLine($"Cursos de {lucia.Nombre}:");
foreach (Curso c in lucia.Cursos)
{
    Console.WriteLine($"  - {c.Nombre}: {c.Inscripciones.Count} inscripto(s)");
}
