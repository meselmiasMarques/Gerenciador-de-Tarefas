﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTask.Models;

#nullable disable

namespace Web.ManagerTask.Migrations
{
    [DbContext(typeof(DbContextImpacta))]
    [Migration("20240331001502_novo_Campo_lactive_para_tarefas")]
    partial class novo_Campo_lactive_para_tarefas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyTask.Models.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Conteudo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataHoraComentario")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TarefaId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("MyTask.Models.HistoricoAtividade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataHoraAtividade")
                        .HasColumnType("datetime");

                    b.Property<int?>("TarefaId")
                        .HasColumnType("int")
                        .HasColumnName("TarefaID");

                    b.Property<string>("TipoAtividade")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("UsuarioID");

                    b.HasKey("Id")
                        .HasName("PK__Historic__3214EC2769C92F17");

                    b.HasIndex("TarefaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("HistoricoAtividades");
                });

            modelBuilder.Entity("MyTask.Models.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataCriacaoProjeto")
                        .HasColumnType("datetime");

                    b.Property<string>("DescricaoProjeto")
                        .HasColumnType("text");

                    b.Property<string>("NomeProjeto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("bit");

                    b.Property<int?>("UsuarioCriadorId")
                        .HasColumnType("int")
                        .HasColumnName("UsuarioCriadorID");

                    b.HasKey("Id")
                        .HasName("PK__Projetos__3214EC27430A783F");

                    b.HasIndex("UsuarioCriadorId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("MyTask.Models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("UsuarioResponsavelId")
                        .HasColumnType("int")
                        .HasColumnName("UsuarioResponsavelID");

                    b.Property<int>("lActive")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Tarefas__3214EC27E9A37145");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("UsuarioResponsavelId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("MyTask.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Usuarios__3214EC27AD244FFB");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MyTask.Models.Comentario", b =>
                {
                    b.HasOne("MyTask.Models.Tarefa", "Tarefa")
                        .WithMany()
                        .HasForeignKey("TarefaId");

                    b.HasOne("MyTask.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MyTask.Models.HistoricoAtividade", b =>
                {
                    b.HasOne("MyTask.Models.Tarefa", "Tarefa")
                        .WithMany()
                        .HasForeignKey("TarefaId");

                    b.HasOne("MyTask.Models.Usuario", "Usuario")
                        .WithMany("HistoricoAtividades")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK__Historico__Usuar__44FF419A");

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MyTask.Models.Projeto", b =>
                {
                    b.HasOne("MyTask.Models.Usuario", "UsuarioCriador")
                        .WithMany("Projetos")
                        .HasForeignKey("UsuarioCriadorId")
                        .HasConstraintName("FK__Projetos__Usuari__398D8EEE");

                    b.Navigation("UsuarioCriador");
                });

            modelBuilder.Entity("MyTask.Models.Tarefa", b =>
                {
                    b.HasOne("MyTask.Models.Projeto", null)
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyTask.Models.Usuario", "UsuarioResponsavel")
                        .WithMany("Tarefas")
                        .HasForeignKey("UsuarioResponsavelId")
                        .HasConstraintName("FK__Tarefas__Usuario__3C69FB99");

                    b.Navigation("UsuarioResponsavel");
                });

            modelBuilder.Entity("MyTask.Models.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("MyTask.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("HistoricoAtividades");

                    b.Navigation("Projetos");

                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
