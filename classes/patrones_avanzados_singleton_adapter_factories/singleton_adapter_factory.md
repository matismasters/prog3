## Clase de C# .NET Core 8: Patrones Singleton, Adapter y Factory

A continuación se presenta un documento en **Markdown** para guiar una clase práctica de C# (.NET 8) usando una aplicación de consola. Se incluyen explicaciones claras, ejemplos didácticos y minimalistas, utilizando únicamente nombres en español para clases y variables.

---

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
     dotnet new console -n AplicacionPatrones
     cd AplicacionPatrones
     ```
   * Esto genera un proyecto con un único archivo `Program.cs` y un archivo de proyecto `<AplicacionPatrones>.csproj`.

3. **Estructura de carpetas (opcional)**
   Para organizar los archivos por patrón, se puede crear esta estructura:

   ```
   AplicacionPatrones/
   ├─ Program.cs
   ├─ Singleton/
   │  └─ GestorGeolocalizacion.cs
   ├─ Adapter/
   │  ├─ IEnchufeDestino.cs
   │  ├─ EnchufeTresPatillas.cs
   │  └─ AdaptadorEnchufe.cs
   └─ Factory/
      ├─ IVehiculo.cs
      ├─ Coche.cs
      ├─ Moto.cs
      └─ FabricaVehiculo.cs
   ```

   Sin embargo, para una clase didáctica basta con colocar cada bloque de código en secciones separadas.

4. **Probar con `dotnet run`**

   * Cada vez que se modifique código, compilar y ejecutar:

     ```bash
     dotnet run
     ```

---

## 2. Patrón Singleton (Biblioteca de Geolocalización)

### 2.1. Intención del patrón

* **Garantizar** que una clase encargada de geolocalización tenga una única instancia durante toda la aplicación.
* **Proveer** un punto de acceso global para obtener coordenadas o servicios de ubicación.

### 2.2. Contexto de uso

* Cuando necesitamos un único directorio o servicio de geolocalización (por ejemplo, acceso a la misma API de mapas, caché de datos de ubicación).
* Evitar crear múltiples instancias que gestionen conexiones, tokens o configuraciones de la misma fuente de geolocalización.

### 2.3. Elementos imprescindibles

1. **Constructor privado** para impedir instanciación externa.
2. **Campo estático** que almacene la referencia a la única instancia.
3. **Método público estático** que devuelva esa instancia (creándola la primera vez si no existe).
4. (Opcional) **Clase sellada** (`sealed`) para evitar herencias que pudieran generar nuevas instancias.

> **Si falta alguno de estos puntos**:
>
> * Sin constructor privado, cualquiera puede usar `new` y crear varias instancias.
> * Sin campo estático, no hay un repositorio único de la instancia.
> * Sin método público estático, no existe un punto controlado de acceso.
> * Sin `sealed`, otra clase podría heredar y evadir el control.

### 2.4. Implementación mínima en C# (.NET 8)

```csharp
// Archivo: Singleton/GestorGeolocalizacion.cs
namespace AplicacionPatrones.Singleton
{
    // 1. Clase sellada para evitar herencia
    public sealed class GestorGeolocalizacion
    {
        // 2. Campo privado y estático que guarda la única instancia
        private static GestorGeolocalizacion? unicaInstancia = null;

        // 3. Constructor privado
        private GestorGeolocalizacion()
        {
            // Inicializaciones necesarias, por ejemplo:
            // - Configurar clave de API
            // - Preparar caché de ubicaciones
        }

        // 4. Método público estático que retorna la única instancia
        public static GestorGeolocalizacion ObtenerInstancia()
        {
            if (unicaInstancia == null)
            {
                unicaInstancia = new GestorGeolocalizacion();
            }
            return unicaInstancia;
        }

        // Ejemplo de método para obtener coordenadas aproximadas de una dirección
        public (double latitud, double longitud) ObtenerCoordenadas(string direccion)
        {
            // En un escenario real se llamaría a un servicio externo,
            // pero aquí devolvemos valores simulados para la clase didáctica.
            if (direccion.ToLower().Contains("oficina"))
            {
                return (latitud: -34.90328, longitud: -56.18816);
            }
            else
            {
                return (latitud: -34.90111, longitud: -56.16453);
            }
        }
    }
}
```

**Explicación paso a paso:**

1. **`sealed class GestorGeolocalizacion`**: previene herencias que puedan crear instancias nuevas o diferentes.
2. **`private static GestorGeolocalizacion? unicaInstancia`**: campo estático que almacenará la única instancia (inicialmente `null`).
3. **`private GestorGeolocalizacion()`**: constructor privado; impide que se llame `new GestorGeolocalizacion()` desde fuera.
4. **`public static GestorGeolocalizacion ObtenerInstancia()`**: método de acceso:

   * Verifica si `unicaInstancia` es `null`.
   * Si aún no existe, crea la instancia.
   * Devuelve la referencia única.

El método `ObtenerCoordenadas` simula una llamada a un servicio de geolocalización; en la práctica, aquí se gestionaría un cliente HTTP a una API de mapas.

### 2.5. Ejemplo de uso en `Program.cs`

```csharp
using AplicacionPatrones.Singleton;

namespace AplicacionPatrones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Intentamos obtener la instancia en dos lugares distintos
            var gestor1 = GestorGeolocalizacion.ObtenerInstancia();
            var gestor2 = GestorGeolocalizacion.ObtenerInstancia();

            // Ambas referencias deben apuntar al mismo objeto
            if (ReferenceEquals(gestor1, gestor2))
            {
                System.Console.WriteLine("¡Ambas referencias apuntan al mismo Gestor de Geolocalización!");
            }

            // Usamos el único gestor para obtener coordenadas
            var coordenadasOficina = gestor1.ObtenerCoordenadas("Oficina Central");
            System.Console.WriteLine($"Oficina Central: Latitud {coordenadasOficina.latitud}, Longitud {coordenadasOficina.longitud}");

            var coordenadasCasa = gestor2.ObtenerCoordenadas("Casa del usuario");
            System.Console.WriteLine($"Casa del usuario: Latitud {coordenadasCasa.latitud}, Longitud {coordenadasCasa.longitud}");

            // Output:
            // ¡Ambas referencias apuntan al mismo Gestor de Geolocalización!
            // Oficina Central: Latitud -34.90328, Longitud -56.18816
            // Casa del usuario: Latitud -34.90111, Longitud -56.16453
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe un método estático (`ObtenerInstancia`) que siempre devuelve la misma referencia.
  * El constructor de la clase es privado.
  * No hay forma de usar `new GestorGeolocalizacion()` desde fuera.
* **Qué falta si “no es un Singleton”**:

  * Si el constructor no es privado, alguien podría usar `new GestorGeolocalizacion()`.
  * Si no hay campo estático para almacenar la instancia, cada llamado a `ObtenerInstancia` crearía un nuevo objeto.
  * Si no hubiera método estático, no existiría el punto único de acceso.

---
## 3. Patrón Adapter

### 3.1. Intención del patrón

* **Permitir** que un sistema moderno utilice componentes antiguos sin modificarlos.
* **Convertir** la interfaz de un sistema antiguo (por ejemplo, pagos con cheque) a la interfaz que espera el sistema moderno (pagos electrónicos).

### 3.2. Contexto de uso

* Cuando tienes un sistema legado (por ejemplo, procesamiento de cheques) y necesitas integrarlo con una nueva interfaz (pagos electrónicos).
* Cuando no puedes modificar el código del sistema antiguo, pero sí puedes crear un adaptador.

### 3.3. Elementos imprescindibles

1. **Interfaz destino (`IPagoElectronico`)**: define el método que espera el sistema moderno (`Pagar()`).
2. **Clase adaptada (`ProcesadorCheque`)**: representa el sistema antiguo con un método diferente (`ProcesarCheque()`).
3. **Clase adaptador (`AdaptadorCheque`)**:
   * Implementa `IPagoElectronico`.
   * Contiene internamente una referencia a `ProcesadorCheque`.
   * Traduce la llamada de `Pagar()` a `ProcesarCheque()`.

### 3.4. Implementación mínima en C# (.NET 8)

```csharp
// Interfaz moderna que espera el cliente
public interface IPagoElectronico
{
    void Pagar(decimal monto);
}

// Sistema antiguo que solo sabe procesar cheques
public class ProcesadorCheque
{
    public void ProcesarCheque(decimal cantidad)
    {
        Console.WriteLine($"Procesando cheque por ${cantidad}");
    }
}

// Adapter: permite usar ProcesadorCheque donde se espera IPagoElectronico
public class AdaptadorCheque : IPagoElectronico
{
    private readonly ProcesadorCheque _procesadorCheque;

    public AdaptadorCheque(ProcesadorCheque procesadorCheque)
    {
        _procesadorCheque = procesadorCheque;
    }

    public void Pagar(decimal monto)
    {
        // Traduce la llamada moderna a la del sistema antiguo
        _procesadorCheque.ProcesarCheque(monto);
    }
}
```

### 3.5. Ejemplo de uso en `Program.cs`

```csharp
class Program
{
    static void Main()
    {
        IPagoElectronico pago = new AdaptadorCheque(new ProcesadorCheque());
        pago.Pagar(1500); // Output: Procesando cheque por $1500
    }
}
```

**Ventajas de este ejemplo:**
- El cliente solo conoce la interfaz moderna (`IPagoElectronico`).
- El sistema antiguo (`ProcesadorCheque`) no se modifica.
- El Adapter (`AdaptadorCheque`) traduce la llamada moderna a la antigua.
- Puedes agregar otros métodos de pago modernos implementando la misma interfaz.

---

## 4. Patrón Factory (Vehículos)

### 4.1. Intención del patrón

* **Definir una interfaz** para crear objetos (en este caso, vehículos), pero dejar que la decisión de qué tipo concreto instanciar dependa de un método fábrica.
* **Desacoplar** la creación de vehículos de su uso en la aplicación.

> **Nota**: Este ejemplo muestra el **Factory Method** simple. El cliente usará la interfaz `IVehiculo` sin instanciar directamente `Coche` o `Moto`.

### 4.2. Contexto de uso

* Cuando la lógica de creación de vehículos depende de parámetros que el cliente no debe conocer.
* Cuando se quiere que el cliente utilice siempre un método fábrica, para facilitar agregar nuevos tipos de vehículos sin cambiar el código cliente.

### 4.3. Elementos imprescindibles

1. **Interfaz o clase abstracta vehículo** (`IVehiculo`), que defina la funcionalidad común (por ejemplo, `Conducir()`).
2. **Clases concretas** que implementen `IVehiculo` (`Coche`, `Moto`, …).
3. **Clase fábrica** (o método estático) que proporcione un **método fábrica** que devuelva `IVehiculo` según algún parámetro (por ejemplo, un texto o un tipo).

> **Si falta alguno**:
>
> * Sin interfaz (`IVehiculo`), el cliente conocería las clases concretas y se acoplaría a ellas.
> * Sin método estático o clase fábrica, el cliente tendría que usar `new Coche()` o `new Moto()`, rompiendo el patrón.
> * Si la fábrica no decide qué vehículo crear, el cliente seguiría instanciando directamente.

### 4.4. Implementación mínima en C# (.NET 8)

```csharp
// Archivo: Factory/IVehiculo.cs
namespace AplicacionPatrones.Factory
{
    // 1. Interfaz común que todos los vehículos implementarán
    public interface IVehiculo
    {
        void Conducir();
    }
}

// Archivo: Factory/Coche.cs
namespace AplicacionPatrones.Factory
{
    // 2. Clase concreta Coche
    public class Coche : IVehiculo
    {
        public void Conducir()
        {
            System.Console.WriteLine("Coche: conduciendo un automóvil con motor a gasolina.");
        }
    }
}

// Archivo: Factory/Moto.cs
namespace AplicacionPatrones.Factory
{
    // 2. Clase concreta Moto
    public class Moto : IVehiculo
    {
        public void Conducir()
        {
            System.Console.WriteLine("Moto: conduciendo una motocicleta ágil.");
        }
    }
}

// Archivo: Factory/FabricaVehiculo.cs
namespace AplicacionPatrones.Factory
{
    // 3. Fábrica estática que crea instancias de IVehiculo
    public static class FabricaVehiculo
    {
        // Método fábrica: recibe un texto y devuelve el vehículo adecuado
        public static IVehiculo Crear(string tipoVehiculo)
        {
            if (tipoVehiculo.ToUpper() == "COCHE")
            {
                return new Coche();
            }
            else if (tipoVehiculo.ToUpper() == "MOTO")
            {
                return new Moto();
            }
            else
            {
                throw new System.ArgumentException("Tipo de vehículo no válido");
            }
        }
    }
}
```

**Explicación paso a paso:**

1. **`IVehiculo`**: define la firma `Conducir()` que todos los vehículos deben implementar.
2. **`Coche` y `Moto`**: implementan la interfaz `IVehiculo`. Cada uno define su propia versión de `Conducir()`.
3. **`FabricaVehiculo`** (clase estática):

   * Contiene el método `Crear(string tipoVehiculo)`.
   * Según el valor de `tipoVehiculo`, devuelve `new Coche()` o `new Moto()`.
   * Si se añaden más tipos (por ejemplo, `Bicicleta`), solo hace falta modificar este método, sin tocar al cliente.

### 4.5. Ejemplo de uso en `Program.cs`

```csharp
using AplicacionPatrones.Factory;

namespace AplicacionPatrones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // El cliente solo conoce IVehiculo y la fábrica
            IVehiculo vehiculo1 = FabricaVehiculo.Crear("Coche");
            vehiculo1.Conducir();
            // Output: Coche: conduciendo un automóvil con motor a gasolina.

            IVehiculo vehiculo2 = FabricaVehiculo.Crear("Moto");
            vehiculo2.Conducir();
            // Output: Moto: conduciendo una motocicleta ágil.

            // Si en el futuro se agrega Bicicleta, solo:
            // - Crear clase Bicicleta : IVehiculo
            // - Añadir en FabricaVehiculo.Crear:
            //      else if (tipoVehiculo.ToUpper() == "BICICLETA") return new Bicicleta();
            // El cliente sigue sin cambios.
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una clase/fábrica con un **método estático** (`Crear`) que devuelve siempre la interfaz `IVehiculo`.
  * El cliente **no** utiliza `new Coche()` o `new Moto()` directamente.
  * Las clases concretas implementan la misma interfaz (`IVehiculo`) para garantizar uniformidad.
* **Qué faltaría para NO ser Factory Method**:

  * Si el cliente hace directamente `new Coche()`, no hay fábrica.
  * Si no existe una interfaz común (`IVehiculo`), el cliente tendría que conocer las clases concretas.
  * Si la fábrica no decide qué objeto crear, o si el cliente decide internamente la clase concreta, se rompe el patrón.

---

## 5. Resumen y puntos clave para cada patrón

| Patrón    | ¿Qué hace?                                                                     | Elementos indispensables | ¿Cómo identificar “la forma correcta”? |
| --------- | ------------------------------------------------------------------------------ | ------------------------ | -------------------------------------- |
| Singleton | Garantiza una única instancia global (p. ej., del servicio de geolocalización) | 1. Constructor `private` |                                        |

2. Campo estático
3. Método público estático que retorne/la cree
4. (Opcional) Clase `sealed` para evitar herencia                                                            | - Existe un método estático (`ObtenerInstancia`) que devuelve siempre la misma referencia

* No hay forma de usar `new` desde fuera
* Si falta cualquiera de estos, no se está implementando claramente el patrón                                                                                                                                                       |
  \| Adapter   | Convierte la interfaz de un enchufe de 3 patillas en la interfaz que espera una toma Schuko      | 1. Interfaz destino (`IEnchufeDestino`)

2. Clase `EnchufeTresPatillas` con método incompatible
3. Clase `AdaptadorEnchufe` que implemente `IEnchufeDestino` y contenga internamente a `EnchufeTresPatillas` | - El cliente conoce solo `IEnchufeDestino`, no “EnchufeTresPatillas”

* `AdaptadorEnchufe` implementa `IEnchufeDestino` y traduce internamente a `EnchufeTresPatillas`
* Si no hay traducción o no implementa la interfaz destino, no hay Adapter                                                                                                                     |
  \| Factory   | Encapsula la creación de vehículos, devolviendo instancias a través de un método fábrica        | 1. Interfaz (`IVehiculo`)

2. Clases concretas (`Coche`, `Moto`, …)
3. Clase fábrica con método estático (`Crear`) que devuelva `IVehiculo` según parámetro                                 | - El cliente nunca llama `new` directamente a la clase concreta

* Hay un punto único (FábricaVehiculo) que devuelve la interfaz `IVehiculo`
* Si el cliente hace `new` o no existe interfaz común, no es Factory Method                                                         