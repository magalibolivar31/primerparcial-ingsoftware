namespace Servicios
{
    // PATRON OBSERVER — Interfaz del Sujeto (Subject).
    // Equivalente a ISujetoProducto del ejemplo de catedra.
    // Define el contrato para registrar/quitar observadores y notificarlos.
    public interface ISujetoSubasta
    {
        void Agregar(IObserverPostor observer);
        void Quitar(IObserverPostor observer);
        void Notificar();
    }
}
