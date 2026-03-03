using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoIndicativaRepository
    {
        List<ClassificacaoIndicativa> Listar();

        ClassificacaoIndicativa ObterPorId(int id);

        bool ClassificacaoExiste(string nome, int? classificacaoIdAtual = null);

        void Adicionar(ClassificacaoIndicativa classificacao);

        void Atualizar(ClassificacaoIndicativa classificacao);

        void Remover(int id);
    }
}
