using System;
using System.Collections.Generic;

namespace BLL
{
    public class PostorBLL : Interfaces.IPostorService
    {
        private readonly DAL.PostorDAL      _dalPostor  = new DAL.PostorDAL();
        private readonly DAL.SuscripcionDAL _dalSusc    = new DAL.SuscripcionDAL();
        private readonly DAL.SubastaDAL     _dalSubasta = new DAL.SubastaDAL();

        private readonly GestorPujas _gestorPujas = GestorPujas.GetInstance();

        public List<BE.Postor> ObtenerTodos() => _dalPostor.ObtenerTodos();

        public BE.Postor ObtenerPorId(int id) => _dalPostor.ObtenerPorId(id);

        public int Alta(BE.Postor postor)
        {
            Validar(postor);
            if (_dalPostor.ExisteDniCuit(postor.DniCuit))
                throw new Exception($"Ya existe un postor con DNI/CUIT {postor.DniCuit}.");
            postor.FechaAlta = DateTime.Now;
            int id = _dalPostor.Alta(postor);
            postor.Id = id;
            return id;
        }

        public void Modificar(BE.Postor postor)
        {
            Validar(postor);
            _dalPostor.Modificar(postor);
        }

        public void Baja(int id)
        {
            _dalPostor.Baja(id);
        }

        // RF-05: suscribir un postor a una subasta activa.
        // Persiste en BD y registra el observer en el GestorPujas (Singleton).
        public void Suscribir(int idPostor, int idSubasta)
        {
            var subasta = _dalSubasta.ObtenerPorId(idSubasta);
            if (subasta == null)
                throw new Exception($"Subasta {idSubasta} no encontrada.");
            if (!subasta.EstaActiva)
                throw new Exception("Solo se puede suscribir a subastas en estado Activo.");
            if (_dalSusc.ExisteSuscripcionActiva(idPostor, idSubasta))
                throw new Exception("El postor ya está suscripto a esta subasta.");

            var postor = _dalPostor.ObtenerPorId(idPostor);
            if (postor == null)
                throw new Exception($"Postor {idPostor} no encontrado.");

            _dalSusc.Alta(new BE.Suscripcion
            {
                IdPostor         = idPostor,
                IdSubasta        = idSubasta,
                FechaSuscripcion = DateTime.Now,
                Activa           = true
            });

            // Registrar el observer en el GestorPujas para notificaciones en tiempo real.
            _gestorPujas.AgregarObserver(idSubasta, new Servicios.NotificadorPostor(postor));
        }

        // RF-08: dar de baja la suscripcion.
        public void Desuscribir(int idPostor, int idSubasta)
        {
            if (!_dalSusc.ExisteSuscripcionActiva(idPostor, idSubasta))
                throw new Exception("No existe una suscripción activa para ese postor en esa subasta.");
            _dalSusc.Baja(idPostor, idSubasta);
        }

        public List<BE.Suscripcion> ObtenerSuscripcionesActivas(int idSubasta)
            => _dalSusc.ObtenerActivasPorSubasta(idSubasta);

        private void Validar(BE.Postor p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (string.IsNullOrWhiteSpace(p.Nombre))
                throw new Exception("El nombre del postor es obligatorio.");
            if (string.IsNullOrWhiteSpace(p.DniCuit))
                throw new Exception("El DNI/CUIT del postor es obligatorio.");
            if (string.IsNullOrWhiteSpace(p.Email))
                throw new Exception("El email del postor es obligatorio.");
        }
    }
}
