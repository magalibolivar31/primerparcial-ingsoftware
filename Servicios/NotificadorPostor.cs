using System.Diagnostics;

namespace Servicios
{
    // PATRON OBSERVER — Observer concreto (Concrete Observer).
    // Equivalente a Usuario del ejemplo de catedra (patr-n-observer-c-shar).
    // Recibe la notificacion cuando cambia el precio o se cierra la subasta.
    //
    // Esta implementacion registra el evento en el Debug output.
    // La GUI implementara su propio IObserverPostor (formularios) para mostrar
    // las notificaciones en pantalla, igual que en WardrobeFlow donde los
    // formularios implementan IIdiomaObserver.
    public class NotificadorPostor : IObserverPostor
    {
        private readonly BE.Postor _postor;

        public NotificadorPostor(BE.Postor postor)
        {
            _postor = postor;
        }

        // RF-06 / RF-07: recibe la actualizacion del Subject (GestorNotificaciones).
        public void Actualizar(BE.Subasta subasta)
        {
            string mensaje;
            if (subasta.EstaCerrada)
            {
                mensaje = subasta.EsDesierta
                    ? $"[{_postor.Nombre}] La subasta '{subasta.NombreUnidad}' cerró sin ofertas (desierta)."
                    : $"[{_postor.Nombre}] La subasta '{subasta.NombreUnidad}' cerró. Ganador: {subasta.NombreGanador} — Precio final: ${subasta.PrecioFinal:N2}";
            }
            else
            {
                mensaje = $"[{_postor.Nombre}] Nueva puja en '{subasta.NombreUnidad}': precio vigente ${subasta.PrecioVigente:N2}";
            }

            Debug.WriteLine($"[NOTIFICACION] {mensaje}");
        }

        public override string ToString() => _postor.Nombre;
    }
}
