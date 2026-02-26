# Programación 3 (Arquitecturas con IA)

*Plan 2026 — Analista Programador — Semestre 3*

## Introducción

Este curso profundiza los conocimientos de Programación 2 con un enfoque en el análisis, diseño y **arquitectura** de aplicaciones, la integración con bases de datos relacionales mediante ORM y el desarrollo de servicios web y APIs. Programación 3 es la materia que **inicia** a los estudiantes en el **uso responsable de la IA en la programación**. Por eso **todo lo referente a IA se aborda al inicio del semestre** (primeras **cuatro** semanas): herramientas, IDE, CLI, limitaciones de los modelos, criterios de calidad, práctica con pruebas/refactorización/documentación y comparación de modelos. De esa forma, el estudiante utiliza la IA durante todo el curso en el resto de las unidades. El objetivo es que salga capaz de diseñar y defender una aplicación Full-Stack con persistencia compleja e integración con APIs de terceros, utilizando IA de manera crítica y responsable.

## Objetivos

- Profundizar en el paradigma de programación orientada a objetos con énfasis en arquitecturas empresariales y soporte de bases de datos relacionales.
- Introducir y aplicar patrones arquitectónicos y de diseño (Repository, Unit of Work, Dependency Injection) en el desarrollo de software.
- Desarrollar aplicaciones con persistencia de datos usando Entity Framework como ORM.
- Aplicar ASP.NET Core y MVC en nivel avanzado: enrutamiento, filtros, Web API y servicios REST.
- Implementar una capa de servicios web y consumir APIs de terceros.
- **Iniciarse en el uso responsable de la IA en el desarrollo:** uso avanzado de IDE y CLI, herramientas, comparación entre modelos (pequeños vs grandes), limitaciones y criterios de calidad para código generado; generación de pruebas unitarias, refactorización y documentación técnica con apoyo de IA.

## Carga horaria

| Concepto        | Valor |
|-----------------|-------|
| Semanas         | 16    |
| Clases por semana | 2   |
| Clases totales  | 32    |
| Horas por clase | 3     |
| **Horas totales** | **96** |

## Uso de IA en el curso

La Inteligencia Artificial es parte explícita del plan 2026. En este curso se utiliza como una herramienta para acelerar tareas repetitivas, explorar alternativas de diseño y mejorar la calidad del código. **Las primeras cuatro semanas del semestre** se dedican íntegramente a IA (herramientas, IDE, CLI, limitaciones, uso responsable, práctica con pruebas/refactor/documentación y comparación de modelos), utilizando únicamente **opciones con versión gratuita**. Una vez incorporado ese bagaje, se espera que el estudiante **use la IA en todas las unidades siguientes** del curso. Por tanto:

- Utilices un IDE ligero y la **línea de comandos** para compilar, ejecutar y probar aplicaciones .NET (`dotnet build`, `dotnet run`, `dotnet test`).
- Utilices asistentes de código (por ejemplo Cursor, GitHub Copilot para estudiantes, o equivalentes) **durante todo el semestre** en las unidades 5 a 16.
- Conozcas las **limitaciones** de la programación con IA (modelos pequeños vs grandes, contexto, mantenibilidad) y apliques criterios de calidad al revisar código generado.
- Generes o complementes pruebas unitarias, refactorización y documentación técnica con apoyo de IA, y revises su pertinencia y corrección.

El uso de IA no reemplaza la comprensión de los conceptos ni la defensa oral del proyecto; ambos siguen siendo evaluados.

## Requerimientos de hardware y software

### Software

- **IDE** — Editor o IDE ligero para desarrollo .NET. Se trabaja con **Visual Studio Code** o **Cursor**, no con Visual Studio. Se prioriza el uso de la **línea de comandos** para crear proyectos, compilar, ejecutar y ejecutar pruebas.
- **.NET 8 SDK** — Desarrollo desde terminal: `dotnet new`, `dotnet build`, `dotnet run`, `dotnet test`.
- **SQL Server Express** o **SQL Server LocalDB** — Base de datos para desarrollo (instalación independiente del IDE).
- **Git** — Control de versiones.
- **Herramientas de IA** — Para las unidades de IA y el resto del curso: uso de al menos una de las siguientes (o equivalentes que el docente indique): **Cursor**, **GitHub Copilot** (para estudiantes), u otras que se indiquen en clase. Se trabajará también con la comparación entre modelos más pequeños y más grandes cuando estén disponibles.

### Hardware

- **Procesador:** Intel Core i5 o superior (o equivalente).
- **Memoria RAM:** 8 GB o superior (recomendado 16 GB para IDE + herramientas de IA).
- **Almacenamiento:** 256 GB SSD o superior.

## Contenidos

### Unidad 1: IA en el desarrollo — Herramientas, IDE y CLI

#### Objetivos
- Explorar el uso de IA en el desarrollo (herramientas indicadas en Requerimientos).
- Dominar el uso avanzado del IDE (VS Code o Cursor) y de la línea de comandos para .NET.
- Integrar asistentes de código en el flujo de trabajo diario (edición, compilación, ejecución, pruebas).

#### Temas de clase
- Uso avanzado del IDE: atajos, terminal integrada, extensiones para C# y .NET.
- Flujo de trabajo con .NET desde CLI: `dotnet new`, `dotnet build`, `dotnet run`, `dotnet test`, estructura de soluciones y proyectos.
- Herramientas de IA: Cursor, GitHub Copilot (estudiantes), u otras que indique el docente. Configuración y uso en el editor.
- Prompts efectivos para generación y completado de código; buenas prácticas al trabajar con sugerencias de IA.
- Integración del asistente con el ciclo compilar–ejecutar–probar desde terminal.

#### Actividades
- Configurar el entorno (IDE + .NET CLI + una herramienta de IA) y documentar el flujo.
- Resolver ejercicios de código utilizando el asistente y la CLI; comparar tiempos y calidad con y sin IA.
- Generar pruebas unitarias y documentación básica con IA y revisar el resultado.

---

### Unidad 2: Limitaciones y uso responsable de la IA en programación

#### Objetivos
- Comprender las limitaciones de la programación asistida por IA.
- Comparar resultados entre modelos más pequeños y modelos más grandes (cuando existan).
- Establecer criterios de calidad para el código generado: validaciones, mantenibilidad, arquitectura.

#### Temas de clase
- Limitaciones típicas de los modelos de generación de código: contexto, coherencia, seguridad, casos borde y validaciones.
- Comparación práctica: modelos más pequeños vs modelos más grandes. Análisis de diferencias en completado de código, pruebas generadas y refactorización.
- Uso responsable: cuándo confiar en la sugerencia y cuándo revisar o reescribir. Responsabilidad del desarrollador sobre el código que se entrega.
- Criterios de calidad para aplicaciones desarrolladas con apoyo de IA: validaciones completas, código mantenible, buena arquitectura y separación de responsabilidades.
- Repaso de buenas prácticas: revisión de código generado, pruebas y documentación.

#### Actividades
- Ejercicios con un modelo de menor capacidad y comparación con un modelo de mayor capacidad; informe breve de diferencias y limitaciones observadas.
- Revisar y corregir código generado por IA que presente fallos de validación o arquitectura; documentar los criterios aplicados.
- Discusión en clase sobre casos reales de uso y mal uso de IA en el desarrollo.

---

### Unidad 3: IA aplicada — Pruebas unitarias, refactorización y documentación

#### Objetivos
- Practicar la generación de **pruebas unitarias** asistida por IA a partir de código existente (C# / xUnit).
- Practicar la **refactorización** de código con apoyo de IA (legibilidad, nombres, extracción de métodos) y validar los cambios.
- Practicar la **generación de documentación** técnica (comentarios XML, README, descripción de APIs) con IA y adaptarla a estándares.

#### Temas de clase
- Pruebas unitarias en .NET: estructura de un test, arrange-act-assert. Uso de IA para proponer casos de prueba y código de test; revisión crítica de cobertura y pertinencia.
- Refactorización asistida por IA: qué pedir (extraer método, renombrar, simplificar condiciones), cómo revisar el diff y asegurar que el comportamiento se mantenga.
- Documentación técnica con IA: comentarios en métodos públicos, README de proyecto, descripción de contratos; formato consistente y revisión.
- Integración en el flujo: compilar, ejecutar tests (`dotnet test`) y documentar como ciclo con apoyo de IA.

#### Actividades
- Las actividades se realizan sobre **código de ejemplo proporcionado por el docente** o un **mini proyecto guiado** (aún no se ha visto persistencia ni arquitectura en capas en el curso). Dado un pequeño módulo de código C#, generar con IA un conjunto de pruebas unitarias, ejecutarlas y completar o corregir lo que falle.
- Refactorizar un fragmento de código “sucio” con ayuda de IA y documentar qué cambios se hicieron y por qué.
- Redactar documentación (XML + README) para un mini proyecto con apoyo de IA y revisar que sea precisa y útil.

---

### Unidad 4: Comparación de modelos y cierre del bloque IA

#### Objetivos
- Profundizar en la **comparación entre modelos** (menor vs mayor capacidad) en tareas de código: completado, pruebas, refactorización.
- Consolidar **criterios de calidad** y uso responsable antes de pasar al resto del programa.
- Cerrar el bloque de IA con un resumen aplicable al resto del semestre.

#### Temas de clase
- Experimentos guiados: mismo prompt o misma tarea con distintos modelos (o distintas configuraciones); análisis de diferencias en calidad, contexto y errores típicos.
- Criterios de calidad en la práctica: checklist para revisar código generado (validaciones, bordes, arquitectura, mantenibilidad).
- Uso responsable: resumen de cuándo confiar, cuándo revisar y cuándo reescribir; responsabilidad sobre el código entregado.
- Resumen del bloque: herramientas, límites, buenas prácticas; expectativa de uso de IA en unidades 5 a 16.

#### Actividades
- Realizar 2–3 experimentos (misma tarea, distinto modelo o distinta herramienta) y redactar un informe breve: diferencias observadas y conclusiones.
- Elaborar en clase una lista de criterios de calidad para código generado por IA (para reutilizar en el trabajo obligatorio).
- Cierre del bloque: preguntas y repaso de lo que se usará el resto del semestre.

---

### Unidad 5: Repaso de POO y análisis/diseño de aplicaciones

#### Objetivos
- Repasar principios de programación orientada a objetos.
- Introducir el análisis y diseño de aplicaciones y el uso de diagramas UML (casos de uso e interacciones).
- Aplicar las herramientas y criterios de IA de las unidades 1 a 4 para apoyo en diagramas y especificación. Diagramas Mermaid.

#### Temas de clase
- Repaso de clases, objetos, herencia, polimorfismo, encapsulamiento y abstracción.
- Introducción a casos de uso: definición, características, especificación. Diagramas de casos de uso en UML.
- Interacciones entre objetos en UML. Buenas prácticas de diseño de software.
- Uso de IA para generar o completar diagramas de clases y casos de uso; redacción de especificaciones y detección de ambigüedades (con revisión crítica).

#### Actividades
- Crear diagramas de casos de uso para una aplicación de ejemplo. Mermaid.
- Desarrollo de casos de uso y diagramas de interacción entre objetos, usando IA para esbozos y aplicando revisión crítica según lo visto en las Unidades 1 a 4.

---

### Unidad 6: Persistencia de datos en aplicaciones web

#### Objetivos
- Introducir el manejo de bases de datos en aplicaciones web.
- Repasar ADO.NET y su integración con ASP.NET Core.

#### Temas de clase
- Conceptos básicos de persistencia de datos.
- Introducción a ADO.NET y su uso en C#.

#### Actividades
- Crear una conexión a una base de datos usando ADO.NET.
- Realizar operaciones básicas (insertar, eliminar, consultar datos). Uso de CLI para compilar y ejecutar el proyecto.

---

### Unidad 7: Entity Framework y ORM

#### Objetivos
- Introducir Entity Framework como herramienta de persistencia.
- Comprender el concepto de ORM (Object Relational Mapping).

#### Temas de clase
- Introducción a Entity Framework (EF).
- Configuración de un proyecto con EF desde línea de comandos y desde el IDE.
- Mapeo de entidades y relaciones.

#### Actividades
- Crear un modelo de datos con Entity Framework.
- Realizar operaciones CRUD utilizando EF. Flujo de trabajo con `dotnet run` y pruebas manuales.

---

### Unidad 8: Patrones arquitectónicos — Arquitectura en capas

#### Objetivos
- Comprender la arquitectura en capas y su diferencia con tiers.
- Organizar la lógica de dominio, el acceso a datos y la integración entre capas en un mismo proyecto.
- Reforzar la separación de responsabilidades.

#### Temas de clase
- Diferencia entre capas y tiers.
- Organización de la lógica de dominio y capa de acceso a datos.
- Integración de la capa de presentación con la capa de lógica de negocio.
- Organización de la capa de acceso a datos y estructura de proyectos con `dotnet new` y solución desde CLI.

#### Actividades
- Diseñar una aplicación con arquitectura en capas y dividir el proyecto en capas (presentación, lógica de negocio, acceso a datos).
- Crear una aplicación que utilice arquitectura en capas y desarrollar un acceso a datos eficiente con separación de responsabilidades.

---

### Unidad 9: ASP.NET Core MVC — Repaso y avance

#### Objetivos
- Repasar los conceptos fundamentales de ASP.NET Core MVC.
- Introducir vistas dinámicas y plantillas de diseño.

#### Temas de clase
- Revisión de MVC (Model-View-Controller).
- Razor View Engine: vistas dinámicas.
- Plantillas de diseño para vistas.
- Creación y ejecución del proyecto desde CLI (`dotnet new mvc`, `dotnet run`).

#### Actividades
- Crear vistas dinámicas con Razor View Engine.
- Personalizar vistas con plantillas de diseño.

---

### Unidad 10: ASP.NET Core MVC — AJAX y actualización de vistas

#### Objetivos
- Utilizar AJAX para actualizar vistas sin recargar la página.

#### Temas de clase
- Integración de AJAX con ASP.NET Core MVC.
- Actualización de vistas de forma dinámica.

#### Actividades
- Implementar AJAX para la actualización de una vista.

---

### Unidad 11: Integración de ASP.NET Core MVC con Entity Framework

#### Objetivos
- Conectar la capa de presentación con la base de datos mediante Entity Framework.

#### Temas de clase
- Integración de MVC y Entity Framework.
- Manipulación de datos desde el controlador.

#### Actividades
- Crear un formulario en una vista para agregar y gestionar datos en la base de datos.

---

### Unidad 12: Introducción a los servicios web

#### Objetivos
- Introducir los conceptos de servicios web y APIs REST.
- Preparar el consumo de APIs externas, en particular **APIs de modelos LLM** (tema central de la siguiente unidad).

#### Temas de clase
- Concepto y utilidad de los servicios web.
- Implementación de servicios web en ASP.NET Core.
- Uso de `HttpClient` para consumir APIs externas (GET, POST, headers, JSON).

#### Actividades
- Desarrollar un servicio web básico en ASP.NET Core.
- Realizar una petición HTTP a un endpoint público de prueba desde C#.

---

### Unidad 13: Consumo de APIs — Comunicación con modelos LLM (Hugging Face)

#### Objetivos
- Consumir APIs de **modelos LLM concretos** desde una aplicación ASP.NET Core.
- Integrar la **Inference API de Hugging Face** (u otro proveedor de LLM vía REST) como caso principal: autenticación, envío de prompts y manejo de respuestas.
- Aplicar buenas prácticas: manejo de errores, reintentos, configuración de tokens y parámetros de generación.

#### Temas de clase
- APIs de inferencia para LLMs: concepto, autenticación por token (Bearer), endpoints por modelo.
- **Hugging Face Inference API:** URL base (`https://api-inference.huggingface.co/models/{model_id}`), token de acceso, tareas text-generation y chat completion. Parámetros típicos: `inputs`, `parameters` (max_new_tokens, temperature, etc.). Respuesta JSON y uso en C#.
- Consumo desde ASP.NET Core: `HttpClient`, serialización/deserialización de request y response (System.Text.Json), inyección de dependencias para el cliente HTTP.
- Buenas prácticas: no exponer el token en el front-end, usar configuración (appsettings, variables de entorno), manejo de códigos de estado y límites de tasa (rate limiting).
- Integración en arquitectura en capas: servicio dedicado que encapsula las llamadas al LLM y es consumido por la capa de presentación o de negocio.

#### Actividades
- Crear una cuenta en Hugging Face, obtener token de acceso y probar la Inference API desde Postman o cURL.
- Implementar en C# un cliente que envíe un prompt a un modelo concreto (por ejemplo text-generation) y muestre la respuesta en una vista o en consola.
- Opcional: implementar una variante con chat completion (mensajes con rol system/user/assistant) y comparar con text-generation.
- Integrar la llamada al LLM en una aplicación con capas (servicio → controlador → vista o API propia).

---

### Unidad 14: Patrones de diseño — Repository, Unit of Work y Dependency Injection

#### Objetivos
- Implementar en **una semana (dos clases)** los patrones Repository, Unit of Work y Dependency Injection.
- Aplicar principios SOLID en el diseño y registrar dependencias en el contenedor de ASP.NET Core.
- Introducir pruebas unitarias con `dotnet test` y uso de IA para generarlas y revisarlas (apoyándose en lo visto en las Unidades 1 a 4).

#### Temas de clase
- Patrones Repository y Unit of Work en aplicaciones con persistencia.
- Dependency Injection en .NET: registro y resolución de dependencias.
- Principios SOLID aplicados en el mismo proyecto.
- Pruebas unitarias en .NET desde CLI (`dotnet test`). Uso de IA para generar y completar pruebas; revisión y criterios de calidad.

#### Actividades
- Implementar Repository, Unit of Work y DI en una aplicación ASP.NET Core en el marco de esa semana (dos clases).
- Escribir pruebas unitarias para la lógica de negocio con apoyo de IA y revisar cobertura y pertinencia.

---

### Unidad 15: Web API en ASP.NET Core

#### Objetivos
- Introducir el concepto de Web API y su implementación en ASP.NET Core.

#### Temas de clase
- Creación de una Web API en ASP.NET Core.
- Enrutamiento avanzado y filtros.
- Documentación de APIs (por ejemplo con Swagger/OpenAPI) y uso de IA para generar documentación técnica.
- Ejecución y pruebas desde CLI cuando corresponda.

#### Actividades
- Desarrollar una Web API en ASP.NET Core.
- Documentar endpoints y contratos con apoyo de IA y estándares de industria.

---

### Unidad 16: Integración Full-Stack y cierre

#### Objetivos
- Integrar en una sola aplicación: capas, persistencia con EF, Web API, consumo de APIs de terceros y calidad (pruebas, documentación).
- Asegurar que la aplicación cumpla con criterios de calidad: validaciones completas, arquitectura clara y código mantenible.
- Preparar la defensa del trabajo obligatorio.

#### Temas de clase
- Configuración avanzada de Web API y integración con front-end o clientes externos.
- Revisión de criterios de evaluación: aplicación Full-Stack, persistencia compleja, **integración con API de modelo LLM** (p. ej. Hugging Face), pruebas unitarias y documentación; calidad del código y **uso de IA documentado** (prompts, requerimientos, planes de implementación, revisión del resultado).
- Uso de IA en el ciclo de desarrollo: resumen y buenas prácticas de uso responsable (retomando Unidades 1 a 4).

#### Actividades
- Completar y documentar una aplicación Full-Stack que integre **al menos una API de modelo LLM** (por ejemplo Hugging Face) y persistencia compleja, con validaciones adecuadas y arquitectura mantenible.
- Asegurar pruebas unitarias (asistidas por IA cuando corresponda) y documentación técnica generada o complementada con IA.
- Preparar la defensa del proyecto ante tribunal o docente.

---

## Evaluación y acreditación

### Evaluación

- **Parcial 1:** Semana 8 (25% de la nota final).
- **Parcial 2:** Semana 16 (25% de la nota final).
- **Trabajo obligatorio:** 30% de la nota final. Entrega: fecha a definir. Debe incluir arquitectura en capas, patrones de diseño (Repository, Unit of Work, Dependency Injection), integración con **al menos una API de terceros (por ejemplo API de un modelo LLM, p. ej. Hugging Face)**, persistencia con Entity Framework, pruebas unitarias y documentación técnica. El **uso de IA documentado** incluye prompts utilizados, requerimientos o especificaciones dadas a la IA, planes de implementación y revisión del resultado por el estudiante. La aplicación debe ser correcta en funcionalidad, con **validaciones completas**, **código mantenible** y **buena arquitectura**.
- **Participación, disciplina e involucramiento:** 20% de la nota final (asistencia, participación en clase e involucramiento con las actividades).
- **Defensa:** Defensa de la aplicación Full-Stack ante tribunal o docente (requisito para la acreditación del trabajo obligatorio). El formato de evaluación de la defensa es el estándar de la carrera.
- **Puntaje final:** Se dará a conocer la semana siguiente a la última clase.

### Acreditación

- **Menos de 70 puntos:** Recursar.
- **Aprobación (ganancia de curso):** 70 puntos o más.
- **Exoneración:** 86 puntos o más.

---

*Documento adaptado al Plan 2026 — Integración transversal de IA, especialización .NET y orientación a producción profesional.*

---

## Apéndice: Contexto sobre Hugging Face (para el docente)

**Qué es:** Hugging Face es una plataforma de modelos de ML/LLM con un **Inference API serverless** que permite llamar a modelos por REST sin desplegar infraestructura. Ideal para el curso porque hay tier gratuito y muchos modelos públicos.

**Acceso:** Cuenta en [huggingface.co](https://huggingface.co) → Settings → Access Tokens. Token con permiso de lectura (o “Inference”) para la API serverless.

**Endpoint base:** `https://api-inference.huggingface.co/models/{model_id}`  
Ejemplo: `https://api-inference.huggingface.co/models/meta-llama/Llama-3.2-3B-Instruct`

**Dos formas de uso típicas:**

1. **Text-generation (clásico):** POST con body JSON: `{"inputs": "Tu prompt aquí", "parameters": {"max_new_tokens": 100, "temperature": 0.7}}`. Headers: `Authorization: Bearer hf_xxx`, `Content-Type: application/json`. La respuesta trae el texto generado (estructura depende del modelo; a veces viene en `[0].generated_text` o similar).
2. **Chat completion (estilo OpenAI):** Algunos modelos soportan mensajes con roles (`system`, `user`, `assistant`). Payload: `messages`, `max_tokens`, `stream`, etc. Ver [docs Chat Completion](https://huggingface.co/docs/api-inference/tasks/chat-completion). Útil para conversaciones y para homogeneizar con otros proveedores.

**Límites tier gratuito:** Alrededor de 300 peticiones/hora (puede variar). Cold starts en modelos no usados recientemente. Modelos muy grandes (>10GB) pueden no estar disponibles en serverless.

**Desde .NET:** No hay SDK oficial en C#. Se usa `HttpClient` con `Authorization: Bearer {token}`, serialización con `System.Text.Json` y deserialización de la respuesta. Encapsular en un servicio inyectable (por ejemplo `IHuggingFaceClient`) mantiene la capa de presentación desacoplada.

**Modelos sugeridos para prácticas:** Qwen2.5 (varios tamaños), Llama 3.2, Gemma 2, DeepSeek-R1 (razonamiento). Buscar en [huggingface.co/models](https://huggingface.co/models) con filtro “Inference API” o “text-generation-inference”. Playground: [huggingface.co/playground](https://huggingface.co/playground) para probar prompts antes de codificar.
