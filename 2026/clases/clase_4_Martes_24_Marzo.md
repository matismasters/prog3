# Programacion 3 2026
## Clase 4

---

# Matias Verges

- Analista en Tecnologias de la Informacion
- 15+ anos de experiencia
- `matis@matis.io`

---

# Hoy

- Cierre Unidad 2
- Limitaciones (desarrollo)
- Modelos chicos vs grandes
- Uso responsable y criterios de calidad
- Buenas practicas y discusion

Esta clase completa la Unidad 2 del programa. Conectamos con lo que vivieron el lunes en el ejercicio con Antigravity y lo sistematizamos.

---

# Venimos de aca

- Ejercicio largo: pagina web con requerimientos
- Antigravity como herramienta
- Primera experiencia con fallas y ajustes en vivo

Hoy ponemos nombre a lo que ya vieron y sumamos comparacion entre modelos y marco de responsabilidad.

---

# Unidad 2
## Objetivos del programa

- Comprender limitaciones de la programacion asistida por IA
- Comparar modelos mas pequeños y mas grandes cuando exista
- Criterios de calidad: validaciones, mantenibilidad, arquitectura

---

# Limitaciones tipicas
## Modelos de generacion de codigo

- Contexto: ventana finita, detalles que se pierden en conversaciones largas
- Coherencia: suena seguro aunque este incompleto o mal
- Seguridad: secretos en repo, SQL mal armado, XSS si no se piensa el front
- Casos borde y validaciones: a menudo hay que exigirlas explicitamente

La IA optimiza por "respuesta plausible", no por certeza formal. Por eso la revision humana no es un detalle: es parte del oficio.

---

# Comparacion practica
## Modelo mas chico vs mas grande

- Misma tarea, distinto modelo o distinta herramienta cuando se pueda
- Observar: completado de codigo, pruebas sugeridas, refactor
- Tamaño no garantiza "mejor" en todo: a veces el chico es mas conservador o mas claro

Si en el ejercicio del lunes usaron un solo modelo, hoy podemos repetir un fragmento con otro y contrastar en vivo o en breakout corto.

---

# Uso responsable

- Cuando confiar: tareas mecanicas, esqueletos, explicaciones, variantes
- Cuando revisar: logica de negocio, seguridad, datos sensibles, contratos publicos
- Cuando reescribir: cuando la estructura no escala o viola responsabilidades claras

El codigo que entregan lleva su firma profesional, no la de la herramienta.

---

# Responsabilidad del desarrollador

- Entender lo que se despliega
- Poder explicar y defender decisiones
- No delegar validaciones ni revision de bordes

En el curso y en el trabajo obligatorio esto se evalua: no alcanza "la IA lo hizo".

---

# Criterios de calidad
## Con apoyo de IA

- Validaciones completas en entradas y flujos criticos
- Codigo mantenible: nombres, modulos, no todo en un solo archivo gigante
- Arquitectura y separacion de responsabilidades donde corresponda

Si la IA propone un monolito confuso, ustedes tienen que pedir o aplicar otra estructura.

---

# Repaso de buenas practicas

- Revisar codigo generado linea por linea cuando el riesgo es alto
- Pedir tests y luego juzgar si cubren lo importante
- Documentar lo que queda decidido (README, comentarios donde aportan)

Esto conecta con la Unidad 3 del programa: pruebas, refactor y documentacion con IA.

---

# Actividades de la unidad
## Que podemos hacer hoy

- Breve comparacion chico vs grande con un mismo prompt (en clase o demo)
- Revisar juntos un fragmento "malo" generado por IA: que falla y que criterio aplicamos
- Discusion: casos reales de uso y mal uso de IA en desarrollo

Elijan segun tiempo y dinamica del grupo.

---

# Conexion con el lunes

- Que patrones de error repitieron en el ejercicio
- Que hubieran pedido distinto al modelo sabiendo lo de hoy
- Que llevan como checklist mental para la proxima vez

Si trajeron notas del ejercicio, son oro para esta parte.

---

# Para el integrador y el curso

- IA si, ausencia de criterio no
- Documentar uso de IA donde el programa lo pida
- Calidad final: validaciones, arquitectura, pruebas, defensa oral

---

# Cierre Unidad 2

- Limitaciones conocidas y esperables
- Comparacion de modelos como habito
- Responsabilidad y criterios de calidad
- Revision sistematica de lo generado

---

# Lo que sigue

- Unidad 3: pruebas unitarias, refactorizacion y documentacion con IA
- Practica sobre codigo de ejemplo o mini proyecto guiado
