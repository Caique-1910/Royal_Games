using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Exceptions;
using System.Security.Claims;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        private int ObterUsuarioIdLogado()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idTexto))
            {
                throw new DomainException("Usuario não autenticado");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult <List<LerJogoDTO>> Listar()
        {
            List<LerJogoDTO> jogos = _service.Listar();
            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDTO> ObterPorId(int id)
        {
            LerJogoDTO jogo = _service.ObterPorId(id);

            if (jogo == null)
            {
                return NotFound();
            }

            return Ok(jogo);
        }

        [HttpGet("{id}/imagens")]
        public ActionResult<LerJogoDTO> ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);
                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Consumes("multipart/form-data")] //Indica que recebe dados no formato multipart/form-data necessario quando enviamos arquivos
        [Authorize]
        public ActionResult Adicionar([FromForm] CriarJogoDTO jogodto)
        {
            try
            {
                int usuarioId = ObterUsuarioIdLogado();
                _service.Adicionar(jogodto, usuarioId);
                return StatusCode(201); // Created
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Atualizar([FromForm] AtualizarJogoDTO jogoDto)
        {
            try
            {
               int usuarioId = ObterUsuarioIdLogado();
                _service.Atualizar(jogoDto, usuarioId);
                return NoContent(); // 204 No Content
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent(); // 204 No Content
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
