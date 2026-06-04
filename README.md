# FormulariosUAF

Sistema de gestión digital de declaraciones de **Beneficiario Final**, **PEP** y carga de **Carpeta Tributaria** para clientes empresa.

---

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server 2019+ o SQL Server Express / LocalDB
- Visual Studio 2022 o VS Code con extensión C#

---

## Instalación rápida

### 1. Clonar y restaurar paquetes

```bash
git clone <repo-url>
cd FormulariosUAF/FormulariosUAF
dotnet restore
```

### 2. Configurar conexión a base de datos

Editar `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=FormulariosUAF_Dev;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 3. Crear la base de datos con EF Core Migrations

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

O usar el script SQL manualmente:

```bash
# SQL Server Management Studio → Abrir SQL\CreateDatabase.sql → Ejecutar
```

### 4. Ejecutar la aplicación

```bash
dotnet run
# Navegar a: https://localhost:7000
```

---

## Usuarios de prueba (seed inicial)

| Email | Contraseña | Rol |
|---|---|---|
| `admin@uaf.cl` | `Admin@123!` | Administrador |
| `vendedor@uaf.cl` | `Vendedor@123!` | Vendedor |
| `cumplimiento@uaf.cl` | `Cumplimiento@123!` | Cumplimiento |

---

## Estructura del proyecto

```
FormulariosUAF/
├── Data/
│   ├── ApplicationDbContext.cs     # EF Core DbContext
│   └── Seed/SeedData.cs            # Datos iniciales (roles + usuarios)
├── Models/
│   ├── Domain/                     # Entidades de negocio
│   └── Enums/                      # Enumeraciones tipadas
├── Extensions/
│   └── EnumExtensions.cs           # Extensiones de display para enums
├── Services/
│   ├── IFileStorageService.cs      # Abstracción de almacenamiento
│   ├── LocalFileStorageService.cs  # Implementación disco local
│   ├── IPdfService.cs              # Generación de PDF
│   ├── PdfService.cs               # QuestPDF implementation
│   ├── IAuditService.cs            # Auditoría
│   ├── ITokenService.cs            # Tokens de cliente
│   └── IRequestService.cs         # Lógica de solicitudes
├── Pages/
│   ├── Account/                    # Login / Logout
│   ├── Vendedor/                   # Dashboard vendedor
│   ├── Cumplimiento/               # Panel de revisión
│   ├── Cliente/                    # Wizard 7 pasos (acceso por token)
│   └── Shared/                     # Layouts y partials
├── wwwroot/
│   ├── css/site.css                # Estilos Bootstrap 5 personalizados
│   └── js/site.js                  # JavaScript utilitario
├── Repository/                     # Almacenamiento de archivos (generado en runtime)
│   └── Clientes/{RUT}/{SolicitudId}/
├── SQL/
│   └── CreateDatabase.sql          # Script DDL completo
├── Program.cs
└── appsettings.json
```

---

## Flujo de uso

```
Vendedor crea solicitud
        ↓
Sistema genera token único (válido N días)
        ↓
Vendedor copia enlace → envía al cliente
        ↓
Cliente abre: /Cliente/Inicio?token=XXX
        ↓
Wizard 7 pasos:
  1. Datos empresa
  2. Beneficiarios finales
  3. Control efectivo
  4. Declaración PEP
  5. Datos declarante
  6. Carga documentos
  7. Revisión y envío
        ↓
Estado → CompletadaPorCliente
        ↓
Cumplimiento revisa → Aprueba / Observa / Rechaza
        ↓
Se generan PDFs firmados digitalmente (firma simple)
```

---

## Almacenamiento de archivos

Los archivos se guardan localmente por defecto en:

```
Repository/Clientes/{RUT_CLIENTE}/{SOLICITUD_ID}/
```

Naming convention: `{RUT}_{SolicitudId}_{TipoDocumento}_{FechaHora}.{ext}`

Para migrar a Azure Blob Storage, implementar `IFileStorageService` con el SDK de Azure:

```csharp
// Services/AzureBlobStorageService.cs
public class AzureBlobStorageService : IFileStorageService { ... }

// Program.cs — cambiar el registro:
builder.Services.AddScoped<IFileStorageService, AzureBlobStorageService>();
```

---

## Despliegue en IIS (Windows Server)

### 1. Publicar la aplicación

```bash
dotnet publish -c Release -o ./publish
```

### 2. Configurar IIS

1. Instalar **Windows Hosting Bundle** (.NET 8):
   https://dotnet.microsoft.com/download/dotnet/8

2. En IIS Manager:
   - Crear nuevo sitio web apuntando a la carpeta `./publish`
   - Application Pool: **No Managed Code**
   - Configurar binding HTTPS (puerto 443, certificado SSL)

3. Permisos de carpeta `Repository`:
   ```
   icacls "C:\inetpub\wwwroot\FormulariosUAF\Repository" /grant "IIS_IUSRS:(OI)(CI)M"
   ```

4. Variables de entorno en `web.config` o `appsettings.Production.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=SQLSERVER;Database=FormulariosUAF;..."
     }
   }
   ```

5. Ejecutar migraciones en producción:
   ```bash
   dotnet ef database update --connection "Server=..."
   ```

---

## Seguridad — Checklist

- [x] HTTPS obligatorio (`UseHttpsRedirection`, HSTS)
- [x] Tokens de cliente únicos y con expiración configurable
- [x] Antiforgery CSRF activado en todos los formularios
- [x] Validación de extensión y MIME type de archivos
- [x] Rate limiting en endpoints de cliente (`/Cliente/`)
- [x] Logs de auditoría con IP, User-Agent, fecha/hora
- [x] Control de acceso por perfil (roles: Admin, Vendedor, Cumplimiento)
- [x] Soft-delete con query filters en EF Core
- [x] Contraseñas con política mínima (8 chars, mayúscula, dígito)
- [x] Lockout automático tras 5 intentos fallidos
- [x] Session HTTP-only con expiración de 4 horas
- [x] No exposición de rutas físicas del servidor
- [ ] Escaneo antivirus de archivos (interfaz `IAntivirusService` pendiente)
- [ ] Cifrado de archivos en reposo (Azure Blob SSE o similar)
- [ ] Integración con Azure AD / Microsoft 365 (segunda etapa)
- [ ] Firma electrónica avanzada (arquitectura lista, implementación pendiente)

---

## Variables de configuración importantes

| Variable | Descripción | Default |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | Cadena SQL Server | LocalDB |
| `FileStorage:LocalPath` | Ruta de repositorio local | `Repository` |
| `FileStorage:MaxFileSizeMB` | Tamaño máximo por archivo | `20` |
| `TokenSettings:ExpirationDays` | Vigencia del enlace cliente | `30` |
| `AppSettings:BaseUrl` | URL base para enlaces | `https://localhost:7000` |
| `AppSettings:CompanyName` | Nombre empresa en documentos | Mi Empresa S.A. |

---

## Tecnologías utilizadas

| Componente | Tecnología |
|---|---|
| Backend | ASP.NET Core 8 Razor Pages |
| ORM | Entity Framework Core 8 |
| Base de datos | SQL Server |
| Autenticación | ASP.NET Core Identity |
| PDF | QuestPDF (Community License) |
| Logging | Serilog |
| Frontend | Bootstrap 5.3, Font Awesome 6 |
| Validación | DataAnnotations + jquery-validate |

---

## Licencia y cumplimiento normativo

Esta aplicación NO reemplaza la validación legal ni la revisión de cumplimiento. La información declarada debe ser conservada conforme al plazo indicado por la normativa interna y regulatoria aplicable. Los textos y umbrales son configurables desde `appsettings.json`.
