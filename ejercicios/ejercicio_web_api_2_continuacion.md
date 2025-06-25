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

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### 1.4 Configurar autenticación en `Program.cs`

1. En **builder** (antes de `AddControllers()`):

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer              = "TuApp",
            ValidAudience            = "TuApp",
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("tu-clave-super-secreta-para-jwt"))
        };
    });

builder.Services.AddAuthorization();
```

2. En la **pipeline** (antes de `MapControllers()`):

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

### 1.5 Generación de token: método `CrearToken`

Implementa en un servicio o helper una función que:

1. Reciba un objeto `Usuario`.
2. Construya una lista de `Claim`:

   * `new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())`
   * `new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())`
   * `new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario)`
3. Establezca fechas de emisión (`iat`) y expiración (`exp`).
4. Genere el token firmado:

```csharp
public string CrearToken(Usuario usuario)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
        new Claim(JwtRegisteredClaimNames.Iat,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu-clave-super-secreta-para-jwt"));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.UtcNow.AddHours(2);

    var token = new JwtSecurityToken(
        issuer: "TuApp",
        audience: "TuApp",
        claims: claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

---

## 2. Ejercicio Práctico: Extensión de la API existente

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
