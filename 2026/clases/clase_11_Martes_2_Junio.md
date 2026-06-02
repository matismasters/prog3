# Programación 3 2026
## Clase 11

# Hoy

- Arrancamos **Unidad 6: persistencia de datos en aplicaciones web**
- Primer contacto con **bases de datos** desde una app .NET
- El **modelo de conexión de ADO.NET**: cómo habla tu código con una base
- Clase distinta: hoy aprendemos jugando un **Safari de Datos**

Hasta ahora todo lo que hicimos vivía en memoria: se cerraba el programa y se perdía. Hoy empezamos a **guardar datos de verdad**, en una base que sigue ahí cuando apagás la máquina. Es el cimiento de todo lo que viene: EF, capas, MVC con datos, el integrador.

---

# Venimos de acá

- Unidad 5: repaso de POO, relaciones entre clases (asociación, agregación, composición), diagramas
- Aprendimos a **modelar** un dominio: qué entidades hay y cómo se relacionan
- Pero esos objetos vivían y morían con el programa

Hoy aparece la pregunta que el modelado no contesta: **¿dónde se guardan esos datos cuando el programa termina?** La respuesta es una **base de datos**, y la forma más directa de hablarle desde .NET es **ADO.NET**.

---

# Unidad 6
## Objetivos

- Entender **qué es la persistencia** y por qué necesitamos una base de datos
- Conocer el **modelo de conexión de ADO.NET**: conexión, comando, lectura de resultados
- Hacer las **operaciones básicas**: consultar (`SELECT`), filtrar (`WHERE`), insertar (`INSERT`)
- Trabajar desde la **CLI** y con nuestra propia herramienta, no con una GUI mágica

Esto es la base. Más adelante (Unidad 7) viene Entity Framework, que automatiza mucho de esto. Pero primero hay que entender **qué pasa por debajo**.

---

# Qué es persistir

- **Persistir** = guardar datos en un medio que sobrevive al programa
- Una **base de datos relacional** guarda los datos en **tablas** (filas y columnas)
- Nuestro programa no toca el disco directamente: **le habla a la base** y la base se encarga
- Hoy usamos **PostgreSQL** (una base relacional gratuita y muy usada en la industria)

La clave mental: tu app y la base son **dos cosas separadas** que se comunican. Tu código manda **SQL**, la base responde **datos**.

---

# El modelo de conexión de ADO.NET

ADO.NET es la capa de acceso a datos de .NET. El flujo es **siempre el mismo**:

| Pieza | Qué hace |
|---|---|
| **Connection string** | Los datos para conectarse: host, puerto, base, usuario, password |
| **Conexión** | Se **abre**, se usa, se **cierra** |
| **Comando** | El SQL que ejecutás sobre esa conexión |
| **Lectura** | El resultado: un valor (`ExecuteScalar`) o varias filas (`ExecuteReader`) |

Con Postgres el driver se llama **Npgsql**, pero el patrón es idéntico a SQL Server, MySQL, etc.

---

# Dos reglas que no se negocian

Vienen directo del **checklist de calidad** de la Unidad 4:

- **Cerrá siempre lo que abrís.** Una conexión es un recurso caro y limitado. Si no la cerrás, la "perdés" (leak) y la base se queda sin conexiones. En C#: bloque **`using`** → se cierra sola, aunque haya excepción.
- **Nunca concatenes strings en el SQL.** Cuando un valor viene de afuera, se manda con **parámetros**, no pegado con `+`. Concatenar abre la puerta a **inyección SQL**.

Estas dos las vamos a ver hoy en acción, y van a importar el resto del curso.

---

# Hoy no es una clase normal
## Es un juego

En vez de copiar un ejemplo, hoy aprendés ADO.NET **jugando**.

- Hay una base PostgreSQL compartida que monto yo.
- Cada uno construye su **propia herramienta** (una mini app MVC) para conectarse y mandar consultas.
- Con esa herramienta van a **explorar, cazar, criar y repoblar** un mundo de reservas naturales.
- Hay un **leaderboard en vivo** proyectado. Gana quien más puntos tenga al final del día.

Todo lo que hacen en el juego es **persistencia real**: `SELECT`, `WHERE`, `INSERT` contra una base de verdad.

---

# Por qué un juego
## El límite de la IA, otra vez

Esto conecta con las Unidades 1 a 4. El juego está **diseñado para que la IA no lo resuelva por vos**:

- La IA escribe SQL, sí. Pero **no ve el contenido vivo de la base.**
- No sabe qué reservas existen, ni qué nombres adivinar, ni dónde está cada animal.
- Esa información está **solo en la base, en vivo**. Descubrirla e interpretarla lo hacés **vos**.

La IA es una pala. El tesoro lo encontrás vos. Es exactamente el uso responsable que venimos practicando: la herramienta acelera, pero el criterio y el trabajo son tuyos.

---

# Cómo funciona la clase

1. **Estudian la letra solos.** Les reparto las reglas completas. **No respondo preguntas**: entender la spec es parte del juego (y de la era IA).
2. **30 minutos para armar el tooling.** Meta concreta: que **`SELECT 1` devuelva 1 en pantalla**. Les paso el connection string.
3. Al minuto 30 **activo el mundo** (go-live) y arranca el Safari.
4. Juegan. Yo circulo, pero no destrabo el juego: destrabo conexión.
5. **Cierre juntos:** repasamos qué consultas usaron, qué defectos aparecieron, y el checklist de calidad sobre su propio código.

---

# Los materiales

Dos documentos para ustedes (carpeta `archivos/safari-de-datos/`):

- **`01-conexion-y-tooling.md`** — cómo construir tu herramienta y lograr el `SELECT 1`. **Empezá por acá.**
- **`02-reglas-del-juego.md`** — la letra completa del Safari. Leela con calma; no la voy a explicar.

> El tercer documento (`03-plan-implementacion.md`) es **mío** (montaje de la base). No es material de ustedes.

---

# La meta de la primera media hora

> **Que `SELECT 1` devuelva `1` en pantalla.**

- Si lográs eso, ya tenés **conexión y tooling funcionando**.
- Lo podés probar **desde el arranque**, antes de que active el mundo.
- A partir de ahí, todo el Safari es **cambiar el SQL que mandás**: explorar una reserva, filtrar animales, registrar una captura, crear y liberar una cría.

Conexión primero. El juego después.

---

# Qué practicás hoy (sin que parezca estudio)

- **Conectarte** a una base real desde una app .NET (ADO.NET / Npgsql)
- **`SELECT`** para traer datos
- **`WHERE`** para filtrar (encontrar los animales reales entre el ruido)
- **`INSERT`** para guardar (registrarte, cazar, criar, liberar)
- **`using`** y **parámetros** como reflejo, no como obligación

Y de fondo: que persistir datos deje de ser un concepto abstracto y sea algo que **hiciste con las manos**.

---

# Para la próxima

- Hoy hablamos ADO.NET "a mano": vos escribís el SQL.
- En la **Unidad 7** llega **Entity Framework**: un ORM que mapea tus objetos a tablas y te escribe gran parte del SQL.
- Vas a valorar EF mucho más **después** de haber sufrido un poco el SQL a mano hoy.

Buena cacería.
