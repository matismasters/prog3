# Safari de Datos — Reglas del Juego

> Leé esto con calma. **El docente no responde preguntas durante el juego.** Entender esta letra es parte del juego: si no la leés bien, vas a perder tiempo (y puntos) por algo que ya estaba escrito acá. Releé las veces que haga falta. En serio.

---

## 1. La premisa

Sos un explorador en un mundo de **reservas naturales**. El mundo es una base de datos PostgreSQL compartida que hostea el docente, y vos te conectás a ella con la herramienta que construiste vos mismo (la del documento de conexión).

En ese mundo hay **reservas naturales**, y cada reserva es una **tabla**. Dentro de cada reserva viven **animales**, que son las **filas** de esa tabla. Tu trabajo como explorador es:

1. **Descubrir** reservas (adivinar cómo se llaman).
2. **Cazar** animales reales escondidos entre montañas de ruido.
3. **Criar** animales armando parejas y soltar las crías al mundo.
4. **Repoblar** el mundo común para que crezca solo.

Todo lo que hacés suma (o no suma) **puntos**. **Gana quien tenga más puntos al final del día.**

### Por qué la IA no te resuelve esto

Vas a tener la tentación de pedirle a una IA que te lo haga. Probá, pero entendé el límite: **la IA escribe consultas, pero no ve el contenido vivo de la base.** No sabe qué reservas existen, no sabe qué nombres adivinar, no sabe qué animales hay cargados ni dónde está el ruido. Toda esa información está **solo en la base, en vivo**, y descubrirla e interpretarla lo tenés que hacer **vos**. La IA es una pala; el tesoro lo encontrás vos.

---

## 2. Cómo te conectás

Usás **tu propia herramienta** (la que armaste siguiendo el documento de conexión) apuntando a la base Postgres compartida del docente. Todas las acciones del juego —registrarte, explorar, cazar, criar, liberar— son **operaciones sobre tablas de esa base**. En esta letra te describo **qué tenés que lograr** en cada paso de forma funcional; **traducir eso a las consultas concretas con tu herramienta es parte del juego.**

---

## 3. Registro (tu primer paso, obligatorio)

Antes de tocar nada, **registrate**: agregá una fila a la tabla `jugadores` con el campo `nombre`.

- El `nombre` es **único**. Si dos jugadores eligen el mismo nombre, **el segundo rebota** (la base no lo deja entrar dos veces). Elegí algo tuyo y memorable.
- Esa identidad es tu ancla: **toda tu colección y todos tus puntos cuelgan de tu `nombre`.** Si no estás registrado, no existís en el juego.

Cuidá tu nombre: vas a usarlo en cada acción para decir "esto lo hice yo".

---

## 4. Descubrir reservas

Las reservas **no se pueden listar.** No hay un catálogo, no hay un menú, no hay una tabla que te diga "estas son las reservas". Tenés que **adivinar** cómo se llaman.

- Los nombres de las reservas son nombres de **reservas naturales, parques o islas REALES del mundo**, en minúscula (estilo `galapagos`, `serengeti`, y así). Son lugares que existen de verdad.
- **Formato:** van siempre en minúscula y **sin espacios**. Si el lugar es de varias palabras, no escribas un espacio (te traba): probá unirlas con guión bajo (estilo `masai_mara`) o usar su forma más corta e icónica.
- Para explorar una reserva usás una **función propia de esta base** llamada `explorar`, que recibe el nombre de la reserva. **Ojo: esto NO es SQL estándar.** `explorar` no existe en otras bases ni la vas a encontrar googleando — la pusimos nosotros para este juego. Por eso te la damos directamente acá. Se invoca así, como si fuera una tabla:

```sql
SELECT * FROM explorar('galapagos');
```

  Si el nombre existe, te devuelve las filas de esa reserva. Y como devuelve filas, **la podés filtrar con `WHERE`** igual que cualquier consulta — por ejemplo, para sacarte el ruido de encima (ver Sección 5):

```sql
SELECT * FROM explorar('galapagos') WHERE sexo IS NOT NULL;
```

  De acá en adelante, **todas las demás acciones (capturar, criar, liberar) son operaciones normales sobre tablas comunes** y las consultas las armás vos: la única cosa "rara" del juego es esta función `explorar`.

- **Si la reserva no existe, la respuesta tarda.** Hay una **penalización de tiempo** deliberada para los nombres equivocados. Por eso, **tirar nombres a lo bruto con un diccionario es lento y no te conviene**: vas a pasar el día esperando respuestas vacías.

**Pista vaga (la única que vas a tener):** el nombre del lugar suele decir **qué tipo de fauna vive ahí**. Pensá geografía real, pensá qué animal es famoso de cada lugar, y vas a adivinar mejor que tirando al azar. Pensar barato; adivinar a ciegas, caro.

En total hay **20 reservas** para descubrir.

---

## 5. Filtrar: encontrar a los animales reales

Cuando abrís una reserva, te vas a encontrar con un problema: **cada reserva tiene MILES de filas de ruido** y, escondidos ahí adentro, solo **3 a 5 animales reales**.

¿Cómo distinguís lo real del ruido?

- Los **animales reales** tienen su **especie** y su **sexo** cargados (datos completos).
- El **ruido** no tiene esos datos cargados (vienen vacíos).

Tu trabajo es **filtrar** la reserva para quedarte solo con las filas que tienen **sexo** cargado — el ruido nunca tiene sexo; ojo que algunas filas de ruido traen una especie inventada para engañarte, así que el dato que nunca miente es el sexo. Esas pocas filas son los animales que realmente podés cazar. Mirá bien: la **especie** y el **sexo** de cada animal te van a importar muchísimo en la Fase 2.

---

## 6. Fase 1 — Cacería

En esta fase **solo leés** reservas y **registrás capturas**. No hay cría todavía.

**Qué hacés:**

1. Descubrís una reserva (Sección 4).
2. La filtrás y encontrás sus animales reales (Sección 5).
3. **Capturás un animal** de esa reserva: agregá una fila a tu colección, la tabla `mi_isla`, con estos campos:
   - **tu jugador** (tu `nombre`),
   - **el id del animal** que cazaste,
   - **de qué reserva salió** (el nombre de la reserva).

**Regla de oro de la Fase 1:** **máximo UN animal por reserva fija.** De cada reserva podés llevarte uno solo, así que elegí con cabeza cuál te conviene (acordate de la especie y el sexo: te van a servir para armar parejas después).

**Meta de la Fase 1 (por jugador):** tener en tu `mi_isla` un animal de **cada una de las 20 reservas**.

Cuando completás las 20, **se te habilita la Fase 2.**

---

## 7. Fase 2 — Cría y repoblación

Recién acá podés empezar a **reproducir** animales y hacer crecer el mundo.

### Armar una pareja

Para criar necesitás, dentro de tu `mi_isla`, **un macho Y una hembra de la MISMA especie**.

Y acá está el truco estratégico: como en la Fase 1 **solo podés tener un animal por reserva fija**, para juntar una pareja de una especie tuviste que haber **encontrado esa misma especie en DOS reservas distintas, con sexos opuestos.** No alcanza con cazar en una sola. **Toda tu Fase 1 debería estar pensada en función de esto:** ¿qué especies aparecen en más de una reserva? ¿Estoy juntando un macho y una hembra, o dos del mismo sexo que no sirven para nada?

### Crear la cría

Cuando tenés la pareja (macho + hembra, misma especie), **creás una cría**: agregá una fila a la tabla `crias` con:

- **tu jugador** (tu `nombre`),
- la **especie** de la cría,
- el **sexo** de la cría.

**Una cría por especie:** de cada especie podés criar **una sola vez** (si intentás criar dos veces la misma especie, la base te rechaza con un error). Para sumar más crías, conseguí parejas de **otras** especies.

### Liberar la cría al mundo

Una cría recién nacida no vale puntos hasta que la **soltás al mundo común.** Para liberarla, agregá una fila a **una de las tres zonas comunes**:

- `amazonas`
- `sahara`
- `polo_norte`

En esa fila va el **nombre del animal** (que es **único por zona** — si el nombre ya existe en esa zona, rebota, así que poné otro), su **especie**, su **sexo**, y **quién la libera (tu `nombre` de jugador)** — ese campo es el que te da los puntos cuando otro la caza.

### Por qué esto hace crecer el mundo

Las crías que liberás **quedan vivas en las zonas comunes**, y **cualquier otro jugador puede cazarlas** y registrarlas en su propia `mi_isla`. Vos repoblás el mundo, otros lo cazan, y el mundo crece solo. Esto no es solo decorativo: **te da puntos cada vez que alguien caza una cría tuya** (ver tabla de puntos). Soltar crías es invertir.

---

## 8. Puntos

| Acción | Puntos |
|---|---|
| Capturar un animal de una reserva fija | **+10** |
| Completar las 20 reservas (bonus único) | **+50** |
| Capturar una cría en una zona común | **+3** |
| Reproducir y liberar una cría | **+30** |
| Cada vez que **otro** jugador caza una cría que **vos** liberaste | **+5** (para vos) |

**Letra chica que conviene leer:**

- El bonus de **+50** por completar las 20 reservas es **único**: se cobra una sola vez.
- Capturar una cría ajena en una zona común te da **+3** (mucho menos que las +10 de una reserva fija, pero está disponible y es rápido).
- Liberar tus propias crías es donde está el oro a largo plazo: **+30** por liberarla, y **+5 cada vez que otro la caza.** Una cría popular te puede dar puntos muchas veces. Ojo: de cada especie criás **una sola vez** (ver Sección 7), así que el oro está en liberar crías de **especies distintas**.
- **Cazar tus propias crías NO cuenta** para el bonus de +5. No podés farmear contra vos mismo. Si querés que esa cría te rinda, tiene que cazarla **otro**.

---

## 9. Leaderboard en vivo

Hay un **tablero web** que se **refresca cada minuto**. Te muestra:

- el **ranking por puntos** de todos los jugadores, y
- el **progreso global** del mundo (cuánto se descubrió en total entre todos).

Lo que **NO** te muestra es **qué encontró cada uno**. No vas a poder espiar las reservas o las especies de los demás por ahí; eso te lo tenés que ganar explorando. El tablero sirve como **brújula**: te dice cuánto falta por descubrir en el mundo y dónde estás parado en la carrera.

---

## 10. Cómo se gana

**Gana quien tenga más puntos cuando termina el día.** No hay sorpresas: es la suma de todo lo de la Sección 8.

Estrategia en una línea: **descubrí rápido (pensando los nombres, no tirando al azar), cazá pensando en armar parejas, completá las 20 para el bonus, y liberá crías que otros quieran cazar para cobrar +5 una y otra vez.**

---

## 11. Cierre

Repito lo del principio porque importa: **el docente no responde preguntas durante el juego.** Todo lo que necesitás para jugar bien está en esta letra. Si algo no te cierra, **la respuesta está acá arriba** — volvé a leer la sección que corresponda. Entender la spec **es** el juego.

Buena cacería.
