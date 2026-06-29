# Programación 3 2026 — Clase 15

## Unidad 5 · Clases abstractas: el molde que no se puede usar directamente

> **.NET 8 · C# · Consola**

La clase pasada cerramos la relación entre diagrama y código: sabemos cómo traducir un `1-N` en una `List<T>`, un `N-N` en una clase pivote, y cómo navegar en ambas direcciones. Antes de pasar a la siguiente unidad hay un concepto de POO que aparece constantemente en los proyectos reales y que, si no se entiende bien, genera mucha confusión: **las clases abstractas**.

Hoy lo vemos con el proyecto `RelacionesEnMemoria` que ya tenemos funcionando. Las clases `Docente` y `Estudiante` tienen código duplicado, y eso es exactamente la situación en la que aparecen las clases abstractas como solución. Vamos a ver el problema, la solución incorrecta, y por qué las clases abstractas son la respuesta correcta.

---

## 1. El problema: código duplicado entre clases relacionadas

Mirá las definiciones actuales de `Docente` y `Estudiante`:

```csharp
// Docente.cs
public class Docente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Especialidad { get; set; } = "";
    public List<Curso> Cursos { get; set; } = new();
}

// Estudiante.cs
public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
```

`Id` y `Nombre` están en los dos. Si mañana necesito agregar `Email` o `FechaNacimiento`, tengo que acordarme de hacerlo en **ambas** clases. Eso es código duplicado, y el código duplicado es una fuente de bugs: cuando cambio uno y me olvido del otro, los dos empiezan a comportarse distinto.

La intuición correcta es: **extraer lo común a una clase base**. Tanto `Docente` como `Estudiante` son personas; tienen `Id` y `Nombre` porque son personas, no porque sean docentes o estudiantes en particular.

---

## 2. El primer intento: clase base concreta

El primer impulso es crear una clase `Persona` normal y hacer que ambas hereden de ella:

```csharp
// Intento 1 — clase base CONCRETA (todavía hay un problema)
public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}

public class Docente : Persona
{
    public string Especialidad { get; set; } = "";
    public List<Curso> Cursos { get; set; } = new();
}

public class Estudiante : Persona
{
    public List<Inscripcion> Inscripciones { get; set; } = new();
}
```

Esto compila y elimina la duplicación. Pero deja abierta una pregunta incómoda: **¿qué significa `new Persona()`?**

```csharp
Persona p = new Persona { Id = 99, Nombre = "Fantasma" };
```

En el dominio del sistema, eso no tiene sentido. No existe "una persona" que no sea ni docente ni estudiante. `Persona` es un concepto abstracto, una generalización. No se puede instanciar porque no representa nada concreto en la realidad del sistema.

Necesitamos una forma de decirle al compilador: **"esta clase existe para ser heredada, pero nadie puede crear instancias de ella directamente"**. Eso es exactamente lo que hace la palabra clave `abstract`.

---

## 3. La clase abstracta: el molde que no se instancia

Una **clase abstracta** es una clase que existe únicamente como base para otras clases. Se declara con `abstract` antes de `class`. Si alguien intenta hacer `new Persona()`, el compilador lo rechaza con un error:

```
Error CS0144: Cannot create an instance of the abstract type or interface 'Persona'
```

```csharp
// Persona.cs
public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}
```

Con eso, el código que tenía `new Persona(...)` ya no compila. La clase puede usarse como tipo para variables, parámetros y listas, pero **nunca como argumento de `new`**.

```csharp
Persona p = new Persona();   // ❌ Error de compilación
Persona p = new Docente();   // ✅ Docente hereda de Persona
```

Esto no es una restricción caprichosa. Es una declaración de intención: **"este tipo existe en el diseño, pero en la ejecución siempre aparece como una subclase concreta"**.

---

## 4. Métodos abstractos: el contrato que las subclases deben cumplir

Las clases abstractas pueden ir un paso más allá: además de propiedades y métodos concretos (con implementación), pueden declarar **métodos abstractos** — métodos sin cuerpo que la clase base le exige a cada subclase que implemente.

```csharp
public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    // Método abstracto: cada subclase DEBE implementarlo
    public abstract string ObtenerRol();

    // Método concreto: ya tiene implementación, las subclases la heredan
    public string Saludo() => $"Hola, soy {Nombre} ({ObtenerRol()})";
}
```

Si una subclase hereda de `Persona` y no implementa `ObtenerRol()`, el compilador da error:

```
Error CS0534: 'Docente' does not implement inherited abstract member 'Persona.ObtenerRol()'
```

Ese error en tiempo de compilación es el objetivo. Es mucho mejor enterarse de que falta algo **cuando compilamos** que cuando el programa explota en producción.

---

## 5. Las subclases implementan el contrato

Cada subclase concreta debe usar `override` para implementar los métodos abstractos:

```csharp
// Docente.cs
public class Docente : Persona
{
    public string Especialidad { get; set; } = "";
    public List<Curso> Cursos { get; set; } = new();

    public override string ObtenerRol() => $"Docente de {Especialidad}";
}

// Estudiante.cs
public class Estudiante : Persona
{
    public List<Inscripcion> Inscripciones { get; set; } = new();

    public override string ObtenerRol() => "Estudiante";
}
```

`Id` y `Nombre` ya no se repiten: los heredan de `Persona`. Solo `ObtenerRol()` varía por subclase, y cada una lo implementa a su manera.

---

## 6. El polimorfismo: tratar a docentes y estudiantes como personas

Una de las ganancias más poderosas de la herencia —y en particular de las clases abstractas— es el **polimorfismo**: podemos tener una variable del tipo `Persona` que en tiempo de ejecución contiene un `Docente` o un `Estudiante`, y el método correcto se llama solo.

```csharp
// Una lista que puede contener cualquier subtipo de Persona
List<Persona> personas = new List<Persona>();
personas.Add(lucia);    // Docente
personas.Add(ana);      // Estudiante
personas.Add(luis);     // Estudiante

// Iteramos sin saber ni preguntar el tipo concreto
foreach (Persona p in personas)
{
    Console.WriteLine(p.Saludo());
}
```

Resultado:

```
Hola, soy Lucía Rodríguez (Docente de Bases de datos)
Hola, soy Ana García (Estudiante)
Hola, soy Luis Martínez (Estudiante)
```

El método `Saludo()` está definido una sola vez en `Persona`. Llama a `ObtenerRol()` internamente. Como `ObtenerRol()` es abstracto, cada objeto responde con su propia versión. Eso es polimorfismo: **misma llamada, comportamiento diferente según el tipo real del objeto**.

Esto es especialmente útil cuando tenemos lógica que aplica a "todas las personas" sin importar si son docentes o estudiantes: mostrar un listado, buscar por nombre, exportar a un reporte. Esa lógica se escribe una vez sobre `Persona` y funciona para todos los subtipos.

---

## 7. Resumen: clase abstracta vs clase concreta

| | Clase concreta | Clase abstracta |
|---|---|---|
| Se puede instanciar con `new` | ✅ Sí | ❌ No |
| Puede tener propiedades y métodos con implementación | ✅ Sí | ✅ Sí |
| Puede declarar métodos abstractos (sin cuerpo) | ❌ No | ✅ Sí |
| Puede ser base de otras clases | ✅ Sí | ✅ Sí |
| Las subclases deben implementar los métodos abstractos | — | ✅ Obligatorio |
| Representa algo concreto en el dominio | ✅ Generalmente sí | ✅ No: es una generalización |

**¿Cuándo usar una clase abstracta?**
- Cuando el tipo base es una generalización que en la realidad nunca aparece sola (una `Persona` siempre es docente o estudiante, nunca "solo persona").
- Cuando querés obligar a las subclases a implementar ciertos métodos, garantizando un contrato.
- Cuando hay código común que todas las subclases deben compartir (las propiedades `Id` y `Nombre`), pero también comportamiento que cada una define diferente (`ObtenerRol()`).

---

## 8. El programa actualizado

El proyecto `RelacionesEnMemoria` queda con cinco archivos de clase:

```bash
relaciones_en_memoria/
├── Persona.cs       ← nueva clase abstracta base
├── Docente.cs       ← hereda de Persona
├── Estudiante.cs    ← hereda de Persona
├── Curso.cs         ← sin cambios
├── Inscripcion.cs   ← sin cambios
└── Program.cs       ← agrega la demo de polimorfismo
```

**Persona.cs**

```csharp
namespace RelacionesEnMemoria;

public abstract class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";

    public abstract string ObtenerRol();

    public string Saludo() => $"Hola, soy {Nombre} ({ObtenerRol()})";
}
```

**Docente.cs**

```csharp
namespace RelacionesEnMemoria;

public class Docente : Persona
{
    public string Especialidad { get; set; } = "";
    public List<Curso> Cursos { get; set; } = new();

    public override string ObtenerRol() => $"Docente de {Especialidad}";
}
```

**Estudiante.cs**

```csharp
namespace RelacionesEnMemoria;

public class Estudiante : Persona
{
    public List<Inscripcion> Inscripciones { get; set; } = new();

    public override string ObtenerRol() => "Estudiante";
}
```

El fragmento nuevo en **Program.cs**, que se agrega al final del código existente:

```csharp
// ================================================================
// CLASE ABSTRACTA Y POLIMORFISMO
// ================================================================

Console.WriteLine("\n=== Polimorfismo: lista de Persona ===");
List<Persona> personas = new List<Persona> { lucia, ana, luis };
foreach (Persona p in personas)
{
    Console.WriteLine(p.Saludo());
}
```

Resultado:

```
=== Polimorfismo: lista de Persona ===
Hola, soy Lucía Rodríguez (Docente de Bases de datos)
Hola, soy Ana García (Estudiante)
Hola, soy Luis Martínez (Estudiante)
```

Para correr el proyecto actualizado:

```bash
cd archivos/relaciones_en_memoria
dotnet run
```

---

## 9. Conexión con Entity Framework

En los proyectos con EF, las clases abstractas aparecen exactamente igual. Podés tener una entidad abstracta `Persona` y dos tablas separadas `Docentes` y `Estudiantes`, o una sola tabla con una columna discriminadora. EF llama a eso **herencia de tabla por jerarquía (TPH)** o **tabla por tipo (TPT)**. El modelo de clases es el mismo que estamos viendo hoy; lo que cambia es cómo EF lo mapea a SQL.

Cuando en la Unidad 8 (Arquitectura en capas) diseñemos el dominio, si detectamos que dos entidades comparten propiedades y representan una generalización del mismo concepto, la clase abstracta es la herramienta correcta.

---

## Para practicar

Los ejercicios 1 al 6 son **obligatorios** y se construyen sobre el proyecto `RelacionesEnMemoria` con la clase abstracta `Persona` (estimado: 60–75 minutos). Los ejercicios 7 y 8 son **opcionales / desafío**.

---

**1. ¿Por qué `Persona` es abstracta?** *(~10 minutos, sin código)*

Explicá con tus palabras, en tres o cuatro oraciones, por qué tiene sentido que `Persona` sea abstracta y no concreta. ¿Qué representa en el dominio del sistema? ¿Qué problema evitaríamos al impedir `new Persona()`?

---

**2. Una subclase nueva: `Administrativo`** *(~10 minutos)*

Agregá una clase `Administrativo : Persona` con una propiedad `Area` (string) y su propia implementación de `ObtenerRol()` que devuelva algo como `"Administrativo de Bedelía"`. Creá un administrativo, agregalo a la `List<Persona>` junto con los docentes y estudiantes, y verificá que `Saludo()` funciona sin tocar el `foreach`.

---

**3. Un segundo método abstracto** *(~10 minutos)*

Agregá a `Persona` un método abstracto `string ObtenerDescripcion()`. Implementalo en `Docente`, `Estudiante` y `Administrativo`, cada uno con una descripción apropiada (por ejemplo, el docente incluye su especialidad; el estudiante, su cantidad de inscripciones). Compilá: si te olvidás de implementarlo en alguna subclase, copiá el error `CS0534` que da el compilador.

---

**4. Un método concreto que usa los abstractos** *(~10 minutos)*

Agregá a `Persona` un método **concreto** `string FichaCompleta()` que combine `Nombre`, `ObtenerRol()` y `ObtenerDescripcion()` en una sola línea. Como es concreto, se escribe una sola vez en `Persona` y lo heredan todas las subclases. Recorré la `List<Persona>` mostrando la ficha de cada una. Observá que el método base llama a métodos abstractos que resuelve cada subclase: eso es polimorfismo.

---

**5. Polimorfismo en un método propio** *(~10 minutos)*

Escribí un método estático `MostrarRoles(List<Persona> personas)` que reciba la lista y muestre el rol de cada una usando `ObtenerRol()`, **sin preguntar ni una vez el tipo concreto** (nada de `if (p is Docente)`). Probá que funciona igual para docentes, estudiantes y administrativos.

---

**6. Reproducir los errores del contrato** *(~10 minutos)*

Hacé dos pruebas y copiá el mensaje exacto del compilador en cada una:
- a) Intentá crear `Persona p = new Persona();`. ¿Qué error da? (Pista: `CS0144`.)
- b) Creá una subclase `Invitado : Persona` que **no** implemente `ObtenerRol()`. ¿Qué error da? (Pista: `CS0534`.)

Después arreglá ambos: borrá el `new Persona()` e implementá `ObtenerRol()` en `Invitado`.

---

**7. Integrador con archivo de logs** *(desafío — ~30 minutos)*

El archivo `archivos/sistema_logs_2026.zip` contiene registros de los sistemas de una empresa a lo largo de varios meses. Descomprimilo antes de empezar.

Construí una aplicación de consola que lea el archivo y muestre un **dashboard**, pero modelando los registros con una **clase abstracta**: definí `RegistroLog` (abstracta) con las propiedades comunes (fecha, mensaje) y un método abstracto `string Resumen()`. Creá subclases según el nivel del registro —por ejemplo `RegistroInfo`, `RegistroAdvertencia`, `RegistroError`—, cada una con su propio `Resumen()`. Cargá todos los registros en una `List<RegistroLog>` y generá el dashboard recorriéndola **polimórficamente** (por ejemplo, contando cuántos hay de cada tipo y mostrando el resumen de los errores).

El objetivo es que el dashboard se apoye en la clase abstracta y el polimorfismo, no en `if` por tipo.

---

**8. Clase abstracta vs interfaz: anticipo** *(desafío — ~10 minutos, sin código)*

`Persona` es una clase abstracta porque sus subclases **son** personas y comparten código (`Id`, `Nombre`, `Saludo()`). Pensá en un comportamiento que podrían tener tanto un `Docente` como un `Curso` (que **no** es una persona) —por ejemplo, "poder exportarse a texto"—. ¿Te alcanza una clase abstracta para eso? Anotá tu intuición: la vamos a necesitar para la próxima clase, donde aparecen las **interfaces**.
