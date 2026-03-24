# Ejemplos de prompts segun el modelo (comparacion Unidad 2)

Este archivo sirve para la **comparacion practica** entre un modelo de **menor** y **mayor** capacidad: la **misma intencion** se puede pedir con distinto nivel de detalle. Los modelos mas chicos suelen beneficiarse de **contexto fijo**, **pasos** y **criterios de salida** explícitos. Los mas grandes a veces resuelven bien con un pedido **mas corto**, pero conviene igual **revisar** bordes y seguridad.

**Regla util:** si el modelo chico falla, no siempre hay que cambiar de modelo: a veces alcanza con **reescribir el prompt** (mas estructura). Si el modelo grande alucina APIs o se pasa de vueltas, **acotar** y **pedir por pasos** suele estabilizar la respuesta.

---

## 1. Generar un metodo en C#

### Version compacta (suele funcionar mejor en modelos grandes)

```text
Metodo en C#: validar email simple (formato basico) y devolver bool. Sin librerias externas.
```

### Version explicita (suele funcionar mejor en modelos chicos o para menos alucinaciones)

```text
Contexto: proyecto .NET 8, C#.
Tarea: escribir un metodo estatico `bool EsEmailValido(string? email)`.
Reglas:
- Si email es null o vacio o solo espacios, devolver false.
- Debe tener un @, texto antes y despues del @, y un punto despues del @ en la parte del dominio (validacion simple, no RFC completo).
- No uses paquetes NuGet; solo BCL.
Salida: solo el codigo del metodo y una linea de uso de ejemplo en comentario.
```

**Que comparar entre modelos:** null/empty, claridad de nombres, si inventa regex gigante, si explica demasiado o poco.

---

## 2. Pedir tests unitarios (xUnit)

### Version compacta

```text
Tests xUnit para ValidarEdad: acepta int edad, true si entre 18 y 120 inclusive.
```

### Version explicita

```text
Stack: C#, xUnit, .NET 8.
Clase bajo prueba: `public static bool ValidarEdad(int edad)` — true si 18 <= edad <= 120, false si no.
Pedido:
- Escribi una clase de tests `ValidarEdadTests`.
- Inclui casos: 17, 18, 120, 121, 0, -1.
- Un test por caso con nombre descriptivo (Arrange-Act-Assert).
- No mockees nada; es logica pura.
Mostrar solo codigo.
```

**Que comparar:** si el modelo chico olvida casos negativos o bordes; si el grande agrega tests redundantes o dependencias innecesarias.

---

## 3. Refactor sin cambiar comportamiento

### Version compacta

```text
Refactoriza este metodo para legibilidad, mismo comportamiento: [pegar codigo]
```

### Version explicita

```text
Refactor pedido: legibilidad y nombres claros.
Restricciones:
- No cambies la firma publica de metodos.
- No cambies el comportamiento observable (mismos resultados para mismas entradas).
- No agregues nuevas dependencias ni NuGet.
- Si extraes metodos privados, nombra segun intencion de negocio.
Entrega: codigo completo de la clase y lista breve (3 viñetas) de que cambiaste y por que.
```

**Que comparar:** el chico a veces deja codigo duplicado; el grande a veces “simplifica” y altera un borde. **Siempre** correr tests o probar manualmente despues.

---

## 4. Explicar un error de compilacion

### Version compacta

```text
Que significa este error en C#: CS1503 Argument 1: cannot convert from 'string' to 'int'
```

### Version explicita

```text
Tengo este error al compilar en .NET 8 (C#). Explicame en español:
1) Que significa en lenguaje simple.
2) Tres causas posibles ordenadas de mas probable a menos.
3) Que revisar primero en mi codigo (checklist corto).
4) Un ejemplo minimo ficticio que produzca el error y otro que lo corrija.

Error completo: [pegar mensaje y 2-3 lineas de contexto del archivo]
```

**Que comparar:** utilidad del checklist; si el modelo grande se va por la tangente; si el chico se queda corto en causas.

---

## 5. API / seguridad (misma tarea, distinto enfasis)

### Version compacta (riesgo: omite seguridad)

```text
Endpoint GET que devuelve lista de usuarios desde SQL Server en ASP.NET Core.
```

### Version explicita (obliga a pensar capas y riesgos)

```text
ASP.NET Core 8, C#.
Necesito diseño (no codigo completo del proyecto) de un endpoint GET /api/usuarios que lista usuarios desde SQL Server.
Inclui:
- Donde va la consulta (capa / clase sugerida), sin SQL concatenado con strings del usuario.
- Que validaciones y autorizacion minimas pedirias antes de exponer el listado.
- Que NO harías (anti-ejemplos): p.ej. connection string en el front, query armada con concatenacion.
Formato: viñetas, maximo 15 lineas.
```

**Que comparar:** si el compacto sugiere practicas peligrosas; si el explicito alinea mejor con criterios de calidad del curso.

---

## 6. Mismo prompt literal para ambos modelos (experimento controlado)

Para el **informe de la unidad**, conviene repetir **exactamente** el mismo texto en dos modelos:

```text
Implementá en C# (.NET 8) una clase `CalculadoraDescuento` con:
- `decimal Aplicar(decimal precio, decimal porcentaje)` donde porcentaje es 0-100.
- Si precio < 0 o porcentaje fuera de rango, lanzar ArgumentOutOfRangeException con mensaje claro.
- El resultado no puede ser negativo; si el descuento dejaria negativo, el resultado es 0.
Inclui 4 tests xUnit que cubran al menos un caso borde cada uno.
Sin NuGet externos.
```

**Que anotar:** diferencias en validaciones, mensajes de excepcion, nombres de tests, si compila a la primera, cuanto tuviste que corregir vos.

---

## Plantilla rapida para copiar en comparaciones

```text
Rol: actuas como dev senior C# / .NET 8.
Contexto: [1-2 frases del proyecto o consigna]
Tarea: [que querés]
Restricciones: [stack, sin paquetes X, estilo de codigo]
Formato de salida: [solo codigo / codigo + explicacion breve / pasos numerados]
Criterio de listo: [que debe cumplir para dar por terminado]
```

Los modelos chicos suelen seguir mejor esta plantilla; los grandes a veces la resumen solos — igual podes **forzarla** en ambos para comparar manzanas con manzanas.

---

## Recordatorio (Unidad 2)

- **Mismo prompt** → distintos modelos: medis calidad, bordes y tiempo de revision.
- **Distinto prompt** → mismo modelo: medis cuanto mejora la **ingenieria del prompt** sin cambiar de herramienta.

Las dos cosas son habilidades distintas y las dos valen para el curso y para el trabajo obligatorio.
