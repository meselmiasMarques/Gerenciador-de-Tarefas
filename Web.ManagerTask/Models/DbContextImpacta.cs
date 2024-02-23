using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyTask.Models;

public partial class DbContextImpacta : DbContext
{
    public DbContextImpacta()
    {
    }

    public DbContextImpacta(DbContextOptions<DbContextImpacta> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<HistoricoAtividade> HistoricoAtividades { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=dbManagerTask;User ID=meselmias;Password=lima1038;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC2754D3EC43");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Conteudo).HasColumnType("text");
            entity.Property(e => e.DataHoraComentario).HasColumnType("datetime");
            entity.Property(e => e.TarefaId).HasColumnName("TarefaID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Tarefa).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.TarefaId)
                .HasConstraintName("FK__Comentari__Taref__403A8C7D");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Comentari__Usuar__412EB0B6");
        });

        modelBuilder.Entity<HistoricoAtividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historic__3214EC2769C92F17");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataHoraAtividade).HasColumnType("datetime");
            entity.Property(e => e.TarefaId).HasColumnName("TarefaID");
            entity.Property(e => e.TipoAtividade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Tarefa).WithMany(p => p.HistoricoAtividades)
                .HasForeignKey(d => d.TarefaId)
                .HasConstraintName("FK__Historico__Taref__440B1D61");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistoricoAtividades)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Historico__Usuar__44FF419A");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projetos__3214EC27430A783F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataCriacaoProjeto).HasColumnType("datetime");
            entity.Property(e => e.DescricaoProjeto).HasColumnType("text");
            entity.Property(e => e.NomeProjeto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCriadorId).HasColumnName("UsuarioCriadorID");

            entity.HasOne(d => d.UsuarioCriador).WithMany(p => p.Projetos)
                .HasForeignKey(d => d.UsuarioCriadorId)
                .HasConstraintName("FK__Projetos__Usuari__398D8EEE");
        });

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarefas__3214EC27E9A37145");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasColumnType("text");
            entity.Property(e => e.ProjetoId).HasColumnName("ProjetoID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioResponsavelId).HasColumnName("UsuarioResponsavelID");

            entity.HasOne(d => d.Projeto).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.ProjetoId)
                .HasConstraintName("FK__Tarefas__Projeto__3D5E1FD2");

            entity.HasOne(d => d.UsuarioResponsavel).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.UsuarioResponsavelId)
                .HasConstraintName("FK__Tarefas__Usuario__3C69FB99");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC27AD244FFB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
