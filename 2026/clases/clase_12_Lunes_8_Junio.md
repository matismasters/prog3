# Programación 3 2026 — Clase 12

## Unidad 6 · Un CRUD a mano con ADO.NET (y por qué después vamos a querer Entity Framework)

> **.NET 8 · C# · ASP.NET Core MVC · ADO.NET · SQLite**

La clase pasada jugamos el **Safari de Datos**: nos conectamos a una base PostgreSQL de verdad y mandamos `SELECT`, `WHERE` e `INSERT` con nuestra propia herramienta. Fue el primer contacto con la persistencia. Hoy damos el paso siguiente: construir un **CRUD completo** —crear, leer, actualizar y borrar— sobre nuestra propia base, todo **a mano con ADO.NET**.

Lo vamos a hacer a propósito "a la antigua", sin ninguna herramienta que nos ahorre trabajo. La idea no es que esta sea la forma definitiva de trabajar, sino la contraria: queremos **sentir el trabajo repetitivo y los riesgos** que aparecen cuando hacés persistencia a mano. Cada incomodidad que encontremos hoy es una pista de qué cosa nos va a resolver **Entity Framework** en la Unidad 7. Cuando lleguemos a EF no va a parecer magia: vamos a saber exactamente qué problema viene a resolver, porque hoy lo vamos a haber vivido.

> **Una nota sobre la base de hoy.** En el Safari usamos PostgreSQL, que es un servidor que hay que instalar y dejar corriendo. Hoy, para que el proyecto sea **fácil de levantar en cualquier máquina sin instalar nada**, usamos **SQLite**: una base que vive en **un solo archivo** (`animales.db`) que se crea solo. Lo importante es que el **modelo de conexión de ADO.NET es idéntico** —conexión, comando, parámetros, reader—; solo cambian las clases del driver (`Sqlite...` en vez de `Npgsql...`). Todo lo que aprendemos hoy sirve igual contra PostgreSQL o SQL Server.

El proyecto completo y funcionando está en **https://github.com/matismasters/prog3_crud_ado** (`dotnet run` y listo). A lo largo del documento vamos a ir marcando **tres dolores**. No son errores nuestros: son límites de hacer las cosas crudas. Tenelos presentes, porque son la justificación de todo lo que viene.

---

## 1. De dónde venimos: el diccionario crudo

En el Safari, nuestra herramienta tenía un método que ejecutaba cualquier SQL y devolvía las filas así:

```csharp
public List<Dictionary<string, object>> Consultar(string sql)
```

Cada fila era un `Dictionary<string, object>`: un mapa de **nombre de columna → valor**. Funcionó perfecto para explorar. Pero ahora que queremos construir una aplicación de verdad, ese tipo de dato nos empieza a jugar en contra. Veamos por qué con un ejemplo. Supongamos que tenemos una fila de animal y queremos usar sus datos:

```csharp
Dictionary<string, object> fila = filas[0];

string especie = (string)fila["especie"];
int energia = Convert.ToInt32(fila["energia"]);
string sexo = (string)fila["sexo"];
```

Mirá todo lo que está mal acá, aunque "funcione":

- **El compilador no nos cuida.** Si escribimos `fila["espcie"]` con un error de tipeo, el programa compila igual y recién explota cuando se ejecuta esa línea. El error de tipeo en el nombre de una columna es invisible hasta que un usuario lo encuentra por nosotros.
- **No hay autocompletado.** El editor no tiene idea de qué columnas existen, porque para él esto es solo un diccionario de strings. Tenemos que acordarnos de memoria de cómo se llama cada columna.
- **Casteamos en todos lados.** Cada valor sale como `object`. Para usarlo hay que castearlo (`(string)`, `Convert.ToInt32`, etc.) **cada vez**, y si nos equivocamos de tipo, otra vez: explota en ejecución, no al compilar.
- **No hay reglas ni validación.** Nada impide que `energia` sea negativa o que `sexo` traiga un valor que no tenga sentido. El diccionario guarda cualquier cosa.

Es importante notar que esto es **exactamente lo contrario del encapsulamiento** que vimos la clase pasada. El encapsulamiento se trataba de proteger los datos y obligar a usarlos a través de métodos seguros. Acá los datos están crudos, sueltos, expuestos como strings sin tipo y sin nadie que controle las reglas.

> **Dolor 1 — El diccionario crudo.** Los datos sin tipo nos quitan la ayuda del compilador, el autocompletado y la validación. Cualquier error queda escondido hasta que el programa se ejecuta.

La solución a este primer dolor la conocemos: **una clase**. En vez de mover diccionarios, vamos a tener una clase `Animal` con propiedades tipadas. Pero antes de la clase, necesitamos la base.

---

## 2. La base, a mano: el schema

Con SQLite la base es un archivo, así que no hay que instalar ningún servidor: la primera vez que la app se conecta, el archivo `animales.db` se crea solo. Lo que sí escribimos nosotros es **el schema**: el `CREATE TABLE` que define qué columnas tiene la tabla y de qué tipo. El script completo está en `schema.sql` dentro del proyecto; lo central es esto:

```sql
CREATE TABLE IF NOT EXISTS animales (
    id          INTEGER PRIMARY KEY AUTOINCREMENT,
    especie     TEXT    NOT NULL,
    sexo        TEXT    NOT NULL,
    reserva     TEXT    NOT NULL,
    energia     INTEGER NOT NULL,
    fecha_alta  TEXT    NOT NULL DEFAULT (datetime('now'))
);
```

Este paso es chico pero esconde un problema grande, así que conviene detenerse. Fijate que **el schema lo escribimos a mano, en SQL, separado de nuestro código C#**. La base ahora "sabe" que `animales` tiene seis columnas con ciertos tipos. Nuestro programa, por su lado, va a tener una clase `Animal`. **Nadie garantiza que esos dos mundos coincidan.**

Pensá qué pasa la semana que viene cuando queramos agregarle a cada animal un campo `nombre`:

1. Tenemos que acordarnos de ir a la base y correr un `ALTER TABLE animales ADD COLUMN nombre ...` a mano.
2. Tenemos que ir a la clase `Animal` y agregar la propiedad.
3. Tenemos que ir al repositorio y actualizar todos los `INSERT` y `SELECT` para que incluyan la columna nueva.

Si nos olvidamos de cualquiera de los tres pasos, la aplicación se rompe, y el error puede ser confuso (la clase pide una columna que la tabla no tiene, o al revés). Mantener sincronizados **el schema de la base** y **el modelo del código** es un trabajo manual, repetitivo y fácil de equivocar. Y en un equipo es peor: cada compañero tiene que correr los mismos cambios a mano en su propia base, en el mismo orden, sin que nada lo verifique.

> **Dolor 2 — El schema vive divorciado del código.** Crear y modificar las tablas es trabajo manual, separado de las clases, y nada garantiza que la base y el código estén de acuerdo.

Guardá este dolor en la cabeza. Entity Framework lo resuelve con algo que se llama **migraciones**: vas a escribir solo las clases, y EF se encarga de generar y versionar los cambios del schema por vos. Hoy lo hacemos a mano para entender qué es lo que esa herramienta nos va a estar ahorrando.

---

## 3. El modelo del dominio: la clase `Animal`

Acá empieza la solución al Dolor 1. En vez de mover diccionarios de strings, definimos una clase que **representa un animal de verdad**, con sus propiedades tipadas:

```csharp
namespace CrudAdo.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Especie { get; set; } = "";
        public char Sexo { get; set; }
        public string Reserva { get; set; } = "";
        public int Energia { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
```

Compará esto con el diccionario de antes. Las ventajas son inmediatas:

- **Tipos reales.** `Energia` es un `int`, `FechaAlta` es un `DateTime`. No hay que castear cada vez que los usás: ya vienen con su tipo.
- **El compilador nos cuida.** Si escribimos `animal.Espcie`, el código **no compila**. El error aparece al instante, en el editor, no cuando un usuario ejecuta esa pantalla.
- **Autocompletado.** El editor sabe qué propiedades tiene un `Animal` y nos las ofrece. Ya no hay que recordar de memoria los nombres de las columnas.
- **Un solo lugar para las reglas.** Si mañana queremos que la energía nunca pueda ser negativa, ponemos esa regla **una vez**, dentro de la clase, y vale para todo el programa. Eso es encapsulamiento: la clase protege sus propios datos.

La clase `Animal` es lo que en persistencia se llama una **entidad**: una clase que representa una fila de una tabla. La diferencia de fondo con el diccionario es que un `Animal` **significa algo**, mientras que un `Dictionary<string, object>` es apenas una bolsa de valores. Trabajar con cosas que significan algo es lo que hace que el código sea legible y reutilizable: cualquier parte del programa que reciba un `Animal` sabe exactamente qué tiene entre manos.

---

## 4. El repositorio: el CRUD a mano con ADO.NET

Ahora necesitamos el código que **traduce** entre los `Animal` de C# y las filas de la tabla. Esa pieza se llama **repositorio**: concentra en un solo lugar todo el acceso a la base para una entidad. El resto del programa le pide animales al repositorio y no toca nunca el SQL directamente. Eso también es encapsulamiento, aplicado a la persistencia: el repositorio esconde el "cómo se guarda" detrás de métodos claros.

El repositorio va a tener los cuatro métodos del CRUD, más uno para leer por id:

| Método | Operación | SQL |
|---|---|---|
| `ObtenerTodos()` | Leer todos | `SELECT` |
| `ObtenerPorId(int id)` | Leer uno | `SELECT ... WHERE id = @id` |
| `Crear(Animal a)` | Crear | `INSERT` |
| `Actualizar(Animal a)` | Modificar | `UPDATE ... WHERE id = @id` |
| `Eliminar(int id)` | Borrar | `DELETE ... WHERE id = @id` |

Mantenemos las dos reglas de oro que vimos en el Safari, porque acá importan más que nunca:

1. **Cerrá siempre lo que abrís.** Cada conexión y cada comando van dentro de un bloque `using`, que los cierra y libera solos aunque haya una excepción.
2. **Nunca concatenes strings en el SQL.** Cuando un valor viene de afuera (un id, una especie), va como **parámetro**, separado del texto del SQL. Concatenar abre la puerta a la **inyección SQL**.

### 4.1. Leer todos: el mapeo manual

Empecemos por el método que lee todos los animales. Lo interesante no es el `SELECT`, sino lo que pasa después: hay que **convertir cada fila del reader en un `Animal`**, propiedad por propiedad.

```csharp
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

El trabajo fino está en `MapearAnimal`: un método privado que toma la fila actual del reader y arma un `Animal`. Acá es donde el dato pasa de "columna sin tipo" a "propiedad tipada":

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

`GetOrdinal("especie")` nos da la posición de la columna por su nombre, y `GetString`, `GetInt32`, `GetDateTime` leen el valor **ya con el tipo correcto**. Es importante notar que **este mapeo lo escribimos nosotros, línea por línea, una propiedad a la vez**. Si la entidad tiene seis propiedades, son seis líneas; si tuviera veinte, serían veinte. Y este mismo trabajo de mapeo se repite, con pequeñas variantes, en cada método que lea animales.

### 4.2. Leer uno por id: parámetros tipados

Cuando el `SELECT` depende de un valor de afuera, ese valor va como parámetro. La forma recomendada es construir un parámetro con su **tipo explícito** (`SqliteType`), no pegar el valor al texto del SQL:

```csharp
public Animal? ObtenerPorId(int id)
{
    using (SqliteConnection conexion = new SqliteConnection(_connectionString))
    {
        conexion.Open();

        string sql = "SELECT id, especie, sexo, reserva, energia, fecha_alta FROM animales WHERE id = @id";

        using (SqliteCommand comando = new SqliteCommand(sql, conexion))
        {
            SqliteParameter pId = new SqliteParameter("@id", SqliteType.Integer);
            pId.Value = id;
            comando.Parameters.Add(pId);

            using (SqliteDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapearAnimal(reader);
                }
            }
        }
    }

    return null;  // no había un animal con ese id
}
```

El `@id` del SQL nunca toca el valor real: el valor viaja aparte, en el parámetro. Así es imposible que alguien "inyecte" SQL a través de ese dato.

### 4.3. Crear: el camino inverso

Para crear, hacemos el viaje contrario al mapeo: tomamos las propiedades del `Animal` y las metemos como parámetros del `INSERT`. Fijate que volvemos a escribir, una por una, las mismas columnas, pero ahora desde el lado del objeto hacia la base:

```csharp
public void Crear(Animal animal)
{
    using (SqliteConnection conexion = new SqliteConnection(_connectionString))
    {
        conexion.Open();

        string sql = @"INSERT INTO animales (especie, sexo, reserva, energia)
                       VALUES (@especie, @sexo, @reserva, @energia)";

        using (SqliteCommand comando = new SqliteCommand(sql, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@especie", SqliteType.Text)    { Value = animal.Especie });
            comando.Parameters.Add(new SqliteParameter("@sexo",    SqliteType.Text)    { Value = animal.Sexo.ToString() });
            comando.Parameters.Add(new SqliteParameter("@reserva", SqliteType.Text)    { Value = animal.Reserva });
            comando.Parameters.Add(new SqliteParameter("@energia", SqliteType.Integer) { Value = animal.Energia });

            comando.ExecuteNonQuery();
        }
    }
}
```

`ExecuteNonQuery()` se usa cuando el SQL **no devuelve filas** (un `INSERT`, `UPDATE` o `DELETE`); devuelve cuántas filas afectó. No le pasamos ni `id` ni `fecha_alta`: el `id` lo genera la base (`AUTOINCREMENT`) y la `fecha_alta` tiene un valor por defecto (`datetime('now')`). Esos dos detalles también hay que tenerlos en la cabeza al escribir el `INSERT` a mano.

### 4.4. Actualizar y borrar

`Actualizar` y `Eliminar` siguen exactamente el mismo molde, cambiando solo el SQL y los parámetros:

```csharp
public void Actualizar(Animal animal)
{
    // UPDATE animales SET especie = @especie, sexo = @sexo, reserva = @reserva,
    //        energia = @energia WHERE id = @id
    // ... abrir conexión (using), armar el comando, agregar un parámetro por
    //     cada columna MÁS el @id del WHERE, ExecuteNonQuery().
}

public void Eliminar(int id)
{
    // DELETE FROM animales WHERE id = @id
    // ... abrir conexión (using), un solo parámetro @id, ExecuteNonQuery().
}
```

Llegados a este punto, conviene parar y mirar el repositorio completo. **Los cinco métodos son casi el mismo código:** abrir la conexión con `using`, crear el comando, armar los parámetros uno por uno, ejecutar, y —en los que leen— mapear la fila al objeto propiedad por propiedad. Lo único que cambia de verdad entre un método y otro es el texto del SQL y la lista de columnas. Todo lo demás es **repetición**.

> **Dolor 3 — El boilerplate.** Cada operación del CRUD repite la misma estructura: conexión, comando, parámetros uno por uno, mapeo uno por uno. Y por cada columna que agregás a la entidad, tenés que tocar el mapeo, el `INSERT` y el `UPDATE`, a mano y sin red.

---

## 5. Los DTO: lo que entra desde el formulario

Cuando construyamos la pantalla para crear y editar animales, el formulario HTML le va a mandar datos al controller. Sería tentador recibir directamente un `Animal`, pero no es buena idea. El formulario de alta **no debería** poder setear el `Id` (lo genera la base) ni la `FechaAlta` (la pone la base). Si recibimos un `Animal` entero, le estamos dando al navegante la posibilidad de mandar cualquier valor para esos campos.

Por eso usamos un **DTO** (*Data Transfer Object*): una clase chica, pensada solo para **transportar los datos del formulario**, con exactamente los campos que el usuario sí puede cargar:

```csharp
using System.ComponentModel.DataAnnotations;

namespace CrudAdo.Models
{
    public class AnimalFormDto
    {
        [Required(ErrorMessage = "La especie es obligatoria.")]
        [StringLength(100)]
        public string Especie { get; set; } = "";

        [Required]
        [RegularExpression("^[MH]$", ErrorMessage = "El sexo debe ser M o H.")]
        public string Sexo { get; set; } = "";

        [Required]
        public string Reserva { get; set; } = "";

        [Range(0, 100, ErrorMessage = "La energía va de 0 a 100.")]
        public int Energia { get; set; }
    }
}
```

Las anotaciones (`[Required]`, `[Range]`, `[RegularExpression]`) son **reglas de validación** que MVC chequea automáticamente antes de que toquemos la base. Si los datos no cumplen, el formulario se vuelve a mostrar con los mensajes de error, y nunca llegamos a insertar basura.

Acá hay dos clases con responsabilidades distintas, y eso es a propósito:

- **`AnimalFormDto`** modela **lo que el usuario puede escribir** en un formulario.
- **`Animal`** modela **la entidad real** que vive en la base.

El controller se encarga de **traducir** del DTO a la entidad. Separar las dos cosas es, otra vez, encapsulamiento: la pantalla no habla directo con la entidad ni con la tabla; cada capa expone solo lo que la siguiente necesita.

---

## 6. El controller: armar el CRUD en MVC

El controller es el director de orquesta. Recibe el repositorio **por inyección de dependencias** (igual que en el Safari) y expone una acción por cada cosa que el usuario puede hacer. El patrón clásico de un CRUD en MVC son estas acciones:

| Acción | Verbo | Qué hace |
|---|---|---|
| `Index` | GET | Lista todos los animales (`ObtenerTodos`). |
| `Details` | GET | Muestra uno (`ObtenerPorId`). |
| `Create` | GET | Muestra el formulario vacío. |
| `Create` | POST | Valida el DTO, lo convierte en `Animal`, llama a `Crear`. |
| `Edit` | GET | Muestra el formulario con los datos actuales. |
| `Edit` | POST | Valida, arma el `Animal`, llama a `Actualizar`. |
| `Delete` | GET | Pide confirmación. |
| `Delete` | POST | Llama a `Eliminar`. |

Lo más interesante es el `Create` por POST, porque ahí se ve la **traducción del DTO a la entidad**:

```csharp
[HttpPost]
public IActionResult Create(AnimalFormDto form)
{
    if (!ModelState.IsValid)
    {
        // Algún dato no pasó la validación: volvemos a mostrar el form con los errores.
        return View(form);
    }

    // Traducción DTO -> entidad. El Id y la FechaAlta no se tocan: los pone la base.
    Animal animal = new Animal();
    animal.Especie = form.Especie;
    animal.Sexo = form.Sexo[0];
    animal.Reserva = form.Reserva;
    animal.Energia = form.Energia;

    _repositorio.Crear(animal);

    return RedirectToAction("Index");
}
```

Fijate el recorrido completo de un dato: entra como texto del formulario → MVC lo arma en un `AnimalFormDto` y lo **valida** → el controller lo traduce a un `Animal` → el repositorio lo convierte en parámetros de un `INSERT` → la base lo guarda como una fila. Y para leerlo, el camino es el inverso: fila → `MapearAnimal` → `Animal` → la vista lo muestra. Cada flecha de ese recorrido es código que escribimos **nosotros**, a mano.

---

## 7. El cierre: por qué todo esto justifica Entity Framework

Logramos el CRUD completo, y funciona. Pero en el camino nos topamos con tres dolores que no son culpa nuestra: son el costo de hacer persistencia cruda. Vale la pena nombrarlos juntos, porque son la razón de ser de la herramienta que viene:

1. **Dolor 1 — El diccionario crudo.** Lo resolvimos creando la clase `Animal`. Para que esa clase y la base se entiendan, tuvimos que escribir el **mapeo a mano**, propiedad por propiedad, en cada lectura y cada escritura.
2. **Dolor 2 — El schema divorciado del código.** La tabla la creamos a mano en SQL, separada de la clase. Mantener sincronizados el schema y el modelo es trabajo manual y frágil, sobre todo en equipo.
3. **Dolor 3 — El boilerplate.** Cada método del repositorio repite la misma estructura: conexión, comando, parámetros, mapeo. Agregar un campo obliga a tocar varios lugares a mano.

**Entity Framework** es un **ORM** (*Object-Relational Mapper*): una herramienta que sabe traducir entre tus objetos de C# y las tablas de la base. Resuelve, una por una, las tres cosas que hoy hicimos a mano:

- El **mapeo** lo hace EF solo: vos le decís "esta clase es una tabla" y él se encarga de convertir filas en objetos y objetos en filas. Adiós `MapearAnimal`.
- El **schema** lo genera y versiona EF con **migraciones**: vos cambiás la clase y EF produce el cambio de la base, ordenado y repetible para todo el equipo. Adiós `ALTER TABLE` a mano.
- El **boilerplate** desaparece: las operaciones del CRUD pasan a ser un par de líneas (`Add`, `SaveChanges`, `Find`, etc.) en vez de toda la danza de conexión, comando y parámetros.

Es importante entender que **EF no hace nada que no podamos hacer a mano** —hoy lo demostramos—, sino que hace **lo mismo, automáticamente y sin que nos equivoquemos**. Por eso era necesario sufrir el CRUD crudo primero: cuando en la próxima clase EF nos resuelva el mapeo, las migraciones y el boilerplate de un saque, vamos a saber **exactamente qué trabajo nos está sacando de encima**, y no lo vamos a tomar como magia.

---

## Para la próxima

- **Unidad 7 — Entity Framework.** Volvemos a hacer este mismo CRUD de animales, pero con EF, y vamos a comparar lado a lado cuánto código desaparece.
- El proyecto completo de hoy está en **https://github.com/matismasters/prog3_crud_ado**. Cloná, corré `dotnet run` y recorré el código siguiendo este documento.
- Mientras tanto, quedate con la idea de fondo: **una clase del dominio le gana al diccionario crudo** porque tiene tipos, la cuida el compilador y concentra las reglas en un solo lugar. Ese es el encapsulamiento trabajando a favor nuestro.
