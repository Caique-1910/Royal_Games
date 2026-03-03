namespace RoyalGames.DTOs.LogJogoDto
{
    public class LerLogJogoDto 
    {
        public int LogID { get; set; }
        public int? JogoID { get; set; }
        public string NomeAnterior { get; set; } = null!;
        public decimal? PrecoAnterior { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
