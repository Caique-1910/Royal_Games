using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo.Include(jogo => jogo.Genero).Include(jogo => jogo.Plataforma).Include(jogo => jogo.Usuario).ToList();
            return jogos;
        }

        public Jogo ObterPorId (int id) 
        {
            Jogo? jogo = _context.Jogo.Include(jogo => jogo.Genero).Include(jogo => jogo.Plataforma).Include(jogo => jogo.Usuario).FirstOrDefault(jogo => jogo.JogoID == id);
            return jogo;
        }

        public bool JogoExiste (string nome, int? produtoIdAtual = null)
        {

            var jogoConsultado = _context.Jogo.AsQueryable();

            if (produtoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != produtoIdAtual.Value);
            }
            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterPorImagem(int id)
        {
            var jogo = _context.Jogo.Where(jogo => jogo.JogoID == id).Select(jogo => jogo.Imagem).FirstOrDefault();
            return jogo;
        }

        public void Adicionar (Jogo jogo,  List<int> generoIds, List<int> plataformaIds)
        {
            List<Genero> generos = _context.Genero.Where(genero => generoIds.Contains(genero.GeneroID)).ToList();
            List<Plataforma> plataformas = _context.Plataforma.Where(plataforma => plataformaIds.Contains(plataforma.PlataformaId)).ToList();

            jogo.Genero = generos;
            jogo.Plataforma = plataformas;
            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar (Jogo jogo, List<int> generoIds, List<int> plataformaIds)
        {
            Jogo? jogoBanco = _context.Jogo.Include(jogo => jogo.Genero).Include(jogo => jogo.Plataforma).FirstOrDefault(jogoAux => jogoAux.JogoID == jogo.JogoID);


            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;
            jogoBanco.ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if(jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

           
            var generos = _context.Genero.Where(genero => generoIds.Contains(genero.GeneroID)).ToList();
            var plataformas = _context.Plataforma.Where(plataforma => plataformaIds.Contains(plataforma.PlataformaId)).ToList();

            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
            }

            foreach (var plataforma in plataformas)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }

            _context.SaveChanges();
        }

        public void Remover (int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogoAux => jogoAux.JogoID == id);

            if (jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }
    }
}
