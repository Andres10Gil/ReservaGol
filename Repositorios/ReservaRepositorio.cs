using Microsoft.EntityFrameworkCore;
using ReservaGol.context;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Repositorios
{
    public class ReservaRepositorio : IReservaRepositorio
    {
        private readonly BdReservaGolContext _context;

        public ReservaRepositorio(BdReservaGolContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerReserva() =>
            await _context.Reserva.ToListAsync();

        public async Task<Reserva> ObtenerReserva(Guid id) =>
            await _context.Reserva.FirstOrDefaultAsync(x => x.Id_Reserva == id);

        public async Task<List<Reserva>> ObtenerReservasPorCancha(Guid idCancha) =>
            await _context.Reserva.Where(r => r.Id_Cancha == idCancha).ToListAsync();

        public async Task<bool> CrearReserva(Reserva reserva)
        {
            reserva.Id_Reserva = Guid.NewGuid();
            _context.Reserva.Add(reserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarReserva(Guid id)
        {
            var existente = await _context.Reserva.FirstOrDefaultAsync(x => x.Id_Reserva == id);
            if (existente == null) return false;
            _context.Reserva.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarReserva(Reserva reserva)
        {
            var existente = await _context.Reserva.FirstOrDefaultAsync(x => x.Id_Reserva == reserva.Id_Reserva);
            if (existente == null) return false;
            existente.Fecha_reserva = reserva.Fecha_reserva;
            existente.Hora_inicio = reserva.Hora_inicio;
            existente.Hora_fin = reserva.Hora_fin;
            existente.Estado = reserva.Estado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
