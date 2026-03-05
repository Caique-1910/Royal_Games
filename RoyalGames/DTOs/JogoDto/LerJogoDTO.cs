namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDTO
    {
        public int JogoID { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public byte[] Imagem { get; set; } = null!;
        public bool? StatusJogo { get; set; }

        public string? UsuarioNome { get; set; }

        public string? UsuarioEmail { get; set; }
        public int? UsuarioID { get; set; }
        public int? ClassificacaoIndicativaID { get; set; }
        public List<string> Generos { get; set; } = new List<string>();
        public List<int> GenerosIds { get; set; } = new();

        public List<string> Plataformas { get; set; } = new List<string>();
        public List<int> PlataformasIds { get; set; } = new();
    }
}
