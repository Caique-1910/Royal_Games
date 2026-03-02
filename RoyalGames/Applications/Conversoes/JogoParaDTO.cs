using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Applications.Conversoes
{
    public class JogoParaDTO
    {
        public static LerJogoDTO ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDTO
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email,

                ClassificacaoIndicativaID = jogo.ClassificacaoIndicativaID,

                GenerosIds = jogo.Genero.Select(genero => genero.GeneroID).ToList(),
                Generos = jogo.Genero.Select(genero => genero.Nome).ToList(),
            };
        }
    }
}
