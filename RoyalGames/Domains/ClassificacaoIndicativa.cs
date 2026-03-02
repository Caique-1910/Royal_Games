using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class ClassificacaoIndicativa
{
    public int ClassificacaoInditicativoID { get; set; }

    public string Classificacao { get; set; } = null!;

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
