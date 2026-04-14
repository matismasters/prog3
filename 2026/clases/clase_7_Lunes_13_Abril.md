# Programacion 3 2026
## Clase 7

# Hoy

- Inicio **Unidad 4**
- Comparacion practica entre modelos o herramientas
- Mismo prompt, distintos resultados
- Analisis de calidad: correctitud, bordes, mantenibilidad y seguridad
- Primer borrador del **informe breve** de la unidad

Esta semana cerramos el bloque inicial de IA del curso. Ya usamos IA para programar, revisar limites, generar tests, refactorizar y documentar. Ahora el objetivo es comparar resultados con mas metodo: no decir "este modelo me gusta mas", sino justificar **que hizo mejor**, **que omitio** y **cuanto trabajo humano exigio**.

---

# Venimos de aca

- Unidad 1: herramientas, IDE, CLI y flujo basico
- Unidad 2: limites, uso responsable y criterios de calidad
- Unidad 3: tests con xUnit, refactorizacion y documentacion

Hoy juntamos todo eso en una practica controlada. Si un modelo genera codigo que parece bueno, igual lo pasamos por los mismos filtros: compila, cubre bordes, mantiene comportamiento, no inventa dependencias y se puede defender.

---

# Unidad 4
## Comparacion de modelos y cierre del bloque IA

Objetivos del programa:

- Profundizar en la **comparacion entre modelos** en tareas de codigo
- Consolidar **criterios de calidad** para revisar codigo generado
- Cerrar el bloque de IA con un resumen aplicable al resto del semestre

Hoy nos concentramos en la comparacion. Manana cerramos con checklist, acuerdos de trabajo y puente hacia la Unidad 5.

---

# Comparar no es opinar
## Es observar con criterio

Una comparacion util no alcanza con:

- "El modelo A respondio mas lindo"
- "El modelo B escribio mas codigo"
- "El modelo C fue mas rapido"

Eso puede ser parte de la experiencia, pero no alcanza para evaluar calidad. En programacion importan evidencias:

- El codigo **compila**?
- Los tests **pasan**?
- Cubre los **casos borde** pedidos?
- Agrego dependencias o arquitectura que nadie pidio?
- La respuesta se puede mantener y explicar?

---

# Experimento controlado
## Mismo prompt, dos modelos

Para comparar en serio, al menos una parte debe mantenerse fija:

- Misma consigna
- Mismo contexto
- Misma restriccion de stack
- Mismo criterio de evaluacion

Si cambian el prompt y el modelo al mismo tiempo, no saben si la diferencia vino del modelo o de la consigna. Por eso hoy hacemos un experimento simple: **mismo prompt literal** en dos modelos o herramientas disponibles.

---

# Prompt base sugerido
## CalculadoraDescuento

Usar este prompt igual en ambos modelos:

```text
Implementa en C# (.NET 8) una clase `CalculadoraDescuento` con:
- `decimal Aplicar(decimal precio, decimal porcentaje)` donde porcentaje es 0-100.
- Si precio < 0 o porcentaje fuera de rango, lanzar ArgumentOutOfRangeException con mensaje claro.
- El resultado no puede ser negativo; si el descuento dejaria negativo, el resultado es 0.
Inclui 4 tests xUnit que cubran al menos un caso borde cada uno.
Sin NuGet externos.
```

Este prompt esta pensado para que aparezcan diferencias reales: validaciones, excepciones, nombres de tests, calculo con `decimal`, casos borde y estilo.

---

# Que observar
## Antes de ejecutar

Lean la respuesta antes de copiarla:

- Respeta .NET 8 y C#?
- Usa `decimal` o cambia tipos sin permiso?
- Valida `precio < 0`?
- Valida `porcentaje < 0` y `porcentaje > 100`?
- Los tests realmente cubren bordes o solo camino feliz?
- Hay aserciones debiles?

Si algo ya esta mal en lectura, se anota. No hace falta esperar a compilar para detectar una regla omitida.

---

# Que observar
## Al compilar y probar

Despues de copiar el resultado en un proyecto chico:

```text
dotnet build
dotnet test
```

Registrar:

- Si compilo a la primera
- Si los tests pasaron
- Que errores hubo y si eran faciles de corregir
- Si el codigo generado por el modelo y los tests generados por el mismo modelo se "perdonan" entre si

Ese ultimo punto es importante: un modelo puede generar codigo incorrecto y tests que no lo detectan. Por eso la revision humana sigue siendo obligatoria.

---

# Modelo chico vs modelo grande
## Patrones frecuentes

Un modelo mas chico puede:

- Seguir mejor si el prompt es muy explicito
- Omitir casos borde si la consigna es corta
- Dar respuestas mas simples y a veces mas faciles de revisar

Un modelo mas grande puede:

- Inferir mejor reglas implicitas
- Generar mejores nombres o tests mas completos
- Sobre-ingenierizar o agregar explicaciones largas si no se acota

Tamano no significa "mejor en todo". La pregunta profesional es: **cual salida puedo convertir en codigo correcto con menos riesgo y menos retrabajo**.

---

# Comparacion con prompts distintos
## Segunda variante si hay tiempo

Despues del experimento controlado, se puede probar otra habilidad: mejorar el prompt para un modelo que fallo.

Ejemplo de prompt mas estructurado:

```text
Rol: actuas como dev senior C# / .NET 8.
Contexto: logica pura, sin base de datos, sin consola y sin paquetes externos.
Tarea: implementar `CalculadoraDescuento` y tests xUnit.
Restricciones:
- Usar decimal.
- Validar precio negativo y porcentaje fuera de 0 a 100.
- Tests con Arrange-Act-Assert.
- Incluir bordes: 0%, 100%, precio 0, porcentaje invalido.
Formato de salida: dos archivos completos, clase y tests.
Criterio de listo: `dotnet test` debe pasar y cada test debe verificar una regla distinta.
```

Aca ya no se compara solo modelo contra modelo. Se compara **calidad del prompt**.

---

# Informe breve
## Estructura minima

Cada equipo o estudiante debe dejar una nota corta con evidencia. No es una monografia.

Formato sugerido:

```text
# Comparacion Unidad 4

## Prompt usado
[pegar prompt literal]

## Modelo o herramienta A
- Compilo: si/no
- Tests pasaron: si/no
- Fortalezas:
- Problemas:
- Correcciones humanas:

## Modelo o herramienta B
- Compilo: si/no
- Tests pasaron: si/no
- Fortalezas:
- Problemas:
- Correcciones humanas:

## Conclusion
Que salida aceptaria como base para un PR y por que.
```

La conclusion debe hablar de calidad, no de preferencia personal.

---

# Actividad principal
## Comparacion guiada

1. Crear o reutilizar una solucion minima con proyecto de clase y proyecto xUnit
2. Ejecutar el mismo prompt en dos modelos o herramientas
3. Copiar la version A, compilar y probar
4. Copiar la version B, compilar y probar
5. Completar el informe breve con diferencias observables

Si el tiempo no da para crear todo desde cero, se puede trabajar sobre el ejemplo de `Calculadora` de la Unidad 3 y cambiar la consigna a `CalculadoraDescuento`.

---

# Revision de resultados
## Preguntas para la puesta en comun

- Que modelo omitio una validacion importante?
- Cual escribio mejores nombres?
- Cual genero tests mas utiles?
- Alguno agrego complejidad innecesaria?
- Cual requirio menos correccion humana?

La puesta en comun es parte de la clase: escuchar errores de otros equipos evita repetirlos en el integrador.

---

# Conexion con el integrador

En el trabajo obligatorio no se evalua solo que usaron IA. Se evalua si la usaron de forma **documentada, estrategica y tecnicamente justificada**.

Este informe de Unidad 4 es entrenamiento para `ia_docs`:

- Guardar prompts relevantes
- Registrar planes o decisiones
- Explicar correcciones humanas
- Mostrar criterio, no solo capturas o chats largos

---

# Cierre de hoy

- Comparar modelos exige controlar variables
- Mismo prompt permite observar diferencias reales
- `dotnet build` y `dotnet test` convierten opiniones en evidencia
- La mejor respuesta no siempre es la mas larga

---

# Para el martes

- Traer el informe breve o las notas del experimento
- Vamos a construir un **checklist de calidad** reutilizable
- Cerramos el bloque de IA y pasamos a la Unidad 5: POO, analisis, diseno y diagramas
