namespace RoyalGames.DTOs.JogoDto
{
    public class AtualizarJogoDTO
    {
        public int JogoID { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public bool? StatusJogo { get; set; }
        public int? UsuarioID { get; set; }
        public int? ClassificacaoIndicativaID { get; set; }
        public List<int> GeneroIDs { get; set; } = new List<int>();
    }
}
