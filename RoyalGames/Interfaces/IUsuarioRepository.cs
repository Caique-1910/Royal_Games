using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario? ObterPorId(int id);

        Usuario? ObterPorEmail(string email);

        bool EmailExiste(string email);

        void Adicionar (Usuario usuario);

        void Atualizar (Usuario usuario);
    
        void Deletar (int id);
    }
}
