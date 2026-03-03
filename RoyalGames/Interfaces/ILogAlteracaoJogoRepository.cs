using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface ILogAlteracaoJogoRepository
    {
        List<Log_AlteracaoJogo> Listar();
        List<Log_AlteracaoJogo> ListarPorJogo(int jogoId);
    }
}
