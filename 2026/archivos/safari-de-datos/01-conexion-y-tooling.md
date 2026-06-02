# Unidad 6 — Safari de Datos: Conexión y Tooling

> **.NET 8 · C# · ASP.NET Core MVC · Npgsql (ADO.NET)**

En esta etapa vas a construir tu propia herramienta para conectarte a una base **PostgreSQL** que provee el docente. Esa base es contra la que vas a jugar el **Safari de Datos**: tu herramienta es la que te va a servir para mandar consultas durante el juego.

La meta concreta de hoy es chica y clara: que **`SELECT 1` devuelva `1` en pantalla**. Si lográs eso, ya tenés conexión y tooling funcionando.

---

## 1. Qué es "tooling" y por qué lo construís vos

**Tooling** = tu propia herramienta/utilitario para trabajar. En este caso, una **mini app web** (ASP.NET Core MVC) que te deja:

1. Escribir una consulta SQL.
2. Mandarla a la base del docente.
3. Ver el resultado en pantalla.

¿Por qué armar la tuya en vez de usar un cliente ya hecho (pgAdmin, DBeaver, etc.)?

- Porque el objetivo del curso es que **entiendas el modelo de conexión de ADO.NET por dentro**, no apretar botones de una GUI.
- Porque durante el juego vas a querer una herramienta **tuya**, que adaptes a tu ritmo y a las consultas que necesites.
- Porque es el mismo patrón de acceso a datos que vas a usar en cualquier app .NET real.

---

## 2. El modelo de conexión de ADO.NET (conceptual)

ADO.NET es la capa de acceso a datos de .NET. Con Npgsql (el driver de Postgres) el flujo es siempre el mismo:

| Pieza | Qué es |
|---|---|
| **Connection string** | El texto con los datos para conectarse: host, puerto, base, usuario, password. Te lo pasa el docente. |
| **`NpgsqlConnection`** | La conexión a la base. Se **abre** (`Open()`), se usa, y se **cierra**. |
| **`NpgsqlCommand`** | El comando: el SQL que vas a ejecutar sobre esa conexión. |
| **`ExecuteScalar()`** | Ejecuta y devuelve **un solo valor** (la primera celda). Ideal para `SELECT 1`. |
| **`ExecuteReader()`** | Ejecuta y devuelve **varias filas/columnas**, que recorrés con un lector. |

### Abrir y cerrar: siempre con `using`

Una conexión es un recurso caro y limitado. Si la abrís y no la cerrás, la "perdés" (leak) y la base se queda sin conexiones disponibles. Por eso **siempre** cerrás lo que abrís.

La forma correcta en C# es el bloque `using`: garantiza que la conexión (y el comando) se cierren y liberen (`Dispose`) **aunque haya una excepción**. No tenés que acordarte de llamar `Close()` a mano.

### Parámetros: nunca concatenes strings

Cuando una consulta dependa de un valor que viene del usuario, **no lo pegues con `+` dentro del SQL**. Eso abre la puerta a inyección SQL y a errores de tipos. Se usan **parámetros**, que mandan el valor aparte del texto SQL. Hoy con `SELECT 1` no hace falta, pero tenelo presente desde el arranque.

> **Nota sobre parámetros.** Vas a ver mucho `cmd.Parameters.AddWithValue(...)` por ahí. Conviene evitarlo: infiere mal los tipos (sobre todo con Postgres) y te puede dar errores raros. Mejor construí un `NpgsqlParameter` con su tipo explícito (`NpgsqlDbType`) y agregalo a `cmd.Parameters`.

---

## 3. Pasos concretos (comando a comando)

### 3.1. Crear el proyecto MVC

```bash
dotnet new mvc -n SafariDeDatos
cd SafariDeDatos
```

### 3.2. Agregar el paquete Npgsql

```bash
dotnet add package Npgsql --version 8.0.*
```

> **Nota.** Fijamos la rama `8.0.*` porque es la compatible con .NET 8. Sin versión, NuGet puede traer una rama más nueva (9.x o superior) que no enganche con tu runtime y te falle al compilar o al ejecutar.

### 3.3. Guardar el connection string

El servidor del juego es **público**: la base PostgreSQL está en el host `safari.codice.uy`. El docente te pasa el `Port`, `Database`, `Username` y `Password` (el puerto puede no ser el 5432 por defecto). Como te conectás por internet, agregamos SSL. Ponelo en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Safari": "Host=safari.codice.uy;Port=<puerto>;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

> **NO uses `localhost`.** `localhost` apunta a **tu propia máquina**, no al servidor del juego. Si ponés `localhost` te vas a conectar (o fallar) contra tu PC, no contra la base del Safari. El host es **`safari.codice.uy`** y punto.

### 3.4. Código mínimo: un servicio que ejecuta `SELECT 1`

Creá `Services/SafariRepository.cs`. Fijate que **no usamos `var`**: tipos explícitos siempre.

```csharp
using Npgsql;

namespace SafariDeDatos.Services
{
    public class SafariRepository
    {
        private readonly string _connectionString;

        public SafariRepository(IConfiguration configuration)
        {
            // GetConnectionString devuelve string? : puede ser null si falta la clave.
            // Validamos con un throw claro en vez de arrastrar un null que después
            // explota con un NullReferenceException confuso al abrir la conexión.
            string? cs = configuration.GetConnectionString("Safari");
            _connectionString = cs ?? throw new InvalidOperationException(
                "Falta el connection string 'Safari' en appsettings.json");
        }

        public long ProbarConexion()
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(_connectionString))
            {
                conexion.Open();

                using (NpgsqlCommand comando = new NpgsqlCommand("SELECT 1", conexion))
                {
                    object? resultado = comando.ExecuteScalar();
                    return Convert.ToInt64(resultado);
                }
            }
            // Al salir de los using, el comando y la conexión se cierran solos.
        }
    }
}
```

### 3.4.b. El método que de verdad usás en el juego: ejecutar SQL arbitrario

`ProbarConexion()` es solo el primer hito. **La herramienta del Safari es una caja donde vos tipeás SQL y ves las filas que vuelven.** En el juego vas a llamar a una función como `explorar('leon')` (devuelve varias filas y columnas), hacer `SELECT ... WHERE ...`, e `INSERT`. Para eso necesitás un método general que reciba el SQL, use `ExecuteReader` y te devuelva **todas las filas**.

Lo modelamos como `List<Dictionary<string, object>>`: una lista de filas, donde cada fila es un diccionario `nombre de columna → valor`. Agregalo a `SafariRepository`:

```csharp
public List<Dictionary<string, object>> Consultar(string sql)
{
    List<Dictionary<string, object>> filas = new List<Dictionary<string, object>>();

    using (NpgsqlConnection conexion = new NpgsqlConnection(_connectionString))
    {
        conexion.Open();

        using (NpgsqlCommand comando = new NpgsqlCommand(sql, conexion))
        using (NpgsqlDataReader reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                Dictionary<string, object> fila = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columna = reader.GetName(i);
                    object valor = reader.GetValue(i);
                    fila[columna] = valor;
                }

                filas.Add(fila);
            }
        }
    }

    return filas;
}
```

`reader.Read()` avanza fila por fila (devuelve `false` cuando no quedan más). `reader.FieldCount` es la cantidad de columnas; con `GetName(i)` sacás el nombre y con `GetValue(i)` el valor de cada una. Este mismo método te sirve para `SELECT`, para llamar a `explorar(...)` y para cualquier consulta que devuelva filas.

### 3.5. Registrar el servicio

En `Program.cs`, antes de `var app = builder.Build();` (esa línea de `var` es del template; tu código va sin `var`):

```csharp
builder.Services.AddScoped<SafariDeDatos.Services.SafariRepository>();
```

### 3.6. Acción de controlador

En `Controllers/HomeController.cs`, recibí el servicio por el constructor y agregá una acción:

```csharp
using SafariDeDatos.Services;

public class HomeController : Controller
{
    private readonly SafariRepository _repositorio;

    public HomeController(SafariRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public IActionResult ProbarConexion()
    {
        long resultado = _repositorio.ProbarConexion();
        ViewBag.Resultado = resultado;
        return View();
    }

    // GET: muestra la caja vacía para tipear SQL.
    [HttpGet]
    public IActionResult Consola()
    {
        return View();
    }

    // POST: ejecuta el SQL que vino del formulario y muestra las filas.
    [HttpPost]
    public IActionResult Consola(string sql)
    {
        ViewBag.Sql = sql;
        List<Dictionary<string, object>> filas = _repositorio.Consultar(sql);
        ViewBag.Filas = filas;
        return View();
    }
}
```

### 3.7. Vista

Creá `Views/Home/ProbarConexion.cshtml`:

```html
@{
    ViewData["Title"] = "Probar conexión";
}

<h1>Probar conexión</h1>
<p>SELECT 1 devolvió: <strong>@ViewBag.Resultado</strong></p>
```

### 3.7.b. La vista de la herramienta: una caja para tipear SQL

**Esta vista es la herramienta del juego.** Un `<textarea>` para escribir cualquier consulta, un botón para mandarla, y una `<table>` que muestra todas las filas que volvieron. Creá `Views/Home/Consola.cshtml`:

```html
@{
    ViewData["Title"] = "Consola SQL";
    List<Dictionary<string, object>>? filas = ViewBag.Filas as List<Dictionary<string, object>>;
}

<h1>Consola SQL</h1>

<form method="post">
    <textarea name="sql" rows="5" cols="80" placeholder="SELECT * FROM explorar('leon');">@ViewBag.Sql</textarea>
    <br />
    <button type="submit">Ejecutar</button>
</form>

@if (filas != null)
{
    @if (filas.Count == 0)
    {
        <p>La consulta no devolvió filas.</p>
    }
    else
    {
        <table border="1" cellpadding="4">
            <thead>
                <tr>
                    @foreach (string columna in filas[0].Keys)
                    {
                        <th>@columna</th>
                    }
                </tr>
            </thead>
            <tbody>
                @foreach (Dictionary<string, object> fila in filas)
                {
                    <tr>
                        @foreach (object valor in fila.Values)
                        {
                            <td>@valor</td>
                        }
                    </tr>
                }
            </tbody>
        </table>
    }
}
```

Con esto ya tenés lo que vas a usar en el Safari: escribís `SELECT * FROM explorar('leon');`, apretás **Ejecutar**, y ves las filas en pantalla. El mismo cuadro te sirve para `SELECT ... WHERE`, para `INSERT`, y para cualquier consulta del juego.

### 3.8. Correr y probar

```bash
dotnet run
```

Entrá a `/Home/ProbarConexion` en el navegador. Si ves **`SELECT 1 devolvió: 1`**, ganaste: tu tooling se conecta a la base. Después entrá a `/Home/Consola` y probá `SELECT 1;` desde la caja de texto: esa es la herramienta con la que vas a jugar el Safari.

---

## 4. La meta de esta etapa

> **Lograr que `SELECT 1` devuelva `1` en pantalla.**

El docente te pasa el connection string. Con eso ya podés probar la conexión con `SELECT 1` desde el arranque, **antes incluso de que empiece el juego**. Si `SELECT 1` funciona, el resto del Safari es cambiar el SQL que mandás.

---

## 5. Para cerrar — recordá siempre

- **Cerrá las conexiones siempre.** Usá `using` y dejá que `Dispose` haga el trabajo. Nunca dejes una conexión abierta colgada.
- **Usá parámetros**, no concatenación de strings, cuando la consulta dependa de un valor de entrada.
- **Esta herramienta es la que vas a usar en el juego.** Cuidala y tenela lista: el Safari se juega con tu tooling.
