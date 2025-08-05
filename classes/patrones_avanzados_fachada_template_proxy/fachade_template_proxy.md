# Clase de C# .NET Core 8: Patrones Fachada, Template y Proxy

## 2. Patrón Fachada

### 2.1. Intención del patrón

* **Simplificar** el acceso a un conjunto de subsistemas complejos agrupando sus interfaces en una clase unificada.
* **Proveer** una interfaz de nivel superior que haga más fácil la interacción con múltiples componentes.

### 2.2. Contexto de uso

* Cuando existe un sistema con varias clases complejas (sub­si­ste­mas) y se quiere ofrecer una interfaz simple para los clientes.
* Para **ocultar** la complejidad interna y reducir el acoplamiento entre el cliente y los subsistemas.

### 2.3. Elementos imprescindibles

1. **Subsistemas** independientes que ofrecen operaciones concretas.
2. **Clase Fachada** que agrupa y coordina llamadas a esos subsistemas.
3. El **cliente** utiliza únicamente la Fachada y no interactúa directamente con los subsistemas.

> **Si falta alguno**:
>
> * Sin subsistemas claramente delimitados, no hay mucha complejidad que esconder.
> * Sin la clase Fachada, el cliente tendría que invocar cada subsistema individualmente.
> * Si el cliente llama directamente a los subsistemas, no se está aplicando Fachada.

### 2.4. Ejemplo: Sistema de alarma del hogar

Imaginemos que tenemos tres subsistemas:

* Sensores de puerta/ventana (`SubSistemaSensores`)
* Control de luces exteriores (`SubSistemaLuces`)
* Alarma sonora (`SubSistemaSonido`)

La **Fachada** (`SistemaAlarma`) ofrecerá métodos sencillos como `ActivarAlarma()` y `DesactivarAlarma()`, que internamente coordinarán todos los subsistemas.

#### 2.4.1. SubSistemaSensores.cs

```csharp
namespace PatronesAvanzados.Fachada
{
    // Subsistema: gestiona sensores de puertas y ventanas
    public class SubSistemaSensores
    {
        public void ActivarSensores()
        {
            System.Console.WriteLine("SubSistemaSensores: Sensores activados.");
        }

        public void DesactivarSensores()
        {
            System.Console.WriteLine("SubSistemaSensores: Sensores desactivados.");
        }

        public bool VerificarIntrusion()
        {
            // Simulación: siempre retorna falso para demo
            return false;
        }
    }
}
```

#### 2.4.2. SubSistemaLuces.cs

```csharp
namespace PatronesAvanzados.Fachada
{
    // Subsistema: gestiona luces exteriores
    public class SubSistemaLuces
    {
        public void EncenderLuces()
        {
            System.Console.WriteLine("SubSistemaLuces: Luces exteriores encendidas.");
        }

        public void ApagarLuces()
        {
            System.Console.WriteLine("SubSistemaLuces: Luces exteriores apagadas.");
        }
    }
}
```

#### 2.4.3. SubSistemaSonido.cs

```csharp
namespace PatronesAvanzados.Fachada
{
    // Subsistema: gestiona alarma sonora
    public class SubSistemaSonido
    {
        public void ActivarSirena()
        {
            System.Console.WriteLine("SubSistemaSonido: Sirena activada.");
        }

        public void DesactivarSirena()
        {
            System.Console.WriteLine("SubSistemaSonido: Sirena desactivada.");
        }
    }
}
```

#### 2.4.4. SistemaAlarma.cs (Fachada)

```csharp
namespace PatronesAvanzados.Fachada
{
    // Fachada: unifica y simplifica la interacción con los subsistemas
    public class SistemaAlarma
    {
        private readonly SubSistemaSensores sensores;
        private readonly SubSistemaLuces luces;
        private readonly SubSistemaSonido sonido;

        public SistemaAlarma()
        {
            sensores = new SubSistemaSensores();
            luces = new SubSistemaLuces();
            sonido = new SubSistemaSonido();
        }

        // Activa todos los componentes de la alarma
        public void ActivarAlarma()
        {
            System.Console.WriteLine("Fachada: Activando sistema de alarma...");
            sensores.ActivarSensores();
            luces.EncenderLuces();
            sonido.ActivarSirena();
            System.Console.WriteLine("Fachada: Alarma activada.\n");
        }

        // Desactiva todos los componentes de la alarma
        public void DesactivarAlarma()
        {
            System.Console.WriteLine("Fachada: Desactivando sistema de alarma...");
            sonido.DesactivarSirena();
            luces.ApagarLuces();
            sensores.DesactivarSensores();
            System.Console.WriteLine("Fachada: Alarma desactivada.\n");
        }

        // Verifica el estado de los sensores y reacciona si hay intrusión
        public void Monitorear()
        {
            System.Console.WriteLine("Fachada: Verificando sensores...");
            bool intruso = sensores.VerificarIntrusion();
            if (intruso)
            {
                System.Console.WriteLine("Fachada: ¡Intrusión detectada! Activando sirena.");
                sonido.ActivarSirena();
            }
            else
            {
                System.Console.WriteLine("Fachada: No hay intrusión.\n");
            }
        }
    }
}
```

### 2.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.Fachada;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // El cliente solo interactúa con la Fachada
            SistemaAlarma alarma = new SistemaAlarma();

            // Activar alarma
            alarma.ActivarAlarma();
            // Output:
            // Fachada: Activando sistema de alarma...
            // SubSistemaSensores: Sensores activados.
            // SubSistemaLuces: Luces exteriores encendidas.
            // SubSistemaSonido: Sirena activada.
            // Fachada: Alarma activada.

            // Monitorear sin intrusión
            alarma.Monitorear();
            // Output:
            // Fachada: Verificando sensores...
            // Fachada: No hay intrusión.

            // Desactivar alarma
            alarma.DesactivarAlarma();
            // Output:
            // Fachada: Desactivando sistema de alarma...
            // SubSistemaSonido: Sirena desactivada.
            // SubSistemaLuces: Luces exteriores apagadas.
            // SubSistemaSensores: Sensores desactivados.
            // Fachada: Alarma desactivada.
        }
    }
}
```

* **Cómo identificarlo**:

  * Hay varios **subsistemas** (`SubSistemaSensores`, `SubSistemaLuces`, `SubSistemaSonido`) con responsabilidades independientes.
  * Existe una **clase Fachada** (`SistemaAlarma`) que agrupa y expone operaciones de alto nivel (`ActivarAlarma`, `DesactivarAlarma`, `Monitorear`).
  * El **cliente** solo llama a la Fachada y no debe conocer la complejidad interna.

---

## 3. Patrón Template (Template Method)

### 3.1. Intención del patrón

* **Definir** el esqueleto de un algoritmo en una clase abstracta, dejando pasos específicos a ser implementados por subclases.
* Permitir que las subclases **redefinan** ciertos pasos sin cambiar la estructura general del algoritmo.

### 3.2. Contexto de uso

* Cuando hay varias clases que comparten una secuencia de pasos similar, pero difieren en implementaciones específicas de algunos pasos.
* Para **evitar duplicar código** y garantizar que el flujo general del algoritmo se mantenga constante.

### 3.3. Elementos imprescindibles

1. **Clase abstracta** que define el método plantilla (por ejemplo, `EjecutarProceso`) con pasos concretos y algunos métodos abstractos que deberán implementarse en subclases.
2. **Subclases concretas** que extienden la clase abstracta y proporcionan la implementación de los métodos abstractos.
3. El **método plantilla** (por ejemplo, `GenerarReporte`) invoca pasos en un orden fijo y mezcla métodos concretos y abstractos.

> **Si falta alguno**:
>
> * Sin método plantilla que defina el flujo, no hay estructura común.
> * Sin métodos abstractos, las subclases no pueden variar pasos específicos.
> * Si cada clase implementa todo el algoritmo por separado, se duplica código y no es Template Method.

### 3.4. Ejemplo: Proceso para generar reportes

Imaginemos que todos los reportes deben seguir este flujo:

1. Preparar encabezado
2. Generar cuerpo específico (ventas, inventario, etc.)
3. Agregar pie de página

La **clase abstracta** `AbstractoProcesoReporte` implementa los pasos comunes y señala qué pasos deben implementarse en subclases.

#### 3.4.1. AbstractoProcesoReporte.cs

```csharp
namespace PatronesAvanzados.MetodoPlantilla
{
    // 1. Clase abstracta que define el flujo del algoritmo
    public abstract class AbstractoProcesoReporte
    {
        // Método plantilla: define el flujo general
        public void GenerarReporte()
        {
            PrepararEncabezado();
            GenerarCuerpo();
            AgregarPieDePagina();
            System.Console.WriteLine("Reporte completado.\n");
        }

        // Paso común: preparar encabezado
        private void PrepararEncabezado()
        {
            System.Console.WriteLine("Encabezado: Reporte de la compañía XYZ");
        }

        // Paso abstracto: cuerpo específico que cada subclase implementa
        protected abstract void GenerarCuerpo();

        // Paso común: pie de página
        private void AgregarPieDePagina()
        {
            System.Console.WriteLine("Pie de página: Fecha y firma de responsable.\n");
        }
    }
}
```

#### 3.4.2. ReporteVentas.cs

```csharp
namespace PatronesAvanzados.MetodoPlantilla
{
    // 2. Subclase concreta para generar reporte de ventas
    public class ReporteVentas : AbstractoProcesoReporte
    {
        protected override void GenerarCuerpo()
        {
            System.Console.WriteLine("Cuerpo (Ventas): Listado de ventas del mes:");
            System.Console.WriteLine("- Producto A: 120 unidades vendidas");
            System.Console.WriteLine("- Producto B: 75 unidades vendidas");
        }
    }
}
```

#### 3.4.3. ReporteInventario.cs

```csharp
namespace PatronesAvanzados.MetodoPlantilla
{
    // 2. Subclase concreta para generar reporte de inventario
    public class ReporteInventario : AbstractoProcesoReporte
    {
        protected override void GenerarCuerpo()
        {
            System.Console.WriteLine("Cuerpo (Inventario): Estado del inventario:");
            System.Console.WriteLine("- Producto A: 200 unidades en stock");
            System.Console.WriteLine("- Producto B: 50 unidades en stock");
        }
    }
}
```

### 3.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.MetodoPlantilla;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear reporte de ventas y generarlo
            AbstractoProcesoReporte reporteVentas = new ReporteVentas();
            System.Console.WriteLine("=== Generando Reporte de Ventas ===");
            reporteVentas.GenerarReporte();
            // Output:
            // Encabezado: Reporte de la compañía XYZ
            // Cuerpo (Ventas): Listado de ventas del mes:
            // - Producto A: 120 unidades vendidas
            // - Producto B: 75 unidades vendidas
            // Pie de página: Fecha y firma de responsable.
            // Reporte completado.

            // Crear reporte de inventario y generarlo
            AbstractoProcesoReporte reporteInventario = new ReporteInventario();
            System.Console.WriteLine("=== Generando Reporte de Inventario ===");
            reporteInventario.GenerarReporte();
            // Output:
            // Encabezado: Reporte de la compañía XYZ
            // Cuerpo (Inventario): Estado del inventario:
            // - Producto A: 200 unidades en stock
            // - Producto B: 50 unidades en stock
            // Pie de página: Fecha y firma de responsable.
            // Reporte completado.
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una **clase abstracta** (`AbstractoProcesoReporte`) que define el método plantilla (`GenerarReporte`) con pasos fijos.
  * Los pasos que varían (`GenerarCuerpo`) están marcados como `abstract` y deben implementarse en subclases.
  * El flujo general (encabezado → cuerpo → pie) nunca cambia.
* **Qué faltaría para NO ser Template Method**:

  * Si cada clase concreta repitiera todo el algoritmo en vez de usar la clase abstracta, se estaría duplicando código.
  * Si no hay un método que llame a los pasos en un orden fijo, no existe un “método plantilla”.

---

## 4. Patrón Proxy

### 4.1. Intención del patrón

* **Proporcionar** un objeto sustituto o representante de otro, controlando el acceso, añadiendo funcionalidad adicional o retardando la creación del objeto real (carga perezosa).
* **Desacoplar** al cliente del objeto real, permitiendo agregar lógica de control, acceso remoto, caché, o seguridad.

### 4.2. Contexto de uso

* Cuando la creación o acceso a un objeto real es costoso (por ejemplo, cargar imágenes grandes, conectar a un servicio remoto).
* Cuando se necesita **controlar** el acceso (p. ej., verificación de permisos) o **agregar** comportamiento adicional (registro, caché).

### 4.3. Elementos imprescindibles

1. **Interfaz común** que defina los métodos expuestos (`IRepositorioImagenes`).
2. **Clase real** (`RepositorioImagenesReal`) que implementa la interfaz y realiza la operación costosa (p. ej., cargar de disco).
3. **Proxy** (`RepositorioImagenesProxy`) que también implemente la misma interfaz, contenga una referencia al objeto real (o lo cree bajo demanda) y añada lógica extra antes/después de delegar al real.

> **Si falta alguno**:
>
> * Sin interfaz común, el cliente no puede intercambiar real y proxy.
> * Si el proxy no delega a la clase real, no cumple la función de sustituto.
> * Si el cliente invoca directamente al objeto real, no se está usando Proxy.

### 4.4. Ejemplo: Repositorio de imágenes con carga perezosa

Imaginemos que tenemos un `RepositorioImagenesReal` que carga imágenes de disco (simulado con un retraso). El `RepositorioImagenesProxy` retrasará la creación del real hasta que se necesite y mantendrá una copia en caché para no recargar repetidamente.

#### 4.4.1. IRepositorioImagenes.cs

```csharp
namespace PatronesAvanzados.Proxy
{
    // 1. Interfaz común que define operaciones para obtener imágenes
    public interface IRepositorioImagenes
    {
        string ObtenerImagen(string nombre);
    }
}
```

#### 4.4.2. RepositorioImagenesReal.cs

```csharp
using System;
using System.Collections.Generic;
using System.Threading;

namespace PatronesAvanzados.Proxy
{
    // 2. Clase real que carga imágenes (simulación de operación costosa)
    public class RepositorioImagenesReal : IRepositorioImagenes
    {
        private readonly Dictionary<string, string> almacenamientoSimulado;

        public RepositorioImagenesReal()
        {
            // Simulación de carga inicial: llenar un diccionario con nombres y rutas
            almacenamientoSimulado = new Dictionary<string, string>
            {
                { "imagen1", "/ruta/imagen1.png" },
                { "imagen2", "/ruta/imagen2.png" },
                { "imagen3", "/ruta/imagen3.png" }
            };
            System.Console.WriteLine("RepositorioImagenesReal: Inicializando y cargando recursos...");
            Thread.Sleep(1000); // Simula un retraso en la carga
        }

        public string ObtenerImagen(string nombre)
        {
            System.Console.WriteLine($"RepositorioImagenesReal: Buscando '{nombre}' en almacenamiento.");
            Thread.Sleep(500); // Simula tiempo de lectura
            if (almacenamientoSimulado.ContainsKey(nombre))
            {
                return almacenamientoSimulado[nombre];
            }
            else
            {
                return "Imagen no encontrada";
            }
        }
    }
}
```

#### 4.4.3. RepositorioImagenesProxy.cs

```csharp
using System;
using System.Collections.Generic;

namespace PatronesAvanzados.Proxy
{
    // 3. Proxy que controla el acceso al RepositorioImagenesReal
    public class RepositorioImagenesProxy : IRepositorioImagenes
    {
        private RepositorioImagenesReal? repositorioReal;            // Instancia perezosa del real
        private readonly Dictionary<string, string> cacheLocal;       // Caché para resultados anteriores

        public RepositorioImagenesProxy()
        {
            cacheLocal = new Dictionary<string, string>();
        }

        public string ObtenerImagen(string nombre)
        {
            // 1. Verificar si la imagen ya está en caché local
            if (cacheLocal.ContainsKey(nombre))
            {
                System.Console.WriteLine($"Proxy: Devolviendo '{nombre}' desde caché local.");
                return cacheLocal[nombre];
            }

            // 2. Si no existe, crear (si aún no se creó) el repositorio real
            if (repositorioReal == null)
            {
                System.Console.WriteLine("Proxy: Creando instancia de RepositorioImagenesReal...");
                repositorioReal = new RepositorioImagenesReal();
            }

            // 3. Delegar la llamada al repositorio real
            string resultado = repositorioReal.ObtenerImagen(nombre);

            // 4. Almacenar en caché si se encontró
            if (resultado != "Imagen no encontrada")
            {
                cacheLocal[nombre] = resultado;
            }

            return resultado;
        }
    }
}
```

### 4.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.Proxy;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // El cliente solo conoce la interfaz IRepositorioImagenes
            IRepositorioImagenes proxy = new RepositorioImagenesProxy();

            // 1. Obtener imagen1 (no está en caché, crea repositorio real)
            System.Console.WriteLine($"Resultado: {proxy.ObtenerImagen("imagen1")}\n");
            // Output aproximado:
            // Proxy: Creando instancia de RepositorioImagenesReal...
            // RepositorioImagenesReal: Inicializando y cargando recursos...
            // RepositorioImagenesReal: Buscando 'imagen1' en almacenamiento.
            // Resultado: /ruta/imagen1.png

            // 2. Obtener imagen1 de nuevo (ya está en caché)
            System.Console.WriteLine($"Resultado: {proxy.ObtenerImagen("imagen1")}\n");
            // Output:
            // Proxy: Devolviendo 'imagen1' desde caché local.
            // Resultado: /ruta/imagen1.png

            // 3. Obtener imagen2 (no está en caché, pero repositorioReal ya existe)
            System.Console.WriteLine($"Resultado: {proxy.ObtenerImagen("imagen2")}\n");
            // Output:
            // RepositorioImagenesReal: Buscando 'imagen2' en almacenamiento.
            // Resultado: /ruta/imagen2.png

            // 4. Obtener imagenX (no existe en almacenamiento)
            System.Console.WriteLine($"Resultado: {proxy.ObtenerImagen("imagenX")}\n");
            // Output:
            // RepositorioImagenesReal: Buscando 'imagenX' en almacenamiento.
            // Resultado: Imagen no encontrada
        }
    }
}
```

* **Cómo identificarlo**:

  * Hay una **interfaz común** (`IRepositorioImagenes`).
  * Existe la **clase real** (`RepositorioImagenesReal`) con la lógica costosa de cargar datos.
  * El **proxy** (`RepositorioImagenesProxy`) implementa la misma interfaz, crea la instancia real de forma perezosa, agrega lógica de **caché** y controla el acceso.
  * El **cliente** solo interactúa con la interfaz, sin saber si está usando el proxy o el objeto real.
* **Qué faltaría para NO ser Proxy**:

  * Si el cliente invoca directamente a `RepositorioImagenesReal`, no hay proxy.
  * Si el proxy no delega nunca al real (o no implementa la interfaz correcta), no funciona como sustituto.
  * Si no se retrasa la creación o no se agrega lógica adicional (caché o control de acceso), podría convertirse en un simple envoltorio innecesario.

---
## 5. Resumen y puntos clave para cada patrón

### Patrón: **Fachada (Facade)**

**¿Qué hace?**  
Simplifica la interacción con múltiples subsistemas complejos agrupando sus operaciones en una única interfaz unificada.

**Elementos indispensables:**
- Subsistemas independientes (por ejemplo: `SubSistemaSensores`, `SubSistemaLuces`, `SubSistemaSonido`)
- Clase Fachada (por ejemplo: `SistemaAlarma`) que coordina las llamadas entre subsistemas
- Cliente que interactúa solamente con la clase Fachada

**¿Cómo identificar “la forma correcta”?**
- Existe una clase que reúne múltiples subsistemas y ofrece métodos de alto nivel (`ActivarAlarma`, `DesactivarAlarma`)
- El cliente no necesita conocer los detalles de cada subsistema
- Si el cliente debe llamar a cada subsistema por separado, se pierde la ventaja del patrón

---

### Patrón: **Método Plantilla (Template Method)**

**¿Qué hace?**  
Define la estructura general de un algoritmo en una clase abstracta, dejando algunos pasos específicos a las subclases.

**Elementos indispensables:**
- Clase abstracta (por ejemplo: `AbstractoProcesoReporte`) con un método plantilla (`GenerarReporte`) que define el flujo
- Métodos abstractos (`GenerarCuerpo`, etc.) que serán implementados por las subclases
- Subclases concretas (como `ReporteVentas` o `ReporteInventario`) que completan los pasos específicos del algoritmo

**¿Cómo identificar “la forma correcta”?**
- Hay un método plantilla que define el orden del algoritmo (`PrepararEncabezado` → `GenerarCuerpo` → `AgregarPieDePagina`)
- Las subclases implementan únicamente los pasos variables, sin repetir el flujo general
- Si cada subclase repite todo el flujo completo, no se está utilizando correctamente este patrón

---

### Patrón: **Proxy**

**¿Qué hace?**  
Proporciona un sustituto o intermediario que controla el acceso a un objeto real, añadiendo lógica extra como caché, carga perezosa o control de acceso.

**Elementos indispensables:**
- Interfaz común (por ejemplo: `IRepositorioImagenes`)
- Clase real (`RepositorioImagenesReal`) que ejecuta operaciones costosas
- Clase proxy (`RepositorioImagenesProxy`) que implementa la misma interfaz, instancia el real bajo demanda, y agrega funcionalidad adicional como caché
- Cliente que usa únicamente la interfaz, sin saber si está usando el proxy o la clase real

**¿Cómo identificar “la forma correcta”?**
- Existe una interfaz común con métodos como `ObtenerImagen`
- El proxy implementa la misma interfaz y actúa como intermediario (por ejemplo, instancia el real al necesitarlo)
- El cliente nunca interactúa directamente con la clase real
- Si el proxy no delega correctamente o no implementa la interfaz, no es un Proxy válido


---

## 6. Conclusión y Ejercicios prácticos

1. **Identificación en el código propio**

   * ¿Ven una clase que agrupe y oculte la complejidad de varias clases? → Fachada.
   * ¿Identifican una clase abstracta que define el flujo de un algoritmo y subclases con pasos concretos? → MetodoPlantilla.
   * ¿Detectan una clase que actúa como sustituto de otra, retrasando creación o agregando caché? → Proxy.

2. **Practicar variaciones**

   * En Fachada: extender con más subsistemas (p. ej., `SubSistemaCamaras`, `SubSistemaPuertas`) y ver cómo la Fachada puede exponer métodos adicionales (p. ej., `ModoVacaciones`).
   * En MetodoPlantilla: crear un nuevo tipo de reporte (por ejemplo, `ReporteUsuarios`) implementando `GenerarCuerpo` sin modificar la clase abstracta.
   * En Proxy: implementar un proxy que controle permisos (p. ej., verificar que el usuario tenga acceso antes de delegar al real).

3. **Ejercicios**

   * Proveer un conjunto de clases complejas (p. ej., subsistemas bancarios) y pedir que creen la Fachada que unifique operaciones como `AbrirCuenta` o `CerrarCuenta`.
   * Entregar una clase abstracta incompleta sin método plantilla y solicitar que la completen para que distintas subclases la usen correctamente.
   * Dar una clase con alta latencia (p. ej., simulación de consulta a base de datos) y pedir que construyan un Proxy que implemente caché para no repetir la consulta si se solicita el mismo dato.

4. **Reflexionar sobre cuándo NO usar cada patrón**

   * **Fachada**: si los subsistemas no son complejos o no se repiten varias llamadas, podría ser innecesario añadir una capa intermedia.
   * **MetodoPlantilla**: si no hay variaciones en el flujo general del algoritmo, no es útil extraerlo en una clase abstracta.
   * **Proxy**: si el objeto real es liviano y rápido de crear, añadir un proxy solo “por si acaso” puede resultar en código innecesariamente complejo.
