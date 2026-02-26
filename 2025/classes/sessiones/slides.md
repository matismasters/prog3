<!-- Diapositiva 1 -->
# Gestión de Variables de Sesión en ASP.NET Core MVC

## Programación 2 - Visual Studio 2022 - .NET 8

---

<!-- Diapositiva 2 -->
## ¿Qué es una sesión?

- Una **sesión** permite almacenar información temporal para un usuario entre diferentes requests.
- Es útil para:
  - Autenticación de usuarios.
  - Persistir datos durante la navegación.
  - Carritos de compra, formularios múltiples, etc.

---

<!-- Diapositiva 3 -->
## Habilitar el uso de sesiones

### Paso 1: Configurar en `Program.cs`

~~~csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Habilitar servicios de sesión

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Usar sesiones

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
~~~

---

<!-- Diapositiva 4 -->
## Cómo guardar una variable en la sesión

### En un Controller

~~~csharp
HttpContext.Session.SetString("Usuario", "Matías");
HttpContext.Session.SetInt32("Edad", 30);
~~~

> Solo se pueden guardar valores **string** o **int** directamente. Para otros tipos, se usa serialización.

---

<!-- Diapositiva 5 -->
## Cómo recuperar una variable de la sesión

~~~csharp
string? usuario = HttpContext.Session.GetString("Usuario");
int? edad = HttpContext.Session.GetInt32("Edad");
~~~

> Recordá que estos métodos pueden devolver `null` si no existe la clave.

---

<!-- Diapositiva 6 -->
## Cómo eliminar o limpiar sesión

### Eliminar una clave específica:

~~~csharp
HttpContext.Session.Remove("Usuario");
~~~

### Eliminar toda la información de sesión:

~~~csharp
HttpContext.Session.Clear();
~~~

---

<!-- Diapositiva 7 -->
## Ejemplo práctico: guardar nombre de usuario

~~~csharp
public class LoginController : Controller
{
    public IActionResult Login(string usuario)
    {
        HttpContext.Session.SetString("Usuario", usuario);
        return RedirectToAction("Index", "Home");
    }
}
~~~

### Luego, en el controlador de Home:

~~~csharp
public IActionResult Index()
{
    string? nombre = HttpContext.Session.GetString("Usuario");
    ViewBag.Saludo = nombre != null ? $"Hola {nombre}!" : "Hola visitante!";
    return View();
}
~~~

---

<!-- Diapositiva 8 -->
## Consideraciones

- Las variables de sesión son **específicas del navegador del usuario**.
- Si se cierra el navegador o expira la sesión, se pierden los datos.
- Para tipos complejos, se recomienda **serializar a JSON**:

~~~csharp
var objeto = new MiClase { Nombre = "Ejemplo" };
HttpContext.Session.SetString("objeto", JsonSerializer.Serialize(objeto));
~~~

Y para leer:

~~~csharp
var objeto = JsonSerializer.Deserialize<MiClase>(
    HttpContext.Session.GetString("objeto")!
);
~~~

---

<!-- Diapositiva 9 -->
## ¿Dónde se almacenan las sesiones?

- Por defecto: **memoria del servidor (InMemory)**.
- También se puede configurar para Redis, SQL Server u otras fuentes externas.

> En desarrollo, la opción por defecto es suficiente.

---

<!-- Diapositiva 10 -->
## Resumen

- Agregá `AddSession()` y `UseSession()` en `Program.cs`  
- Usá `SetString`, `SetInt32`, `GetString`, `GetInt32` en el `HttpContext.Session`  
- Podés eliminar claves específicas o limpiar todo  
- Para objetos complejos, usá JSON

