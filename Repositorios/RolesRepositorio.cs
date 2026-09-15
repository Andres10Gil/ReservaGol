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
    }
}
