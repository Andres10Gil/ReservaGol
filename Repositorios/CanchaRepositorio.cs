using Microsoft.EntityFrameworkCore;
using ReservaGol.context;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Repositorios
{
    public class CanchaRepositorio : ICanchaRepositorio
    {
        private readonly BdReservaGolContext _context;

        public CanchaRepositorio(BdReservaGolContext context)
        {
            _context = context;
        }

        public async Task<List<Cancha>> ObtenerCancha() =>
            await _context.Canchas.ToListAsync();

        public async Task<Cancha> ObtenerCancha(Guid id) =>
            await _context.Canchas.FirstOrDefaultAsync(x => x.Id_Canchas == id);

        public async Task<bool> CrearCancha(Cancha cancha)
        {
            cancha.Id_Canchas = Guid.NewGuid();
            _context.Canchas.Add(cancha);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCancha(Guid id)
        {
            var existente = await _context.Canchas.FirstOrDefaultAsync(x => x.Id_Canchas == id);
            if (existente == null) return false;
            _context.Canchas.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarCancha(Cancha cancha)
        {
            var existente = await _context.Canchas.FirstOrDefaultAsync(x => x.Id_Canchas == cancha.Id_Canchas);
            if (existente == null) return false;
            existente.Nombre = cancha.Nombre;
            existente.Ubicacion = cancha.Ubicacion;
            existente.Dimenciones = cancha.Dimenciones;
            existente.Precio_Hora = cancha.Precio_Hora;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
