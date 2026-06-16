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


// ================================================================

Console.WriteLine("====================================================");
Console.WriteLine("====================================================");
Console.WriteLine("====================================================");
Console.WriteLine("====================================================");

// Cursos:
// db1: Base de datos 1
// - Tema: SQL Basico
// - Tema: Servidores de Base de datos
// - Tema: Insert
// - Tema: Join
// db2: Base de datos 2
// - Tema: SQL Avanzado
// - Tema: Normalización
// - Tema: Optimización
// - Tema: Seguridad

Tema sqlBasico = new Tema { Nombre = "SQL Basico" };
Tema servidoresBaseDeDatos = new Tema { Nombre = "Servidores de Base de datos" };
Tema insert = new Tema { Nombre = "Insert" };
Tema join = new Tema { Nombre = "Join" };
Tema sqlAvanzado = new Tema { Nombre = "SQL Avanzado" };
Tema normalizacion = new Tema { Nombre = "Normalización" };
Tema optimizacion = new Tema { Nombre = "Optimización" };
Tema seguridad = new Tema { Nombre = "Seguridad" };

TemaCurso temaCurso1 = new TemaCurso { Tema = sqlBasico, Curso = db1 };
TemaCurso temaCurso2 = new TemaCurso { Tema = servidoresBaseDeDatos, Curso = db1 };
TemaCurso temaCurso3 = new TemaCurso { Tema = insert, Curso = db1 };
TemaCurso temaCurso4 = new TemaCurso { Tema = join, Curso = db1 };
TemaCurso temaCurso5 = new TemaCurso { Tema = sqlAvanzado, Curso = db2 };
TemaCurso temaCurso6 = new TemaCurso { Tema = normalizacion, Curso = db2 };
TemaCurso temaCurso7 = new TemaCurso { Tema = optimizacion, Curso = db2 };
TemaCurso temaCurso8 = new TemaCurso { Tema = seguridad, Curso = db2 };

db1.TemaCursos.Add(temaCurso1);
db1.TemaCursos.Add(temaCurso2);
db1.TemaCursos.Add(temaCurso3);
db1.TemaCursos.Add(temaCurso4);

db2.TemaCursos.Add(temaCurso5);
db2.TemaCursos.Add(temaCurso6);
db2.TemaCursos.Add(temaCurso7);
db2.TemaCursos.Add(temaCurso8);
