using ReservaGol.Modelos;

namespace ReservaGol.Servicios.Interfaces
{
    public interface IJwtService
    {
        string GenerarToken(Usuario usuario);
    }
}
