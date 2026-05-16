# 🏛️ La Almoneda Nacional
### Sistema de Gestión de Subastas — Primer Parcial Ingeniería de Software · UAI 2026

> Aplicación de escritorio Windows Forms (.NET 4.7.2) que gestiona el ciclo completo de subastas:
> catálogo de artículos y lotes, apertura/cierre de subastas, registro de pujas en tiempo real
> y reportes de jornada.

---

## 🗂️ Arquitectura

```
┌─────────────────────────────────────────────────────┐
│                    GUI (WinForms MDI)                │
│  Login · Menu · Catálogo · Subastas · Postores      │
│  Registrar Puja · Reporte de Jornada · Usuarios     │
├──────────────────────────────────────────────────────┤
│                        BLL                           │
│  CatalogoBLL · SubastaBLL · PostorBLL · UsuarioBLL  │
│  GestorPujas (Singleton)                            │
├──────────────────────────────────────────────────────┤
│            Servicios              Seguridad          │
│  GestorNotificaciones   SessionManager (Singleton)  │
│  NotificadorPostor      Encriptador (SHA-256)       │
│  ReporteJornada · Bitacora                          │
├──────────────────────────────────────────────────────┤
│                        DAL                           │
│  Acceso (Singleton) · SubastaDAL · PujaDAL          │
│  UnidadDeVentaDAL · PostorDAL · UsuarioDAL · ...    │
├──────────────────────────────────────────────────────┤
│                         BE                           │
│  UnidadDeVenta · ArticuloIndividual · Lote          │
│  Subasta · Puja · Postor · Usuario · Suscripcion    │
└──────────────────────────────────────────────────────┘
                          │
                   SQL Server Express
                   AlmonedaNacionalDB
```

---

## 🎨 Patrones de Diseño

### Composite — Catálogo de Unidades de Venta
```
UnidadDeVenta (Component)
├── ArticuloIndividual (Leaf)    → ObtenerPrecioBase() = ValorDeclarado
└── Lote (Composite)             → ObtenerPrecioBase() = Σ hijos (recursivo)
```
Permite tratar artículos individuales y lotes agrupados de forma uniforme.
El precio de un lote se calcula recursivamente sumando todos sus componentes.

### Observer — Notificaciones en Tiempo Real
```
ISujetoSubasta          IObserverPostor
      │                       │
GestorNotificaciones ←── NotificadorPostor
      │
   Notificar() ──► itera sobre la lista de observers
```
Cada vez que se acepta una puja, todos los postores suscriptos reciben
una notificación automática con el nuevo precio vigente.

### Singleton — Control de Concurrencia y Sesión
| Singleton | Responsabilidad |
|-----------|----------------|
| `GestorPujas` | Serializa pujas concurrentes con `lock`; evita doble adjudicación |
| `DAL.Acceso` | Instancia única de acceso a BD (connection pooling) |
| `Seguridad.SessionManager` | Usuario autenticado disponible en toda la app |

Todos usan **double-checked locking** con `volatile` + `lock`.

---

## 🖥️ Formularios

| Formulario | Rol requerido | Función |
|-----------|--------------|---------|
| `Login` | — | Autenticación con bloqueo tras 3 intentos |
| `Menu` (MDI) | Todos | Shell principal con menú filtrado por rol |
| `FrmCatalogo` | Martillero / Admin | TreeView del árbol Composite |
| `FrmNuevoArticulo` | Martillero / Admin | Alta de artículo individual (Leaf) |
| `FrmNuevoLote` | Martillero / Admin | Alta de lote (Composite node) |
| `FrmSubastas` | Martillero / Admin | Abrir · Cerrar · Ver activas |
| `FrmRegistrarPuja` | Operador / Admin | Registra puja via `GestorPujas` Singleton |
| `FrmPostores` | Operador / Admin | ABM de postores + suscripciones Observer |
| `FrmReporteJornada` | Todos | Recorre el árbol Composite recursivamente |
| `FrmGestionUsuarios` | Admin | Desbloquear cuentas · Resetear contraseñas |

---

## 👥 Usuarios de prueba

| Email | Contraseña | Rol | Acceso |
|-------|-----------|-----|--------|
| `admin@almoneda.com` | `admin123` | Administrador | Total |
| `martillero@almoneda.com` | `admin123` | Martillero | Catálogo + Subastas + Reportes |
| `operador@almoneda.com` | `admin123` | Operador | Subastas activas + Pujas + Postores |
| `supervisor@almoneda.com` | `admin123` | Supervisor | Reportes (solo lectura) |

> Las contraseñas se almacenan como hash SHA-256. La cuenta se bloquea automáticamente tras 3 intentos fallidos.

---

## ⚙️ Cómo ejecutar

### Requisitos
- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- SQL Server Express (instancia `.\SQLEXPRESS`)

### Pasos

**1. Crear la base de datos**
```sql
-- Ejecutar en SQL Server Management Studio o sqlcmd:
-- BD/AlmonedaNacional.sql
```
El script crea la BD `AlmonedaNacionalDB`, todas las tablas, índices y los 4 usuarios seed.

**2. Verificar la cadena de conexión**

En `Primer Parcial - Bolivar Cruz Magali/App.config`:
```xml
<add name="AlmonedaNacionalDB"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=AlmonedaNacionalDB;Integrated Security=True"
     providerName="System.Data.SqlClient" />
```
Cambiar `.\SQLEXPRESS` por el nombre de tu instancia si difiere.

**3. Compilar y ejecutar**
```
Abrir: Primer Parcial - Bolivar Cruz Magali.sln
Build → Rebuild Solution  (Ctrl+Shift+B)
Run                        (F5)
```

---

## 🗄️ Modelo de datos (resumen)

```
UnidadDeVenta ──┬── ArticuloIndividual
                └── Lote
                      │
                   Subasta ──── Puja ──── Postor
                      │                     │
                   Adjudicacion          Suscripcion
                      │
                   Bitacora
```

---

## 📁 Estructura del repositorio

```
/
├── BD/                          Script SQL + usuarios.txt
├── BE/                          Entidades del dominio
├── DAL/                         Acceso a datos (ADO.NET)
├── BLL/                         Lógica de negocio + Interfaces
│   └── Interfaces/
├── Servicios/                   Observer + Reporte + Bitácora
├── Seguridad/                   SessionManager + Encriptador
└── Primer Parcial - Bolivar Cruz Magali/
    ├── GUI.csproj
    ├── Program.cs
    ├── Login.cs / Menu.cs
    ├── Frm*.cs                  Formularios operativos
    └── App.config
```

---

## 👩‍💻 Autora

**Magali Bolivar Cruz** — Ingeniería de Software · UAI 2026
