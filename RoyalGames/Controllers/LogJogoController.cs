using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Repositories;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoRepository _service;

        public LogJogoController(LogAlteracaoJogoRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("jogo/{id}")]
        public ActionResult ListarJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }
    }
}
