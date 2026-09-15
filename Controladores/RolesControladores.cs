using Microsoft.AspNetCore.Mvc;
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
    }
}
