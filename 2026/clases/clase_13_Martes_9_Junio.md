# Programación 3 2026 — Clase 13

## Unidad 7 · El mismo CRUD, ahora con Entity Framework: qué cambia y qué no

> **.NET 8 · C# · ASP.NET Core MVC · Entity Framework Core · SQLite**

La clase pasada construimos un CRUD de animales **a mano con ADO.NET** y, en el camino, sufrimos tres dolores: el mapeo manual de filas a objetos, el schema escrito a mano y divorciado del código, y el boilerplate repetido en cada operación. Cerramos prometiendo que **Entity Framework** resolvía los tres.

Hoy cumplimos esa promesa de la forma más honesta posible: tomamos **el mismísimo proyecto** y lo reescribimos con EF. El dominio, las pantallas y las reglas son los mismos; lo que cambia es **cómo se guardan los datos**. Y ahí aparece la sorpresa más grande: EF no solo achica el código de persistencia, sino que nos deja **borrar una capa entera** que en el proyecto a mano era imprescindible. Comparando los dos repos lado a lado se ve con precisión qué nos saca de encima EF y qué no toca.

Los dos proyectos están publicados y funcionando:

- **A mano (ADO.NET):** `archivos/crud_ado` — https://github.com/matismasters/prog3_crud_ado
- **Con EF Core:** `archivos/crud_ef` — https://github.com/matismasters/prog3_crud_ef

La forma de estudiar esta clase es tenerlos abiertos en paralelo e ir comparando los archivos que se nombran acá.

---

## Lo primero: la cara de la aplicación NO cambió

Antes de mirar las diferencias, mirá qué quedó **idéntico** entre los dos repos:

| Archivo | ¿Cambió? |
|---|---|
| `Models/Animal.cs` | **Idéntico.** La entidad es la misma clase tipada. |
| `Models/AnimalFormDto.cs` | **Idéntico.** El DTO y sus validaciones no se tocan. |
| `Views/Animales/*.cshtml` | **Idénticas.** Index, Details, Create, Edit, Delete. |

Por fuera, las dos aplicaciones son **la misma**: mismas pantallas, mismos campos, mismas validaciones. El usuario no podría distinguirlas. Lo que cambió vive todo del lado de la persistencia, y son **tres cosas**: que el repositorio desaparece, cómo se mapean los datos, y cómo se maneja el schema. Vamos una por una.

---

## Diferencia 1 — El repositorio desaparece (el DbContext ya es uno)

Esta es la diferencia más fuerte, y conviene entenderla bien.

En `crud_ado` hay un archivo `Repositories/AnimalRepository.cs`. ¿Por qué existe? Para **esconder la danza de ADO.NET**: abrir la conexión, armar el comando, cargar los parámetros uno por uno, recorrer el reader y mapear cada fila a un `Animal`. Así de pesado es cada método. Mirá `ObtenerTodos`:

```csharp
// crud_ado/Repositories/AnimalRepository.cs
public List<Animal> ObtenerTodos()
{
    List<Animal> animales = new List<Animal>();

    using (SqliteConnection conexion = new SqliteConnection(_connectionString))
    {
        conexion.Open();
        string sql = "SELECT id, especie, sexo, reserva, energia, fecha_alta FROM animales ORDER BY id";

        using (SqliteCommand comando = new SqliteCommand(sql, conexion))
        using (SqliteDataReader reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                animales.Add(MapearAnimal(reader));
            }
        }
    }

    return animales;
}
```

El repositorio es la capa que se "come" todo ese ruido para que el controller no lo vea. **Es imprescindible en `crud_ado`.**

En `crud_ef` ese archivo **no existe**. Y no es que lo hayamos escondido en otro lado: es que **no hace falta**. El `DbContext` de EF (nuestro `SafariContext`) **ya es** un repositorio y, además, una **unidad de trabajo**:

- `DbSet<Animal>` —la propiedad `Animales` del contexto— es el repositorio: te da `Add`, `Remove`, `Find` y consultas (`Where`, `OrderBy`, `ToList`).
- `SaveChanges()` es la unidad de trabajo: junta todos los cambios pendientes y los confirma en **una sola transacción**.

Por eso, en `crud_ef`, el `AnimalesController` recibe el `SafariContext` directamente y le habla a él. Compará el mismo `Index`:

```csharp
// crud_ado/Controllers/AnimalesController.cs  -> pasa por el repositorio
List<Animal> animales = _repositorio.ObtenerTodos();   // el repo hace la danza de arriba
```

```csharp
// crud_ef/Controllers/AnimalesController.cs  -> habla directo con el DbContext
List<Animal> animales = _context.Animales.OrderBy(a => a.Id).ToList();
```

Las demás operaciones son igual de directas: `_context.Animales.Find(id)` para leer uno, `_context.Animales.Add(animal)` + `_context.SaveChanges()` para crear, `Remove` + `SaveChanges` para borrar. Lo que en `crud_ado` eran ~20 líneas por operación dentro del repositorio, en `crud_ef` es **una o dos líneas en el propio controller**, sin ninguna capa intermedia. Ese era el **Dolor 3 — el boilerplate**, y EF no lo achicó: lo hizo desaparecer junto con el repositorio.

> **La pregunta filosa:** "¿entonces el repositorio es malo?" No. En `crud_ado` se gana el sueldo porque esconde ADO.NET crudo. En `crud_ef` sería una capa de **paso** que solo reenvía llamadas al `DbContext` (que ya es un repositorio), y eso es ruido sin valor. La regla práctica: con EF, usá el `DbContext` directo, y agregá un repositorio propio **solo** si te da algo concreto (métodos con nombre de dominio, un borde para testear), nunca un reenvío de una línea.

### Un detalle de EF que vale la pena: el `Edit`

El `Edit` por POST en `crud_ef` muestra el patrón idiomático de EF, **cargar → modificar → guardar**:

```csharp
Animal? animal = _context.Animales.Find(id);   // 1. traigo la entidad (EF la rastrea)
if (animal == null) return NotFound();

animal.Especie = form.Especie;                 // 2. modifico solo lo que cambió
animal.Sexo = form.Sexo[0];
animal.Reserva = form.Reserva;
animal.Energia = form.Energia;

_context.SaveChanges();                         // 3. EF detecta qué cambió y arma el UPDATE
```

EF **rastrea** ese objeto (lo que se llama *change tracking*): al guardar, compara el estado actual con el que leyó y genera un `UPDATE` que toca **solo las columnas que realmente cambiaron**. La `FechaAlta`, que nunca tocamos, queda intacta. Eso lo da EF gratis; a mano habría que tener cuidado de no pisarla.

---

## Diferencia 2 — El mapeo: ya nadie copia columna por columna

**Archivos a comparar:** el método `MapearAnimal` en `crud_ado/Repositories/AnimalRepository.cs` contra `crud_ef/Data/SafariContext.cs`.

En el proyecto a mano, traducir una fila a un `Animal` era trabajo explícito, propiedad por propiedad:

```csharp
private Animal MapearAnimal(SqliteDataReader reader)
{
    Animal animal = new Animal();
    animal.Id = reader.GetInt32(reader.GetOrdinal("id"));
    animal.Especie = reader.GetString(reader.GetOrdinal("especie"));
    animal.Sexo = reader.GetString(reader.GetOrdinal("sexo"))[0];
    animal.Reserva = reader.GetString(reader.GetOrdinal("reserva"));
    animal.Energia = reader.GetInt32(reader.GetOrdinal("energia"));
    animal.FechaAlta = reader.GetDateTime(reader.GetOrdinal("fecha_alta"));
    return animal;
}
```

En `crud_ef` **ese método no existe** —y, lo más importante, **tampoco escribimos la configuración del mapeo**. EF mapea la clase a la tabla **por convención**: con solo declarar el `DbSet` en el `DbContext` (`Data/SafariContext.cs`)...

```csharp
public DbSet<Animal> Animales { get; set; }
```

...EF ya sabe que hay una tabla `Animales` con una columna por cada propiedad de la clase —`Id`, `Especie`, `Sexo`, `Reserva`, `Energia`, `FechaAlta`—. El nombre de la tabla lo saca del `DbSet`, y el de cada columna, de cada propiedad. **No enumeramos nada.**

Por eso el `OnModelCreating` de `crud_ef` **no describe el mapeo**. Solo contiene las tres cosas que EF **no puede adivinar** —y que, además, no tienen equivalente como anotación en el modelo, así que viven sí o sí acá—:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Animal>(animal =>
    {
        animal.Property(a => a.Sexo).HasConversion<string>();                     // 1
        animal.Property(a => a.FechaAlta).HasDefaultValueSql("datetime('now')");  // 2
        animal.HasData( /* los 4 animales de ejemplo */ );                        // 3
    });
}
```

1. **`Sexo` es un `char`** y le pedimos que se guarde como texto (`'M'`/`'H'`) con `HasConversion<string>()`. Es la única propiedad con una particularidad de tipo.
2. **`FechaAlta`** toma el valor por defecto de la base (`datetime('now')`) cuando no se la setea.
3. **El seed** de los cuatro animales de ejemplo, que viaja dentro de la migración (ver Diferencia 3).

Sacando esas tres cosas —que no son el mapeo—, el contexto está **vacío de configuración de columnas**: las seis las dedujo EF.

La diferencia de fondo: a mano escribíamos **el cómo** (leé esta columna, casteala a este tipo, asignala a esta propiedad) en cada lectura y cada escritura. Con EF **no escribimos el mapeo en ningún lado**: lo deduce de la clase, por convención, en todas las operaciones. Ese era el **Dolor 1 — el mapeo a mano**.

> **¿Y si una columna se tuviera que llamar distinto que la propiedad?** Ahí —y solo ahí— se usa `HasColumnName("...")` en el `DbContext` (o `[Column("...")]` sobre la propiedad). En este proyecto no hace falta: dejamos que EF nombre las columnas como las propiedades, en PascalCase. Es justo lo contrario del proyecto a mano, donde **cada** nombre de columna lo escribíamos nosotros, a mano, en cada `SELECT` e `INSERT`.

---

## Diferencia 3 — El schema: del `schema.sql` a las migraciones

Esta es la diferencia más importante de toda la clase, porque resuelve el dolor que más caro se paga en un equipo.

### Cómo nace la tabla en `crud_ado`

A mano, el schema es un archivo SQL que escribimos nosotros (`crud_ado/schema.sql`) y que la app aplica al arrancar con un método propio, `InicializarBaseSiHaceFalta()` en el repositorio:

```sql
CREATE TABLE IF NOT EXISTS animales (
    id          INTEGER PRIMARY KEY AUTOINCREMENT,
    especie     TEXT    NOT NULL,
    ...
);
```

El problema que vimos la clase pasada: ese SQL vive **separado** de la clase `Animal`. Si cambiás la clase, tenés que acordarte de cambiar el SQL a mano, y nada verifica que coincidan.

### Cómo nace la tabla en `crud_ef`

Con EF **no escribimos el `CREATE TABLE`**. Lo genera EF a partir de la clase y su configuración, con un comando de consola:

```bash
dotnet ef migrations add InitialCreate
```

Eso crea la carpeta `crud_ef/Migrations/` con un archivo (`..._InitialCreate.cs`) que contiene el schema **derivado del modelo**. Si abrís ese archivo vas a ver el `CreateTable` con una columna por cada propiedad de `Animal` —`Id`, `Especie`, `Sexo`, `Reserva`, `Energia`, `FechaAlta`—, con los nombres tomados de las propiedades por convención (en el proyecto a mano los escribíamos nosotros en minúscula: `id`, `especie`, …). Acá **EF lo escribió por nosotros**. Hasta los datos de ejemplo viajan ahí dentro: el `HasData` del `DbContext` se traduce en un `InsertData` dentro de la migración.

La migración se aplica sola al arrancar, en `Program.cs`:

```csharp
context.Database.Migrate();
```

### Por qué esto importa

La diferencia clave está en el **flujo de cambios**. Imaginá que mañana le agregás a `Animal` una propiedad `Nombre`:

- En **`crud_ado`** tenés que tocar tres lugares a mano: la clase, el `schema.sql` (más un `ALTER TABLE` para las bases que ya existían) y todos los `INSERT`/`SELECT` del repositorio. Si te olvidás de uno, rompe.
- En **`crud_ef`** agregás la propiedad a la clase y corrés `dotnet ef migrations add AgregarNombre`. EF compara el modelo nuevo con el anterior, **genera el cambio de schema solo**, lo deja versionado en `Migrations/`, y cada compañero del equipo lo aplica con un `dotnet ef database update` (o al arrancar). El historial de cómo evolucionó la base queda guardado y es repetible.

Ese era el **Dolor 2 — el schema divorciado del código**. Las migraciones lo cierran: el schema pasa a derivarse del código y a viajar versionado junto a él.

---

## Diferencia 4 — Configuración del arranque y paquetes

Hay dos diferencias menores de plomería que conviene ver, en `Program.cs` y en el `.csproj`.

**Paquetes NuGet:**

- `crud_ado` usa `Microsoft.Data.Sqlite` (el proveedor ADO.NET "pelado").
- `crud_ef` usa `Microsoft.EntityFrameworkCore.Sqlite` (el proveedor de EF para SQLite) y `Microsoft.EntityFrameworkCore.Design` (las herramientas que necesita `dotnet ef`).

**Registro y arranque (`Program.cs`):**

```csharp
// crud_ado
builder.Services.AddScoped<AnimalRepository>();
// ... y al arrancar:
repositorio.InicializarBaseSiHaceFalta();   // crea la tabla a mano si no existe
```

```csharp
// crud_ef
builder.Services.AddDbContext<SafariContext>(opciones => opciones.UseSqlite(cs));
// (no se registra ningún repositorio: el DbContext ya es uno)
// ... y al arrancar:
context.Database.Migrate();                  // aplica las migraciones
```

En EF aparece un actor nuevo, el **`DbContext`** (`SafariContext`), que se registra como servicio y representa la sesión con la base. Es lo único que se inyecta: el `AnimalesController` lo recibe directo, sin repositorio en el medio.

---

## Resumen: los tres dolores y su cura

| Dolor (en `crud_ado`) | Dónde se veía | Cómo lo resuelve EF (en `crud_ef`) |
|---|---|---|
| **1. Mapeo a mano** | `MapearAnimal` y los `INSERT`/`UPDATE` | EF mapea por convención; no se escribe el mapeo |
| **2. Schema divorciado** | `schema.sql` + `InicializarBaseSiHaceFalta` | Migraciones generadas con `dotnet ef migrations add` |
| **3. Boilerplate** | ~20 líneas por método en el repositorio | El `DbContext` ya es el repositorio: una o dos líneas en el controller |

Y un cambio que abarca a los tres: **el repositorio entero desaparece**. En `crud_ado` era imprescindible para esconder ADO.NET; en `crud_ef` el `DbContext` ya cumple ese rol (repositorio + unidad de trabajo), así que el controller le habla directo.

Lo que **no** cambió: el modelo, el DTO y las vistas. La cara de la app es idéntica.

La idea que cierra la Unidad 6 y abre la 7: **EF no hace magia ni nada que no podamos hacer a mano** —lo demostramos haciéndolo a mano primero—. Hace exactamente lo mismo, automáticamente, versionado y sin que nos equivoquemos. Por eso valía la pena sufrir el CRUD crudo: ahora que ves los dos proyectos lado a lado, sabés con precisión qué trabajo te está ahorrando cada línea de EF.

---

## Para practicar

- Cloná los dos repos y corré `dotnet run` en cada uno. Son la misma aplicación por fuera.
- Poné lado a lado el `crud_ado/Controllers/AnimalesController.cs` + su `Repositories/AnimalRepository.cs` contra el `crud_ef/Controllers/AnimalesController.cs`. Fijate cómo en `crud_ef` el controller hace solo lo que en `crud_ado` necesitaba toda una capa de repositorio.
- Animate a un cambio: agregale a `Animal` una propiedad nueva (por ejemplo `Nombre`) en **los dos** proyectos, y compará el trabajo que te lleva en cada uno. En `crud_ef`, el cambio se cierra con `dotnet ef migrations add AgregarNombre`.
