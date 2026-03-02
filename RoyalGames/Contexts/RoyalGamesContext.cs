using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Domains;

namespace RoyalGames.Contexts;

public partial class RoyalGamesContext : DbContext
{
    public RoyalGamesContext()
    {
    }

    public RoyalGamesContext(DbContextOptions<RoyalGamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Jogo> Jogo { get; set; }

    public virtual DbSet<Plataforma> Plataforma { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RoyalGames;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoInditicativoID).HasName("PK__Classifi__00BF984B6D25D5B7");

            entity.Property(e => e.Classificacao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D02688E525B7A");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoID).HasName("PK__Jogo__5919685590B0BA72");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AltercaoProduto");
                    tb.HasTrigger("trg_ExclusaoJogo");
                });

            entity.HasIndex(e => e.Nome, "UQ__Jogo__7D8FE3B25FF74A50").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusJogo).HasDefaultValue(true);

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.ClassificacaoIndicativaID)
                .HasConstraintName("FK__Jogo__Classifica__778AC167");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogo)
                .HasForeignKey(d => d.UsuarioID)
                .HasConstraintName("FK__Jogo__UsuarioID__76969D2E");

            entity.HasMany(d => d.Genero).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("FK_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("FK_JogoGenero_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "GeneroID");
                    });

            entity.HasMany(d => d.Plataforma).WithMany(p => p.Jogo)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("FK_JogoPlataforma_Plataforma"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoID")
                        .HasConstraintName("FK_JogoPlataforma_Jogo"),
                    j =>
                    {
                        j.HasKey("JogoID", "PlataformaID");
                    });
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaId).HasName("PK__Platafor__B83567ED3E16E63B");

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE7983BD96883");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105342FFEB653").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
