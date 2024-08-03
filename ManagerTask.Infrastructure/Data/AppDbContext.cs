using System;
using System.Collections.Generic;
using ManagerTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagerTask.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<HistoricoAtividade> HistoricoAtividades { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=dbManagerTask;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasIndex(e => e.TarefaId, "IX_Comentarios_TarefaId");

            entity.HasIndex(e => e.UsuarioId, "IX_Comentarios_UsuarioId");

            entity.HasOne(d => d.Tarefa).WithMany(p => p.Comentarios).HasForeignKey(d => d.TarefaId);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Comentarios).HasForeignKey(d => d.UsuarioId);
        });

        modelBuilder.Entity<HistoricoAtividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historic__3214EC2769C92F17");

            entity.HasIndex(e => e.TarefaId, "IX_HistoricoAtividades_TarefaID");

            entity.HasIndex(e => e.UsuarioId, "IX_HistoricoAtividades_UsuarioID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataHoraAtividade).HasColumnType("datetime");
            entity.Property(e => e.TarefaId).HasColumnName("TarefaID");
            entity.Property(e => e.TipoAtividade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Tarefa).WithMany(p => p.HistoricoAtividades).HasForeignKey(d => d.TarefaId);

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistoricoAtividades)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Historico__Usuar__44FF419A");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projetos__3214EC27430A783F");

            entity.HasIndex(e => e.UsuarioCriadorId, "IX_Projetos_UsuarioCriadorID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataCriacaoProjeto).HasColumnType("datetime");
            entity.Property(e => e.DescricaoProjeto).HasColumnType("text");
            entity.Property(e => e.NomeProjeto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.UsuarioCriadorId).HasColumnName("UsuarioCriadorID");

            entity.HasOne(d => d.UsuarioCriador).WithMany(p => p.Projetos)
                .HasForeignKey(d => d.UsuarioCriadorId)
                .HasConstraintName("FK__Projetos__Usuari__398D8EEE");
        });

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarefas__3214EC27E9A37145");

            entity.HasIndex(e => e.ProjetoId, "IX_Tarefas_ProjetoId");

            entity.HasIndex(e => e.UsuarioResponsavelId, "IX_Tarefas_UsuarioResponsavelID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataCriacao).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasColumnType("text");
            entity.Property(e => e.LActive).HasColumnName("lActive");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioResponsavelId).HasColumnName("UsuarioResponsavelID");

            entity.HasOne(d => d.Projeto).WithMany(p => p.Tarefas).HasForeignKey(d => d.ProjetoId);

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
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
