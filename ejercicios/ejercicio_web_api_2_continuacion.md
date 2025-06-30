# Ejercicio Práctico (Continuación): Autenticación JWT y Asociación de Artistas a Usuario

**Tecnologías:** ASP.NET Core 8, Entity Framework Core, SQL Server, JWT Bearer Auth, Swagger/Postman

---

## 1. Guía paso a paso: Conceptos y configuración de JWT en ASP.NET Core 8

### 1.1 ¿Qué es JWT? y sus partes

Un **JSON Web Token (JWT)** es un estándar para representar de forma segura información entre dos partes. Consta de tres secciones codificadas en Base64URL:

1. **Header**: define el tipo de token (`JWT`) y el algoritmo de firma (p. ej. `HS256`).
2. **Payload**: contiene un conjunto de **claims** (declaraciones) que representan datos sobre el usuario o el token.
3. **Signature**: firma criptográfica que garantiza la integridad del token y su origen.

Formato: `xxxxx.yyyyy.zzzzz`

### 1.2 ¿Qué es un claim? Claims comunes

Un **claim** es una propiedad dentro del payload que describe algo acerca del usuario o del token. Existen:

* **Registered claims**: estándar, p. ej.:

  * `sub` (subject): identificador único del usuario.
  * `name` o `unique_name`: nombre de usuario.
  * `iat`: fecha de emisión.
  * `exp`: fecha de expiración.
* **Public claims**: definidos por terceros.
* **Private claims**: específicos de tu aplicación (p. ej. roles, permisos).

En ASP.NET Core, el claim `ClaimTypes.NameIdentifier` suele mapearse al `sub` del JWT.

### 1.3 Instalar paquete de JWT

Si usas **NuGet Package Manager Console**, ejecuta:

```powershell
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.0
```

Si prefieres **.NET CLI**, el comando es:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
```

### 1.4 Configurar autenticación en `Program.cs`

1. En **builder** (antes de `AddControllers()`):

```csharp

// Leer valores de JWT desde appsettings.json
var jwtKey      = builder.Configuration["Jwt:Key"];
var jwtIssuer   = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,

            // ahora leemos desde la configuración
            ValidIssuer              = jwtIssuer,
            ValidAudience            = jwtAudience,
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });


builder.Services.AddAuthorization();
```

2. En la **pipeline** (antes de `MapControllers()`):

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

### 1.4.1 Configuración en `appsettings.json`

Agrega la sección **Jwt** en tu archivo `appsettings.json`:

```json
{
  // ... otras secciones ...
  "Jwt": {
    "Key": "tu-clave-super-secreta-para-jwt",
    "Issuer": "TuApp",
    "Audience": "TuApp",
    "DurationHours": 2
  }
}
```

### 1.5 Generación de token: método `CrearToken`

Utiliza la siguiente implementación que lee la configuración desde `IConfiguration`:

```csharp
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// Alias para asegurarnos de usar el Claim correcto
using JwtClaim = System.Security.Claims.Claim;

namespace Artistas.Helpers
{
    public class Autentica
    {
        private readonly IConfiguration _config;

        public Autentica(IConfiguration config)
        {
            _config = config;
        }

        public string CrearToken(Usuario usuario)
        {
            // 1) Leer configuración de JWT
            var key       = _config["Jwt:Key"];
            var issuer    = _config["Jwt:Issuer"];
            var audience  = _config["Jwt:Audience"];
            var hours     = int.Parse(_config["Jwt:DurationHours"] ?? "2");

            // 2) Crear claims
            var claims = new List<JwtClaim>
            {
                new JwtClaim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new JwtClaim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new JwtClaim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
                new JwtClaim(
                    JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                )
            };

            // 3) Generar clave y credenciales
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds        = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            // 4) Fecha de expiración
            var expires = DateTime.UtcNow.AddHours(hours);

            // 5) Construir el token
            var token = new JwtSecurityToken(
                issuer:             issuer,
                audience:           audience,
                claims:             claims,
                expires:            expires,
                signingCredentials: creds
            );

            // 6) Serializar y devolver
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Modelo Usuario para referencia
    public class Usuario
    {
        public int    Id            { get; set; }
        public string NombreUsuario { get; set; }
    }
}
```

---

## 2. Ejemplo de uso de [Authorize]

Antes de comenzar con los ejercicios prácticos, aquí un ejemplo de cómo aplicar la anotación `[Authorize]` en un controller:

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artistas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ArtistasController : ControllerBase
    {
        // Este endpoint requiere un token JWT válido
        [HttpGet]
        public IActionResult GetArtistas()
        {
            // lógica para obtener artistas del usuario autenticado
            return Ok();
        }

        // Métodos sin [Authorize] en clase pueden tener [AllowAnonymous]
        [AllowAnonymous]
        [HttpGet("publico")]
        public IActionResult GetPublico()
        {
            // lógica pública
            return Ok();
        }
    }
}
```

### 2.1 Ejemplo de petición para obtener el token

Para generar el JWT, realiza una **POST** a `/api/usuarios/login` con un payload JSON como el siguiente:

```json
{
  "nombreUsuario": "usuario1",
  "password": "TuPassword123"
}
```

La respuesta exitosa tendrá este formato:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### 2.2 Ejemplo de envío del token en una petición protegida

Para llamar a un endpoint protegido, añade el header **Authorization** con el esquema `Bearer`:

```
GET /api/artistas HTTP/1.1
Host: localhost:5000
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

En Swagger, ve a **Authorize** (ícono de candado), pega el valor `Bearer <tu-token>` y aplica.

### Si no ves el botón de **Authorize** en Swagger:
Asegúrate de que tu `Program.cs` tenga la configuración correcta para Swagger:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Artistas API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido de un espacio y su token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
```

## 2. Ejercicio Práctico: Extensión de la API existente: Extensión de la API existente

Los alumnos deben **extender** la API ya creada para añadir autenticación y vincular artistas al usuario.

### Parte A – Modelo y endpoints de Usuario

1. **Crear entidad `Usuario`** con:

   * `Id` (int)
   * `NombreUsuario` (string)
   * `PasswordHash` (string)
   * Relación uno-a-muchos con `Artista`
2. **POST** `/api/usuarios/registrar` (recibe nombre/clave, guarda hash, retorna 201).
3. **POST** `/api/usuarios/login` (valida credenciales, retorna `{ token }`).

> Basarse en la implementación de `CrearToken` anterior sin más ejemplos de endpoints.

### Parte B – Foreign Key en `Artista`

1. Agregar en `Artista`:

   * `UsuarioId` (int)
   * `Usuario` (navegación)
2. Actualizar migración para incluir nueva columna.

### Parte C – Proteger y filtrar endpoints de Artistas

1. Aplicar `[Authorize]` a todos los métodos de `ArtistasController`.
2. En **POST `/api/artistas`**, leer el claim `ClaimTypes.NameIdentifier` (o `sub`) para asignar `UsuarioId`.
3. **GET `/api/artistas`** devuelve solo los artistas del usuario autenticado.
4. **PUT** y **DELETE** devuelven **403 Forbidden** si el artista no pertenece al usuario.

### Parte D – Pruebas y documentación

1. En Swagger/Postman probar:

   * Registro y login (obtener token).
   * Operaciones con y sin token.
   * Acceso a datos ajenos (403).
2. Documentar cada prueba con:

   * Endpoint y headers
   * Body
   * Código HTTP
   * Resultado esperado y obtenido

---

## 3. Entrega Final

* Repositorio Git con actualizaciones de JWT y migraciones.
* Capturas de pantalla de:

  * Registro/login (token).
  * Creación, lectura, edición y borrado de artistas (incluyendo errores 401/403).
* `README.md` que incluya:
  * Configuración de la clave JWT y connection string.
  * Pasos para registrar, loguear y usar el token en Swagger/Postman.