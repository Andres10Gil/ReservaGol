using ReservaGol.Modelos;

namespace ReservaGol.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<Usuario>> ObtenerUsuario();
        Task<Usuario> ObtenerUsuario(Guid id);
        Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
        Task<bool> CrearUsuarios(Usuario usuario);
        Task<bool> RegistrarUsuario(Usuario usuario);
        Task<bool> ActualizarUsuarios(Usuario usuario);
        Task<bool> EliminarUsuarios(Guid id);
    }
}
