# Ejercicio Práctico: Web API ABM de Artistas y Categorías

**Tecnologías:** ASP.NET Core 8, Entity Framework Core, SQL Server, Swagger/Postman

---

## 1. Objetivos

1. Diseñar los modelos de datos para dos entidades relacionadas:

   * **CategoríaArtista** (p. ej. Música, Pintura, Escultura)
   * **Artista** (con atributos sugeridos: Nombre, Género, FechaNacimiento, Nacionalidad)
2. Configurar Entity Framework Core para manejar migraciones y persistencia en base de datos.
3. Crear controladores RESTful con métodos **GET**, **POST**, **PUT** y **DELETE** para ambas entidades.
4. Probar todos los endpoints y manejar respuestas de éxito y error.

---

## 2. Crear proyecto, instalar y configurar

* Instalar y configurar:
* Crear un proyecto API vacío y configurado un connection string en `appsettings.json`.
  * Visual Studio 2022 (o VS Code) con plantilla **ASP.NET Core Web API**
  * .NET 8 SDK
  * SQL Server Express
* Paquetes NuGet:
  * `Microsoft.EntityFrameworkCore.SqlServer`
  * `Microsoft.EntityFrameworkCore.Tools`

---

## 3. Ejercicio

### Parte A – Definición de modelos

1. Define dos modelos:

   * `CategoriaArtista`:
     * `Id` (int)
     * `Nombre` (string)
     * `Descripción` (string)
     * Relación con colección de `Artista`

   * `Artista`:
     * `Id` (int)
     * `Nombre` (string)
     * `Género` (string)
     * `FechaNacimiento` (DateTime)
     * `Nacionalidad` (string)
     * `CategoriaArtistaId` (int) como clave foránea

2. Incluir anotaciones de validación básicas (`[Required]`, `[StringLength]`).

---

### Parte B – Configuración de Entity Framework Core

1. Crea un `DbContext` con `DbSet<CategoriaArtista>` y `DbSet<Artista>`.
2. Configura la relación uno-a-muchos entre `CategoriaArtista` y `Artista`.
3. Registra el contexto en `Program.cs` usando tu connection string.
4. Ejecuta `Add-Migration Inicial` y `Update-Database`.

**Entregar al profesor:** captura de consola de migraciones exitosas.

---

### Parte C – Controlador de Categorías de Artista

1. Define las rutas RESTful:

   * **GET** `/api/categorias_artistas`
   * **GET** `/api/categorias_artistas/{id}`
   * **POST** `/api/categorias_artistas`
   * **PUT** `/api/categorias_artistas/{id}`
   * **DELETE** `/api/categorias_artistas/{id}`
2. Retorna códigos HTTP apropiados (200, 201, 204, 400, 404).
3. Valida entradas y maneja errores de modelo inválido.

**Entregar al profesor:** captura de pantalla definiciones de endpoints en Swagger.

---

### Parte D – Controlador de Artistas

1. Define las rutas RESTful:

   * **GET** `/api/artistas` (incluye datos de la categoría)
   * **GET** `/api/artistas/{id}`
   * **POST** `/api/artistas`
   * **PUT** `/api/artistas/{id}`
   * **DELETE** `/api/artistas/{id}`
2. Al crear o actualizar, verifica que la categoría indicada exista; si no, devuelve 400.
3. Maneja la inclusión de la entidad relacionada cuando sea necesario.

**Entregar al profesor:** pasos de validación y ejemplo en Swagger de error y éxito.

---

### Parte E – Pruebas y validaciones

1. Usar Swagger o Postman para:

   * Crear varias categorías (Música, Pintura, Escultura)
   * Crear artistas asignados a esas categorías
   * Consultar, modificar y eliminar registros
2. Registrar los casos de prueba con el código HTTP y descripción breve.

**Entregar al profesor:** tabla o listado de casos de prueba con resultados.

---

## 4. Entrega final

* Repositorio Git con el código completo y migraciones.
* Documento con capturas y casos de prueba.
* README con todo lo arriba mencionado.
