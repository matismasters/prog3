# Clase de C# .NET Core 8: Patrones Strategy, Decorator y Command

## 1. Preparación del proyecto de consola en .NET 8

1. **Instalar/Verificar SDK .NET 8**

   * Desde la terminal (o PowerShell), ejecutar:

     ```bash
     dotnet --version
     ```

     Debe mostrarse algo como `8.x.x`. Si no está instalado, descargarlo desde [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/8.0).

2. **Crear la aplicación de consola**

   * En la terminal, ir a la carpeta donde se quiere crear el proyecto y ejecutar:

     ```bash
     dotnet new console -n PatronesAdicionales
     cd PatronesAdicionales
     ```
   * Esto genera un proyecto con un único archivo `Program.cs` y `<PatronesAdicionales>.csproj`.

3. **Estructura de carpetas (opcional)**
   Para organizar los archivos por patrón, puede usarse:

   ```
   PatronesAdicionales/
   ├─ Program.cs
   ├─ Strategy/
   │  ├─ IEstrategiaDescuento.cs
   │  ├─ DescuentoPorcentaje.cs
   │  ├─ DescuentoFijo.cs
   │  └─ CalculadoraVenta.cs
   ├─ Decorator/
   │  ├─ IBebida.cs
   │  ├─ CafeSimple.cs
   │  ├─ DecoradorLeche.cs
   │  └─ DecoradorAzucar.cs
   └─ Command/
      ├─ IComando.cs
      ├─ Lampara.cs
      ├─ ComandoEncenderLampara.cs
      ├─ ComandoApagarLampara.cs
      └─ ControlRemoto.cs
   ```

   Para la clase didáctica, basta con mostrar cada bloque de código en secciones separadas.

4. **Probar con `dotnet run`**

   * Cada vez que se modifique código, compilar y ejecutar:

     ```bash
     dotnet run
     ```

---

## 2. Patrón Strategy

### 2.1. Intención del patrón

* **Definir** una familia de algoritmos (estrategias) intercambiables.
* **Permitir** que el comportamiento (algoritmo) cambie en tiempo de ejecución sin modificar la clase que los usa.

### 2.2. Contexto de uso

* Cuando existe **variación** en un algoritmo (p. ej., cálculo de descuentos) y se desea evitar condicionales múltiples dentro de la misma clase.
* Para **desacoplar** la lógica de cálculo de la clase principal, facilitando la extensión de nuevos algoritmos sin modificar el código existente.

### 2.3. Elementos imprescindibles

1. **Interfaz estrategia** (`IEstrategiaDescuento`) que define el método común (`CalcularDescuento`).
2. **Clases concretas** que implementen la interfaz (`DescuentoPorcentaje`, `DescuentoFijo`).
3. **Contexto** (`CalculadoraVenta`) que mantenga una referencia a la estrategia y delegue la tarea de cálculo a esa estrategia.

> **Si falta alguno**:
>
> * Sin interfaz común, no hay contrato unificado.
> * Sin clases concretas que implementen la interfaz, no hay algoritmos intercambiables.
> * Sin contexto que use la estrategia, el patrón carece de propósito.

### 2.4. Implementación mínima en C# (.NET 8)

```csharp
// Archivo: Strategy/IEstrategiaDescuento.cs
namespace PatronesAdicionales.Strategy
{
    // 1. Interfaz que define el método de cálculo de descuento
    public interface IEstrategiaDescuento
    {
        decimal CalcularDescuento(decimal monto);
    }
}

// Archivo: Strategy/DescuentoPorcentaje.cs
namespace PatronesAdicionales.Strategy
{
    // 2. Estrategia que aplica un porcentaje de descuento
    public class DescuentoPorcentaje : IEstrategiaDescuento
    {
        private readonly decimal porcentaje; // 0.10 para 10%, 0.20 para 20%, etc.

        public DescuentoPorcentaje(decimal porcentajeDescuento)
        {
            porcentaje = porcentajeDescuento;
        }

        public decimal CalcularDescuento(decimal monto)
        {
            return monto * porcentaje;
        }
    }
}

// Archivo: Strategy/DescuentoFijo.cs
namespace PatronesAdicionales.Strategy
{
    // 2. Estrategia que aplica un monto fijo de descuento
    public class DescuentoFijo : IEstrategiaDescuento
    {
        private readonly decimal montoFijo; // p. ej. 50.00 unidades monetarias

        public DescuentoFijo(decimal montoDescuento)
        {
            montoFijo = montoDescuento;
        }

        public decimal CalcularDescuento(decimal monto)
        {
            // Retorna el monto fijo, sin importar el valor total (o mínimo 0)
            return (montoFijo > monto) ? monto : montoFijo;
        }
    }
}

// Archivo: Strategy/CalculadoraVenta.cs
namespace PatronesAdicionales.Strategy
{
    // 3. Contexto que utiliza una estrategia de descuento
    public class CalculadoraVenta
    {
        private IEstrategiaDescuento estrategiaDescuento;

        // Inyectar la estrategia al crear la calculadora
        public CalculadoraVenta(IEstrategiaDescuento estrategia)
        {
            estrategiaDescuento = estrategia;
        }

        // Permitir cambiar la estrategia en tiempo de ejecución
        public void EstablecerEstrategia(IEstrategiaDescuento nuevaEstrategia)
        {
            estrategiaDescuento = nuevaEstrategia;
        }

        // Método que calcula el total aplicando el descuento
        public decimal CalcularTotal(decimal montoOriginal)
        {
            decimal descuento = estrategiaDescuento.CalcularDescuento(montoOriginal);
            return montoOriginal - descuento;
        }
    }
}
```

### 2.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAdicionales.Strategy;

namespace PatronesAdicionales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal montoVenta = 1000m;

            // Usar estrategia de porcentaje (10%)
            IEstrategiaDescuento estrategiaPorcentaje = new DescuentoPorcentaje(0.10m);
            CalculadoraVenta calculadora = new CalculadoraVenta(estrategiaPorcentaje);
            decimal totalConPorcentaje = calculadora.CalcularTotal(montoVenta);
            System.Console.WriteLine($"Venta con 10% de descuento: {totalConPorcentaje}");

            // Cambiar en tiempo de ejecución a estrategia de descuento fijo (150 unidades)
            IEstrategiaDescuento estrategiaFijo = new DescuentoFijo(150m);
            calculadora.EstablecerEstrategia(estrategiaFijo);
            decimal totalConFijo = calculadora.CalcularTotal(montoVenta);
            System.Console.WriteLine($"Venta con 150 de descuento fijo: {totalConFijo}");

            // Output:
            // Venta con 10% de descuento: 900
            // Venta con 150 de descuento fijo: 850
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una **interfaz estrategia** (`IEstrategiaDescuento`) y varias **clases** que la implementan.
  * El **contexto** (`CalculadoraVenta`) recibe la estrategia por constructor y delega el cálculo al método `CalcularDescuento`.
  * Se puede **cambiar** la estrategia en tiempo de ejecución sin modificar el contexto.

* **Qué faltaría para NO ser Strategy**:

  * Si la calculadora contiene condicionales (`if porcentaje then ... else if fijo then ...`) en lugar de delegar a clases distintas, no es Strategy.
  * Si no hay interfaz común y se usan métodos diferentes en cada clase, no hay “contrato” unificado.

---

## 3. Patrón Decorator

### 3.1. Intención del patrón

* **Agregar** responsabilidades (funcionalidades) a un objeto de forma dinámica y transparente, sin modificar la clase original.
* Para **extender** (decorar) objetos con nuevas características sin crear subclases infinitas.

### 3.2. Contexto de uso

* Cuando se requieren **combinaciones** flexibles de funcionalidades (p. ej., agregar ingredientes a una bebida).
* Para **evitar** una jerarquía de subclases con cada combinación de características posibles.

### 3.3. Elementos imprescindibles

1. **Interfaz o clase base** (`IBebida`) que declare métodos (p. ej., `ObtenerDescripcion`, `CalcularCosto`).
2. **Clase concreta base** (`CafeSimple`) que implemente la interfaz y ofrezca funcionalidad mínima.
3. **Clase decorador abstracta** (en este ejemplo, se implementarán directamente decoradores concretos: `DecoradorLeche`, `DecoradorAzucar`) que también implemente la interfaz y mantenga una referencia al objeto original.
4. **Decoradores concretos** que añadan comportamiento antes o después de delegar al objeto decorado.

> **Si falta alguno**:
>
> * Sin interfaz común, no hay contrato para decorar.
> * Sin clase base concreta, no hay elemento al que aplicar decoradores.
> * Si el decorador no mantiene referencia al objeto original, no hay delegación y no funciona el patrón.

### 3.4. Implementación mínima en C# (.NET 8)

```csharp
// Archivo: Decorator/IBebida.cs
namespace PatronesAdicionales.Decorator
{
    // 1. Interfaz que define los métodos para una bebida
    public interface IBebida
    {
        string ObtenerDescripcion();
        decimal CalcularCosto();
    }
}

// Archivo: Decorator/CafeSimple.cs
namespace PatronesAdicionales.Decorator
{
    // 2. Bebida base: café simple
    public class CafeSimple : IBebida
    {
        public string ObtenerDescripcion()
        {
            return "Café simple";
        }

        public decimal CalcularCosto()
        {
            return 2.00m; // costo base en unidades monetarias
        }
    }
}

// Archivo: Decorator/DecoradorLeche.cs
namespace PatronesAdicionales.Decorator
{
    // 3. Decorador abstracto: en este ejemplo, implementamos directamente el decorador concreto
    public class DecoradorLeche : IBebida
    {
        private readonly IBebida bebidaOriginal;

        public DecoradorLeche(IBebida bebida)
        {
            bebidaOriginal = bebida;
        }

        public string ObtenerDescripcion()
        {
            // Añadimos descripción de leche y delegamos al objeto original
            return bebidaOriginal.ObtenerDescripcion() + " con leche";
        }

        public decimal CalcularCosto()
        {
            // Añadimos costo de la leche y delegamos al objeto original
            return bebidaOriginal.CalcularCosto() + 0.50m;
        }
    }
}

// Archivo: Decorator/DecoradorAzucar.cs
namespace PatronesAdicionales.Decorator
{
    // 4. Otro decorador que añade azúcar
    public class DecoradorAzucar : IBebida
    {
        private readonly IBebida bebidaOriginal;

        public DecoradorAzucar(IBebida bebida)
        {
            bebidaOriginal = bebida;
        }

        public string ObtenerDescripcion()
        {
            return bebidaOriginal.ObtenerDescripcion() + " con azúcar";
        }

        public decimal CalcularCosto()
        {
            return bebidaOriginal.CalcularCosto() + 0.20m;
        }
    }
}
```

### 3.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAdicionales.Decorator;

namespace PatronesAdicionales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Crear la bebida base
            IBebida miCafe = new CafeSimple();
            System.Console.WriteLine($"{miCafe.ObtenerDescripcion()} cuesta {miCafe.CalcularCosto():0.00}");

            // 2. Decorar con leche
            miCafe = new DecoradorLeche(miCafe);
            System.Console.WriteLine($"{miCafe.ObtenerDescripcion()} cuesta {miCafe.CalcularCosto():0.00}");

            // 3. Decorar con azúcar (encadenar decoradores)
            miCafe = new DecoradorAzucar(miCafe);
            System.Console.WriteLine($"{miCafe.ObtenerDescripcion()} cuesta {miCafe.CalcularCosto():0.00}");

            // Output:
            // Café simple cuesta 2.00
            // Café simple con leche cuesta 2.50
            // Café simple con leche con azúcar cuesta 2.70
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una **interfaz común** (`IBebida`) y una **clase base** (`CafeSimple`).
  * Los **decoradores** (`DecoradorLeche`, `DecoradorAzucar`) implementan la misma interfaz y **mantienen referencia** al objeto que están decorando.
  * Cada decorador **añade** funcionalidad (descripción o costo) y luego delega al objeto original.
* **Qué faltaría para NO ser Decorator**:

  * Si la clase base (`CafeSimple`) simplemente incorporara “con leche” o “con azúcar” mediante condicionales, no habría decorador; se trataría de variaciones internas.
  * Si el decorador no llama a `bebidaOriginal.ObtenerDescripcion()` o a `CalcularCosto()`, no delegaría y rompería el patrón.

---

## 4. Patrón Command

### 4.1. Intención del patrón

* **Encapsular** una petición (operación) como un objeto, separando al invocador de quien ejecuta la acción.
* **Permitir** parametrizar objetos con acciones, encolar o deshacer operaciones, y soportar acciones remotas.

### 4.2. Contexto de uso

* Cuando se desea **desacoplar** la acción (por ejemplo, encender o apagar un dispositivo) de quien la invoca (por ejemplo, un control remoto).
* Para **registrar** comandos, encolarlos o implementarlos como operaciones “revertibles” (undo/redo).

### 4.3. Elementos imprescindibles

1. **Interfaz comando** (`IComando`) que declare el método `Ejecutar()`.
2. **Clases receptoras** (p. ej., `Lampara`) que definan la lógica propia de cada acción.
3. **Clases comando concretas** (`ComandoEncenderLampara`, `ComandoApagarLampara`) que implementen `IComando` y mantengan referencia al receptor para invocar la acción.
4. **Invocador** (`ControlRemoto`) que almacene el comando y lo ejecute cuando se indique.

> **Si falta alguno**:
>
> * Sin interfaz común, no hay contrato unificado para los comandos.
> * Sin receptor (la clase que efectivamente realiza la acción), el comando no tendría a quién delegar.
> * Si el invocador no mantiene ni ejecuta el comando, no se produce la separación acción-invocador.

### 4.4. Implementación mínima en C# (.NET 8)

```csharp
// Archivo: Command/IComando.cs
namespace PatronesAdicionales.Command
{
    // 1. Interfaz que define la operación a ejecutar
    public interface IComando
    {
        void Ejecutar();
    }
}

// Archivo: Command/Lampara.cs
namespace PatronesAdicionales.Command
{
    // 2. Receptor: la lámpara que puede encenderse o apagarse
    public class Lampara
    {
        private bool encendida = false;

        public void Encender()
        {
            if (!encendida)
            {
                encendida = true;
                System.Console.WriteLine("Lámpara encendida.");
            }
            else
            {
                System.Console.WriteLine("La lámpara ya está encendida.");
            }
        }

        public void Apagar()
        {
            if (encendida)
            {
                encendida = false;
                System.Console.WriteLine("Lámpara apagada.");
            }
            else
            {
                System.Console.WriteLine("La lámpara ya está apagada.");
            }
        }
    }
}

// Archivo: Command/ComandoEncenderLampara.cs
namespace PatronesAdicionales.Command
{
    // 3. Comando concreto que encenderá la lámpara
    public class ComandoEncenderLampara : IComando
    {
        private readonly Lampara lampara;

        public ComandoEncenderLampara(Lampara lamparaReceptor)
        {
            lampara = lamparaReceptor;
        }

        public void Ejecutar()
        {
            lampara.Encender();
        }
    }
}

// Archivo: Command/ComandoApagarLampara.cs
namespace PatronesAdicionales.Command
{
    // 3. Comando concreto que apagará la lámpara
    public class ComandoApagarLampara : IComando
    {
        private readonly Lampara lampara;

        public ComandoApagarLampara(Lampara lamparaReceptor)
        {
            lampara = lamparaReceptor;
        }

        public void Ejecutar()
        {
            lampara.Apagar();
        }
    }
}

// Archivo: Command/ControlRemoto.cs
namespace PatronesAdicionales.Command
{
    // 4. Invocador que recibe un comando y lo ejecuta
    public class ControlRemoto
    {
        private IComando comando;

        public ControlRemoto(IComando comandoInicial)
        {
            comando = comandoInicial;
        }

        // Permitir cambiar el comando asignado
        public void EstablecerComando(IComando nuevoComando)
        {
            comando = nuevoComando;
        }

        // Ejecutar el comando actual
        public void PresionarBoton()
        {
            comando.Ejecutar();
        }
    }
}
```

### 4.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAdicionales.Command;

namespace PatronesAdicionales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Crear el receptor (la lámpara)
            Lampara miLampara = new Lampara();

            // 2. Crear comandos concretos
            IComando comandoEncender = new ComandoEncenderLampara(miLampara);
            IComando comandoApagar = new ComandoApagarLampara(miLampara);

            // 3. Crear el invocador (control remoto) con el comando de encender
            ControlRemoto control = new ControlRemoto(comandoEncender);

            // 4. Ejecutar acción: encender lámpara
            control.PresionarBoton(); // Output: Lámpara encendida.

            // 5. Cambiar al comando de apagar y ejecutar
            control.EstablecerComando(comandoApagar);
            control.PresionarBoton(); // Output: Lámpara apagada.

            // 6. Intentar apagar de nuevo (estado ya apagado)
            control.PresionarBoton(); // Output: La lámpara ya está apagada.

            // 7. Volver a encender
            control.EstablecerComando(comandoEncender);
            control.PresionarBoton(); // Output: Lámpara encendida.
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una **interfaz comando** (`IComando`) con un único método `Ejecutar()`.
  * Hay un **receptor** (`Lampara`) que define la lógica real de encender/apagar.
  * Existen **comandos concretos** (`ComandoEncenderLampara`, `ComandoApagarLampara`) que implementan `IComando` y **delegan** al receptor.
  * El **invocador** (`ControlRemoto`) mantiene una referencia a un `IComando` y lo ejecuta con `PresionarBoton()`.
* **Qué faltaría para NO ser Command**:

  * Si no hay interfaz unificada y los métodos de encender/apagar se llaman directamente en `Program.cs`, no hay desacoplamiento.
  * Si el invocador llama al receptor directamente (ej. `miLampara.Encender()`), no existe el patrón Command.
  * Si el comando no mantiene referencia al receptor, no puede delegar la acción.

---

## 5. Resumen y puntos clave para cada patrón

### Patrón: **Strategy**

**¿Qué hace?**  
Define una familia de algoritmos intercambiables y permite cambiar el comportamiento de una clase en tiempo de ejecución, sin modificar su código.

**Elementos indispensables:**
- Interfaz de estrategia (por ejemplo: `IEstrategiaDescuento`)
- Clases concretas que implementan la interfaz (`DescuentoPorcentaje`, `DescuentoFijo`)
- Contexto (`CalculadoraVenta`) que utiliza la estrategia

**¿Cómo identificar “la forma correcta”?**
- Existe una interfaz común con un método unificado (`CalcularDescuento`)
- Varias clases implementan esa interfaz con distintas lógicas
- El contexto recibe la estrategia (por constructor, método o seteo) y puede cambiarla dinámicamente
- No hay `if` o `switch` dentro del contexto para seleccionar qué algoritmo usar; esa responsabilidad se delega

---

### Patrón: **Decorator**

**¿Qué hace?**  
Agrega responsabilidades adicionales a un objeto de forma dinámica, sin modificar su clase original, evitando una explosión de subclases.

**Elementos indispensables:**
- Interfaz común o clase base (`IBebida`)
- Clase concreta base (`CafeSimple`)
- Decoradores (`DecoradorLeche`, `DecoradorAzucar`, etc.) que implementan la misma interfaz y envuelven el objeto original

**¿Cómo identificar “la forma correcta”?**
- Todos los elementos (base y decoradores) implementan la misma interfaz (`IBebida`)
- Los decoradores reciben un `IBebida` en su constructor y almacenan esa referencia
- Cada decorador agrega comportamiento propio y luego delega al objeto original
- Se componen objetos decorados en cadena en lugar de crear subclases específicas para cada combinación

---

### Patrón: **Command**

**¿Qué hace?**  
Encapsula una petición u operación como un objeto independiente, desacoplando el invocador del receptor que realiza la acción.

**Elementos indispensables:**
- Interfaz de comando (`IComando`) con el método `Ejecutar()`
- Receptor con la lógica real (por ejemplo: `Lampara`)
- Comandos concretos (`ComandoEncenderLampara`, `ComandoApagarLampara`) que implementan `IComando` y llaman al receptor
- Invocador (`ControlRemoto`) que almacena y ejecuta comandos

**¿Cómo identificar “la forma correcta”?**
- Hay una interfaz `IComando` con un método como `Ejecutar()`
- Los comandos encapsulan toda la lógica necesaria para ejecutar una acción sobre el receptor
- El invocador no conoce los detalles del receptor; solo ejecuta el comando
- Si el invocador llama directamente al receptor, no hay desacoplamiento y no se aplica correctamente el patrón

