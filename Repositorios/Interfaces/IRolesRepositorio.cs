using ReservaGol.Modelos;

namespace ReservaGol.Repositorios.Interfaces
{
    public interface IRolesRepositorio
    {
        Task<List<Roles>> ObtenerRoles();
        Task<Roles> ObtenerRoles(Guid id);
    }
}
