using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerUsuarioDTO>> Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDTO> ObterPorId(int id)
        {
            try
            {
                return Ok(_service.ObterPorid(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDTO> ObterPorEmail(string email)
        {
            try
            {
                return Ok(_service.ObterPorEmail(email));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LerUsuarioDTO> Adicionar(CriarUsuarioDTO usuarioDTO)
        {
            try
            {
                LerUsuarioDTO usuarioCriado = _service.Adicionar(usuarioDTO);
                return CreatedAtAction(nameof(ObterPorId), new { id = usuarioCriado.UsuarioID }, usuarioCriado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDTO> Atualizar(int id, CriarUsuarioDTO usuarioDTO)
        {
            try
            {
                LerUsuarioDTO usuarioAtualizado = _service.Atualizar(id, usuarioDTO);
                return StatusCode(200, usuarioAtualizado);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Deletar(int id)
        {
            try
            {
                _service.Deletar(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
