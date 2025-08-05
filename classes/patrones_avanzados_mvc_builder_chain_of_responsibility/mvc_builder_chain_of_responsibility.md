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

### Patrón: **MVC (Model–View–Controller)**

**¿Qué hace?**  
Separa la lógica de negocio (modelo), la presentación (vista) y el control de flujo (controlador), facilitando el mantenimiento y la escalabilidad de aplicaciones.

**Elementos indispensables:**
- **Modelo** (por ejemplo: `ModeloProducto`) que contiene los datos y la lógica de negocio mínima
- **Vista** (`VistaProducto`) que presenta la información al usuario sin lógica de negocio
- **Controlador** (`ControladorProducto`) que coordina el modelo y la vista

**¿Cómo identificar “la forma correcta”?**
- Hay un **modelo** independiente de la vista
- La **vista** no contiene lógica de negocio, solo muestra datos
- El **controlador** recibe acciones del usuario, actualiza el modelo y ordena qué debe mostrar la vista
- Si la lógica de datos y la de presentación están mezcladas, no se está utilizando correctamente el patrón MVC

---

### Patrón: **Builder**

**¿Qué hace?**  
Encapsula la construcción de objetos complejos paso a paso, usando un `Director` que coordina a un constructor concreto para armar el producto final.

**Elementos indispensables:**
- **Producto complejo** (`ProductoComplejo`) que representa el objeto final
- **Interfaz constructor** (`IConstructorProducto`) con métodos para construir las partes
- **Constructores concretos** (como `ConstructorConMotor`, `ConstructorSinMotor`) que implementan la interfaz
- **Director** (`DirectorProducto`) que orquesta los pasos en un orden específico

**¿Cómo identificar “la forma correcta”?**
- Existe un objeto que se construye en varios pasos en lugar de un único constructor
- El **Director** llama a los métodos del constructor en un orden fijo
- El cliente solicita el producto a través del Director, sin involucrarse en los detalles de construcción
- Si el cliente arma todas las partes manualmente, no se está aplicando el patrón Builder

---

### Patrón: **Cadena de Responsabilidad (Chain of Responsibility)**

**¿Qué hace?**  
Permite pasar una petición a lo largo de una cadena de objetos manejadores hasta que alguno la procese, desacoplando al emisor del receptor.

**Elementos indispensables:**
- **Solicitud** (`Solicitud`) que encapsula la petición
- **Interfaz** (`IProcesadorSolicitud`) con métodos `SetSiguiente` y `Procesar`
- **Procesadores concretos** (`ProcesadorBasico`, `ProcesadorAvanzado`, `ProcesadorExperto`) que deciden si procesan o delegan
- Cliente que configura la cadena y envía la solicitud al primer procesador

**¿Cómo identificar “la forma correcta”?**
- Hay una **cadena** de manejadores, y cada uno decide si procesa o pasa la solicitud al siguiente
- El **cliente** desconoce cuál será el manejador final; solo inicia el proceso en la cabeza de la cadena
- Si el cliente elige explícitamente qué manejador usar, no se está aplicando correctamente este patrón

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
