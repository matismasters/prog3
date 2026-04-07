# Prompts: proyectos .NET 8 con xUnit (consola y MVC)

Estos textos están pensados para **copiar y pegar** en el asistente (Cursor, Copilot, etc.) y obtener una solución **revisable**: siguen la idea de **prompt mejor** de la Unidad 1 (objetivo, contexto, restricciones, criterio de salida) y el estilo **explícito** de `prompts_comparacion_modelos.md`.

**Después de generar el código, siempre:** `dotnet build` y `dotnet test` en la raíz de la solución. Si algo falla, pedir corrección por pasos o arreglar a mano; no dar por cerrado sin que los comandos pasen.

**Tests de integracion con MVC** (sin IA, paso a paso con `WebApplicationFactory`): ver `instructivo_mvc_tests_integracion.md` en esta misma carpeta.

---

## 1. Proyecto de consola + proyecto xUnit (solucion lista para `dotnet test`)

### Prompt (copiar todo el bloque siguiente)

```text
Contexto: estoy en una carpeta vacia (o quiero crear todo desde cero). Curso Programacion 3, .NET 8, C#, sin Visual Studio: trabajo con terminal (dotnet CLI).

Objetivo: generar una solucion .NET 8 con:
1) Un proyecto de aplicacion de consola (por ejemplo nombre `MiApp.Consola`).
2) Un proyecto de pruebas xUnit (por ejemplo `MiApp.Consola.Tests`).
3) La solucion (.sln) debe incluir ambos proyectos y el proyecto de tests debe tener `ProjectReference` al de consola.

Restricciones:
- Target framework: net8.0 en ambos proyectos.
- En todo el codigo C# generado, no usar `var`: declarar variables locales con el tipo explicito.
- No agregar paquetes NuGet externos salvo los que ya trae la plantilla xUnit (Microsoft.NET.Test.Sdk, xunit, xunit.runner.visualstudio, coverlet opcional).
- En el proyecto de consola, exponer al menos una clase publica con logica pura y testeable, por ejemplo `public static class Calculadora` con metodo `public static int Sumar(int a, int b)` (puede ser otro nombre, pero debe ser logica deterministica sin consola ni archivos).
- `Program.cs` debe llamar a esa logica y mostrar un resultado en consola (para poder ejecutar `dotnet run` en el proyecto de consola).
- En el proyecto de tests, clase `CalculadoraTests` (o equivalente) con al menos 3 tests xUnit usando Arrange-Act-Assert: caso normal y al menos dos bordes razonables (por ejemplo negativos o cero, segun encaje con la logica elegida).
- Usar [Fact] (si queres agregar un [Theory] con InlineData, mejor).

Criterio de salida:
- Listame los comandos `dotnet` en orden exacto que yo debo ejecutar desde la carpeta raiz para crear la misma estructura si lo hiciera a mano (dotnet new sln, dotnet new console, dotnet new xunit, dotnet sln add, dotnet add reference), O bien entrega la estructura de carpetas y el contenido completo de cada archivo.
- Al final, debe quedar verificable que desde la carpeta donde esta el .sln: `dotnet build` y `dotnet test` terminan sin errores.
- No uses top-level statements confusos en la clase bajo prueba; la consola puede usar top-level statements en Program.cs si queres.
- Respuesta en español; código y nombres de proyecto en inglés o espanglish consistente.
```

### Checklist rápido (alumno)

- [ ] Existe un archivo `.sln` y los dos proyectos estan agregados.
- [ ] `MiApp.Consola.Tests` referencia al proyecto de consola (no al reves).
- [ ] `dotnet test` descubre y ejecuta los tests sin fallos.
- [ ] `dotnet run --project <carpeta consola>` imprime algo coherente.

---

## 2. Proyecto ASP.NET Core MVC + proyecto xUnit (tests unitarios sobre logica de aplicacion)

### Prompt (copiar todo el bloque siguiente)

```text
Contexto: Programacion 3, .NET 8, ASP.NET Core MVC. Crear solucion nueva desde cero con dotnet CLI (sin Visual Studio IDE).

Objetivo: una solucion con:
1) Proyecto web MVC (por ejemplo `MiApp.Web`, plantilla `dotnet new mvc`).
2) Proyecto xUnit (por ejemplo `MiApp.Web.Tests`) con referencia al proyecto web.

Restricciones importantes:
- net8.0 en ambos.
- En todo el codigo C# generado, no usar `var`: declarar variables locales con el tipo explicito.
- No agregar NuGet externos para los tests salvo los de la plantilla xUnit. Los tests deben ser **unitarios** sobre clases con logica pura (sin levantar Kestrel ni usar WebApplicationFactory).
- Dentro del proyecto MVC, crear una carpeta clara (por ejemplo `Services/`) con una clase publica injectable o instanciable simple, por ejemplo `public class SaludoService` con metodo `string ArmarSaludo(string? nombre)` que devuelve "Hola, invitado." si nombre es null o vacio o blanco, y "Hola, {nombre}." si hay nombre valido (recortar espacios extremos).
- Registrar ese servicio en DI en Program.cs con AddSingleton o AddScoped (lo mas simple posible).
- Opcional pero deseable: que `HomeController` reciba `SaludoService` por constructor y pase el saludo a la vista Index (Model o ViewBag), para que la app sea coherente; si hace falta, ajustar la vista Index minima.

Proyecto de tests:
- Clase `SaludoServiceTests` con tests xUnit y Arrange-Act-Assert.
- Casos minimos: null, string vacio, nombre valido, nombre con espacios alrededor que debe recortarse.
- Sin mocks salvo que la plantilla lo requiera; aqui no hace falta mock.

Criterio de salida:
- Entregar arbol de carpetas y contenido completo de: .sln, ambos .csproj, Program.cs, la clase de servicio, el/los controlador/es tocados, la vista Index si se modifico, y todos los archivos de test.
- Indicar comandos exactos: crear sln, crear mvc, crear xunit, agregar a sln, add reference de tests -> web.
- Verificacion final obligatoria en tus instrucciones: desde la raiz de la solucion, `dotnet build` y `dotnet test` deben pasar; y `dotnet run --project MiApp.Web` debe levantar la app (puedo cancelar con Ctrl+C).
- No incluir secretos, credenciales ni connection strings.
- Respuesta en español; nombres de tipos y proyectos en inglés coherentes.
```

### Checklist rápido (alumno)

- [ ] El proyecto de tests referencia al proyecto **web**, no al reves.
- [ ] Los tests instancian `new SaludoService()` (o el nombre que hayan elegido) y no dependen de HTTP.
- [ ] `dotnet test` pasa sin abrir navegador.
- [ ] Si el modelo agrego paquetes extra, revisar que no sean innecesarios; para esta consigna deberia bastar el SDK.

---

## Notas para el docente

- Si el modelo **omite** `dotnet sln add` o la referencia entre proyectos, un segundo prompt corto suele alcanzar: "La solucion no ejecuta tests: agrega el proyecto de tests al .sln y la ProjectReference correcta; mostra solo los archivos .sln y .csproj modificados."
- Para alumnos que ya tienen carpeta sucia: agregar al inicio del prompt "Crear todo dentro de una subcarpeta `EjercicioConsolaTests`" (o similar) para no mezclar con otros repos.
