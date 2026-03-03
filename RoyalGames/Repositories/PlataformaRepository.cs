using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar(Plataforma plataforma)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(p => p.PlataformaId == plataforma.PlataformaId);

            if(plataformaBanco == null)
            {
                return;
            }

            plataformaBanco.Nome = plataforma.Nome;

            _context.SaveChanges();
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }

        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var consulta = _context.Plataforma.AsQueryable();

            if(plataformaIdAtual.HasValue)
            {
                consulta = consulta.Where(p => p.PlataformaId != plataformaIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

        public Plataforma ObterPorId(int id)
        {
            Plataforma plataforma = _context.Plataforma.FirstOrDefault(p => p.PlataformaId == id);

            return plataforma;
        }

        public void Remover(int id)
        {
            Plataforma plataforma = _context.Plataforma.FirstOrDefault(p => p.PlataformaId == id);

            if(plataforma == null)
            {
                return;
            }

            _context.Plataforma.Remove(plataforma);
            _context.SaveChanges();
        }
    }
}
