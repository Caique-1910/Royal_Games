using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult <TokenDTO> Login (LoginDTO loginDTO)
        {
            try
            {
                var token = _service.Login(loginDTO);
                return Ok(token);
            }
            catch (DomainException ex)
            {
                return BadRequest(new{message = ex.Message});
            }
        }
    }
}
