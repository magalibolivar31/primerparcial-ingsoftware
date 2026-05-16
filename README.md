# La Almoneda Nacional — Primer Parcial Ingeniería de Software · UAI 2026

**Alumna:** Bolivar Cruz, Magali  
**Docente:** Juan Ignacio Silva  
**Sistema:** Gestión de subastas con catálogo jerárquico, pujas en tiempo real y reportes de jornada.

---

## Proyectos de la solución

| Proyecto | Tipo | Responsabilidad |
|----------|------|----------------|
| `BE` | Class Library | Entidades del dominio (sin dependencias externas) |
| `DAL` | Class Library | Acceso a SQL Server via ADO.NET puro |
| `BLL` | Class Library | Lógica de negocio + interfaces + `GestorPujas` (Singleton) |
| `Servicios` | Class Library | Observer + Reporte de Jornada + Bitácora |
| `Seguridad` | Class Library | Sesión (Singleton) + Hash SHA-256 |
| `GUI` | WinExe | WinForms MDI — formularios operativos |

**Flujo de dependencias:**
```
GUI       → BLL → DAL → BE
Servicios → BLL → DAL → BE
```

---

## Patrones implementados

### Composite — RF-01, RF-02, RF-03, RF-04, RF-13

**Problema:** el catálogo contiene artículos individuales y lotes que agrupan otros artículos o lotes. El precio base y la descripción deben calcularse de forma uniforme sin importar la profundidad del árbol.

```
BE.UnidadDeVenta          ← Component (abstracto)
├── BE.ArticuloIndividual ← Leaf       → ObtenerPrecioBase() = ValorDeclarado
└── BE.Lote               ← Composite  → ObtenerPrecioBase() = Σ hijos (recursivo)
```

- `DAL.UnidadDeVentaDAL` discrimina el tipo por la columna `Tipo` (`ARTICULO` / `LOTE`).
- `BLL.CatalogoBLL.ConstruirArbolDesde(id)` reconstituye el árbol en memoria con recursión.
- `Servicios.ReporteJornada` reutiliza la misma recursión para generar el informe de jornada (RF-13).

---

### Observer — RF-05, RF-06, RF-07, RF-08

**Problema:** cuando se acepta una puja, todos los postores suscriptos a esa subasta deben recibir la notificación de inmediato, sin que ellos la soliciten.

```
Servicios.ISujetoSubasta          ← Subject interface
Servicios.IObserverPostor         ← Observer interface
Servicios.GestorNotificaciones    ← Concrete Subject  (uno por subasta activa)
Servicios.NotificadorPostor       ← Concrete Observer (uno por postor suscripto)
```

- `BLL.SubastaBLL.AbrirSubasta()` crea un `GestorNotificaciones` y lo registra en el `GestorPujas`.
- `BLL.PostorBLL.Suscribir()` crea un `NotificadorPostor` y lo agrega al gestor de la subasta.
- `BLL.GestorPujas.RegistrarPuja()` llama `gestor.Notificar()` tras aceptar la puja (RF-06).
- `BLL.GestorPujas.NotificarCierre()` notifica el resultado a todos los suscriptores antes de eliminar el gestor (RF-07).
- `BLL.PostorBLL.Desuscribir()` quita el observer de inmediato (RF-08).

---

### Singleton — RF-09

**Problema:** pujas simultáneas pueden adjudicar el mismo lote a dos postores; también se necesita una única conexión a la BD y una única sesión de usuario.

Todos implementan **double-checked locking** (`volatile` + `lock`):

| Clase | Garantía |
|-------|----------|
| `BLL.GestorPujas` | Pujas serializadas con `_pujaLock`; la segunda puja ve el precio ya actualizado y es rechazada |
| `DAL.Acceso` | Una sola cadena de conexión compartida; connection pool de ADO.NET |
| `Seguridad.SessionManager` | Un único usuario activo en memoria; `Login`/`Logout` bajo lock |

---

## Formularios GUI

| Formulario | Rol | Patrón que demuestra |
|-----------|-----|---------------------|
| `Login` | — | Seguridad / SessionManager |
| `Menu` (MDI) | Todos | Menú dinámico por rol en `ConfigurarMenuPorRol()` |
| `FrmCatalogo` | Martillero | Composite — DataGridView + botón "Ver Detalle (Composite)" |
| `FrmNuevoArticulo` | Martillero | Composite — alta de hoja (ArticuloIndividual) |
| `FrmNuevoLote` | Martillero | Composite — alta de nodo (Lote) |
| `FrmSubastas` | Martillero | Observer — apertura crea el Subject (GestorNotificaciones) |
| `FrmRegistrarPuja` | Operador | Singleton + Observer — puja serializada y notificación automática |
| `FrmPostores` | Ambos | Observer — suscripción registra el Observer (NotificadorPostor) |
| `FrmReporteJornada` | Ambos | Composite — recorrido recursivo del catálogo (RF-13) |
| `FrmGestionUsuarios` | Martillero | Seguridad — desbloqueo y reset de contraseña |

---

## Base de datos

Script completo en `BD/AlmonedaNacional.sql`.  
Cadena de conexión en `Primer Parcial - Bolivar Cruz Magali/App.config` → `AlmonedaNacionalDB`.

**Usuarios de prueba** (contraseña: `admin123`):

| Email | Rol |
|-------|-----|
| `martillero` | Martillero |
| `operador` | Operador |
