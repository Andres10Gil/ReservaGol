using Microsoft.EntityFrameworkCore;
using ReservaGol.context;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;
using ReservaGol.Servicios.Interfaces;

namespace ReservaGol.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BdReservaGolContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioRepositorio(BdReservaGolContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<Usuario>> ObtenerUsuario() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario> ObtenerUsuario(Guid id) =>
            await _context.Usuarios.FirstOrDefaultAsync(x => x.Id_Usuario == id);

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo) =>
            await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == correo);

        public async Task<bool> CrearUsuarios(Usuario usuario)
        {
            usuario.Id_Usuario = Guid.NewGuid();
            usuario.Contraseña = _passwordHasher.HashPassword(usuario.Contraseña);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            var rolCliente = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre_rol == "Cliente");
            if (rolCliente == null)
            {
                rolCliente = new Roles
                {
                    Id_Roles = Guid.NewGuid(),
                    Nombre_rol = "Cliente",
                    Descripcion = "Usuario registrado desde el sitio público",
                    Nivel_acceso = 1,
                    Activo = true,
                    Creando_em = DateTime.UtcNow,
                };
                _context.Roles.Add(rolCliente);
            }

            usuario.Id_Usuario = Guid.NewGuid();
            usuario.Id_Roles = rolCliente.Id_Roles;
            usuario.Contraseña = _passwordHasher.HashPassword(usuario.Contraseña);
            usuario.Fecha_registro = DateTime.UtcNow;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarUsuarios(Guid id)
        {
            var existente = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id_Usuario == id);
            if (existente == null) return false;
            _context.Usuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarUsuarios(Usuario usuario)
        {
            var existente = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id_Usuario == usuario.Id_Usuario);
            if (existente == null) return false;
            existente.Nombre = usuario.Nombre;
            existente.Correo = usuario.Correo;
            existente.Telefono = usuario.Telefono;
            existente.Contraseña = _passwordHasher.HashPassword(usuario.Contraseña);
            existente.Fecha_registro = usuario.Fecha_registro;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
