# Patrones de Diseño de Aplicaciones  

## Patrón Observer (comportamental)

El patrón **Observer** (Observador) define una dependencia *uno-a-muchos* entre objetos de modo que, cuando uno cambia su estado, todos sus dependientes son notificados y actualizados automáticamente.  
Es ideal cuando varios componentes necesitan reaccionar a los cambios de otro sin acoplarse fuertemente a él.

### Participantes  
- **ISujeto**: mantiene el estado y una lista de observadores.  
- **IObservador**: define la interfaz para recibir actualizaciones.  
- **EstacionMeteorologica / PanelDePantalla**: implementaciones concretas.

### Ventajas  
- **Desacopla** emisor y receptores.  
- Facilita agregar nuevos observadores sin tocar el sujeto.  
- Permite **event-driven** y **pub/sub** internos sin frameworks externos.

---

### Implementación paso a paso en C# (identificadores en español)

#### Paso 1 – Interfaz `IObservador`

```csharp
public interface IObservador
{
    void Actualizar(float temperatura, float humedad);
}
```

#### Paso 2 – Interfaz `ISujeto`

```csharp
public interface ISujeto
{
    void RegistrarObservador(IObservador observador);
    void QuitarObservador(IObservador observador);
    void NotificarObservadores();
}
```

#### Paso 3 – `EstacionMeteorologica` (ConcreteSubject)

```csharp
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

    public void RegistrarObservador(IObservador observador) => observadores.Add(observador);
    public void QuitarObservador(IObservador observador)   => observadores.Remove(observador);

    public void NotificarObservadores()
    {
        foreach (var obs in observadores)
            obs.Actualizar(temperatura, humedad);
    }
}
```

#### Paso 4 – Observador `PanelDePantalla`

```csharp
public class PanelDePantalla : IObservador
{
    public void Actualizar(float temperatura, float humedad)
    {
        Console.WriteLine($"Pantalla → Temp: {temperatura}°C – Humedad: {humedad}%");
    }
}
```

#### Paso 5 – Uso del patrón

```csharp
var estacion = new EstacionMeteorologica();
var panel    = new PanelDePantalla();

estacion.RegistrarObservador(panel);

estacion.EstablecerMediciones(22.5f, 65f);  // notifica automáticamente
```

Cualquier otro observador (logger, alarma, etc.) puede registrarse sin modificar la estación.

---

## Patrón Adapter (estructural)

El patrón **Adapter** permite que clases con interfaces incompatibles trabajen juntas envolviendo (“adaptando”) una clase existente en la interfaz que el cliente espera.

### Casos de uso típicos  
- Integrar **librerías legacy** con código nuevo.  
- Permitir **sustitución** de dependencias sin reescribir clientes.  
- Unificar APIs heterogéneas (distintos proveedores).

---

### Implementación paso a paso (Object Adapter, identificadores en español)

Supongamos esta clase de una librería externa:

```csharp
public class RegistroCsv       // librería externa
{
    public void EscribirLinea(string linea) =>
        File.AppendAllText("log.csv", linea + "\n");
}
```

Nuestro sistema espera esta interfaz:

```csharp
public interface IRegistrador   // interfaz objetivo
{
    void Registrar(string mensaje);
}
```

#### Paso 1 – Crear el `AdaptadorRegistroCsv`

```csharp
public class AdaptadorRegistroCsv : IRegistrador
{
    private readonly RegistroCsv registroCsv;

    public AdaptadorRegistroCsv(RegistroCsv registroCsv) =>
        this.registroCsv = registroCsv;

    public void Registrar(string mensaje)
    {
        var linea = $"{DateTime.Now:O};{mensaje}";
        registroCsv.EscribirLinea(linea);
    }
}
```

#### Paso 2 – Uso en el cliente

```csharp
IRegistrador registrador = new AdaptadorRegistroCsv(new RegistroCsv());

registrador.Registrar("Usuario Juan inició sesión");
registrador.Registrar("Usuario Juan cerró sesión");
```

El cliente interactúa con `IRegistrador` sin saber nada de la clase externa.

### Beneficios  
- **Principio abierto/cerrado**: adaptamos sin modificar código existente.  
- Evitamos heredar de una clase que **no controlamos**.  
- Podemos crear varios adapters para la misma interfaz (ej.: `AdaptadorRegistroJson`, `AdaptadorRegistroConsola`) y seleccionarlos en tiempo de ejecución.

---

## Conclusión

| Patrón | Categoría | Problema que resuelve | Beneficio clave |
|--------|-----------|-----------------------|-----------------|
| Observer | Comportamental | Mantener múltiples objetos sincronizados con los cambios de otro | Desacopla emisor y receptores |
| Adapter  | Estructural    | Compatibilizar clases con interfaces incompatibles | Reutilizar código existente sin modificarlo |
