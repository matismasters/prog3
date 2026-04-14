# Programacion 3 2026
## Clase 8

# Hoy

- Cierre **Unidad 4**
- Puesta en comun de comparaciones entre modelos
- Checklist de calidad para codigo generado con IA
- Acuerdos para usar IA durante el resto del semestre
- Puente hacia **Unidad 5:** POO, analisis, diseno, casos de uso y Mermaid

Hoy termina el primer bloque del curso dedicado explicitamente a IA. A partir de la proxima clase la IA deja de ser "el tema" y pasa a ser una herramienta de trabajo permanente para aprender arquitectura, persistencia, MVC, APIs y el integrador.

---

# Venimos de aca

- Ayer comparamos modelos o herramientas con una tarea C# y tests xUnit
- Observamos diferencias en validaciones, nombres, tests, errores y sobre-ingenieria
- Practicamos una forma simple de documentar evidencia

Hoy transformamos esas observaciones en un checklist concreto para el resto del curso.

---

# Unidad 4
## Cierre del bloque IA

Objetivos que cerramos:

- Comparar modelos en tareas de codigo
- Consolidar criterios de calidad y uso responsable
- Dejar una guia practica para trabajar con IA en unidades 5 a 16

La idea no es "usar menos IA". La idea es usarla mejor: con contexto, pasos chicos, revision, pruebas y documentacion.

---

# Puesta en comun
## Que paso en la comparacion

Preguntas para ordenar la discusion:

- Que modelo o herramienta compilo a la primera?
- Cual cubrio mejor los bordes?
- Cual genero tests que realmente podian fallar?
- Cual agrego codigo innecesario?
- Que correcciones humanas fueron inevitables?
- Que prompt produjo mejores resultados?

El objetivo es separar **percepcion** de **evidencia**. Si un modelo "parece mejor" pero omite validaciones, el resultado no es mejor para entregar.

---

# Leccion central
## La IA no cierra tareas sola

Una tarea de programacion con IA no termina cuando aparece una respuesta en pantalla. Termina cuando:

- El codigo fue leido
- El proyecto compila
- Los tests relevantes pasan
- Los bordes importantes fueron revisados
- La solucion se puede explicar
- La documentacion no contradice el codigo

Eso vale para un metodo chico y tambien para el trabajo integrador.

---

# Checklist de calidad
## Para codigo generado con IA

Antes de aceptar un cambio:

- **Correctitud:** compila, corre y cumple la regla pedida
- **Bordes:** null, vacio, cero, negativos, maximos, estados invalidos
- **Tests:** existen, son pertinentes y fallan si el codigo esta mal
- **Mantenibilidad:** nombres claros, metodos chicos, sin duplicacion innecesaria
- **Arquitectura:** responsabilidades separadas segun lo visto en el curso
- **Seguridad:** sin secretos en codigo, sin SQL concatenado, sin datos sensibles expuestos
- **Documentacion:** README, XML o comentarios alineados con el comportamiento real

Este checklist es acumulativo: en marzo mirabamos herramientas y CLI; mas adelante agregaremos EF, capas, DI, APIs y despliegue.

---

# Checklist rapido
## Preguntas de defensa

Si entrego este codigo, puedo responder:

- Que problema resuelve?
- Por que esta clase o metodo existe?
- Que entradas invalidas rechaza?
- Que tests lo protegen?
- Que parte genero la IA y que corregi yo?
- Que cambiaria si el requerimiento creciera?

Si no pueden explicarlo, todavia no esta listo para entregar aunque compile.

---

# Prompts responsables
## Plantilla reutilizable

Para el resto del semestre conviene pedir con estructura:

```text
Rol: actuas como dev senior C# / .NET 8.
Contexto: [proyecto, capa, archivo o regla de negocio].
Tarea: [cambio concreto].
Restricciones: [sin paquetes nuevos, no cambiar firmas, mantener tests, etc.].
Formato de salida: [diff, codigo completo, pasos, tests].
Criterio de listo: [compila, tests, validaciones, documentacion].
```

Esto ayuda especialmente cuando el modelo es chico, pero tambien reduce respuestas vagas en modelos grandes.

---

# Prompts por tipo de tarea
## Ejemplos para seguir usando

Para tests:

```text
Genera tests xUnit con Arrange-Act-Assert para esta clase.
Inclui camino feliz y bordes: null, vacio, cero, negativos y valores limite segun corresponda.
No inventes dependencias. Explica en una linea que regla cubre cada test.
```

Para refactor:

```text
Refactoriza para legibilidad sin cambiar comportamiento observable.
No cambies firmas publicas.
No agregues paquetes.
Mostrame primero una lista breve de cambios y luego el codigo.
```

Para documentacion:

```text
Redacta README breve con comandos reales para .NET 8.
No prometas scripts, endpoints ni configuraciones que no aparezcan en el codigo.
Inclui como compilar, ejecutar tests y estructura del proyecto.
```

---

# Acuerdos de trabajo
## Para unidades 5 a 16

Durante el resto del curso se espera que usen IA, pero con estas reglas:

- Trabajar en iteraciones chicas
- No aceptar codigo que no entienden
- Compilar y probar despues de cambios importantes
- Guardar prompts y decisiones relevantes para `ia_docs`
- Pedir ayuda a la IA para pensar alternativas, no para esconder desconocimiento
- Revisar arquitectura y validaciones con criterio propio

La IA acelera el trabajo; la responsabilidad profesional queda en ustedes.

---

# Conexion con Unidad 5
## POO, analisis y diseno

La siguiente unidad cambia el foco:

- Repaso de clases, objetos, herencia, polimorfismo, encapsulamiento y abstraccion
- Casos de uso y especificacion
- Diagramas UML y diagramas Mermaid
- Interacciones entre objetos
- Buenas practicas de diseno

La IA va a servir para generar borradores de diagramas o especificaciones, pero vamos a revisar ambiguedades, actores, responsabilidades y reglas de negocio.

---

# IA para diagramas
## Uso esperado

Un uso razonable:

- Pedir una primera lista de actores y casos de uso
- Pedir un diagrama Mermaid inicial
- Revisar si falta algun actor o caso importante
- Corregir nombres para que reflejen el dominio
- Validar el diagrama contra los requerimientos reales

Un uso riesgoso:

- Copiar un diagrama sin entender relaciones
- Aceptar actores inventados por la IA
- Mezclar casos de uso con detalles de implementacion
- Presentar un diagrama que no coincide con lo que el sistema hara

---

# Mini practica de cierre
## De requisito a criterio

Tomar una funcionalidad simple del integrador o de una empresa ficticia:

```text
Un usuario registrado puede crear una reserva indicando fecha, hora y cantidad de personas.
El sistema debe rechazar fechas pasadas y reservas sin cupo.
```

Preguntas:

- Que validaciones aparecen?
- Que tests se podrian pedir a la IA?
- Que casos de uso hay?
- Que ambiguedades faltan preguntar al docente-empresa?
- Que parte iria a `ia_docs` si esto fuera del integrador?

Este ejercicio conecta IA, calidad, requerimientos y diseno.

---

# Actividad de la unidad
## Entrega o evidencia

Para cerrar Unidad 4, dejar al menos una de estas evidencias:

- Informe breve de comparacion entre dos modelos o herramientas
- Checklist de calidad propio, adaptado desde el de clase
- Nota corta con prompts usados y correcciones humanas realizadas

No buscamos volumen. Buscamos que la evidencia muestre criterio.

---

# Cierre del bloque IA

- Unidad 1: herramientas y flujo
- Unidad 2: limites y responsabilidad
- Unidad 3: tests, refactor y documentacion
- Unidad 4: comparacion, checklist y cierre

Desde ahora, cada tema tecnico del curso se trabaja con esta base. Cuando veamos POO, persistencia, EF, MVC, APIs o DI, la pregunta va a ser doble: **como se programa** y **como se revisa lo que la IA propone**.

---

# Lo que sigue

- **Unidad 5:** repaso de POO y analisis/diseno de aplicaciones
- Casos de uso, especificaciones y diagramas Mermaid
- Empezar a transformar requerimientos del integrador en modelos entendibles y defendibles
