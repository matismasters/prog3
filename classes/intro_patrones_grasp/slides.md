<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
# Patrones de Diseño GRASP
### Una guía práctica para asignación de responsabilidades

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## **General**
## **Responsibility**
## **Assignment**
## **Software**
## **Patterns**

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->

### Una guía práctica para asignación de responsabilidades

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## ¿Qué es GRASP?

- Son patrones que nos ayudan a decidir **quién** debería hacer **qué** en un sistema.
- ¿Qué clase debería tener qué responsabilidad?
- ¿Cómo distribuir las responsabilidades entre objetos?

> **Meta**: Promover un diseño de software de alta cohesión y bajo acoplamiento.

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## Los 9 patrones GRASP

1. **Information Expert**
2. **Creator**
3. **Controller**
4. **Low Coupling**
5. **High Cohesion**
6. **Polymorphism**
7. **Pure Fabrication**
8. **Indirection**
9. **Protected Variations**

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 1. Information Expert

- ¿Quién tiene la información necesaria para realizar la tarea?
- **Principio clave**: Delegar la responsabilidad a quien tiene los datos.

> Ejemplo: Clase **Factura** calcula el total porque conoce sus **líneas de factura**.



<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 2. Creator

- ¿Quién debe crear una instancia de otra clase?
- **Regla**: La clase que contiene o usa estrechamente a otra, debe crearla.

> Ejemplo: **Pedido** crea sus propias **Líneas de Pedido**.



<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 3. Controller

- ¿Quién recibe y coordina las solicitudes externas?
- **Controlador** es un intermediario entre la UI y el modelo de negocio.

> Ejemplo: **SistemaVentas** procesa la solicitud de "realizar venta".


<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 4. Low Coupling

- Minimizar las dependencias entre clases.
- Facilita cambios y mantenimiento.

> Estrategias: Delegar tareas a clases con menos dependencias externas.

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 5. High Cohesion

- Mantener las clases enfocadas en una sola tarea.
- Evitar clases "Dios" que hagan de todo.

> **Pregunta**: ¿Cómo identificar una clase con baja cohesión?

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 6. Polymorphism

- Usar polimorfismo para manejar comportamientos variantes.

> Ejemplo: **Empleado** es clase base, **EmpleadoPorHora** y **EmpleadoAsalariado** implementan su propia lógica de cálculo de salario.

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 7. Pure Fabrication

- Crear una clase "ficticia" para reducir acoplamiento o aumentar cohesión.

> Ejemplo: **RepositorioDeUsuarios** para manejar la persistencia, separando lógica de negocio.



<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 8. Indirection

- Introducir un intermediario para desacoplar clases.

> Ejemplo: Un **Service Layer** que actúa entre controladores y objetos de dominio.



<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## 9. Protected Variations

- Proteger elementos de posibles cambios o variaciones.

> Ejemplo: Usar interfaces para proteger la lógica de negocio de cambios en la base de datos.



<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
## Relación entre patrones

- **Low Coupling** y **High Cohesion** son principios guía.
- **Information Expert** y **Controller** ayudan a asignar responsabilidades.
- **Polymorphism**, **Indirection** y **Protected Variations** manejan el cambio.

<!-- .slide: data-background="#0f2027" data-background-gradient="linear-gradient(to right, #2c5364, #203a43, #0f2027)" -->
# ¡Gracias!
