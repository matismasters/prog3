# Instructivo: proyecto ASP.NET Core MVC (.NET 8) y tests de integracion

Pasos en orden, **comando a comando**, desde una carpeta vacia. Objetivo: tener una app MVC que levanta con `dotnet run` y un proyecto xUnit que usa **`WebApplicationFactory<Program>`** para llamar a la app **en memoria** sin abrir un puerto manualmente.

Requisitos: **.NET 8 SDK** instalado (`dotnet --version`).

Convenciones del curso en codigo de ejemplo: **sin `var`**, tipos explicitos.

---

## 0. Carpeta de trabajo

Elegi un directorio vacio o crea uno para la solucion.

```bash
mkdir MiAppMvcIntegracion
cd MiAppMvcIntegracion
```

Todos los comandos siguientes asumen que el **directorio actual** es `MiAppMvcIntegracion`.

---

## 1. Solucion (.sln)

```bash
dotnet new sln -n MiApp
```

Comprueba que se creo `MiApp.sln`.

---

## 2. Proyecto web MVC

```bash
dotnet new mvc -n MiApp.Web -o MiApp.Web
```

- `-n` nombre del proyecto  
- `-o` carpeta donde se genera  

Agregar el proyecto a la solucion:

```bash
dotnet sln MiApp.sln add MiApp.Web/MiApp.Web.csproj
```

Probar que compila y corre:

```bash
dotnet build MiApp.sln
dotnet run --project MiApp.Web/MiApp.Web.csproj
```

Abri `http://localhost:5xxx` en el navegador segun el puerto que muestre la consola. Cortar con **Ctrl+C**.

---

## 3. Proyecto de tests (xUnit)

```bash
dotnet new xunit -n MiApp.Web.Tests -o MiApp.Web.Tests
```

```bash
dotnet sln MiApp.sln add MiApp.Web.Tests/MiApp.Web.Tests.csproj
```

Referencia del proyecto de tests **al** proyecto web:

```bash
dotnet add MiApp.Web.Tests/MiApp.Web.Tests.csproj reference MiApp.Web/MiApp.Web.csproj
```

---

## 4. Paquete para integracion: `Microsoft.AspNetCore.Mvc.Testing`

La version mayor del paquete debe alinearse con **.NET 8** (8.0.x). Sin `--version`, NuGet suele resolver la ultima 8.x compatible.

```bash
dotnet add MiApp.Web.Tests/MiApp.Web.Tests.csproj package Microsoft.AspNetCore.Mvc.Testing
```

Si necesitas fijar parche explicito (equipo docente o CI), por ejemplo:

```bash
dotnet add MiApp.Web.Tests/MiApp.Web.Tests.csproj package Microsoft.AspNetCore.Mvc.Testing --version 8.0.11
```

```bash
dotnet restore MiApp.sln
```

---

## 5. Exponer `Program` al proyecto de tests

Con la plantilla actual, `Program.cs` usa **instrucciones de nivel superior**. Para que exista el tipo `Program` y `WebApplicationFactory<Program>` compile, agrega al **final** del archivo `MiApp.Web/Program.cs`, **despues** de la linea que arranca el servidor (`app.Run();` o `await app.RunAsync();`), esta clase parcial:

```csharp
public partial class Program { }
```

- Debe quedar en el **mismo ensamblado** que la app web (`MiApp.Web`).  
- En C# permiten declarar tipos **despues** de las instrucciones de nivel superior en el mismo archivo; no pongas esta clase dentro de un metodo.  
- Alternativa equivalente: un archivo nuevo en `MiApp.Web`, por ejemplo `Program.Partial.cs`, con solo ese contenido (mismo namespace global si la plantilla no usa `namespace` en `Program.cs`).

Guarda el archivo.

---

## 6. Archivo de test de integracion

Crea `MiApp.Web.Tests/HomePageIntegrationTests.cs` con este contenido (ajusta el `namespace` si cambiaste nombres de proyecto):

```csharp
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MiApp.Web.Tests;

public class HomePageIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient cliente;

    public HomePageIntegrationTests(WebApplicationFactory<Program> fabrica)
    {
        cliente = fabrica.CreateClient();
    }

    [Fact]
    public async Task Get_Raiz_RetornaOk()
    {
        HttpResponseMessage respuesta = await cliente.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);
    }

    [Fact]
    public async Task Get_Privacy_RetornaOk()
    {
        HttpResponseMessage respuesta = await cliente.GetAsync("/Home/Privacy");

        Assert.Equal(HttpStatusCode.OK, respuesta.StatusCode);
    }
}
```

Notas:

- `WebApplicationFactory<Program>` levanta la aplicacion en memoria para cada clase de test que use el fixture.  
- `CreateClient()` devuelve un `HttpClient` ya configurado contra ese host interno.  
- Las rutas deben coincidir con las de la plantilla MVC por defecto (`/` y `/Home/Privacy`). Si borraste acciones o rutas, adapta las URLs.

---

## 7. Compilar y ejecutar tests

Desde `MiAppMvcIntegracion`:

```bash
dotnet build MiApp.sln
dotnet test MiApp.sln
```

Deberias ver tests pasando. Si falla la compilacion por `Program`, revisa el paso 5.

---

## 8. Opcional: `WebApplicationFactory` personalizada

Si mas adelante necesitas **inyectar** configuracion, base en memoria o variables de entorno solo para tests, se suele crear una clase que herede de `WebApplicationFactory<Program>` y sobrescriba `ConfigureWebHost`. Eso queda fuera de este instructivo minimo.

---

## 9. Problemas frecuentes

| Sintoma | Que revisar |
|--------|-------------|
| Error: `Program` no encontrado | Falta `public partial class Program { }` en el proyecto web. |
| 404 en `/` | Rutas distintas a la plantilla; ajusta la URL en el test o revisa `MapControllerRoute`. |
| Conflicto de versiones NuGet | `Microsoft.AspNetCore.Mvc.Testing` en 8.0.x con proyecto `net8.0`. |
| Tests lentos | Normal al primer arranque; el factory recicla el host segun xUnit. |

---

## Resumen de comandos (copia rapida)

```bash
mkdir MiAppMvcIntegracion && cd MiAppMvcIntegracion
dotnet new sln -n MiApp
dotnet new mvc -n MiApp.Web -o MiApp.Web
dotnet sln MiApp.sln add MiApp.Web/MiApp.Web.csproj
dotnet new xunit -n MiApp.Web.Tests -o MiApp.Web.Tests
dotnet sln MiApp.sln add MiApp.Web.Tests/MiApp.Web.Tests.csproj
dotnet add MiApp.Web.Tests/MiApp.Web.Tests.csproj reference MiApp.Web/MiApp.Web.csproj
dotnet add MiApp.Web.Tests/MiApp.Web.Tests.csproj package Microsoft.AspNetCore.Mvc.Testing
```

Luego: editar `Program.cs` del web, crear `HomePageIntegrationTests.cs`, y:

```bash
dotnet test MiApp.sln
```
