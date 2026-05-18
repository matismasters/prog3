# Encapsulación

La **encapsulación** en programación es como guardar algo importante dentro de una caja, y decidir **qué cosas se pueden tocar desde afuera** y **qué cosas no**.

Imaginá una **máquina expendedora**.

Vos, desde afuera, podés:

* poner monedas
* elegir un producto
* recibir el producto

Pero no podés meter la mano adentro y cambiar los cables, sacar productos directamente o modificar el dinero guardado.

Eso es encapsulación.

La máquina tiene partes internas que están protegidas, pero te da botones para usarla correctamente.

En programación pasa lo mismo.

Un objeto puede tener datos internos, pero no queremos que cualquier parte del programa los cambie de cualquier manera.

Por ejemplo, una cuenta bancaria:

```csharp
public class CuentaBancaria
{
    private decimal saldo;

    public void Depositar(decimal monto)
    {
        if (monto > 0)
        {
            saldo += monto;
        }
    }

    public void Retirar(decimal monto)
    {
        if (monto > 0 && monto <= saldo)
        {
            saldo -= monto;
        }
    }

    public decimal ObtenerSaldo()
    {
        return saldo;
    }
}
```

Acá `saldo` es `private`, o sea que está protegido. Nadie puede hacer esto desde afuera:

```csharp
cuenta.saldo = -1000000;
```

Eso sería peligroso.

En cambio, para cambiar el saldo hay que usar los métodos:

```csharp
cuenta.Depositar(500);
cuenta.Retirar(200);
```

Entonces la clase controla sus propias reglas.

La idea principal es:

> **Encapsular es proteger los datos internos de un objeto y permitir que se usen solo mediante métodos seguros.**

Como decir:

> “No toques mis cosas directamente. Pedime lo que querés hacer, y yo veo si está permitido.”

## Niveles de accesibilidad

En C# esto normalmente se llama **modificadores de acceso** o **niveles de accesibilidad**. Sirven para decir **quién puede usar una clase, propiedad, método o variable**. Microsoft lista los niveles principales como `public`, `protected`, `internal`, `protected internal`, `private` y `private protected`; además existe `file` para tipos visibles solo dentro de un archivo fuente. ([Microsoft Learn][1])

Pensalo como una escuela:

## 1. `public`

**Lo puede usar cualquiera.**

Es como una puerta abierta al público.

```csharp
public class Persona
{
    public string Nombre;
}
```

Desde cualquier parte del programa puedo hacer:

```csharp
Persona p = new Persona();
p.Nombre = "Ana";
```

Usalo cuando algo forma parte de la “cara visible” de tu clase.

---

## 2. `private`

**Solo se puede usar dentro de la misma clase.**

Es como un cajón con llave dentro de tu cuarto. Nadie de afuera lo toca directamente.

```csharp
public class CuentaBancaria
{
    private decimal saldo;

    public void Depositar(decimal monto)
    {
        saldo += monto;
    }
}
```

Desde afuera no puedo hacer:

```csharp
cuenta.saldo = 999999;
```

Eso falla porque `saldo` es privado.

Este es el más importante para **encapsulación**: protegés los datos internos y obligás a usar métodos seguros.

---

## 3. `protected`

**Lo puede usar la clase y también sus clases hijas.**

Es como decir: “esto es privado para el mundo, pero mis hijos lo pueden usar”.

```csharp
public class Animal
{
    protected int energia;
}

public class Perro : Animal
{
    public void Correr()
    {
        energia -= 10;
    }
}
```

`Perro` puede usar `energia` porque hereda de `Animal`.

Pero alguien de afuera no puede hacer:

```csharp
perro.energia = 100;
```

`protected` sirve mucho cuando trabajás con **herencia**.

---

## 4. `internal`

**Lo puede usar cualquier código dentro del mismo proyecto/assembly.**

Un **assembly** es, simplificando, el `.dll` o `.exe` que se genera al compilar tu proyecto.

```csharp
internal class CalculadoraInterna
{
    public int Sumar(int a, int b)
    {
        return a + b;
    }
}
```

Esto significa:

> “Esta clase se puede usar dentro de este proyecto, pero no desde otros proyectos externos.”

Ejemplo típico: clases auxiliares que necesitás dentro de tu aplicación, pero que no querés exponer como API pública.

Según la documentación, `internal` limita el acceso al assembly actual. ([Microsoft Learn][2])

---

## 5. `protected internal`

Este es una combinación un poco más rara.

Significa:

**Lo puede usar cualquier código del mismo proyecto, o una clase hija aunque esté en otro proyecto.**

La clave es el **o**.

```csharp
public class Vehiculo
{
    protected internal int velocidad;
}
```

Pueden acceder:

1. clases del mismo proyecto;
2. clases hijas de `Vehiculo`, incluso si están en otro assembly.

Es más abierto que `protected` y más abierto que `internal` en ciertos casos.

La idea sería:

> “Lo puede usar mi familia de herencia, o cualquiera dentro de mi casa/proyecto.”

---

## 6. `private protected`

Este también combina ideas, pero es más restrictivo.

Significa:

**Lo puede usar la clase y sus clases hijas, pero solo si están dentro del mismo proyecto/assembly.**

La clave es el **y**.

```csharp
public class Documento
{
    private protected string codigoInterno;
}

public class Factura : Documento
{
    public void Procesar()
    {
        codigoInterno = "ABC";
    }
}
```

`Factura` puede usar `codigoInterno` porque:

1. hereda de `Documento`;
2. está en el mismo proyecto/assembly.

Si una clase hija estuviera en otro proyecto, ya no podría acceder.

Microsoft lo define como acceso limitado a la clase contenedora o tipos derivados dentro del assembly actual. ([Microsoft Learn][2])

---

## 7. `file`

`file` es especial.

**Hace que un tipo solo exista para el archivo `.cs` actual.**

```csharp
file class AyudanteDelArchivo
{
    public void HacerAlgo()
    {
    }
}
```

Esto significa:

> “Esta clase solo se puede usar dentro de este archivo.”

No se usa tanto en cursos iniciales. Es más común en casos avanzados, por ejemplo con generadores de código. La documentación aclara que `file` se aplica a tipos de nivel superior, restringe el acceso al mismo archivo fuente y no se puede combinar con otros modificadores de acceso. ([Microsoft Learn][1])

---

# Resumen rápido

| Modificador          | Quién puede acceder                               |
| -------------------- | ------------------------------------------------- |
| `public`             | Cualquiera                                        |
| `private`            | Solo la misma clase                               |
| `protected`          | La misma clase y sus hijas                        |
| `internal`           | Cualquier código del mismo proyecto/assembly      |
| `protected internal` | Mismo proyecto **o** clases hijas                 |
| `private protected`  | Clases hijas, pero solo dentro del mismo proyecto |
| `file`               | Solo el mismo archivo `.cs`                       |

---

# Ejemplo todo junto

```csharp
public class Personaje
{
    public string Nombre;              // Todos pueden verlo
    private int vida;                  // Solo Personaje puede tocarlo
    protected int energia;             // Personaje y sus hijos
    internal int idInterno;            // Todo el proyecto
    protected internal int nivel;      // Proyecto o hijos
    private protected int secreto;     // Hijos, pero del mismo proyecto

    public void RecibirDanio(int cantidad)
    {
        if (cantidad > 0)
        {
            vida -= cantidad;
        }
    }
}
```

La idea importante es esta:

> **Cuanto menos acceso das, más control tenés sobre tu programa.**

Por eso, una regla práctica en C# es:

> Empezá con `private` y abrí el acceso solo cuando realmente haga falta.

---

## Ojo: “scope” también puede significar otra cosa

A veces “scope” no se refiere a `public`, `private`, etc., sino al **alcance de una variable**.

Ejemplo:

```csharp
public void Saludar()
{
    string mensaje = "Hola";

    Console.WriteLine(mensaje);
}
```

La variable `mensaje` solo existe dentro del método `Saludar`.

Si intento usarla afuera:

```csharp
Console.WriteLine(mensaje);
```

No funciona, porque está fuera de su alcance.

Entonces:

* `public`, `private`, `protected`, etc. = **quién puede acceder**
* scope de variable = **dónde existe esa variable**

[1]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers "Access Modifiers - C# reference | Microsoft Learn"
[2]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/accessibility-levels "Accessibility Levels - C# reference | Microsoft Learn"
