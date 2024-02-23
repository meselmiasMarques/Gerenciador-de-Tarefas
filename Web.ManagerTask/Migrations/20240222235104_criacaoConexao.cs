using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.ManagerTask.Migrations
{
    /// <inheritdoc />
    public partial class criacaoConexao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Senha = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__3214EC27AD244FFB", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProjeto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DescricaoProjeto = table.Column<string>(type: "text", nullable: true),
                    DataCriacaoProjeto = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UsuarioCriadorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projetos__3214EC27430A783F", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Projetos__Usuari__398D8EEE",
                        column: x => x.UsuarioCriadorID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Prazo = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UsuarioResponsavelID = table.Column<int>(type: "int", nullable: true),
                    ProjetoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tarefas__3214EC27E9A37145", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Tarefas__Projeto__3D5E1FD2",
                        column: x => x.ProjetoID,
                        principalTable: "Projetos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Tarefas__Usuario__3C69FB99",
                        column: x => x.UsuarioResponsavelID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarefaID = table.Column<int>(type: "int", nullable: true),
                    UsuarioID = table.Column<int>(type: "int", nullable: true),
                    Conteudo = table.Column<string>(type: "text", nullable: true),
                    DataHoraComentario = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comentar__3214EC2754D3EC43", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Comentari__Taref__403A8C7D",
                        column: x => x.TarefaID,
                        principalTable: "Tarefas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Comentari__Usuar__412EB0B6",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAtividades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarefaID = table.Column<int>(type: "int", nullable: true),
                    UsuarioID = table.Column<int>(type: "int", nullable: true),
                    TipoAtividade = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DataHoraAtividade = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historic__3214EC2769C92F17", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Historico__Taref__440B1D61",
                        column: x => x.TarefaID,
                        principalTable: "Tarefas",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Historico__Usuar__44FF419A",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_TarefaID",
                table: "Comentarios",
                column: "TarefaID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioID",
                table: "Comentarios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAtividades_TarefaID",
                table: "HistoricoAtividades",
                column: "TarefaID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAtividades_UsuarioID",
                table: "HistoricoAtividades",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_UsuarioCriadorID",
                table: "Projetos",
                column: "UsuarioCriadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ProjetoID",
                table: "Tarefas",
                column: "ProjetoID");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioResponsavelID",
                table: "Tarefas",
                column: "UsuarioResponsavelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "HistoricoAtividades");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
