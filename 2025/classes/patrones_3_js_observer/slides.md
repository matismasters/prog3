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

