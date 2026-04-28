# Proyecto: **MiniPlanta de Jugos**

Una pequeña planta industrial produce jugos envasados. La aplicación debe ayudar a registrar productos, organizar producción, controlar máquinas, anotar controles de calidad y reportar problemas de planta.

La consigna para los estudiantes sería:

> A partir de estos casos de uso, cada equipo debe identificar qué clases, relaciones y datos necesita guardar.
> El sistema completo no puede superar **5 entidades principales**.

---

# Equipo 1 — Catálogo de lo que fabrica la planta

## Caso de uso 1: Registrar un nuevo producto fabricable

Un encargado de planta quiere registrar un jugo que la fábrica puede producir.

Debe poder ingresar:

* nombre del jugo
* tipo de envase
* precio estimado de venta
* si está activo o no

Ejemplo:

```text
Jugo de naranja
Botella 500ml
$85
Activo
```

## Caso de uso 2: Consultar productos activos

Un operario necesita ver solamente los productos que actualmente se fabrican.

Debe poder:

* ver una lista de productos activos
* ocultar productos inactivos
* acceder al detalle de un producto

## Caso de uso 3: Desactivar un producto

El encargado decide que un producto ya no se fabrica más.

Debe poder:

* buscar el producto
* marcarlo como inactivo
* evitar que aparezca como opción en nuevas producciones

---

# Equipo 2 — Estado de las máquinas de planta

## Caso de uso 4: Registrar una máquina de la planta

El jefe de mantenimiento quiere registrar una máquina disponible en la fábrica.

Debe poder ingresar:

* nombre de la máquina
* sector donde está ubicada
* estado inicial

Ejemplo:

```text
Envasadora 1
Sector Envasado
Operativa
```

## Caso de uso 5: Cambiar el estado de una máquina

Una máquina puede cambiar de estado durante el día.

Estados posibles:

```text
Operativa
Detenida
En mantenimiento
```

Debe poder:

* seleccionar una máquina
* cambiar su estado
* ver cuándo una máquina no está operativa

## Caso de uso 6: Ver máquinas detenidas

El encargado necesita revisar rápidamente qué máquinas no pueden usarse.

Debe poder:

* ver una lista de máquinas detenidas
* ver una lista de máquinas en mantenimiento
* diferenciar las máquinas disponibles de las no disponibles

---

# Equipo 3 — Planificación de producción

## Caso de uso 7: Crear una orden de producción

Un encargado quiere planificar una producción para cierto jugo.

Debe poder indicar:

* qué jugo se va a producir
* en qué máquina se realizará
* cuántas unidades se quieren producir
* fecha de producción
* estado inicial

Estados posibles:

```text
Pendiente
En proceso
Finalizada
Cancelada
```

Ejemplo:

```text
Producir 300 unidades de Jugo de naranja 500ml
Usar Envasadora 1
Fecha: 12/05/2026
Estado: Pendiente
```

## Caso de uso 8: Cambiar el estado de una producción

Durante la jornada, una orden puede avanzar.

Debe poder:

* marcar una producción como en proceso
* marcarla como finalizada
* cancelarla
* ver las producciones pendientes

## Caso de uso 9: Ver el historial de producción

El jefe de planta quiere consultar qué se produjo.

Debe poder filtrar por:

* fecha
* estado
* producto fabricado

---

# Equipo 4 — Control de calidad

## Caso de uso 10: Registrar un control de calidad

Después de una producción, un operario de calidad revisa el resultado.

Debe poder registrar:

* producción revisada
* resultado
* observación
* fecha del control

Resultados posibles:

```text
Aprobado
Observado
Rechazado
```

Ejemplo:

```text
Producción: Jugo de naranja 500ml - 300 unidades
Resultado: Observado
Observación: algunas botellas tienen etiquetas mal colocadas
```

## Caso de uso 11: Ver controles rechazados

El encargado de calidad quiere revisar los problemas graves.

Debe poder:

* listar solamente controles rechazados
* ver la observación registrada
* saber a qué producción corresponde

## Caso de uso 12: Consultar controles de una producción

Al revisar una orden de producción, el usuario quiere ver si tuvo control de calidad.

Debe poder:

* entrar a una producción
* ver su control de calidad asociado, si existe
* saber si fue aprobada, observada o rechazada

---

# Equipo 5 — Incidentes de planta

## Caso de uso 13: Reportar un incidente

Un operario detecta un problema en la planta.

Debe poder registrar:

* máquina afectada
* descripción del problema
* severidad
* estado

Severidad posible:

```text
Baja
Media
Alta
```

Estados posibles:

```text
Abierto
Resuelto
```

Ejemplo:

```text
Máquina: Envasadora 1
Descripción: pérdida de líquido durante el envasado
Severidad: Media
Estado: Abierto
```

## Caso de uso 14: Resolver un incidente

Cuando el problema se soluciona, el encargado debe poder marcarlo como resuelto.

Debe poder:

* seleccionar un incidente abierto
* cambiarlo a resuelto
* mantener visible el historial

## Caso de uso 15: Ver incidentes abiertos

El jefe de planta quiere saber qué problemas siguen pendientes.

Debe poder:

* ver todos los incidentes abiertos
* filtrar por severidad
* ver qué máquina está afectada

---

# Reglas generales para todos los equipos

Estas reglas ayudan a que el proyecto no crezca demasiado:

```text
1. El sistema completo no puede tener más de 5 entidades principales.
2. Cada pantalla debe permitir crear, listar y ver detalle de su información principal.
3. Los estados deben elegirse desde opciones fijas, no escribirse a mano.
4. Cada equipo debe poder trabajar con datos de prueba aunque los demás módulos no estén terminados.
5. Al integrar, todos deben usar los mismos nombres y estados acordados.
```

---

# Integración final esperada

La demo final debería poder mostrar este recorrido:

```text
1. Se registra un jugo fabricable.
2. Se registra una máquina operativa.
3. Se crea una producción para ese jugo usando esa máquina.
4. Se cambia la producción a finalizada.
5. Se registra un control de calidad.
6. Se reporta un incidente en la máquina.
7. Se consulta qué producciones hubo, qué controles fallaron y qué incidentes siguen abiertos.
```

---

# Consigna explícita para los alumnos

Podés darles algo así:

```text
A partir de los casos de uso asignados, cada equipo debe:

1. Identificar qué información necesita guardar.
2. Proponer las clases necesarias para resolver sus casos de uso.
3. Definir atributos mínimos.
4. Definir relaciones con otros conceptos del sistema.
5. Implementar las pantallas o endpoints correspondientes.
6. Preparar datos de prueba para poder trabajar aunque otros equipos no hayan terminado.
```