using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class HorarioAlteracaoJogo
    {
        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay;
            var abertura = new TimeSpan(16, 0, 0); // 16 : 00 pm
            var fechamento = new TimeSpan(23, 0, 0); // 11

            var estarAberto = agora >= abertura && agora <= fechamento;

            if ( estarAberto)
            {
                throw new DomainException("Jogo pode ser alterado somente fora do horário de funcionamento");
            }
        }
    }
}
