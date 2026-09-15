using Microsoft.AspNetCore.Mvc;
using ReservaGol.Modelos;
using ReservaGol.Repositorios.Interfaces;
using ReservaGol.Servicios.Interfaces;

namespace ReservaGol.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutControlador : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AutControlador(IUsuarioRepositorio usuarioRepositorio, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario) || string.IsNullOrEmpty(login.Contraseña))
                return Unauthorized("Credenciales inválidas.");

            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(login.NombreUsuario);

            if (usuario == null || !_passwordHasher.VerifyPassword(login.Contraseña, usuario.Contraseña))
                return Unauthorized("Usuario o contraseña incorrectos.");

            var token = _jwtService.GenerarToken(usuario);
            return Ok(new { token });
        }
    }
}
