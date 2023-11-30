using BTZTransportesAPI.Models;
using BTZTransportesAPI.Repositories;
using BTZTransportesAPI.Repositories.Interfaces;
using BTZTransportesAPI.Services;
using BTZTransportesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BTZTransportesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;
        private IAuthService _authService;
        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;

        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            try
            {
                if (!_usuarioRepository.UsuarioESenhaCorretos(usuario.Login, usuario.Senha))
                    return NotFound("Usuário e Senha Inválidos");

                var token = _authService.GetAuthToken(usuario.Login);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }          
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Usuario usuario)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("Auth")]
        public IActionResult Auth()
        {
            return Ok();
        }



    }
}
