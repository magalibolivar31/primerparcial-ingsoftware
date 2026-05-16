using System;
using System.Collections.Generic;

namespace BLL
{
    // PATRON SINGLETON — Control de unicidad de puja y concurrencia (RF-09).
    //
    // Implementacion identica al patron de DAL.Acceso y Seguridad.SessionManager
    // del repositorio de referencia (double-checked locking, sealed, constructor privado).
    //
    // Problema que resuelve: sin este Singleton, dos pujas simultaneas sobre el mismo
    // lote podrian ambas superar el precio vigente y adjudicarse dos veces.
    // El lock interno garantiza que las pujas se procesen de a una: la segunda
    // llega cuando el precio ya fue actualizado por la primera y es rechazada.
    //
    // Ademas actua como coordinador del patron Observer: tras aceptar una puja,
    // notifica al GestorNotificaciones correspondiente (RF-06).
    public sealed class GestorPujas
    {
        private static volatile GestorPujas _instance;
        private static readonly object _singletonLock = new object();

        // Lock por operacion de puja — independiente del lock de instancia.
        private readonly object _pujaLock = new object();

        // Mapa de GestorNotificaciones activos, uno por subasta activa.
        private readonly Dictionary<int, Servicios.GestorNotificaciones> _gestores
            = new Dictionary<int, Servicios.GestorNotificaciones>();

        private GestorPujas() { }

        public static GestorPujas GetInstance()
        {
            if (_instance == null)
            {
                lock (_singletonLock)
                {
                    if (_instance == null)
                        _instance = new GestorPujas();
                }
            }
            return _instance;
        }

        // Registra el GestorNotificaciones de una subasta recien abierta.
        // Llamado desde SubastaBLL.AbrirSubasta().
        public void RegistrarGestor(int idSubasta, Servicios.GestorNotificaciones gestor)
        {
            lock (_pujaLock)
            {
                _gestores[idSubasta] = gestor;
            }
        }

        // Agrega un observer (postor suscripto) al GestorNotificaciones de la subasta.
        public void AgregarObserver(int idSubasta, Servicios.IObserverPostor observer)
        {
            lock (_pujaLock)
            {
                if (_gestores.TryGetValue(idSubasta, out Servicios.GestorNotificaciones g))
                    g.Agregar(observer);
            }
        }

        // Quita un observer (postor que se dio de baja) del GestorNotificaciones.
        public void QuitarObserver(int idSubasta, Servicios.IObserverPostor observer)
        {
            lock (_pujaLock)
            {
                if (_gestores.TryGetValue(idSubasta, out Servicios.GestorNotificaciones g))
                    g.Quitar(observer);
            }
        }

        // RF-09 / RF-10: procesa una puja con exclusion mutua.
        // Valida monto, persiste, actualiza precio y notifica observers.
        // Devuelve la puja con el Id asignado y el estado resultante.
        public BE.Puja RegistrarPuja(BE.Subasta subasta, int idPostor, decimal monto,
                                     DAL.PujaDAL dalPuja, DAL.SubastaDAL dalSubasta)
        {
            lock (_pujaLock)
            {
                var puja = new BE.Puja
                {
                    IdSubasta = subasta.Id,
                    IdPostor  = idPostor,
                    Monto     = monto,
                    FechaHora = DateTime.Now
                };

                // RF-10: la oferta debe ser mayor al precio vigente.
                if (subasta.Estado != BE.EstadoSubasta.Activa)
                {
                    puja.Estado        = BE.EstadoPuja.Rechazada;
                    puja.MotivoRechazo = "La subasta no se encuentra activa.";
                    dalPuja.Insertar(puja);
                    return puja;
                }

                if (monto <= subasta.PrecioVigente)
                {
                    puja.Estado        = BE.EstadoPuja.Rechazada;
                    puja.MotivoRechazo = $"El monto ${monto:N2} no supera el precio vigente ${subasta.PrecioVigente:N2}.";
                    dalPuja.Insertar(puja);
                    return puja;
                }

                // Puja valida: persistir y actualizar precio.
                puja.Estado = BE.EstadoPuja.Aceptada;
                puja.Id     = dalPuja.Insertar(puja);

                subasta.PrecioVigente = monto;
                dalSubasta.ActualizarPrecioVigente(subasta.Id, monto);

                // Notificar a todos los observers suscritos a esta subasta (RF-06).
                if (_gestores.TryGetValue(subasta.Id, out Servicios.GestorNotificaciones gestor))
                {
                    gestor.ActualizarSubasta(subasta);
                    gestor.Notificar();
                }

                return puja;
            }
        }

        // Elimina el gestor de una subasta al cerrarla.
        public void EliminarGestor(int idSubasta)
        {
            lock (_pujaLock)
            {
                _gestores.Remove(idSubasta);
            }
        }
    }
}
