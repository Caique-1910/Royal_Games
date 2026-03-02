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
            List<Jogo> jogos = _context.Jogo.Include(jogo => jogo.Genero).Include(jogo => jogo.Usuario).ToList();
            return jogos;
        }

        public Jogo ObterPorId (int id) 
        {
            Jogo? jogo = _context.Jogo.Include(jogo => jogo.Genero).Include(jogo => jogo.Usuario).FirstOrDefault(jogo => jogo.JogoID == id);
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

        public void Adicionar (Jogo jogo,  List<int> generoIds)
        {
            List<Genero> generos = _context.Genero.Where(classificacao => generoIds.Contains(genero.GeneroID)).ToList();
            

            jogo.Genero = generos;
            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar (Jogo jogo, List<int> generoIds)
        {
            Jogo? jogoBanco = _context.Jogo.Include(jogo => jogo.Genero).FirstOrDefault(jogoAux => jogoAux.JogoID == jogo.JogoID);

            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if(jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

           
            var generos = _context.Genero.Where(classificacao => generoIds.Contains(genero.GeneroID)).ToList();

            
            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
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
