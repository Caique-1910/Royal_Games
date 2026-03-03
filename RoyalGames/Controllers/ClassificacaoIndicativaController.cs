using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoIndicativaController : ControllerBase
    {
        private readonly ClassificacaoIndicativaService _service;

        public ClassificacaoIndicativaController(ClassificacaoIndicativaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerClassificaoDTO>> Listar()
        {
            List<LerClassificaoDTO> classificacoes = _service.Listar();
            return Ok(classificacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<LerClassificaoDTO> ObterPorId(int id)
        {
            try
            {
                LerClassificaoDTO classificacao = _service.ObterPorId(id);
                return Ok(classificacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarClassificacaoDTO classificacaoDTO)
        {
            try
            {
                _service.Adicionar(classificacaoDTO);
                return Ok("Criada com sucesso");
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarClassificacaoDTO classificacaoDTO)
        {
            try
            {
                _service.Atualizar(id, classificacaoDTO);
                return NoContent();
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
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
