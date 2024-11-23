using GlobalSolution.Models;
using GlobalSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.Authenticate(request.Email, request.Password);
                if (user == null)
                    return Unauthorized("Credenciais inválidas.");

                return Ok(new
                {
                    Message = "Autenticação realizada com sucesso!",
                    User = new { user.Id, user.Nome, user.Email }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação. Tente novamente mais tarde.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = new User
                {
                    Nome = request.Name,
                    Email = request.Email
                };

                var result = await _authService.Register(newUser, request.Password);
                if (!result)
                    return Conflict("E-mail já está em uso.");

                return Ok("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar a solicitação. Tente novamente mais tarde.");
            }
        }
    }
}
