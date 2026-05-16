namespace Servicios
{
    // PATRON OBSERVER — Interfaz del Observador (Observer).
    // Equivalente a IObserverUsuario del ejemplo de catedra.
    // Todo interesado que quiera recibir notificaciones de una subasta
    // debe implementar esta interfaz y registrarse en GestorNotificaciones.
    public interface IObserverPostor
    {
        void Actualizar(BE.Subasta subasta);
    }
}
