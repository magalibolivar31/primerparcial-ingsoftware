using System;
using System.Collections.Generic;

namespace BE
{
    // PATRON COMPOSITE — Componente abstracto.
    // Raiz de la jerarquia del catalogo. ArticuloIndividual (hoja) y Lote (nodo)
    // heredan de esta clase, lo que permite tratarlos de forma uniforme.
    public abstract class UnidadDeVenta
    {
        public int    Id          { get; set; }
        public string Nombre      { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool   Activo      { get; set; }
        public int?   IdLotePadre { get; set; }

        // RF-03: calculo automatico y recursivo del precio base.
        public abstract decimal ObtenerPrecioBase();

        // RF-04: descripcion completa; en Lote devuelve el desglose recursivo.
        public abstract string ObtenerDescripcion();

        // Operacion de composicion — solo Lote la implementa con logica real.
        public abstract void AgregarComponente(UnidadDeVenta unidad);

        public abstract List<UnidadDeVenta> ObtenerHijos();
    }
}
