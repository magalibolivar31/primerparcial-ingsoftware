# La Almoneda Nacional — Primer Parcial Ingeniería de Software · UAI 2026

**Alumna:** Bolivar Cruz, Magali  
**Docente:** Juan Ignacio Silva  
**Sistema:** Gestión de subastas con catálogo jerárquico, pujas en tiempo real y reportes de jornada.

---

## Solución — Proyectos

| Proyecto | Tipo | Responsabilidad |
|----------|------|----------------|
| `BE` | Class Library | Entidades del dominio (sin dependencias externas) |
| `DAL` | Class Library | Acceso a SQL Server via ADO.NET puro |
| `Servicios` | Class Library | Observer + Reporte de Jornada + Bitácora |
| `Seguridad` | Class Library | Sesión (Singleton) + Hash SHA-256 |
| `BLL` | Class Library | Lógica de negocio + interfaces + `GestorPujas` (Singleton) |
| `GUI` | WinExe | WinForms MDI — formularios operativos |

Dependencias: `GUI → BLL → Servicios / Seguridad → DAL → BE`

---

## Patrones implementados

### Composite — RF-01 al RF-04, RF-13

**Problema:** el catálogo contiene artículos individuales y lotes que agrupan otros artículos o lotes. El precio base y la descripción deben calcularse de forma uniforme sin importar la profundidad del árbol.

```
BE.UnidadDeVenta          ← Component (abstracto)
├── BE.ArticuloIndividual ← Leaf       → ObtenerPrecioBase() = ValorDeclarado
└── BE.Lote               ← Composite  → ObtenerPrecioBase() = Σ hijos (recursivo)
```

`DAL.UnidadDeVentaDAL` discrimina el tipo por la columna `Tipo` (`ARTICULO` / `LOTE`).  
`BLL.CatalogoBLL.ConstruirArbolDesde(id)` reconstituye el árbol en memoria con recursión.  
`Servicios.ReporteJornada` reutiliza la misma recursión para generar el informe de jornada.

---

### Observer — RF-05 al RF-08

**Problema:** cuando se acepta una puja, todos los postores suscriptos a esa subasta deben recibir la notificación de inmediato.

```
Servicios.ISujetoSubasta          ← Subject interface
Servicios.IObserverPostor         ← Observer interface
Servicios.GestorNotificaciones    ← Concrete Subject  (uno por subasta activa)
Servicios.NotificadorPostor       ← Concrete Observer (uno por postor suscripto)
```

`BLL.SubastaBLL.AbrirSubasta()` crea un `GestorNotificaciones` para la nueva subasta.  
`BLL.PostorBLL.Suscribir()` crea un `NotificadorPostor` y lo registra en el gestor.  
`BLL.GestorPujas.RegistrarPuja()` llama a `gestor.Notificar()` tras aceptar la puja.

---

### Singleton — RF-09

**Problema:** múltiples hilos pueden intentar registrar pujas sobre la misma subasta al mismo tiempo, o acceder simultáneamente a la base de datos / a la sesión del usuario.

Todos implementan **double-checked locking** (`volatile` + `lock`):

| Clase | Lock | Garantía |
|-------|------|----------|
| `BLL.GestorPujas` | `_pujaLock` separado del lock de instancia | Pujas serializadas; el segundo hilo ve el precio ya actualizado por el primero y es rechazado |
| `DAL.Acceso` | `_lock` de instancia | Una sola cadena de conexión; connection pool de ADO.NET |
| `Seguridad.SessionManager` | `_lock` de instancia | `GetInstance` lanza si no hay sesión activa; `Login` / `Logout` bajo lock |

---

## Formularios GUI

| Formulario | Acceso por rol | Patrón que demuestra |
|-----------|---------------|---------------------|
| `Login` | — | Seguridad / SessionManager |
| `Menu` (MDI) | Todos | Filtro por rol en `ConfigurarMenuPorRol()` |
| `FrmCatalogo` | Martillero, Admin | Composite — TreeView jerárquico |
| `FrmNuevoArticulo` | Martillero, Admin | Composite — alta de hoja |
| `FrmNuevoLote` | Martillero, Admin | Composite — alta de nodo |
| `FrmSubastas` | Martillero, Admin | Observer (apertura crea Subject) |
| `FrmRegistrarPuja` | Operador, Admin | Singleton + Observer (puja → notifica) |
| `FrmPostores` | Operador, Admin | Observer (suscripción registra Observer) |
| `FrmReporteJornada` | Todos | Composite — recorrido recursivo |
| `FrmGestionUsuarios` | Admin | Seguridad — desbloqueo y reset |

---

## Base de datos

Script completo en `BD/AlmonedaNacional.sql`.  
Cadena de conexión en `Primer Parcial - Bolivar Cruz Magali/App.config` → `AlmonedaNacionalDB`.  
Usuarios seed y contraseñas de prueba en `BD/Usuarios.txt`.
