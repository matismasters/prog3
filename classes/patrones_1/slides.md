# Patrones de diseño de aplicaciones

## Introducción a los Patrones de Diseño

Patrones de diseño son soluciones reutilizables a problemas comunes en el desarrollo de software. Ayudan a mejorar la calidad del código, la mantenibilidad y la escalabilidad de las aplicaciones.

## Clases abstractas

Una clase abstracta es una clase que no se puede instanciar directamente y puede contener métodos abstractos (sin implementación) y métodos concretos (con implementación). Se utiliza para definir una interfaz común para un grupo de clases relacionadas.

## Clases abstractas vs Interfaces

Una clase abstracta puede contener implementación de métodos, mientras que una interfaz solo puede contener la firma de los métodos. Una clase puede heredar de una sola clase abstracta, pero puede implementar múltiples interfaces. Las clases abstractas son útiles cuando se necesita compartir código entre clases relacionadas, mientras que las interfaces son útiles para definir un contrato que varias clases pueden implementar.

## Tipos de Patrones de Diseño

- **Patrones Creacionales**: Se ocupan de la creación de objetos. Ejemplos: Singleton, Factory Method, Abstract Factory.
- **Patrones Estructurales**: Se ocupan de la composición de clases y objetos. Ejemplos: Adapter, Composite, Proxy.
- **Patrones Comportamentales**: Se ocupan de la interacción entre objetos. Ejemplos: Observer, Strategy, Command.
- **Patrones de Arquitectura**: Se ocupan de la estructura general de una aplicación. Ejemplos: MVC, MVVM, Microservicios.

## Explicación de cada tipo de patrón

### Patrones creacionales

Los patrones creacionales se centran en la creación de objetos, en cómo, cuándo y dónde se crean.

#### Singleton

El patrón Singleton asegura que una clase tenga una única instancia y proporciona un punto de acceso global a ella. Es útil cuando se necesita exactamente un objeto para coordinar acciones a través del sistema. Si hubieran varias instancias, podrían surgir problemas de inconsistencia de datos o comportamiento inesperado.

```csharp
public class GestorDeEventos
{
    private static GestorDeEventos instance;

    private GestorDeEventos() { }

    public static GestorDeEventos Instance
    {
        if (instance == null)
        {
            instance = new GestorDeEventos();
        }
        return instance;
    }
}
```

Ahora vamos a ir implementando y explicando paso a paso:

```csharp
public class GestorDeEventos
{
  public GestorDeEventos()
  {
  }
}
```

En este primer paso, hemos creado una clase llamada `GestorDeEventos` con un constructor público. Esto significa que cualquier parte de nuestro código puede crear una nueva instancia de `GestorDeEventos`.

```csharp
public class GestorDeEventos
{
  private static GestorDeEventos instance;

  public GestorDeEventos()
  {
  }
}
```

En este segundo paso, hemos agregado una variable estática privada llamada `instance`. Esta variable se utilizará para almacenar la única instancia de la clase `GestorDeEventos` que se creará.

```csharp
public class GestorDeEventos
{
  private static GestorDeEventos instance;

  private GestorDeEventos()
  {
  }
}
```

En este tercer paso, hemos cambiado el modificador de acceso del constructor a `private`. Esto significa que nadie puede crear una nueva instancia de `GestorDeEventos` desde fuera de la clase. Esto es fundamental para el patrón Singleton, ya que queremos asegurarnos de que solo haya una instancia de la clase.

```csharp
public class GestorDeEventos
{
  private static GestorDeEventos instance;

  private GestorDeEventos()
  {
  }

  public static GestorDeEventos Instance
  {
    if (instance == null)
    {
    instance = new GestorDeEventos();
    }
    return instance;
  }
}
```

En este cuarto paso, hemos agregado una propiedad estática llamada `Instance`. Esta propiedad es la que se utilizará para acceder a la única instancia de `GestorDeEventos`. Si `instance` es `null`, significa que aún no se ha creado una instancia, por lo que la creamos y la almacenamos en `instance`. Si ya existe una instancia, simplemente la devolvemos.

```csharp
public class GestorDeEventos
{
  private static GestorDeEventos instance;

  private GestorDeEventos()
  {  
  }

  public static GestorDeEventos Instance
  {
    if (instance == null)
    {
    instance = new GestorDeEventos();
    }
    return instance;
  }

  public void CrearEvento(string nombreEvento)
  {
    // Lógica para crear un evento
  }
}
```

En este último paso, hemos agregado un método público llamado `CrearEvento`. Este método se puede utilizar para crear un evento utilizando la única instancia de `GestorDeEventos`. La lógica para crear el evento se implementaría dentro de este método.

De esta manera, hemos implementado el patrón Singleton en la clase `GestorDeEventos`. Ahora, cualquier parte de nuestro código puede acceder a la única instancia de `GestorDeEventos` a través de `GestorDeEventos.Instance` y utilizar el método `CrearEvento` para crear eventos.

#### Factory Method

El patrón Factory Method define una interfaz para crear un objeto, pero permite a las subclases decidir qué clase instanciar. Es útil cuando no se sabe de antemano qué tipo de objeto se necesita.

```csharp
public abstract class Producto
{
    public abstract void Usar();
}

public class ProductoConcretoA : Producto
{
    public override void Usar()
    {
        Console.WriteLine("Usando Producto Concreto A");
    }
}

public class ProductoConcretoB : Producto
{
    public override void Usar()
    {
        Console.WriteLine("Usando Producto Concreto B");
    }
}

public abstract class Creador
{
    public abstract Producto CrearProducto(string tipo);
}

public class CreadorConcreto : Creador
{
    public override Producto CrearProducto(string tipo)
    {
        if (tipo == "A")
        {
            return new ProductoConcretoA();
        }
        else if (tipo == "B")
        {
            return new ProductoConcretoB();
        }
        else
        {
            throw new ArgumentException("Tipo de producto no válido");
        }
    }
}

```

En este ejemplo, tenemos una clase abstracta `Producto` y dos clases concretas `ProductoConcretoA` y `ProductoConcretoB` que heredan de `Producto`. También tenemos una clase abstracta `Creador` que define el método `CrearProducto`, y una clase concreta `CreadorConcreto` que implementa este método para crear instancias de los productos concretos.

Bien, ahora vamos a implementar el patrón Factory Method paso a paso:

```csharp
public abstract class Producto
{
  public abstract void Usar();
}
```

En este primer paso, hemos creado una clase abstracta llamada `Producto` con un método abstracto `Usar`. Este método será implementado por las clases concretas que hereden de `Producto`.

```csharp
public class ProductoConcretoA : Producto
{
  public override void Usar()
  {
    Console.WriteLine("Usando Producto Concreto A");
  }
}
```

En este segundo paso, hemos creado una clase concreta llamada `ProductoConcretoA` que hereda de `Producto`. Hemos implementado el método `Usar` para que imprima un mensaje indicando que se está usando el Producto Concreto A.

```csharp
public class ProductoConcretoB : Producto
{
  public override void Usar()
  {
    Console.WriteLine("Usando Producto Concreto B");
  }
}
```

En este tercer paso, hemos creado otra clase concreta llamada `ProductoConcretoB` que también hereda de `Producto`. Hemos implementado el método `Usar` para que imprima un mensaje indicando que se está usando el Producto Concreto B.

```csharp
public abstract class Creador
{
  public abstract Producto CrearProducto(string tipo);
}
```

En este cuarto paso, hemos creado una clase abstracta llamada `Creador` que define un método abstracto `CrearProducto`. Este método tomará un parámetro `tipo` que se utilizará para determinar qué tipo de producto crear.

```csharp
public class CreadorConcreto : Creador
{
  public override Producto CrearProducto(string tipo)
  {
    if (tipo == "A")
    {
      return new ProductoConcretoA();
    }
    else if (tipo == "B")
    {
      return new ProductoConcretoB();
    }
    else
    {
      throw new ArgumentException("Tipo de producto no válido");
    }
  }
}
```

En este quinto paso, hemos creado una clase concreta llamada `CreadorConcreto` que hereda de `Creador`. Hemos implementado el método `CrearProducto` para que cree instancias de `ProductoConcretoA` o `ProductoConcretoB` dependiendo del valor del parámetro `tipo`. Si el tipo no es válido, lanzamos una excepción.

```csharp
public class Program
{
  public static void Main(string[] args)
  {
    Creador creador = new CreadorConcreto();
    Producto productoA = creador.CrearProducto("A");
    productoA.Usar(); // Imprime "Usando Producto Concreto A"

    Producto productoB = creador.CrearProducto("B");
    productoB.Usar(); // Imprime "Usando Producto Concreto B"
  }
}
```

- Que ganamos utilizando este patrón?

Utilizando el patrón Factory Method, podemos crear productos sin especificar la clase exacta del objeto que se va a crear. Esto nos permite agregar nuevos tipos de productos sin modificar el código existente, lo que mejora la mantenibilidad y escalabilidad de nuestro sistema.

Por ejemplo, si en el futuro queremos agregar un nuevo tipo de producto, simplemente creamos una nueva clase que herede de `Producto` y actualizamos la implementación del método `CrearProducto` en `CreadorConcreto`. No necesitamos modificar el código que utiliza el creador, lo que reduce el riesgo de introducir errores.
