using Microsoft.EntityFrameworkCore;
using ReservaGol.context;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Repositorios
{
    public class RolesRepositorio : IRolesRepositorio
    {
        private readonly BdReservaGolContext _context;

        public RolesRepositorio(BdReservaGolContext context)
        {
            _context = context;
        }

        public async Task<List<Roles>> ObtenerRoles() =>
            await _context.Roles.ToListAsync();

        public async Task<Roles> ObtenerRoles(Guid id) =>
            await _context.Roles.FirstOrDefaultAsync(x => x.Id_Roles == id);

        public async Task<bool> CrearRoles(Roles rol)
        {
            rol.Id_Roles = Guid.NewGuid();
            rol.Creando_em = DateTime.UtcNow;
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarRoles(Guid id)
        {
            var existente = await _context.Roles.FirstOrDefaultAsync(x => x.Id_Roles == id);
            if (existente == null) return false;
            _context.Roles.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarRoles(Roles rol)
        {
            var existente = await _context.Roles.FirstOrDefaultAsync(x => x.Id_Roles == rol.Id_Roles);
            if (existente == null) return false;
            existente.Nombre_rol = rol.Nombre_rol;
            existente.Descripcion = rol.Descripcion;
            existente.Nivel_acceso = rol.Nivel_acceso;
            existente.Activo = rol.Activo;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
