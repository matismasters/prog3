# Programacion 3 2026
## Clase 5

# Hoy

- Inicio **Unidad 3**
- **Desde cero:** que son las **pruebas unitarias** y el patron **Arrange–Act–Assert**
- Herramienta en .NET: **xUnit** y **`dotnet test`**
- Luego: **IA** para ampliar o acelerar tests, siempre con revision
- Ejercicio practico sobre **codigo de ejemplo** del docente

Esta semana repartimos la Unidad 3 en dos clases. Hoy primero **conceptos y herramienta**; despues **generar y revisar tests** con apoyo de IA. Manana cerramos con **refactorizacion** y **documentacion tecnica**.

---

# Venimos de aca

- Unidades 1 y 2: herramientas, limites, uso responsable, criterios de calidad
- Ya saben que la IA puede **omitir bordes** o sugerir tests **cosmeticos**

Hoy aplicamos eso a **pruebas unitarias**: primero entendemos **que son** y **como se organiza** un test; recien despues metemos la IA como acelerador.

---

# Unidad 3
## IA aplicada — Pruebas, refactor y documentacion

Objetivos del programa:

- Generar **pruebas unitarias** asistidas por IA a partir de codigo existente (**C# / xUnit**)
- **Refactorizar** con IA y validar que el comportamiento se mantenga
- **Documentacion tecnica** (XML, README, contratos) con IA y revision

Hoy cubrimos la primera pata en profundidad; manana las otras dos y el ciclo integrado.

---

# Que son las pruebas unitarias
## Idea central

- Un **test unitario** es codigo automatico que **ejecuta una parte chica** de tu programa (una funcion, un metodo, una regla) y **comprueba** si el resultado es el esperado.
- Objetivo: detectar **regresiones** — que algo que andaba **deje de andar** despues de un cambio — lo antes posible.
- Corren **sin intervencion manual**: no abris el navegador ni haces clic; el entorno de tests ejecuta y dice **paso** o **fallo**.

---

# Que no es el foco de hoy

- **No** es el manual de usuario ni la demo para el cliente.
- **No** es un test end-to-end completo (toda la app + base + red): eso se ve mas adelante en otros tipos de prueba.
- **No** reemplaza **pensar**: el test refleja **lo que vos decidiste** que debe cumplirse.

Hoy la **unidad** es pequena: una clase o metodos con logica clara, sin base de datos ni pantallas obligatorias en el test.

---

# Por que molestarse con tests

- **Documentan comportamiento:** el nombre del test y las aserciones dicen "esto promete el codigo".
- **Dan seguridad al cambiar:** refactor o nueva feature; si rompes algo, un test falla.
- **Encaran casos aburridos:** null, cero, vacio — los que uno olvida al probar "a mano".

---

# Patron AAA: Arrange, Act, Assert
## Tres pasos, siempre en ese orden mental

Es una **convencion** para leer y escribir tests; no es magia ni cosa de IA.

- **Arrange (preparar):** armar datos de entrada, crear el objeto bajo prueba, configurar lo minimo necesario.
- **Act (actuar):** llamar **una vez** al metodo o funcion que queres verificar, es decir la unidad bajo prueba.
- **Assert (afirmar):** comprobar el resultado: valor de retorno, propiedad o excepcion esperada. Si no coincide, el test **falla** y el runner lo marca en rojo.

Ventaja: cualquier persona del equipo lee el test y entiende **que escenario** se probo.

---

# Ejemplo mental con AAA

Supongamos un metodo `EsMayorDeEdad(int edad)` que debe ser true solo si `edad >= 18`.

- **Arrange:** `int edad = 17;`
- **Act:** `bool resultado = EsMayorDeEdad(edad);`
- **Assert:** "espero `false` porque 17 no es mayor de edad".

Otro test, **mismo metodo**, otro escenario:

- **Arrange:** `int edad = 18;`
- **Act:** mismo llamado
- **Assert:** espero `true`.

Cada escenario importante suele ser **un test**. Con `[Theory]` se pueden probar varias entradas en un solo metodo. La IA puede **sugerir** mas casos; vos **validas** si tienen sentido.

---

# De AAA a "test unitario" en la practica

- Un archivo de tests tiene **metodos**; cada metodo suele ser **un escenario** o, como mucho, un grupo muy acotado.
- El nombre del metodo conviene que sea **descriptivo**, por ejemplo `EsMayorDeEdad_17_False` o `CuandoEdadEs17_DebeRetornarFalse`.
- Si mezclas muchos Act y muchos Assert sin orden, el test se vuelve **dificil de leer** y de depurar.

---

# Por que tests con IA

- Propone **casos** que a uno se le escapan: null, vacio, extremos
- Escribe **esqueletos** de test rapido
- Tambien puede generar **tests debiles** o redundantes si el prompt es vago

**Regla:** el valor no es la cantidad de tests, sino que **fallen cuando el codigo este mal** y **pasen cuando este bien**. Eso es independiente de la IA: la IA solo **escribe mas rapido**; el criterio es el mismo que acabamos de ver con AAA.

---

# xUnit en .NET

- Proyecto de pruebas: `dotnet new xunit`
- Referencia al proyecto bajo prueba: `dotnet add reference`
- Ejecutar **`dotnet test`** desde la carpeta de la solucion o del proyecto de tests

Convenciones habituales: clases de test, metodos con `[Fact]` o `[Theory]` para datos tabulados. El docente muestra la estructura en el ejemplo de hoy.

En xUnit, AAA **no es una palabra clave**: es **orden de lineas** dentro del metodo de test. A veces se marcan bloques con comentarios `// Arrange` para guiar la lectura. Lo importante es **no mezclar** preparacion, ejecucion y comprobacion sin orden.

**Prompt util para la IA:** pegar la firma del metodo, listar **reglas del negocio** y pedir tests xUnit con **Arrange-Act-Assert** en bloques claros y **casos borde** explicitos: null, vacio, extremos.

---

# Ejemplo para mostrar en clase
## `Calculadora` y tests xUnit

Version completa y lista para copiar en `2026/archivos/ejemplo_calculadora_y_tests_xunit.md`. Aca el nucleo para proyectar o repasar en vivo.

**Clase bajo prueba** — logica pura, estatica, sin consola adentro:

```csharp
namespace MiApp.Consola;

public static class Calculadora
{
    public static int Sumar(int a, int b)
    {
        return a + b;
    }

    public static int Restar(int a, int b)
    {
        return a - b;
    }

    public static int Multiplicar(int a, int b)
    {
        return a * b;
    }
}
```

**Tests** — mezcla de **`[Fact]`** (un escenario fijo, sin parametros) y **`[Theory]`** con **`[InlineData]`** (misma prueba, varias filas de datos). Comentarios `// Arrange`, `// Act`, `// Assert` donde aportan; **sin `var`**.

```csharp
using MiApp.Consola;
using Xunit;

namespace MiApp.Consola.Tests;

public class CalculadoraTests
{
    [Fact]
    public void Sumar_DosPositivos_RetornaLaSuma()
    {
        // Arrange
        int a = 2;
        int b = 3;

        // Act
        int resultado = Calculadora.Sumar(a, b);

        // Assert
        Assert.Equal(5, resultado);
    }

    [Fact]
    public void Restar_MinuendoMenor_RetornaNegativo()
    {
        int resultado = Calculadora.Restar(3, 8);
        Assert.Equal(-5, resultado);
    }

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 7, 7)]
    [InlineData(-4, -2, -6)]
    [InlineData(100, -100, 0)]
    public void Sumar_VariosPares_RetornaSumaEsperada(int a, int b, int esperado)
    {
        int resultado = Calculadora.Sumar(a, b);
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 4, 6)]
    [InlineData(3, 8, -5)]
    [InlineData(0, 0, 0)]
    public void Restar_VariosPares_RetornaDiferenciaEsperada(
        int minuendo,
        int sustraendo,
        int esperado)
    {
        int resultado = Calculadora.Restar(minuendo, sustraendo);
        Assert.Equal(esperado, resultado);
    }

    [Fact]
    public void Multiplicar_PorCero_RetornaCero()
    {
        int resultado = Calculadora.Multiplicar(0, 42);
        Assert.Equal(0, resultado);
    }

    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-2, 5, -10)]
    [InlineData(-3, -4, 12)]
    public void Multiplicar_VariosPares_RetornaProductoEsperado(int a, int b, int esperado)
    {
        int resultado = Calculadora.Multiplicar(a, b);
        Assert.Equal(esperado, resultado);
    }
}
```

- **Fact:** un caso, nombre muy especifico. **Theory:** varias filas; cada `[InlineData]` es una corrida distinta en `dotnet test`.
- Preguntas rapidas: cuando conviene agregar un **Fact** nuevo vs una **linea InlineData**; que pasaria si el esperado de una fila Theory esta mal escrito.

---

# Revision critica de lo que genera la IA

- ¿Los tests dependen del **orden de ejecucion** o de estado global?
- ¿Hay aserciones **debiles** como `Assert.True(true)` o que no liguen al comportamiento?
- ¿Faltan **null**, lista vacia, cero, negativos, strings vacios?
- ¿Los nombres del test **dicen** que escenario cubren?

Conecta con Unidad 2: **coherencia aparente** tambien en archivos de test.

---

# Cobertura vs pertinencia

- **Muchas lineas cubiertas** no implican que los casos importantes esten probados
- A veces conviene **menos tests** pero alineados a reglas de negocio
- Si la IA inventa dependencias o APIs inexistentes: **compilar** y corregir

---

# Flujo de trabajo en clase

1. Recibir o clonar el **modulo de ejemplo**: una clase o servicio pequeño, sin persistencia ni arquitectura en capas avanzada del curso
2. Con IA: pedir conjunto de tests xUnit siguiendo convenciones del curso
3. **`dotnet build`** y **`dotnet test`**
4. Arreglar **tests rotos** o **codigo** segun corresponda; el fallo indica que se rompio

---

# Ejercicio principal
## Modulo C# + tests con IA

- El docente entrega el codigo base en archivo o repositorio minimo
- Objetivo: suite de tests que compile, ejecute y refleje criterios de negocio razonables
- Documentar brevemente que pidieron a la IA y que tuvieron que corregir

Ese habito de registrar el trabajo con la IA anticipa `ia_docs` y los planes numerados del integrador.

---

# Cierre de hoy

- Un test bueno **protege** un comportamiento; uno malo da **falsa confianza**
- Siempre **correr** tests despues de cambios, sean propios o generados con IA

---

# Para el martes

- Refactorizacion asistida por IA: que pedir, como leer el **diff**, preservar comportamiento
- Documentacion XML + README; ciclo compilar–test–documentar
- Traer dudas de `dotnet test` o del ejercicio de hoy
