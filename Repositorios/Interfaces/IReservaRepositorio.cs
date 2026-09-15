using ReservaGol.Modelos;

namespace ReservaGol.Repositorios.Interfaces
{
    public interface IReservaRepositorio
    {
        Task<List<Reserva>> ObtenerReserva();
        Task<Reserva> ObtenerReserva(Guid id);
        Task<List<Reserva>> ObtenerReservasPorCancha(Guid idCancha);
        Task<bool> CrearReserva(Reserva reserva);
        Task<bool> ActualizarReserva(Reserva reserva);
        Task<bool> EliminarReserva(Guid id);
    }
}
