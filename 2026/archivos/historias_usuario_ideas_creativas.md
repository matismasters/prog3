# Historias de usuario ideas creativas

# Estructura básica de una historia de usuario
Como <TIPO_DE_USUARIO>
quiero <OBJETIVO_DESEADO>
para <RAZÓN_O_BENEFICIO>

## Creación de equipos
Como equipoAlumnos
Quiero poder registrarme como Equipo con usuario y contraseña
Para poder acceder a la plataforma y postular mis ideas creativas.


### Acceptance Criteria / Criterio de aceptación

- Como usuario no registrado,
    - voy a la página de registro de equipo
    - ingreso nombreDeEquipo
    - ingreso password
    - ingreso comprobación de password
    - ingreso número de integrantes
    - ingreso nombre de integrantes
    - ENTONCES
        - el nombre no estaba utilizado
        - veo el mensaje "Ingresado con exito"
        - me lleva a la pagina principal
    
    **
    - ENTONCES
        - el nombre si estaba utilizado
        - veo el mensaje "ya hay un equipo con ese nombre, ingrese otro nombre"
        - me muestra el form con los errores
    
    **
    - ENTONCES
        - el nombre no estaba utilizado
        - el password y la confirmacion no coinciden
        - veo el mensaje "los passwords no coinciden"
        - me muestra el form con los errores

    **
    - ENTONCES
        - el numero de integrantes es superior a 2
        - veo el mensaje "el numero de integrantes no esta permitido"
        - me muestra el form con los errores

## Postular idea
Como equipoAlumnos
Quiero postular una idea creativa
Para asegurarse que otro equipo no la haya postulado antes para el integrador.

### Acceptance Criteria / Criterios de aceptación
- Como usuario no registrado, 
    - voy a la pagina de postulacion de ideas
    - ingreso nombre del equipo
    - ingreso password del equipo
    - ingreso texto de la idea
    - hago click en el botón de postular idea
    - ENTONCES
    - veo un mensaje que dice:
        - "la idea fue postulada con éxito, espere por aprobación".
        - el sistema guarda la fecha y hora exacta de postulación

## Validar idea

Como profesor
Quiero revisar y aprobar las ideas postuladas
Para que los equipos sepan si sus ideas son validas y aprobadas.

### Acceptance Criteria / Criterio de Aceptación

- Como profesor
    - usando el user y pass hardcodeado
    - veo las ideas postuladas ordenadas por fecha y aprobadas o no.
    - veo que equipo postuló las ideas
    - hago click en una
    - ENTONCES
    - veo tres checkboxes que puedo modificar
    - es creativa
    - está bien planteada
    - aprobada
    - Marco todos los ticks
    - veo un mensaje "idea aprobada"
    - la idea correspondiente se marca como aprobada
    - me lleva a la pagina principal