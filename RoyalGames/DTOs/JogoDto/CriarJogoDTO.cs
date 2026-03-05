namespace RoyalGames.DTOs.JogoDto
{
    public class CriarJogoDTO
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public List<int> GeneroIDs { get; set; } = new();
        public List<int> PlataformaIDs { get; set; } = new();
        public int classificacaoId { get; set; }
    }
}
