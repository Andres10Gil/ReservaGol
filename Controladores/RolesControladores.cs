using Microsoft.AspNetCore.Mvc;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;

namespace ReservaGol.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesControladores : ControllerBase
    {
        private readonly IRolesRepositorio _rolesRepositorio;

        public RolesControladores(IRolesRepositorio rolesRepositorio)
        {
            _rolesRepositorio = rolesRepositorio;
        }

        [HttpGet("ObtenerRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _rolesRepositorio.ObtenerRoles();
            if (roles == null || !roles.Any())
                return NotFound("No se encontraron roles.");
            return Ok(roles);
        }

        [HttpGet("ObtenerRolesPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRolesById(Guid id)
        {
            var rol = await _rolesRepositorio.ObtenerRoles(id);
            if (rol == null)
                return NotFound("Rol no encontrado.");
            return Ok(rol);
        }

        [HttpPost("CrearRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearRol([FromBody] Roles rol)
        {
            var resultado = await _rolesRepositorio.CrearRoles(rol);
            if (!resultado)
                return BadRequest("No se pudo crear el rol.");
            return Ok("Rol creado correctamente.");
        }

        [HttpDelete("EliminarRol/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarRol(Guid id)
        {
            var resultado = await _rolesRepositorio.EliminarRoles(id);
            if (!resultado)
                return BadRequest("No se pudo eliminar el rol.");
            return Ok("Rol eliminado correctamente.");
        }

        [HttpPut("ActualizarRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarRol([FromBody] Roles rol)
        {
            var resultado = await _rolesRepositorio.ActualizarRoles(rol);
            if (!resultado)
                return BadRequest("No se pudo actualizar el rol.");
            return Ok("Rol actualizado correctamente.");
        }
    }
}
