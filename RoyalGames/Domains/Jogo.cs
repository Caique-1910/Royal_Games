using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Jogo
{
    public int JogoID { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public byte[] Imagem { get; set; } = null!;

    public bool? StatusJogo { get; set; }

    public int? UsuarioID { get; set; }

    public int? ClassificacaoIndicativaID { get; set; }

    public virtual ClassificacaoIndicativa? ClassificacaoIndicativa { get; set; }

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Genero> Genero { get; set; } = new List<Genero>();

    public virtual ICollection<Plataforma> Plataforma { get; set; } = new List<Plataforma>();
}
