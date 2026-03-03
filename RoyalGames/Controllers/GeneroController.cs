using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.GeneroDto;
using VHBurguer.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroService _service;

        public GeneroController(GeneroService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerGeneroDto>> Listar()
        {
            List<LerGeneroDto> generos = _service.Listar();
            return Ok(generos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerGeneroDto> ObterPorId(int id)
        {
            LerGeneroDto genero = _service.ObterPorId(id);

            if(genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarGeneroDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarGeneroDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch(DomainException ex)
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
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
