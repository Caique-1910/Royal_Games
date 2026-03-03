using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativaRepository
    {
        private readonly RoyalGamesContext _context;

        public ClassificacaoIndicativaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacao = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoInditicativoID == id);

            return classificacao;
        }

        public bool ClassificacaoExiste(string nome, int? classificacaoIdAtual = null)
        {
            var classificacaoConsultada = _context.ClassificacaoIndicativa.AsQueryable();

            if (classificacaoIdAtual.HasValue)
            {
                classificacaoConsultada = classificacaoConsultada.Where(c => c.ClassificacaoInditicativoID != classificacaoIdAtual.Value);
            }

            return classificacaoConsultada.Any(c => c.Classificacao == nome);
        }

        public void Adicionar(ClassificacaoIndicativa classificacao)
        {
            _context.ClassificacaoIndicativa.Add(classificacao);
            _context.SaveChanges();
        }

        public void Atualizar(ClassificacaoIndicativa classificacao)
        { 
            ClassificacaoIndicativa? classificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoInditicativoID == classificacao.ClassificacaoInditicativoID);

            if (classificacaoBanco == null)
            {
                return;
            }

            classificacaoBanco.Classificacao = classificacao.Classificacao;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa? classificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoInditicativoID == id);
            if (classificacaoBanco == null)
            {
                return;
            }
            _context.ClassificacaoIndicativa.Remove(classificacaoBanco);
            _context.SaveChanges();
        }

    }
}
