using Microsoft.AspNetCore.Mvc;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaControladores : ControllerBase
    {
        private readonly IReservaRepositorio _reservaRepositorio;

        public ReservaControladores(IReservaRepositorio reservaRepositorio)
        {
            _reservaRepositorio = reservaRepositorio;
        }

        [HttpGet("ObtenerReservas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllReserva()
        {
            var reservas = await _reservaRepositorio.ObtenerReserva();
            if (reservas == null || !reservas.Any())
                return NotFound("No se encontraron reservas.");
            return Ok(reservas);
        }

        [HttpGet("ObtenerReservaPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservaById(Guid id)
        {
            var reserva = await _reservaRepositorio.ObtenerReserva(id);
            if (reserva == null)
                return NotFound("Reserva no encontrada.");
            return Ok(reserva);
        }

        [HttpPost("CrearReserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearReserva([FromBody] Reserva reserva)
        {
            var resultado = await _reservaRepositorio.CrearReserva(reserva);
            if (!resultado)
                return BadRequest("No se pudo crear la reserva.");
            return Ok("Reserva creada correctamente.");
        }

        [HttpDelete("EliminarReserva/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarReserva(Guid id)
        {
            var resultado = await _reservaRepositorio.EliminarReserva(id);
            if (!resultado)
                return BadRequest("No se pudo eliminar la reserva.");
            return Ok("Reserva eliminada correctamente.");
        }

        [HttpPut("ActualizarReserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarReserva([FromBody] Reserva reserva)
        {
            var resultado = await _reservaRepositorio.ActualizarReserva(reserva);
            if (!resultado)
                return BadRequest("No se pudo actualizar la reserva.");
            return Ok("Reserva actualizada correctamente.");
        }
    }
}
