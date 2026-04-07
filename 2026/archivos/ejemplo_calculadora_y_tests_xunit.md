# Ejemplo: clase `Calculadora` y tests xUnit

Material de apoyo para la **Unidad 3**. Convive con un proyecto de consola .NET 8 y un proyecto xUnit que referencia a la consola. Ajusta el **nombre del namespace** si tu solucion usa otro nombre de proyecto.

Convenciones del curso: **sin `var`**, tipos explicitos en locales.

---

## `[Fact]` y `[Theory]` en xUnit

- **`[Fact]`:** el metodo de test **no recibe parametros**. Describe **un** escenario fijo; el cuerpo del test prepara datos, actua y afirma. Es el caso base para aprender **Arrange–Act–Assert**.
- **`[Theory]`:** el metodo **si recibe parametros**. Cada fila de **`[InlineData(...)]`** encima del metodo es **una ejecucion distinta** del mismo test con otros valores. Sirve cuando muchos casos comparten la misma logica de comprobacion y solo cambian las entradas y el resultado esperado.

En la salida de `dotnet test`, cada combinacion de `[Theory]` + `[InlineData]` suele aparecer como un caso aparte, lo cual ayuda a ver **que fila fallo** si algo se rompe.

---

## `Calculadora.cs` (proyecto de consola)

```csharp
namespace MiApp.Consola;

/// <summary>
/// Operaciones aritmeticas basicas para ejemplos de clase.
/// </summary>
public static class Calculadora
{
    /// <summary>
    /// Devuelve la suma de dos enteros.
    /// </summary>
    public static int Sumar(int a, int b)
    {
        return a + b;
    }

    /// <summary>
    /// Devuelve la resta a - b.
    /// </summary>
    public static int Restar(int a, int b)
    {
        return a - b;
    }

    /// <summary>
    /// Devuelve el producto de dos enteros.
    /// </summary>
    public static int Multiplicar(int a, int b)
    {
        return a * b;
    }
}
```

---

## `CalculadoraTests.cs` (proyecto xUnit)

Referencia al proyecto de consola: `dotnet add reference ../MiApp.Consola/MiApp.Consola.csproj` (ruta segun tu carpeta).

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
    public void Sumar_ConCero_RetornaElOtroOperando()
    {
        // Arrange
        int a = 0;
        int b = 7;

        // Act
        int resultado = Calculadora.Sumar(a, b);

        // Assert
        Assert.Equal(7, resultado);
    }

    [Fact]
    public void Sumar_DosNegativos_RetornaSumaNegativa()
    {
        int resultado = Calculadora.Sumar(-4, -2);
        Assert.Equal(-6, resultado);
    }

    [Fact]
    public void Restar_MinuendoMayor_RetornaPositivo()
    {
        int resultado = Calculadora.Restar(10, 4);
        Assert.Equal(6, resultado);
    }

    [Fact]
    public void Restar_MinuendoMenor_RetornaNegativo()
    {
        int resultado = Calculadora.Restar(3, 8);
        Assert.Equal(-5, resultado);
    }

    // --- Tests con datos tabulados: una ejecucion por cada [InlineData] ---

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 7, 7)]
    [InlineData(-4, -2, -6)]
    [InlineData(100, -100, 0)]
    [InlineData(int.MaxValue, 0, int.MaxValue)]
    public void Sumar_VariosPares_RetornaSumaEsperada(int a, int b, int esperado)
    {
        // Act
        int resultado = Calculadora.Sumar(a, b);

        // Assert
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(10, 4, 6)]
    [InlineData(3, 8, -5)]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 0)]
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
    [InlineData(1, int.MaxValue, int.MaxValue)]
    public void Multiplicar_VariosPares_RetornaProductoEsperado(int a, int b, int esperado)
    {
        int resultado = Calculadora.Multiplicar(a, b);
        Assert.Equal(esperado, resultado);
    }
}
```

---

## Para charlar en clase

- **Fact:** un escenario, parametros fijos en el cuerpo del metodo; comodo para el primer ejemplo AAA en detalle.
- **Theory + InlineData:** varias filas, **misma** estructura de Act/Assert; conviene cuando agregar un caso es solo **una linea nueva** de datos.
- Comparar: cinco `[InlineData]` en `Sumar_VariosPares...` equivale a **cinco corridas** del test; si una falla, el mensaje indica **que fila** rompio.
- Nombres de test: **que metodo**, **que caso**, **que esperamos** en Facts; en Theories suele usarse un nombre mas **general** porque cubre muchas filas.
- Que faltaria si el negocio pidiera "no aceptar overflow": habria que definir comportamiento y nuevos tests; con `int` puro no se valida overflow en estos ejemplos. La fila `int.MaxValue` y `0` sirve para charlar **limites** sin cambiar el codigo de `Sumar`.
- **Multiplicar:** signos distintos dan producto negativo; dos negativos dan positivo. `1 * int.MaxValue` es seguro; pares como `int.MaxValue * 2` **desbordan** en checked o en runtime segun contexto: buen tema para "el test documenta el contrato actual", no magia.

---

## Verificacion

Desde la carpeta de la solucion:

```bash
dotnet build
dotnet test
```
