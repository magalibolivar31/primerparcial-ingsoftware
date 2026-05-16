using System;
using System.Collections.Generic;

namespace Servicios
{
    // PATRON OBSERVER — Sujeto concreto (Concrete Subject).
    // Equivalente a Producto del ejemplo de catedra y a GestorIdioma de WardrobeFlow.
    //
    // Gestiona las suscripciones de observadores por subasta.
    // Cuando el precio de una subasta cambia o la subasta se cierra,
    // BLL llama a Notificar() y todos los observadores registrados
    // reciben Actualizar(subasta) de forma automatica (push, no pull).
    //
    // RF-05: suscribirse a una subasta.
    // RF-06: notificacion automatica ante cambio de precio.
    // RF-07: notificacion de resultado al cierre.
    // RF-08: dar de baja la suscripcion en cualquier momento.
    public class GestorNotificaciones : ISujetoSubasta
    {
        private readonly List<IObserverPostor> _observers = new List<IObserverPostor>();
        private BE.Subasta _subasta;

        public GestorNotificaciones(BE.Subasta subasta)
        {
            _subasta = subasta;
        }

        // RF-05: registra un observador para la subasta.
        public void Agregar(IObserverPostor observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        // RF-08: elimina un observador; deja de recibir alertas de forma inmediata.
        public void Quitar(IObserverPostor observer)
        {
            _observers.Remove(observer);
        }

        // RF-06 / RF-07: notifica a todos los observadores el estado actual de la subasta.
        public void Notificar()
        {
            // Iterar sobre copia para evitar problemas si un observer se da de baja durante la notificacion.
            var copia = new List<IObserverPostor>(_observers);
            foreach (var observer in copia)
            {
                try
                {
                    observer.Actualizar(_subasta);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[GestorNotificaciones] Error al notificar observer: {ex.Message}");
                }
            }
        }

        // Actualiza la referencia a la subasta (necesario tras cambios de precio o cierre).
        public void ActualizarSubasta(BE.Subasta subasta)
        {
            _subasta = subasta;
        }

        public int CantidadObservers => _observers.Count;
    }
}
