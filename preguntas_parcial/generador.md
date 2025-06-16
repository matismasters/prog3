# Generar una pregunta multiple opcion

A continuación te explico los contextos que tienes que tener en cuenta para proceder a la creación de la pregunta.

## Contexto 1: Guia de estudio de todos los temas.

==== COMIENZO DOCUMENTO 1: PATRONES DE DISEÑO GRASP ====

# Patrones de Diseño GRASP
### Una guía práctica para asignación de responsabilidades

## ¿Qué es GRASP?

- Son patrones que nos ayudan a decidir **quién** debería hacer **qué** en un sistema.
- ¿Qué clase debería tener qué responsabilidad?
- ¿Cómo distribuir las responsabilidades entre objetos?

> **Meta**: Promover un diseño de software de alta cohesión y bajo acoplamiento.


## Los 9 patrones GRASP

1. **Information Expert**
2. **Creator**
3. **Controller**
4. **Low Coupling**
5. **High Cohesion**
6. **Polymorphism**
7. **Pure Fabrication**
8. **Indirection**
9. **Protected Variations**


## 1. Information Expert

- ¿Quién tiene la información necesaria para realizar la tarea?
- **Principio clave**: Delegar la responsabilidad a quien tiene los datos.

> Ejemplo: Clase **Factura** calcula el total porque conoce sus **líneas de factura**.

ta-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 2. Creator

- ¿Quién debe crear una instancia de otra clase?
- **Regla**: La clase que contiene o usa estrechamente a otra, debe crearla.

> Ejemplo: **Pedido** crea sus propias **Líneas de Pedido**.


## 3. Controller

- ¿Quién recibe y coordina las solicitudes externas?
- **Controlador** es un intermediario entre la UI y el modelo de negocio.

> Ejemplo: **SistemaVentas** procesa la solicitud de "realizar venta".



## 4. Low Coupling

- Minimizar las dependencias entre clases.
- Facilita cambios y mantenimiento.

> Estrategias: Delegar tareas a clases con menos dependencias externas.


## 5. High Cohesion

- Mantener las clases enfocadas en una sola tarea.
- Evitar clases "Dios" que hagan de todo.

> **Pregunta**: ¿Cómo identificar una clase con baja cohesión?


## 6. Polymorphism

- Usar polimorfismo para manejar comportamientos variantes.

> Ejemplo: **Empleado** es clase base, **EmpleadoPorHora** y **EmpleadoAsalariado** implementan su propia lógica de cálculo de salario.

## 7. Pure Fabrication

- Crear una clase "ficticia" para reducir acoplamiento o aumentar cohesión.

> Ejemplo: **RepositorioDeUsuarios** para manejar la persistencia, separando lógica de negocio.


## 8. Indirection

- Introducir un intermediario para desacoplar clases.

> Ejemplo: Un **Service Layer** que actúa entre controladores y objetos de dominio.


## 9. Protected Variations

- Proteger elementos de posibles cambios o variaciones.

> Ejemplo: Usar interfaces para proteger la lógica de negocio de cambios en la base de datos.


## Relación entre patrones

- **Low Coupling** y **High Cohesion** son principios guía.
- **Information Expert** y **Controller** ayudan a asignar responsabilidades.
- **Polymorphism**, **Indirection** y **Protected Variations** manejan el cambio.

# ¡Gracias!

==== FIN DOCUMENTO 1: PATRONES DE DISEÑO GRASP ====

==== COMIENZO DOCUMENTO 2: PATRONES DE DISEÑO GRASP ====
## Patrón Observer (comportamental)

El patrón **Observer** (Observador) define una dependencia *uno-a-muchos* entre objetos de modo que, cuando uno cambia su estado, todos sus dependientes son notificados y actualizados automáticamente.  
Es ideal cuando varios componentes necesitan reaccionar a los cambios de otro sin acoplarse fuertemente a él.

---

### 1. Implementación en C# (repaso rápido)

> Identificadores en **español**; palabras reservadas en inglés.

#### Participantes  
- **ISujeto** – mantiene estado y lista de observadores.  
- **IObservador** – interfaz para recibir actualizaciones.  
- **EstacionMeteorologica / PanelDePantalla** – implementaciones concretas.

#### Código esencial

```csharp
public interface IObservador
{
    void Actualizar(float temperatura, float humedad);
}

public interface ISujeto
{
    void RegistrarObservador(IObservador observador);
    void QuitarObservador(IObservador observador);
    void NotificarObservadores();
}

public class EstacionMeteorologica : ISujeto
{
    private readonly List<IObservador> observadores = new();
    private float temperatura;
    private float humedad;

    public void EstablecerMediciones(float temperatura, float humedad)
    {
        this.temperatura = temperatura;
        this.humedad     = humedad;
        NotificarObservadores();
    }

    public void RegistrarObservador(IObservador o) => observadores.Add(o);
    public void QuitarObservador(IObservador o)    => observadores.Remove(o);

    public void NotificarObservadores()
    {
        foreach (var obs in observadores)
            obs.Actualizar(temperatura, humedad);
    }
}
```

---

## 2. Observer en JavaScript y el DOM

### 2.1 ¿Por qué los eventos del DOM son Observer?

En el **DOM**, cada nodo actúa como **Sujeto**:  
- Mantiene una lista interna de *callbacks* (observadores) registrados con `addEventListener`.  
- Cuando ocurre un evento (click, mouseover, etc.) el navegador lo “dispara” → ejecuta *todos* los observadores en orden de registro.  

Así, el mecanismo de **eventos** del DOM es una implementación nativa del patrón Observer.

---

### 2.2 Ejemplo práctico: varios observadores a un botón

#### Paso 0 – Estructura HTML

```html
<button id="botonAlerta">Haz clic aquí</button>
```

#### Paso 1 – Declarar observadores en JavaScript

```html
<script>
/* Funciones observadoras (identificadores en español) */
function manejadorSaludo() {
  console.log("¡Hola desde el manejador Saludo!");
}

function manejadorContador() {
  // Variable estática simulada con cierre (closure)
  if (!manejadorContador.contador) manejadorContador.contador = 0;
  manejadorContador.contador++;
  console.log(`El botón se pulsó ${manejadorContador.contador} veces`);
}

function manejadorFecha() {
  console.log("Fecha y hora:", new Date().toLocaleString());
}
</script>
```

#### Paso 2 – Registrar los observadores (sujeto → botón)

```html
<script>
const botonAlerta = document.getElementById("botonAlerta");

botonAlerta.addEventListener("click", manejadorSaludo);
botonAlerta.addEventListener("click", manejadorContador);
botonAlerta.addEventListener("click", manejadorFecha);
</script>
```

1. `botonAlerta` es el **Sujeto**.  
2. Con `addEventListener("click", …)` agregamos cada función a la **lista de observadores** interna.  
3. El navegador **no conoce** la lógica interna de cada observador; solo guarda referencias.

#### Paso 3 – Disparo del evento

Cuando el usuario hace clic:

1. El navegador genera un objeto `MouseEvent` y detecta que hay **3** observadores.  
2. Recorre la lista en el orden de registro:  
   1. `manejadorSaludo()` ⇒ imprime saludo.  
   2. `manejadorContador()` ⇒ actualiza y muestra contador.  
   3. `manejadorFecha()` ⇒ muestra fecha y hora.

Todos se ejecutan **sin** que el botón sepa qué hacen.

#### Paso 4 – Cancelar notificaciones (quitar observadores)

```javascript
botonAlerta.removeEventListener("click", manejadorSaludo);
```

Ahora solo quedan 2 observadores; el saludo deja de mostrarse.

---

### 2.3 Explicación paso a paso

| Fase | Sujeto (botón) | Observadores         | Acción                                                        |
|------|----------------|----------------------|----------------------------------------------------------------|
| Registro | `addEventListener` | `manejadorSaludo`, `manejadorContador`, `manejadorFecha` | El botón almacena referencias a las funciones. |
| Notificación | Usuario hace clic | Navegador recorre la lista | Ejecuta cada función con el objeto `MouseEvent`. |
| Des-registro | `removeEventListener` | `manejadorSaludo` | El botón elimina la referencia; deja de notificarle. |

---

### 2.4 Ventajas en JavaScript

- **Desacoplamiento total**: el botón no necesita conocer la lógica de cada handler.  
- **Escalabilidad**: podemos agregar/retirar observadores dinámicamente.  
- **Reutilización**: un mismo observador puede registrarse en varios sujetos (p. ej. múltiples botones).

---

### ¿Cómo mapeamos cada pieza del **Observer** entre C#, el diagrama clásico y JavaScript/DOM?

| Rol en el diagrama | C# (ejemplo con `ISujeto`/`IObservador`) | JavaScript / DOM | Qué hace exactamente |
|--------------------|------------------------------------------|------------------|----------------------|
| **Subject (Sujeto)** | `EstacionMeteorologica` (implementa `ISujeto`) | Cualquier nodo del DOM (`button`, `div`, `window`, etc.) | Mantiene la **lista de observadores** y el **estado** que puede cambiar. |
| **registerObserver()** | `RegistrarObservador(IObservador)` | **`addEventListener(tipo, callback)`** | Método público por el que los observadores se **suscriben**.<br>Guarda la referencia del callback en la lista interna. |
| **removeObserver()** | `QuitarObservador(IObservador)` | **`removeEventListener(tipo, callback)`** | Método público para **des-suscribirse**. Elimina la referencia del callback de la lista. |
| **notifyObservers()** | `NotificarObservadores()` (recorre la lista y llama `Actualizar`) | Se llama internamente cuando el navegador **dispara** el evento (`click`, `input`, etc.) | Recorre la lista y ejecuta todos los callbacks en el orden de registro, pasándoles el objeto `Event`. |
| **Observer (Observador)** | Cualquier clase que implemente `IObservador` (e.g. `PanelDePantalla`) | Cualquier **función** que uses como callback (`function manejadorSaludo(e) { … }`) | Recibe la notificación y reacciona (método `Actualizar` en C#, función de callback en JS). |

> Piensa en **`addEventListener`** como el “contrato” que expone el DOM para que tú puedas **registrarte** exactamente igual que lo harías con `RegistrarObservador` en tu clase C#.

---

#### Visualízalo paso a paso en JavaScript

```js
// 1️⃣ Sujeto
const boton = document.getElementById("miBoton");

// 2️⃣ Observadores
function observadorA(e) { console.log("A: clic"); }
function observadorB(e) { console.log("B: clic"); }

// 3️⃣ Registro  (equivale a RegistrarObservador)
boton.addEventListener("click", observadorA);
boton.addEventListener("click", observadorB);

// 4️⃣ Notificación (equivale a NotificarObservadores)
//    → La hace el navegador cuando detecta el clic.
//    Internamente recorre [observadorA, observadorB] y los ejecuta.
```

#### ¿Dónde están `IObservable<T>` e `IObserver<T>` de .NET?

- **`IObservable<T>.Subscribe(IObserver<T>)`** cumple el mismo papel que `addEventListener`:  
  registra al observador y devuelve un objeto `IDisposable` para des-suscribirse.
- **`OnNext / OnError / OnCompleted`** de `IObserver<T>` corresponden a los distintos “eventos” que el sujeto puede notificar.

Si prefieres el enfoque integrado de .NET, tu diagrama quedaría:

```
Subject (IObservable<T>)
 ├─ Subscribe()   ← equivalente a addEventListener
 │
 │─ Observers (IObserver<T>)
 │    ├─ OnNext(...)      ← lógica del observador
 │    ├─ OnError(...)
 │    └─ OnCompleted()
```

## 3. Conclusión

El concepto de Observer trasciende lenguajes:

- **C#**: lo implementamos manualmente con `ISujeto` e `IObservador`.  
- **JavaScript**: el navegador nos ofrece Observer de forma nativa en el sistema de **eventos del DOM**.

Comprender ambos enfoques ayuda a diseñar aplicaciones front-end y back-end coherentes, con **bajo acoplamiento** y **alta cohesion**.

==== FIN DOCUMENTO 2: PATRONES DE DISEÑO GRASP ====

==== COMIENZO DOCUMENTO 3: PATRONES DE DISEÑO GRASP ====

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

| Patrón  | ¿Qué hace?                                                                                                        | Elementos indispensables                                                                        | ¿Cómo identificar “la forma correcta”? |
| ------- | ----------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- | -------------------------------------- |
| Fachada | Simplifica la interacción con múltiples subsistemas complejos agrupando sus operaciones en una interfaz unificada | 1. **Subsistemas** independientes (`SubSistemaSensores`, `SubSistemaLuces`, `SubSistemaSonido`) |                                        |

2. **Clase Fachada** (`SistemaAlarma`) que coordina llamadas
3. **Cliente** solo usa la Fachada, no los subsistemas                                                                                                                                                                                                      | - Existe una clase que reúne múltiples clases complejas (subsistemas) en métodos de alto nivel (`ActivarAlarma`, `DesactivarAlarma`).

* El cliente solo interactúa con la Fachada.
* Sin la Fachada, el cliente tendría que llamar a cada subsistema por separado, aumentando el acoplamiento y la complejidad.                                                                                                                                           |
  \| MetodoPlantilla | Define la estructura general de un algoritmo en una clase abstracta, dejando pasos específicos a subclases              | 1. **Clase abstracta** (`AbstractoProcesoReporte`) con un **método plantilla** (`GenerarReporte`) que invoca pasos en orden fijo

2. **Métodos abstractos** (`GenerarCuerpo`) que las subclases implementan
3. **Subclases concretas** (`ReporteVentas`, `ReporteInventario`) que proporcionan implementaciones de los pasos específicos                                                   | - Hay una clase abstracta con un método que define el flujo general (`PrepararEncabezado` → `GenerarCuerpo` → `AgregarPieDePagina`).

* Las subclases implementan solo los pasos que varían (`GenerarCuerpo`).
* Si cada subclase repite el flujo completo, no se está usando Template Method.                                                                                                                                                                                                                        |
  \| Proxy          | Proporciona un sustituto que controla el acceso a un objeto real, añadiendo lógica adicional (como carga perezosa, caché o seguridad) | 1. **Interfaz común** (`IRepositorioImagenes`)

2. **Clase real** (`RepositorioImagenesReal`) con lógica costosa
3. **Proxy** (`RepositorioImagenesProxy`) que implementa la misma interfaz, crea el real bajo demanda, agrega caché y controla el acceso
4. **Cliente** solo usa la interfaz, sin saber si es proxy o real | - Existe una interfaz que define operaciones (`ObtenerImagen`).

* Hay una clase real con lógica costosa.
* El proxy implementa la misma interfaz, retrasa la creación del real y mantiene caché local.
* El cliente llama siempre a la interfaz.
* Si el proxy no delega o no implementa la interfaz, no cumple el patrón. |

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

==== FIN DOCUMENTO 3: PATRONES DE DISEÑO GRASP ====

==== COMIENZO DOCUMENTO 4: PATRONES DE DISEÑO GRASP ====

# Clase de C# .NET Core 8: Patrones MVC, Builder y Chain of Responsibility

## 2. Patrón MVC (Model–View–Controller)

### 2.1. Intención del patrón

* **Separar** la lógica de negocio (modelo), la presentación (vista) y el control de flujo (controlador).
* **Facilitar** el mantenimiento y la escalabilidad, evitando mezclar lógica de datos con lógica de interfaz.

### 2.2. Contexto de uso

* Muy común en aplicaciones web o con interfaz gráfica, aunque aquí lo ilustramos en consola.
* Permite cambiar vistas o modelos sin modificar el controlador, o viceversa.

### 2.3. Elementos imprescindibles

1. **Modelo**: contiene datos y lógica de negocio simple (p. ej., `ModeloProducto`).
2. **Vista**: responsable de presentar los datos al usuario (`VistaProducto`).
3. **Controlador**: recibe peticiones del usuario, actualiza el modelo y elige la vista adecuada (`ControladorProducto`).

> **Si falta alguno**:
>
> * Sin separar modelo y vista, se mezcla lógica de datos con presentación.
> * Si el controlador carece de funciones para interactuar con ambas capas, no cumple MVC.

### 2.4. Implementación mínima en C# (.NET 8)

#### 2.4.1. ModeloProducto.cs

```csharp
namespace PatronesAvanzados.MVC
{
    // 1. Modelo: datos y lógica básica de Producto
    public class ModeloProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public ModeloProducto(int id, string nombre, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }
    }
}
```

#### 2.4.2. VistaProducto.cs

```csharp
using System;

namespace PatronesAvanzados.MVC
{
    // 2. Vista: responsable de mostrar datos al usuario
    public class VistaProducto
    {
        public void MostrarDetalles(ModeloProducto producto)
        {
            Console.WriteLine("=== Detalles del Producto ===");
            Console.WriteLine($"ID: {producto.Id}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Precio: ${producto.Precio:0.00}");
            Console.WriteLine("=============================\n");
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine($"[Vista] {mensaje}\n");
        }
    }
}
```

#### 2.4.3. ControladorProducto.cs

```csharp
using System.Collections.Generic;

namespace PatronesAvanzados.MVC
{
    // 3. Controlador: coordina modelo y vista
    public class ControladorProducto
    {
        private readonly List<ModeloProducto> listaProductos;
        private readonly VistaProducto vista;

        public ControladorProducto(VistaProducto vistaInicial)
        {
            listaProductos = new List<ModeloProducto>();
            vista = vistaInicial;
        }

        // Permite agregar un producto nuevo
        public void AgregarProducto(int id, string nombre, decimal precio)
        {
            var producto = new ModeloProducto(id, nombre, precio);
            listaProductos.Add(producto);
            vista.MostrarMensaje("Producto agregado correctamente.");
        }

        // Obtiene un producto por su ID y delega a la vista para mostrarlo
        public void MostrarProducto(int id)
        {
            var producto = listaProductos.Find(p => p.Id == id);
            if (producto != null)
            {
                vista.MostrarDetalles(producto);
            }
            else
            {
                vista.MostrarMensaje("Producto no encontrado.");
            }
        }

        // Enumerar todos los productos
        public void ListarProductos()
        {
            vista.MostrarMensaje("Listado de productos:");
            foreach (var prod in listaProductos)
            {
                vista.MostrarDetalles(prod);
            }
        }
    }
}
```

### 2.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.MVC;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Crear la vista
            VistaProducto vista = new VistaProducto();

            // 2. Crear el controlador, pasando la vista al constructor
            ControladorProducto controlador = new ControladorProducto(vista);

            // 3. Simular interacciones:
            controlador.AgregarProducto(1, "Café Molido", 5.50m);
            controlador.AgregarProducto(2, "Té Verde", 3.00m);

            controlador.MostrarProducto(1); // Muestra detalles del Café Molido
            controlador.MostrarProducto(3); // Producto no encontrado

            controlador.ListarProductos(); // Muestra todos los productos agregados
        }
    }
}
```

* **Cómo identificarlo**:

  * Hay un **modelo** (`ModeloProducto`) que contiene datos sin preocuparse por cómo mostrarlos.
  * Existe una **vista** (`VistaProducto`) encargada de toda la presentación (consola, en este caso).
  * El **controlador** (`ControladorProducto`) coordina ambos: modifica o lee el modelo y pide a la vista que muestre resultados.
* **Qué faltaría para NO ser MVC**:

  * Si el modelo se imprime directamente desde `Program.cs`, sin paso intermedio por el controlador, se mezcla presentación con lógica.
  * Si la vista accede directamente al modelo para modificarlo, el controlador pierde su rol de coordinador.

---

## 3. Patrón Builder

### 3.1. Intención del patrón

* **Crear** objetos complejos paso a paso, encapsulando la construcción.
* **Permitir** variar la representación interna del objeto sin modificar el código cliente.

### 3.2. Contexto de uso

* Cuando un objeto debe construirse con diversas configuraciones posibles (p. ej., con motor o sin motor, componentes opcionales).
* Evita un constructor con demasiados parámetros o múltiples constructores con combinaciones diferentes.

### 3.3. Elementos imprescindibles

1. **Clase ProductoComplejo**: objeto final con varios atributos.
2. **Interfaz o clase abstracta Constructor** (`IConstructorProducto`) que declare métodos para construir cada parte.
3. **Constructores concretos** (`ConstructorConMotor`, `ConstructorSinMotor`) que implementen la interfaz.
4. **Director** (`DirectorProducto`) que recorra los pasos en el orden correcto, usando un `IConstructorProducto`.

> **Si falta alguno**:
>
> * Sin Director, el cliente debe orquestar manualmente los pasos, perdiendo la ventaja de Builder.
> * Si los constructores concretos no implementan la misma interfaz, no hay polimorfismo.

### 3.4. Implementación mínima en C# (.NET 8)

#### 3.4.1. ProductoComplejo.cs

```csharp
using System;
using System.Collections.Generic;

namespace PatronesAvanzados.Builder
{
    // 1. Clase que representa el producto final con componentes variados
    public class ProductoComplejo
    {
        private readonly List<string> componentes;

        public ProductoComplejo()
        {
            componentes = new List<string>();
        }

        // Agrega un componente al producto
        public void AgregarComponente(string descripcion)
        {
            componentes.Add(descripcion);
        }

        // Muestra el producto completo
        public void MostrarProducto()
        {
            Console.WriteLine("=== Configuración del Producto Complejo ===");
            foreach (var componente in componentes)
            {
                Console.WriteLine($"- {componente}");
            }
            Console.WriteLine("==========================================\n");
        }
    }
}
```

#### 3.4.2. IConstructorProducto.cs

```csharp
namespace PatronesAvanzados.Builder
{
    // 2. Interfaz que define los pasos de construcción
    public interface IConstructorProducto
    {
        void ConstruirChasis();
        void ConstruirMotor();
        void ConstruirRuedas();
        ProductoComplejo ObtenerProducto();
    }
}
```

#### 3.4.3. ConstructorConMotor.cs

```csharp
namespace PatronesAvanzados.Builder
{
    // 3. Constructor concreto que crea un producto con motor
    public class ConstructorConMotor : IConstructorProducto
    {
        private readonly ProductoComplejo producto;

        public ConstructorConMotor()
        {
            producto = new ProductoComplejo();
        }

        public void ConstruirChasis()
        {
            producto.AgregarComponente("Chasis metálico estándar");
        }

        public void ConstruirMotor()
        {
            producto.AgregarComponente("Motor V8 de alto rendimiento");
        }

        public void ConstruirRuedas()
        {
            producto.AgregarComponente("Ruedas de aleación 18 pulgadas");
        }

        public ProductoComplejo ObtenerProducto()
        {
            return producto;
        }
    }
}
```

#### 3.4.4. ConstructorSinMotor.cs

```csharp
namespace PatronesAvanzados.Builder
{
    // 3. Constructor concreto que crea un producto sin motor (p. ej., vehículo remolcado)
    public class ConstructorSinMotor : IConstructorProducto
    {
        private readonly ProductoComplejo producto;

        public ConstructorSinMotor()
        {
            producto = new ProductoComplejo();
        }

        public void ConstruirChasis()
        {
            producto.AgregarComponente("Chasis de aluminio ligero");
        }

        public void ConstruirMotor()
        {
            // No agrega motor; se omite
        }

        public void ConstruirRuedas()
        {
            producto.AgregarComponente("Ruedas reforzadas 20 pulgadas");
        }

        public ProductoComplejo ObtenerProducto()
        {
            return producto;
        }
    }
}
```

#### 3.4.5. DirectorProducto.cs

```csharp
namespace PatronesAvanzados.Builder
{
    // 4. Director que define el orden de construcción
    public class DirectorProducto
    {
        private IConstructorProducto constructor;

        public DirectorProducto(IConstructorProducto constructorInicial)
        {
            constructor = constructorInicial;
        }

        // Permite cambiar el constructor en tiempo de ejecución
        public void EstablecerConstructor(IConstructorProducto nuevoConstructor)
        {
            constructor = nuevoConstructor;
        }

        // Orquesta la construcción paso a paso
        public ProductoComplejo ConstruirProductoComplejo()
        {
            constructor.ConstruirChasis();
            constructor.ConstruirMotor();
            constructor.ConstruirRuedas();
            return constructor.ObtenerProducto();
        }
    }
}
```

### 3.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.Builder;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Construir un producto con motor
            IConstructorProducto constructorMotor = new ConstructorConMotor();
            DirectorProducto director = new DirectorProducto(constructorMotor);

            ProductoComplejo productoConMotor = director.ConstruirProductoComplejo();
            productoConMotor.MostrarProducto();
            // Output:
            // === Configuración del Producto Complejo ===
            // - Chasis metálico estándar
            // - Motor V8 de alto rendimiento
            // - Ruedas de aleación 18 pulgadas
            // ==========================================

            // 2. Construir un producto sin motor
            IConstructorProducto constructorSinMotor = new ConstructorSinMotor();
            director.EstablecerConstructor(constructorSinMotor);

            ProductoComplejo productoSinMotor = director.ConstruirProductoComplejo();
            productoSinMotor.MostrarProducto();
            // Output:
            // === Configuración del Producto Complejo ===
            // - Chasis de aluminio ligero
            // - Ruedas reforzadas 20 pulgadas
            // ==========================================
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe un **ProductoComplejo** que se arma paso a paso.
  * Hay una **interfaz constructor** (`IConstructorProducto`) con métodos definidos para cada parte.
  * Existen **constructores concretos** que implementan esa interfaz, añadiendo componentes específicos (con motor o sin motor).
  * Un **director** (`DirectorProducto`) que llama a los métodos en un orden fijo y luego recupera el producto final.
* **Qué faltaría para NO ser Builder**:

  * Si el cliente arma directamente `new ProductoComplejo()` y llama a componentes sin separar en un constructor, no se aplica el patrón.
  * Si no hay director y el cliente orquesta todos los pasos, se pierde la ventaja de encapsular la construcción.

---

## 4. Patrón CadenaResponsabilidad (Chain of Responsibility)

### 4.1. Intención del patrón

* **Pasar** una petición a lo largo de una cadena de objetos receptores hasta que uno de ellos la procese.
* **Desacoplar** emisor y receptor, permitiendo múltiples manejadores en serie sin conocimiento directo del cliente.

### 4.2. Contexto de uso

* Cuando se necesita que varias clases puedan manejar la misma petición de forma flexible (p. ej., distintos niveles de validación o procesamiento).
* Para **evitar** condicionales anidados: cada manejador decide si procesa la solicitud o la pasa al siguiente.

### 4.3. Elementos imprescindibles

1. **Clase Solicitud**: representa la petición que se encadena.
2. **Interfaz ProcesadorSolicitud** (`IProcesadorSolicitud`) que defina `SetSiguiente` y `Procesar`.
3. **Procesadores concretos** (`ProcesadorBasico`, `ProcesadorAvanzado`, `ProcesadorExperto`) que implementen la interfaz y determinen si atienden o delegan.
4. El **cliente** configura la cadena enlazando los procesadores en orden y envía la solicitud al primero.

> **Si falta alguno**:
>
> * Si no hay un método para establecer el siguiente manejador, no puede formarse la cadena.
> * Si los procesadores no intentan delegar la solicitud, no se propaga la cadena.

### 4.4. Implementación mínima en C# (.NET 8)

#### 4.4.1. Solicitud.cs

```csharp
namespace PatronesAvanzados.CadenaResponsabilidad
{
    // 1. Clase que representa la petición a procesar
    public class Solicitud
    {
        public int Nivel { get; set; }
        public string Mensaje { get; set; }

        public Solicitud(int nivel, string mensaje)
        {
            Nivel = nivel;
            Mensaje = mensaje;
        }
    }
}
```

#### 4.4.2. IProcesadorSolicitud.cs

```csharp
namespace PatronesAvanzados.CadenaResponsabilidad
{
    // 2. Interfaz que define los métodos de un procesador
    public interface IProcesadorSolicitud
    {
        void SetSiguiente(IProcesadorSolicitud siguiente);
        void Procesar(Solicitud solicitud);
    }
}
```

#### 4.4.3. ProcesadorBasico.cs

```csharp
using System;

namespace PatronesAvanzados.CadenaResponsabilidad
{
    // 3. Procesador que maneja solicitudes de nivel 1
    public class ProcesadorBasico : IProcesadorSolicitud
    {
        private IProcesadorSolicitud? siguienteProcesador;

        public void SetSiguiente(IProcesadorSolicitud siguiente)
        {
            siguienteProcesador = siguiente;
        }

        public void Procesar(Solicitud solicitud)
        {
            if (solicitud.Nivel == 1)
            {
                Console.WriteLine($"ProcesadorBasico: Procesando solicitud de nivel 1 - {solicitud.Mensaje}");
            }
            else if (siguienteProcesador != null)
            {
                Console.WriteLine("ProcesadorBasico: No es nivel 1, pasando al siguiente.");
                siguienteProcesador.Procesar(solicitud);
            }
            else
            {
                Console.WriteLine("ProcesadorBasico: No hay siguiente procesador. Solicitud no atendida.");
            }
        }
    }
}
```

#### 4.4.4. ProcesadorAvanzado.cs

```csharp
using System;

namespace PatronesAvanzados.CadenaResponsabilidad
{
    // 3. Procesador que maneja solicitudes de nivel 2
    public class ProcesadorAvanzado : IProcesadorSolicitud
    {
        private IProcesadorSolicitud? siguienteProcesador;

        public void SetSiguiente(IProcesadorSolicitud siguiente)
        {
            siguienteProcesador = siguiente;
        }

        public void Procesar(Solicitud solicitud)
        {
            if (solicitud.Nivel == 2)
            {
                Console.WriteLine($"ProcesadorAvanzado: Procesando solicitud de nivel 2 - {solicitud.Mensaje}");
            }
            else if (siguienteProcesador != null)
            {
                Console.WriteLine("ProcesadorAvanzado: No es nivel 2, pasando al siguiente.");
                siguienteProcesador.Procesar(solicitud);
            }
            else
            {
                Console.WriteLine("ProcesadorAvanzado: No hay siguiente procesador. Solicitud no atendida.");
            }
        }
    }
}
```

#### 4.4.5. ProcesadorExperto.cs

```csharp
using System;

namespace PatronesAvanzados.CadenaResponsabilidad
{
    // 3. Procesador que maneja solicitudes de nivel 3 (experto)
    public class ProcesadorExperto : IProcesadorSolicitud
    {
        private IProcesadorSolicitud? siguienteProcesador;

        public void SetSiguiente(IProcesadorSolicitud siguiente)
        {
            siguienteProcesador = siguiente;
        }

        public void Procesar(Solicitud solicitud)
        {
            if (solicitud.Nivel == 3)
            {
                Console.WriteLine($"ProcesadorExperto: Procesando solicitud de nivel 3 - {solicitud.Mensaje}");
            }
            else if (siguienteProcesador != null)
            {
                Console.WriteLine("ProcesadorExperto: No es nivel 3, pasando al siguiente.");
                siguienteProcesador.Procesar(solicitud);
            }
            else
            {
                Console.WriteLine("ProcesadorExperto: No hay siguiente procesador. Solicitud no atendida.");
            }
        }
    }
}
```

### 4.5. Ejemplo de uso en `Program.cs`

```csharp
using PatronesAvanzados.CadenaResponsabilidad;

namespace PatronesAvanzados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Instanciar procesadores
            var basico = new ProcesadorBasico();
            var avanzado = new ProcesadorAvanzado();
            var experto = new ProcesadorExperto();

            // 2. Configurar la cadena: básico → avanzado → experto
            basico.SetSiguiente(avanzado);
            avanzado.SetSiguiente(experto);

            // 3. Crear distintas solicitudes y enviarlas al primer procesador
            var solicitud1 = new Solicitud(1, "Consulta al nivel básico");
            var solicitud2 = new Solicitud(2, "Requisito para nivel avanzado");
            var solicitud3 = new Solicitud(3, "Petición de nivel experto");
            var solicitud4 = new Solicitud(4, "Solicitud de nivel desconocido");

            basico.Procesar(solicitud1);
            // Output:
            // ProcesadorBasico: Procesando solicitud de nivel 1 - Consulta al nivel básico

            basico.Procesar(solicitud2);
            // Output:
            // ProcesadorBasico: No es nivel 1, pasando al siguiente.
            // ProcesadorAvanzado: Procesando solicitud de nivel 2 - Requisito para nivel avanzado

            basico.Procesar(solicitud3);
            // Output:
            // ProcesadorBasico: No es nivel 1, pasando al siguiente.
            // ProcesadorAvanzado: No es nivel 2, pasando al siguiente.
            // ProcesadorExperto: Procesando solicitud de nivel 3 - Petición de nivel experto

            basico.Procesar(solicitud4);
            // Output:
            // ProcesadorBasico: No es nivel 1, pasando al siguiente.
            // ProcesadorAvanzado: No es nivel 2, pasando al siguiente.
            // ProcesadorExperto: No es nivel 3, pasando al siguiente.
            // ProcesadorExperto: No hay siguiente procesador. Solicitud no atendida.
        }
    }
}
```

* **Cómo identificarlo**:

  * Existe una **interfaz común** (`IProcesadorSolicitud`) con `SetSiguiente` y `Procesar`.
  * Hay varios **procesadores concretos** (`ProcesadorBasico`, `ProcesadorAvanzado`, `ProcesadorExperto`) que intentan manejar la solicitud o delegar al siguiente.
  * El **cliente** configura la cadena y envía la petición al primero, sin conocer qué manejador la procesará al final.
* **Qué faltaría para NO ser CadenaResponsabilidad**:

  * Si el cliente decide internamente qué procesador invocar, no se propaga la petición en cadena.
  * Si no existe un método para enlazar al “siguiente” procesador, no hay cadena dinámica.

---

## 5. Resumen y puntos clave para cada patrón

| Patrón                      | ¿Qué hace?                                                                                                                                    | Elementos indispensables                                                        | ¿Cómo identificar “la forma correcta”? |
| --------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | -------------------------------------- |
| MVC (Model–View–Controller) | Separa la lógica de negocio (modelo), la presentación (vista) y el control de flujo (controlador), facilitando mantenimiento y escalabilidad. | 1. **Modelo** (`ModeloProducto`) que contiene datos y lógica de negocio mínima. |                                        |

2. **Vista** (`VistaProducto`) que presenta información al usuario.
3. **Controlador** (`ControladorProducto`) que coordina modelo y vista.                                                                                                                           | - Hay un **modelo** independiente de la vista.

* Existe una **vista** que no contiene lógica de negocio.
* El **controlador** recibe comandos del cliente, actualiza el modelo y ordena a la vista qué mostrar.
* Si la lógica de datos y la de presentación están mezcladas, no es MVC.                                                                                                                                                                |
  \| Builder                  | Encapsula la construcción de objetos complejos paso a paso, mediante un `Director` que coordina un `Constructor` concreto para armar el producto final. | 1. **Clase ProductoComplejo** que representa el objeto final.

2. **Interfaz constructor** (`IConstructorProducto`) con métodos de construcción.
3. **Constructores concretos** (`ConstructorConMotor`, `ConstructorSinMotor`) que implementan la interfaz.
4. **Director** (`DirectorProducto`) que orquesta los pasos en orden fijo.                                         | - Existe un objeto que se construye en varios pasos en lugar de un solo constructor.

* Hay un **Director** que llama a los métodos del constructor en un orden específico.
* El cliente solicita el producto a través del Director, sin conocer detalles internos.
* Si el cliente arma todas las partes manualmente, no se está usando Builder.                                                                                                                                                                 |
  \| CadenaResponsabilidad    | Pasa una petición a lo largo de una cadena de objetos manejadores hasta que alguno la procese, desacoplando emisor y receptor.                        | 1. **Clase Solicitud** que envuelve la petición.

2. **Interfaz** (`IProcesadorSolicitud`) que define `SetSiguiente` y `Procesar`.
3. **Procesadores concretos** (`ProcesadorBasico`, `ProcesadorAvanzado`, `ProcesadorExperto`) que deciden si procesan o delegan.
4. Cliente configura la cadena y envía la solicitud al primer procesador. | - Existe una **cadena** de manejadores en la que cada uno decide si atiende la solicitud o la pasa al siguiente.

* El **cliente** desconoce cuál será el manejador final; sólo invoca al inicio de la cadena.
* Si el cliente decide directamente el manejador, no es Cadena de Responsabilidad.                                                                                                                                                          |

---

## 6. Conclusión y ejercicios prácticos

1. **Identificación en el código propio**

   * En MVC: buscar clases claramente divididas en modelo, vista y controlador, sin mezclar responsabilidades.
   * En Builder: identificar cuándo un objeto requiere ensamblarse con varios pasos en lugar de un solo constructor.
   * En CadenaResponsabilidad: detectar escenarios donde múltiples clases puedan procesar o delegar una petición sin que el cliente sepa cuál la atenderá.

2. **Practicar variaciones**

   * **MVC**: extender con un repositorio de datos simulado (p. ej., `RepositorioProductos`) para separar aún más la lógica de acceso a datos.
   * **Builder**: crear otro constructor concreto que añada componentes opcionales extras (p. ej., `ConstructorConExtras`) y ver el resultado.
   * **CadenaResponsabilidad**: agregar un `ProcesadorSupervisor` con nivel 4 para demostrar cómo extender la cadena sin cambiar el cliente.

3. **Ejercicios**

   * Dar un ejemplo de código donde la lógica de UI y la de negocio estén mezcladas y pedir que lo refactoricen siguiendo MVC.
   * Proporcionar un objeto con muchos parámetros opcionales y solicitar implantar Builder para limpiar la creación.
   * Presentar múltiples condiciones anidadas (if–else) y pedir que lo refactoricen usando CadenaResponsabilidad.

4. **Reflexionar sobre cuándo NO usar cada patrón**

   * **MVC**: si la aplicación es muy simple (p. ej., un script que solo imprime un mensaje), puede resultar excesivo separar en tres capas.
   * **Builder**: si el objeto es sencillo y no tiene variaciones en su construcción, no vale la pena añadir infraestructura extra.
   * **CadenaResponsabilidad**: si solo existe un posible manejador para la petición, no tiene sentido pasar por varios objetos en cadena.

==== FIN DOCUMENTO 4: PATRONES DE DISEÑO GRASP ====

==== COMIENZO DOCUMENTO 5: PATRONES DE DISEÑO GRASP ====

# Clase de C# .NET Core 8: Patrones Singleton, Adapter y Factory

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

* **Garantizar** que una clase tenga una única instancia durante toda la aplicación.
* **Proveer** un punto de acceso global esta clase y sus funciones.

### 2.2. Contexto de uso

* Cuando necesitamos un único directorio, configuracion global o servicio de geolocalización (por ejemplo, acceso a la misma API de mapas, caché de datos de ubicación).
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

### 3.6. Otro ejemplo de uso

```csharp
// Interfaz moderna que espera el cliente
public interface INotificacionElectronica
{
    void Enviar(string destinatario, string mensaje);
}

// Sistema antiguo que solo sabe enviar SMS
public class ServicioSmsAntiguo
{
    public void EnviarSms(string numero, string texto)
    {
        Console.WriteLine($"Enviando SMS a {numero}: {texto}");
    }
}

// Adapter: permite usar ServicioSmsAntiguo donde se espera INotificacionElectronica
public class AdaptadorSms : INotificacionElectronica
{
    private readonly ServicioSmsAntiguo _servicioSms;

    public AdaptadorSms(ServicioSmsAntiguo servicioSms)
    {
        _servicioSms = servicioSms;
    }

    public void Enviar(string destinatario, string mensaje)
    {
        // Traduce la llamada moderna a la del sistema antiguo
        _servicioSms.EnviarSms(destinatario, mensaje);
    }
}

// Uso en el cliente
class Program
{
    static void Main()
    {
        INotificacionElectronica notificacion = new AdaptadorSms(new ServicioSmsAntiguo());
        notificacion.Enviar("+59812345678", "¡Hola! Este es un mensaje de prueba.");
        // Output: Enviando SMS a +59812345678: ¡Hola! Este es un mensaje de prueba.
    }
}
```

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

==== FIN DOCUMENTO 5: PATRONES DE DISEÑO GRASP ====

==== COMIENZO DOCUMENTO 6: PATRONES DE DISEÑO GRASP ====

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

| Patrón   | ¿Qué hace?                                                                                        | Elementos indispensables                        | ¿Cómo identificar “la forma correcta”? |
| -------- | ------------------------------------------------------------------------------------------------- | ----------------------------------------------- | -------------------------------------- |
| Strategy | Define una familia de algoritmos intercambiables y permite cambiar el comportamiento en ejecución | 1. Interfaz estrategia (`IEstrategiaDescuento`) |                                        |

2. Clases concretas que implementan la interfaz (`DescuentoPorcentaje`, `DescuentoFijo`)
3. Contexto (`CalculadoraVenta`) que usa la estrategia                     | - Existe una interfaz común con un método unificado (`CalcularDescuento`).

* Varias clases concretas implementan esa interfaz.
* El contexto recibe la estrategia por constructor o la cambia en ejecución.
* No hay condicionales para elegir el algoritmo dentro de la clase contexto.                                                                                                                                             |
  \| Decorator | Agrega responsabilidades a un objeto de forma dinámica, sin modificar la clase original       | 1. Interfaz o clase base (`IBebida`)

2. Clase base concreta (`CafeSimple`)
3. Decoradores (`DecoradorLeche`, `DecoradorAzucar`) que implementan la misma interfaz y guardan referencia al objeto original | - Existe una interfaz común (`IBebida`).

* Hay una clase base simple (`CafeSimple`) y decoradores que reciben un `IBebida` en su constructor.
* Cada decorador añade comportamiento (p. ej., descripción o costo) y luego delega al objeto original.
* No se crean subclases explosivas para cada combinación, sino objetos decorados en cadena.                                                                                                        |
  \| Command   | Encapsula una petición (operación) como un objeto, desacoplando invocador de receptor          | 1. Interfaz comando (`IComando`)

2. Receptor que contiene la lógica real (`Lampara`)
3. Comandos concretos (`ComandoEncenderLampara`, `ComandoApagarLampara`) que implementan `IComando` y delegan al receptor
4. Invocador (`ControlRemoto`) que tiene un `IComando` y ejecuta `Ejecutar()` | - Existe una interfaz comando con método `Ejecutar()`.

* Hay clases receptoras que definen las acciones concretas.
* Los comandos concretos encapsulan la llamada al receptor.
* El invocador no conoce la lógica interna del receptor; solo llama a `Ejecutar()` del comando.
* Si el invocador llama directamente al receptor, no hay desacoplamiento y no se aplica el patrón Command.                                                                                      |

==== FIN DOCUMENTO 6: PATRONES DE DISEÑO GRASP ====

## Tema especifico de la pregunta

La pregunta debe ser sobre el patron Singleton. Es importante explorar preguntas que dejen claro que el alumno sabe diferenciarlo, entiende su proposito, ha comprendido su funcionamiento

## Pedido especifico

Tomando en cuenta todo el contexto anterior, y el tema especifico de la pregunta a desarrollar, tienes que crear una pregunta multiple opcion, y anotar la pregunta correcta, acorde al tema especifico y el contexto.

## Output esperado

Debes devolver tu pregunta en formato JSON, siguiendo el siguiente esquema:
```
{
  "pregunta": "<AQUI VA EL TEXTO DE LA PREGUNTA EN FORMATO MARKDOWN>",
  "respuestas": [
    { "porcentajeCorrecto": <NUMERO ENTERO ENTRE 1 y 100>, "textoRespuesta": "<TEXTO DE LA RESPUESTA EN FORMATO MARKDOWN>"},
    ...<2 a 5 mas posibles respuestas>
  ]
}
```