using ReservaGol.Modelos;

namespace ReservaGol.Repositorios.Interfaces
{
    public interface IRolesRepositorio
    {
        Task<List<Roles>> ObtenerRoles();
        Task<Roles> ObtenerRoles(Guid id);
        Task<bool> CrearRoles(Roles rol);
        Task<bool> EliminarRoles(Guid id);
        Task<bool> ActualizarRoles(Roles rol);
    }
}
