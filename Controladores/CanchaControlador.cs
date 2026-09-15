using Microsoft.AspNetCore.Mvc;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanchaControlador : ControllerBase
    {
        private readonly ICanchaRepositorio _canchaRepositorio;
        private readonly IReservaRepositorio _reservaRepositorio;

        public CanchaControlador(ICanchaRepositorio canchaRepositorio, IReservaRepositorio reservaRepositorio)
        {
            _canchaRepositorio = canchaRepositorio;
            _reservaRepositorio = reservaRepositorio;
        }

        [HttpGet("ObtenerCancha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCancha()
        {
            var canchas = await _canchaRepositorio.ObtenerCancha();
            if (canchas == null || !canchas.Any())
                return NotFound("No se encontraron canchas.");
            return Ok(canchas);
        }

        [HttpGet("ObtenerCanchaPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCanchaById(Guid id)
        {
            var cancha = await _canchaRepositorio.ObtenerCancha(id);
            if (cancha == null)
                return NotFound("Cancha no encontrada.");
            return Ok(cancha);
        }

        [HttpPost("CrearCancha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearCancha([FromBody] Cancha cancha)
        {
            var resultado = await _canchaRepositorio.CrearCancha(cancha);
            if (!resultado)
                return BadRequest("No se pudo crear la cancha.");
            return Ok("Cancha creada correctamente.");
        }

        [HttpDelete("EliminarCancha/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarCancha(Guid id)
        {
            var resultado = await _canchaRepositorio.EliminarCancha(id);
            if (!resultado) return NotFound("Cancha no encontrada.");
            return Ok("Cancha eliminada correctamente.");
        }

        [HttpGet("Estadisticas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EstadisticasCancha(Guid id)
        {
            var cancha = await _canchaRepositorio.ObtenerCancha(id);
            if (cancha == null) return NotFound("Cancha no encontrada.");

            var reservas = await _reservaRepositorio.ObtenerReservasPorCancha(id);
            var horariosOcupados = reservas
                .Select(r => r.Hora_inicio.ToString(@"hh\:mm"))
                .Distinct()
                .OrderBy(h => h)
                .ToArray();

            return Ok(new
            {
                nombre = cancha.Nombre,
                precioHora = cancha.Precio_Hora,
                totalReservas = reservas.Count,
                horariosOcupados
            });
        }
    }
}
