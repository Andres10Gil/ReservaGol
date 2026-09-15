using ReservaGol.Modelos;

namespace ReservaGol.Repositorios.Interfaces
{
    public interface ICanchaRepositorio
    {
        Task<List<Cancha>> ObtenerCancha();
        Task<Cancha> ObtenerCancha(Guid id);
        Task<bool> CrearCancha(Cancha cancha);
        Task<bool> EliminarCancha(Guid id);
    }
}
