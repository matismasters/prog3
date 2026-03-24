# Programacion 3 2026
## Clase 4

# Hoy

- Cierre Unidad 2
- Limitaciones con ejemplos
- Modelos chicos vs grandes
- Uso responsable y criterios de calidad
- Buenas practicas, revision y actividades

Esta clase cierra la Unidad 2 del programa: **limitaciones y uso responsable de la IA en programacion**. Conectamos con el ejercicio del lunes (Antigravity, requerimientos, fallas en vivo) y **ponemos nombre y ejemplos** a lo que ya vivieron. El objetivo es que se vayan con criterios claros para revisar codigo generado, comparar modelos y defender lo que entregan.

---

# Venimos de aca

- Ejercicio largo: pagina web con requerimientos
- Antigravity como herramienta
- Primera experiencia con fallas y ajustes en vivo

Hoy no repetimos el ejercicio entero: **sistematizamos** lo que paso. La idea es que cuando la IA se equivoque de nuevo (y va a pasar), reconozcan el tipo de error y sepan que pedir o que corregir ustedes mismos.

---

# Unidad 2
## Objetivos del programa

- Comprender las **limitaciones** de la programacion asistida por IA
- **Comparar** modelos mas pequeños y mas grandes cuando exista
- **Criterios de calidad**: validaciones, mantenibilidad, arquitectura

Estos tres puntos son los que el README exige para esta unidad. Todo lo que vemos hoy cae en alguna de esas categorias. Si algo no encaja, probablemente sea detalle de implementacion; el marco mental son **limites**, **comparacion** y **calidad**.

---

# Por que importa esto

- La IA optimiza por **respuesta plausible**, no por certeza
- Suena segura aunque falte logica o seguridad
- El codigo que entregan lleva **su** firma profesional

En la industria no evaluan "que modelo uso", evaluan **si funciona, si es seguro y si lo pueden mantener**. Por eso la revision humana no es un detalle: es parte del oficio. La Unidad 2 les da vocabulario y checklist para esa revision.

---

# Limitacion: contexto
## Ventana finita

- El modelo no "recuerda" todo el proyecto como ustedes
- En chats largos, **detalles viejos se pierden** o se mezclan
- Archivos que no estan en el prompt no existen para el modelo

**Ejemplo:** En el mensaje 1 pediste "siempre usar .NET 8 y sin paquetes externos". En el mensaje 25 pedis "agrega autenticacion JWT". El modelo puede proponer paquetes, versiones viejas o patrones que **contradicen** lo acordado al principio porque ya no tiene presente la restriccion. **Que hacer:** repetir restricciones criticas, pegar fragmentos de codigo relevantes, o trabajar por tareas chicas con contexto refrescado.

---

# Limitacion: contexto
## Ejemplo de prompt incompleto

- "Agrega un endpoint para listar usuarios"

Sin decir **donde** (controller, minimal API, capa), **con que stack** ni **reglas de acceso**, el modelo inventa una forma generica. Eso no es "mala IA": es **falta de contexto**. Un prompt mejor delimita proyecto, capa y restricciones; asi la respuesta es revisable.

---

# Limitacion: coherencia
## Suena bien, puede estar mal

- Explicaciones fluidas **no** implican codigo correcto
- Puede mezclar APIs de distintas versiones en el mismo archivo
- Puede "cerrar" el problema omitiendo casos

**Ejemplo:** Te devuelve un metodo `CalcularDescuento(precio, porcentaje)` que nunca valida `porcentaje > 100`, `precio < 0` ni division por cero si mas adelante divide. El texto que lo acompaña dice "listo para produccion". **Revision:** leer el codigo como si fuera de un companero desconocido: ¿que pasa con null, negativos, extremos?

---

# Limitacion: coherencia
## Alucinacion de API

- Inventa nombres de metodos que **no existen** en la libreria
- Mezcla sintaxis de otro lenguaje o de otra version

**Ejemplo:** En C# sugiere `string.IsNullOrEmpty` bien, pero a veces inventa `JsonSerializer.Deserialize<T>(stream, options)` con parametros que no coinciden con su version de `System.Text.Json`. **Que hacer:** compilar siempre; si falla, contrastar con documentacion oficial o con el codigo que ya compila en el repo.

---

# Limitacion: seguridad
## Secretos y configuracion

- Sugiere API keys en constantes o en el front
- `appsettings.json` con secretos **commiteados** sin advertencia clara

**Ejemplo malo (conceptual):** "Pone el token de Hugging Face en un `const` en el JavaScript del cliente para probar rapido." Eso expone el token. **Bien:** variables de entorno, user-secrets en desarrollo, configuracion fuera del repo, y el token solo en servidor. La IA a veces prioriza "que funcione ya" sobre el modelo de amenaza.

---

# Limitacion: seguridad
## Datos y web

- Concatenacion de strings para SQL (**inyeccion**)
- Renderizar HTML sin escapar entradas de usuario (**XSS**)
- Autorizacion "por UI" sin chequear en servidor

**Ejemplo:** `query = "SELECT * FROM Users WHERE Name = '" + name + "'"` — la IA lo escribe en tutoriales viejos o simplificados. **Ustedes** deben exigir parametros, ORM o consultas parametrizadas. Igual con datos que vuelven a la vista: Razor suele escapar, pero si concatenan HTML armado a mano, el riesgo vuelve.

---

# Limitacion: casos borde y validaciones
## Lo "feliz" primero

- Muchas respuestas cubren el **camino feliz** (`edad = 25`)
- Olvidan **null**, **lista vacia**, **cero**, **negativos**, **maximos**

**Ejemplo:** Endpoint `POST /reservas` con `cantidadPersonas`. El modelo genera el insert y el 200 OK, pero no valida `cantidadPersonas <= 0`, ni capacidad del turno, ni fechas pasadas. **Criterio:** si en la consigna o el negocio hay reglas, hay que **pedirlas explicitamente** en el prompt o agregarlas ustedes en revision.

---

# Limitacion: casos borde
## Ejemplo numerico

- "Calcula el promedio de la lista"

Codigo que hace `sum / list.Count` sin chequear `list == null` o `Count == 0` **revienta** o es incorrecto. Pidan siempre: "inclui validaciones y lanzá excepciones claras" o "devolvé Result con error", segun el estilo del proyecto. Luego **verifiquen** que los tests o el manual cubran esos bordes.

---

# Comparacion practica
## Modelo mas chico vs mas grande

- **Misma tarea**, distinto modelo o herramienta cuando se pueda
- Observar: completado de codigo, **tests sugeridos**, refactor, explicaciones
- Tamaño **no** garantiza "mejor en todo"

**Ejemplo de tarea unica:** "Metodo en C# que valide email simple (formato basico) y devuelva bool; sin regex gigante." Un modelo chico puede dar un `if` corto y olvidar null. Un modelo grande puede agregar null-check y comentarios, o **sobre-ingenierizar** con librerias. Anoten: ¿que funcion aceptarian ustedes en un PR?

---

# Comparacion practica
## Que mirar en la comparacion

- **Correctitud:** compila, tests pasan, bordes razonables
- **Claridad:** nombres, metodos cortos, menos magia
- **Seguridad y datos:** validaciones, sin secretos mal puestos
- **Mantenibilidad:** no 500 lineas en un solo archivo sin razon

Para la **actividad de la unidad** (informe breve): mismo prompt, dos modelos, tabla con 3–5 filas de diferencias reales. No hace falta teoria: hechos observables.

---

# Uso responsable
## Cuando conviene confiar (mas)

- **Esqueletos** de proyecto, archivos base, estructura de carpetas sugerida
- **Renombrar**, formatear, traducir comentarios, explicar error de compilacion
- **Variantes** ("mostrame otra forma") para comparar en voz alta

Confiar no es "no leer". Confiar es "el riesgo es bajo o el diff es chico y verificable rapido".

---

# Uso responsable
## Cuando revisar siempre

- **Logica de negocio** (descuentos, permisos, reglas del cliente)
- **Seguridad**, autenticacion, datos sensibles, multi-tenant
- **Contratos publicos**: APIs, DTOs que consumen otros equipos
- Cualquier cosa que **no puedan explicar** en una defensa oral

**Ejemplo:** La IA sugiere rol "Admin" hardcodeado para agilizar. Eso en un parcial puede pasar; en trabajo obligatorio o produccion es deuda y riesgo. Revisar significa leer el diff como si fuera codigo ajeno.

---

# Uso responsable
## Cuando reescribir

- La estructura **no escala** (todo en un controller de 400 lineas)
- Mezcla capas: acceso a datos dentro de la vista
- Violaciones claras de responsabilidad o duplicacion masiva

**Ejemplo:** Un unico endpoint que abre SQL, mapea a entidades, aplica reglas de negocio y devuelve JSON. Funciona en el demo. **Reescribir** (o pedir refactor por pasos) en servicios/repositorios segun lo que el curso exija mas adelante. La IA a veces condensa para "una respuesta"; ustedes separan para **mantener**.

---

# Responsabilidad del desarrollador

- Entender **que** se despliega y **por que**
- Poder **explicar y defender** decisiones ante docente o tribunal
- **No delegar** validaciones ni analisis de bordes a la herramienta

En el curso y en el trabajo obligatorio se evalua: no alcanza "la IA lo hizo". Si en la defensa no pueden recorrer el flujo de una peticion o justificar una validacion, el criterio cuenta mas que la cantidad de lineas generadas.

---

# Criterios de calidad
## Validaciones completas

- Entradas de usuario, APIs, archivos: **tipo, rango, null, vacio**
- Reglas del dominio: estados invalidos, transiciones imposibles
- Mensajes de error **utiles** (sin filtrar datos sensibles)

**Ejemplo checklist:** Para cada `public` que recibe datos externos: ¿hay validacion? ¿hay test o prueba manual documentada? Si la IA omitio el caso, **no es opcional** completarlo antes de entregar.

---

# Criterios de calidad
## Codigo mantenible

- **Nombres** que revelan intencion (`CalcularPrecioFinal` vs `Calc2`)
- **Metodos** con una responsabilidad clara
- **Modulos** razonables; evitar "todo en Program.cs" sin razon

**Ejemplo:** La IA nombra `ProcessData` o `HandleRequest`. En revision, renombrar a algo alineado al dominio (`RegistrarReserva`, `ValidarCupos`). Mantenibilidad es lo que el **yo del mes que viene** entiende sin releer el chat entero.

---

# Criterios de calidad
## Arquitectura y separacion

- Presentacion vs negocio vs acceso a datos (cuando el proyecto lo exige)
- Dependencias explicitas; evitar acoplamiento "por comodidad del prompt"
- Configuracion y secretos **fuera** del codigo compartido

**Ejemplo malo:** Controller que instancia `new SqlConnection` en cada accion. **Mejor:** abstraccion inyectada (mas adelante Repository/DI). Aun en un parcial chico, pueden **nombrar** la intencion: "esto idealmente iria a un servicio".

---

# Repaso de buenas practicas

- Revisar codigo generado **linea por linea** cuando el riesgo es alto
- Pedir **tests** y luego **juzgar** si cubren lo importante (no solo cantidad)
- **Documentar** decisiones: README, comentarios donde aportan, no ruido

Esto conecta directo con la **Unidad 3** del programa: pruebas unitarias, refactorizacion y documentacion con IA. Lo que hoy definimos como "criterio" es lo que van a aplicar cuando pidan tests y refactors al modelo.

---

# Actividades de la unidad
## Lo que pide el programa

- Ejercicios con modelo de **menor** y **mayor** capacidad; **informe breve** de diferencias y limitaciones
- **Revisar y corregir** codigo generado con fallos de validacion o arquitectura; documentar criterios
- **Discusion** en clase: casos reales de uso y mal uso

En esta clase podemos: demo comparativa corta, revisar un fragmento "malo" en pantalla, o breakout con el mismo prompt en dos herramientas. Elijan segun tiempo; lo importante es que **cada uno** practique el informe y la revision, no solo mirar la demo del docente.

---

# Actividad en vivo sugerida
## Mismo prompt, dos modelos

- Elegir una tarea chica (validacion + metodo + 2 tests)
- Ejecutar con modelo "chico" y "grande" si la herramienta lo permite
- Anotar: errores, omisiones, sobre-ingenieria, tiempo de revision humana

**Salida:** 5–10 lineas por modelo: que aceptarian en un PR y que descartarian. Eso alimenta el informe de la unidad.

---

# Conexion con el lunes

- Que **patrones de error** repitieron en el ejercicio (contexto, UI, despliegue)
- Que hubieran **pedido distinto** al modelo sabiendo lo de hoy
- Que llevan como **checklist mental** para la proxima vez

Si trajeron notas del ejercicio, son oro para esta parte. Nombrar el error ("olvidó responsive", "rompio al recargar") ya es aplicar la Unidad 2.

---

# Para el integrador y el curso

- IA si, **ausencia de criterio** no
- **Documentar** uso de IA donde el programa lo pida (prompts, revisiones)
- Calidad final: **validaciones**, **arquitectura**, **pruebas**, **defensa oral**

El trabajo obligatorio se evalua con lupa en esos puntos. La Unidad 2 existe para que no les agarre de sorpresa a mitad de semestre.

---

# Cierre Unidad 2

- Limitaciones **conocidas y esperables**: contexto, coherencia, seguridad, bordes
- Comparacion de modelos como **habito**, no como curiosidad
- **Responsabilidad** y **criterios de calidad** por encima de la herramienta
- **Revision sistematica** de lo generado antes de dar por cerrado

Si esto queda claro, el resto del curso usa IA con mas velocidad y menos sustos.

---

# Lo que sigue

- **Unidad 3:** pruebas unitarias, refactorizacion y documentacion con IA
- Practica sobre **codigo de ejemplo** o mini proyecto guiado
- Mismo criterio de hoy: pedir, revisar, correr `dotnet test`, ajustar

Nos vemos en la proxima clase aplicando estos criterios a tests y refactors concretos en C#.
