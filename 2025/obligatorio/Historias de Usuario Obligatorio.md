## EPIC 1: Panel de Control Administrativo en Tiempo Real

### Historia 1.1 (3 puntos)  
**Como** administrador  
**Quiero** un dashboard que combine gráficos interactivos y un mapa en tiempo real  
**Para** analizar tendencias geográficas y temporales de las actividades.

**Sugerencias de arquitectura**  
- Modelo `Actividad` con propiedades `Latitud`, `Longitud`, `Estado`, `CreadoEn`.  
- Controlador `PanelAdminController` con acción `Index` que llame a `ServicioDashboard.ObtenerMetrics()`.  
- Componente de vista `Dashboard.razor` (Blazor) o `Dashboard.cshtml` + JS.  
- Integrar **Chart.js** para series de tiempo y barras; **Leaflet** o Google Maps JS para el mapa.

**Criterios de aceptación**  
1. Gráfico de líneas muestra número de actividades por hora en las últimas 24 h.  
2. Gráfico de barras muestra número de actividades por ciudad en los últimos 7 días.  
3. El mapa muestra marcadores de todas las `Actividad` con `Estado == "Activa"` y se refresca cada 10 s.  
4. La vista responde correctamente en escritorio y tablets (responsive).

---

### Historia 1.2 (2 puntos)  
**Como** administrador  
**Quiero** filtrar el dashboard por rango de fechas y ciudad  
**Para** segmentar los datos según mis necesidades.

**Sugerencias de arquitectura**  
- Clase `FiltroDashboard { int CiudadId; DateTime FechaInicio; DateTime FechaFin; }`.  
- Acción `PanelAdminController.Filtrar(FiltroDashboard filtro)`.  
- Validar en servidor: `(FechaFin – FechaInicio).TotalDays ≤ 90`.

**Criterios de aceptación**  
1. Al enviar filtro, los gráficos y el mapa muestran solo datos del rango y ciudad seleccionados.  
2. Si el rango excede 90 días, se devuelve error “Rango máximo 90 días”.  
3. Si no hay datos en ese filtro, muestra “No hay datos para este filtro”.

---

### Historia 1.3 (2 puntos)  
**Como** administrador  
**Quiero** exportar los datos del dashboard en CSV  
**Para** analizarlos offline o compartirlos con inversores.

**Sugerencias de arquitectura**  
- Servicio `ExportadorDashboard.GenerarCsv(FiltroDashboard filtro)` devolviendo `string`.  
- Ruta `GET /Admin/Panel/ExportarCsv?ciudadId=&fechaInicio=&fechaFin=`.  
- Usar **CsvHelper** para escribir CSV eficientemente.

**Criterios de aceptación**  
1. Se descarga un archivo CSV con columnas `Fecha, Ciudad, TotalActividades`.  
2. La generación y descarga tarda menos de 5 s para rangos hasta 30 días.  
3. El CSV se abre sin errores en Excel u otra hoja de cálculo.

---

### Historia 1.4 (1 punto)  
**Como** administrador  
**Quiero** ver un badge con el número de actividades activas ahora  
**Para** tener un vistazo rápido del uso en vivo.

**Sugerencias de arquitectura**  
- Método en `PanelAdminController`:  
  ```csharp
  public int ObtenerConteoActivas() =>
      _db.Actividades.Count(a => a.Estado == "Activa");
  ```  
- Componente `BadgeActivas.razor` que consuma ese método cada 10 s.

**Criterios de aceptación**  
1. El badge muestra el conteo real de actividades con `Estado == "Activa"`.  
2. Se oculta si el conteo es cero.

---

### Historia 1.5 (1 punto)  
**Como** administrador  
**Quiero** ver un indicador con la cantidad de actividades creadas hoy  
**Para** comparar con días anteriores.

**Sugerencias de arquitectura**  
- Método `ObtenerConteoHoy()` en `PanelAdminController`:  
  ```csharp
  public int ObtenerConteoHoy() =>
      _db.Actividades.Count(a => a.CreadoEn.Date == DateTime.Today);
  ```  
- Mostrar resultado en un `Card` simple.

**Criterios de aceptación**  
1. Muestra el total de `Actividad` con `CreadoEn` igual a hoy.  
2. El valor se recalcula automáticamente a las 00:00 del servidor.

---

### Historia 1.6 (1 punto)  
**Como** administrador  
**Quiero** ordenar la lista de ciudades según la cantidad de actividades  
**Para** identificar rápidamente las zonas más activas.

**Sugerencias de arquitectura**  
- Componente `TablaCiudades.razor` con cabecera de columna clicable para AJAX.

**Criterios de aceptación**  
1. La tabla muestra ciudades ordenadas de mayor a menor por `Total`.  
2. Cambiar el orden no provoca recarga completa de la página.

---

## EPIC 2: Sistema de Reportes de Actividad Automatizados

### Historia 2.1 (3 puntos)  
**Como** founder  
**Quiero** crear un único registro de email semanal con resumen de KPIs (incluye gráficos)  
**Para** automatizar el seguimiento sin intervención manual.

**Sugerencias de arquitectura**  
- Modelo `RegistroEmail` con propiedades `Destinatario`, `Asunto`, `CuerpoHtml`, `FechaProgramada`, `Estado`.  
- Servicio `GeneradorReportes` que genere HTML + gráficos con **Chart.js**, luego lo convierta a PDF con **DinkToPdf** o **WkHtmlToPdf**.  
- Scheduler en .NET Core 8: implementar un `BackgroundService` o usar **Quartz.NET** para disparar cada lunes a las 09:00.

**Criterios de aceptación**  
1. Cada semana, exactamente **un** nuevo `RegistroEmail` es creado con `Estado = Pendiente`.  
2. El campo `FechaProgramada` coincide con el lunes siguiente a las 09:00.  
3. El `RegistroEmail` contiene el PDF o HTML con métricas semanales y gráficos correctos.

---

### Historia 2.2 (2 puntos)  
**Como** founder  
**Quiero** generar manualmente registros de email desde la UI  
**Para** solicitar reportes puntuales en cualquier momento.

**Sugerencias de arquitectura**  
- Botón “Generar reporte” en `/Admin/Reportes/Index`.  
- Acción `ReportesController.CrearRegistroEmailManual()`: invoca `GeneradorReportes.CrearRegistro()`.

**Criterios de aceptación**  
1. Al pulsar el botón, se añade **un nuevo** `RegistroEmail` con `Estado = Pendiente`.  
2. El contenido es idéntico al del reporte semanal.  
3. Se pueden crear múltiples registros manuales, cada uno con su propia `FechaProgramada`.

---

### Historia 2.3 (2 puntos)  
**Como** responsable de marketing  
**Quiero** exportar CSV de actividades por ciudad y rango de fechas  
**Para** evaluar campañas offline.

**Sugerencias de arquitectura**  
- Formulario con campos `CiudadId`, `FechaInicio`, `FechaFin` en la vista `/Admin/Reportes`.  
- Servicio `ExportadorActividades` que genere CSV usando **CsvHelper**.

**Criterios de aceptación**  
1. El CSV descargado incluye columnas `Fecha, Ciudad, TotalActividades`.  
2. La generación y descarga tarda menos de 5 s para rangos de hasta 30 días.

---

### Historia 2.4 (1 punto)  
**Como** administrador  
**Quiero** configurar el día y la hora del envío semanal del reporte  
**Para** adaptarlo a mi zona horaria y equipo.

**Sugerencias de arquitectura**  
- Modelo `ConfiguracionReporte` con `DiaSemana` (enum) y `HoraEnvio` (TimeSpan).  
- Vista de ajustes en `/Admin/ConfiguracionReportes/Edit`.

**Criterios de aceptación**  
1. El scheduler respeta la nueva configuración de `DiaSemana` y `HoraEnvio`.  
2. La UI valida formato de hora “HH:mm”.

---

### Historia 2.5 (1 punto)  
**Como** sistema  
**Quiero** marcar un `RegistroEmail` como `Completado` cuando el PDF esté listo  
**Para** evitar reenvíos innecesarios.

**Sugerencias de arquitectura**  
- Enum `EstadoEmail { Pendiente = 0, Completado = 1, Fallido = 2 }` en `RegistroEmail`.  
- El servicio `GeneradorReportes` actualiza `EstadoEmail` al terminar.

**Criterios de aceptación**  
1. Tras generar el PDF, el registro cambia a `Completado`.  
2. En caso de error, cambia a `Fallido`.

---

### Historia 2.6 (1 punto)  
**Como** administrador  
**Quiero** ver el historial de `RegistroEmail` en una tabla paginada  
**Para** auditar todos los envíos.

**Sugerencias de arquitectura**  
- Paginación con **X.PagedList** o con `Skip()`/`Take()` de EF Core.  
- Ruta `GET /Admin/RegistroEmails/Index`.

**Criterios de aceptación**  
1. La tabla muestra columnas `Destinatario`, `FechaProgramada`, `EstadoEmail`.  
2. Permite filtrar por `EstadoEmail` y rango de fechas.

---

## EPIC 3: Herramientas de Moderación y Seguridad

### Historia 3.1 (3 puntos)  
**Como** moderador  
**Quiero** crear un registro de notificación cada vez que se reporte una actividad  
**Para** gestionar la cola de moderación.

**Sugerencias de arquitectura**  
- Modelo `RegistroNotificacion` con `Tipo = "Actividad"`, `ReferenciaId`, `Motivo`, `EstadoNotificacion`.  
- Servicio `ServicioModeracion.ReportarActividad(int actividadId, int usuarioId, string motivo)`.  
- Vista interna `/Admin/Moderacion/Notificaciones`.

**Criterios de aceptación**  
1. Al reportar, se crea un **solo** `RegistroNotificacion` con `EstadoNotificacion = Pendiente`.  
2. El registro guarda `Motivo` y referencia a la `Actividad`.  
3. Aparece en la bandeja `/Admin/Moderacion/Notificaciones` como “Nuevo”.

---

### Historia 3.2 (2 puntos)  
**Como** usuario  
**Quiero** reportar una actividad desde un modal con opciones predefinidas  
**Para** comunicar rápidamente el motivo.

**Sugerencias de arquitectura**  
- Componente `ModalReportarActividad.js` que llama a `POST /Actividades/{id}/Reportar`.  
- Acción `ActividadesController.Reportar(int id, ReporteDto dto)`.

**Criterios de aceptación**  
1. El modal abre sin recargar la página.  
2. Al enviar, se cierra el modal y aparece “Reporte recibido”.  
3. Un usuario no puede reportar la misma actividad más de una vez.

---

### Historia 3.3 (2 puntos)  
**Como** administrador  
**Quiero** bloquear un usuario desde el panel de moderación  
**Para** detener cuentas maliciosas.

**Sugerencias de arquitectura**  
- Acción `ModeracionController.BloquearUsuario(int usuarioId)`.  
- Propiedad `Estado = "Bloqueado"` en el modelo `Usuario`.

**Criterios de aceptación**  
1. Al pulsar “Bloquear”, el usuario cambia a `Estado = Bloqueado`.  
2. Usuario bloqueado no puede iniciar sesión.

---

### Historia 3.4 (1 punto)  
**Como** moderador  
**Quiero** ver el contador de reportes pendientes  
**Para** saber cuántos casos hay en cola.

**Sugerencias de arquitectura**  
- Consulta LINQ `db.RegistroNotificacion.Count(r => r.EstadoNotificacion == "Pendiente")`.  
- Badge en la cabecera de `/Admin/Moderacion`.

**Criterios de aceptación**  
1. El badge muestra el número correcto de registros pendientes.  
2. Se actualiza al recargar la página.

---

### Historia 3.5 (1 punto)  
**Como** sistema  
**Quiero** impedir que un usuario reporte dos veces la misma actividad  
**Para** evitar spam en la cola de moderación.

**Sugerencias de arquitectura**  
- Validación `[Index(nameof(ReferenciaId), nameof(UsuarioId), IsUnique = true)]` o `HasAlternateKey()`.  

**Criterios de aceptación**  
1. Si un usuario intenta reportar de nuevo, la petición falla con error “Ya reportaste esta actividad”.  
2. No se crea un registro duplicado.

---

### Historia 3.6 (1 punto)  
**Como** usuario  
**Quiero** ver confirmación visual tras reportar  
**Para** saber que mi reporte fue enviado.

**Sugerencias de arquitectura**  
- Notificación tipo toast con JS (`Toast.show("Gracias por tu reporte", 3000)`).  
- Disparar en el callback de AJAX exitoso.

**Criterios de aceptación**  
1. Aparece “Gracias por tu reporte” y desaparece en 3 s.  
2. No bloquea la navegación.

---

## EPIC 4: Módulo de Gestión de Partners y Alianzas

### Historia 4.1 (3 puntos)  
**Como** Partner  
**Quiero** registrar mi negocio como partner (logo, descripción, ubicación)  
**Para** aparecer marcado como “Verificado” en la app.

**Sugerencias de arquitectura**  
- Modelo `Partner` con `Nombre`, `Direccion`, `LogoUrl`, `Descripcion`, `EstadoPartner`.  
- Controlador `PartnersController` con acciones `Create` y `Details`.  
- Workflow de aprobación en `Admin/PartnersController`.

**Criterios de aceptación**  
1. Al enviar el formulario, se crea un `Partner` con `EstadoPartner = Pendiente`.  
2. Aparece en `/Admin/Partners` con opciones “Aprobar”/“Rechazar”.  
3. Al aprobar, el partner ve su badge “Verificado”.

---

### Historia 4.2 (2 puntos)  
**Como** administrador  
**Quiero** aprobar o rechazar solicitudes de partner  
**Para** controlar quién está en la plataforma.

**Sugerencias de arquitectura**  
- Enum `EstadoPartner { Pendiente = 0, Aprobado = 1, Rechazado = 2 }`.  
- Acciones `Admin/PartnersController.Aprobar(int id)` y `Rechazar(int id, string motivo)`.

**Criterios de aceptación**  
1. “Aprobar” cambia a `EstadoPartner = Aprobado`.  
2. “Rechazar” cambia a `EstadoPartner = Rechazado` y guarda `motivo`.

---

### Historia 4.3 (2 puntos)  
**Como** partner  
**Quiero** editar mi perfil básico (nombre, dirección)  
**Para** mantener mi información actualizada.

**Sugerencias de arquitectura**  
- Rutas `GET /Partners/Edit/{id}` y `POST /Partners/Edit/{id}`.  
- Políticas de autorización para que solo el dueño pueda editar.

**Criterios de aceptación**  
1. Al guardar, se actualizan los campos `Nombre` y `Direccion`.  
2. Si ya estaba aprobado, mantiene el badge “Verificado”.

---

### Historia 4.4 (1 punto)  
**Como** administrador  
**Quiero** mostrar un badge “Verificado” en los partners aprobados  
**Para** dar confianza a usuarios.

**Sugerencias de arquitectura**  
- En la vista de lista:  
  ```html
  @if(partner.EstadoPartner == EstadoPartner.Aprobado) {
    <span class="badge badge-success">Verificado</span>
  }
  ```

**Criterios de aceptación**  
1. Solo los partners con `EstadoPartner = Aprobado` muestran el badge.  
2. El tooltip al pasar el cursor dice “Partner verificado”.

---

### Historia 4.5 (1 punto)  
**Como** sistema  
**Quiero** validar que el email y teléfono de partner sean únicos  
**Para** evitar registros duplicados.

**Sugerencias de arquitectura**  
- Decorar `Email` y `Telefono` con `[Index(IsUnique = true)]` o validación en EF Core.

**Criteros de aceptación**  
1. Intento de duplicado lanza error “Email ya registrado” o “Teléfono ya registrado”.  
2. No se persiste el duplicado.

---

### Historia 4.6 (1 punto)  
**Como** local comercial  
**Quiero** un enlace “Registrarme como partner” en el footer  
**Para** encontrar fácilmente el formulario.

**Sugerencias de arquitectura**  
- En `_Layout.cshtml` añadir:  
  ```html
  <footer>
    <a href="/Partners/Create">Registrarme como partner</a>
  </footer>
  ```

**Criterios de aceptación**  
1. El link abre `/Partners/Create`.  
2. Se ve correctamente en mobile y desktop.

---

## EPIC 5: Sistema de Notificaciones y Comunicación Segmentada

### Historia 5.1 (3 puntos)  
**Como** responsable de engagement  
**Quiero** crear registros de notificación segmentada por zona y tipo de usuario  
**Para** planificar campañas dirigidas sin enviarlas aún.

**Sugerencias de arquitectura**  
- Modelo `RegistroNotificacion` con `Titulo`, `Mensaje`, `CriteriosSegmento` (JSON), `FechaProgramada`, `EstadoNotificacion`.  
- Servicio `PlanificadorNotificaciones.CrearRegistro(filtros)` que valide y almacene.  
- Vista en `/Admin/Notificaciones/Create` con selectores de ciudad y tipo de usuario.

**Criterios de aceptación**  
1. Se crea un solo `RegistroNotificacion` con `EstadoNotificacion = Pendiente`.  
2. `CriteriosSegmento` almacena `{ ciudad: [ids], tipoUsuario: [roles] }`.  
3. Aparece en `/Admin/Notificaciones` listado de programadas.

---

### Historia 5.2 (2 puntos)  
**Como** administrador  
**Quiero** crear plantillas de notificación con variables dinámicas  
**Para** reutilizar mensajes en distintas campañas.

**Sugerencias de arquitectura**  
- Modelo `PlantillaNotificacion` con `Nombre`, `AsuntoTemplate`, `CuerpoTemplate`.  
- Editor de plantillas en `/Admin/PlantillasNotificacion`.

**Criterios de aceptación**  
1. Soporta placeholders `{{Usuario.Nombre}}`.  
2. Al guardar, valida que no falten cierres de placeholder.

---

### Historia 5.3 (2 puntos)  
**Como** sistema  
**Quiero** crear un `RegistroNotificacion` automáticamente tras finalizar una actividad  
**Para** enviar follow‑up sin intervención manual.

**Sugerencias de arquitectura**  
- En `Actividad` model:  
  ```csharp
  protected override void OnAfterSave() {
    if (Estado == "Finalizada" && CambioReciente) {
      PlanificadorNotificaciones.CrearRegistroPostEvento(Id, FechaFinalizacion.AddHours(2));
    }
  }
  ```

**Criterios de aceptación**  
1. Cuando `Actividad.Estado` pasa a `Finalizada`, surge un registro con `EstadoNotificacion = Pendiente`.  
2. `FechaProgramada` es `FechaFinalizacion + 2 horas`.

---

### Historia 5.4 (1 punto)  
**Como** sistema  
**Quiero** agregar campo `EstadoNotificacion` en `RegistroNotificacion`  
**Para** saber si está `Pendiente`, `Enviada` o `Fallida`.

**Sugerencias de arquitectura**  
- Migración EF Core que añada:  
  ```csharp
  public enum EstadoNotificacion { Pendiente = 0, Enviada = 1, Fallida = 2 }
  ```

**Criterios de aceptación**  
1. El campo existe con los valores esperados.  
2. La vista `/Admin/Notificaciones` muestra correctamente el estado.

---

### Historia 5.5 (1 punto)  
**Como** sistema  
**Quiero** validar que un usuario no reciba la misma notificación más de una vez por semana  
**Para** evitar spam.

**Sugerencias de arquitectura**  
- Validación en `EnvioNotificacion` (entidad que une `RegistroNotificacion` y `Usuario`) con scope de 7 días.

**Criterios de aceptación**  
1. Si ya existe un envío para ese usuario y registro en la última semana, la creación falla con “Ya recibiste esta notificación esta semana”.  
2. No se persiste el envío duplicado.

---

### Historia 5.6 (1 punto)  
**Como** administrador  
**Quiero** ver la lista de notificaciones planificadas y su estado  
**Para** auditar campañas futuras.

**Sugerencias de arquitectura**  
- Ruta `GET /Admin/Notificaciones/Index`.  
- Componente `TablaNotificaciones.razor` o vista `Index.cshtml`.

**Criterios de aceptación**  
1. La tabla muestra `Titulo`, `FechaProgramada`, `EstadoNotificacion`.  
2. Permite filtrar por `EstadoNotificacion`.