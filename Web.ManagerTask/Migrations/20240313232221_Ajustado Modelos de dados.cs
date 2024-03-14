using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.ManagerTask.Migrations
{
    /// <inheritdoc />
    public partial class AjustadoModelosdedados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Comentari__Taref__403A8C7D",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK__Comentari__Usuar__412EB0B6",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK__Historico__Taref__440B1D61",
                table: "HistoricoAtividades");

            migrationBuilder.DropForeignKey(
                name: "FK__Tarefas__Projeto__3D5E1FD2",
                table: "Tarefas");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Comentar__3214EC2754D3EC43",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "ProjetoID",
                table: "Tarefas",
                newName: "ProjetoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_ProjetoID",
                table: "Tarefas",
                newName: "IX_Tarefas_ProjetoId");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Comentarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "TarefaID",
                table: "Comentarios",
                newName: "TarefaId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Comentarios",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_UsuarioID",
                table: "Comentarios",
                newName: "IX_Comentarios_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_TarefaID",
                table: "Comentarios",
                newName: "IX_Comentarios_TarefaId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraComentario",
                table: "Comentarios",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Comentarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Tarefas_TarefaId",
                table: "Comentarios",
                column: "TarefaId",
                principalTable: "Tarefas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoAtividades_Tarefas_TarefaID",
                table: "HistoricoAtividades",
                column: "TarefaID",
                principalTable: "Tarefas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Tarefas_TarefaId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoAtividades_Tarefas_TarefaID",
                table: "HistoricoAtividades");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "ProjetoId",
                table: "Tarefas",
                newName: "ProjetoID");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_ProjetoId",
                table: "Tarefas",
                newName: "IX_Tarefas_ProjetoID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Comentarios",
                newName: "UsuarioID");

            migrationBuilder.RenameColumn(
                name: "TarefaId",
                table: "Comentarios",
                newName: "TarefaID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comentarios",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                newName: "IX_Comentarios_UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_TarefaId",
                table: "Comentarios",
                newName: "IX_Comentarios_TarefaID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraComentario",
                table: "Comentarios",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Comentarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Comentar__3214EC2754D3EC43",
                table: "Comentarios",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Comentari__Taref__403A8C7D",
                table: "Comentarios",
                column: "TarefaID",
                principalTable: "Tarefas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Comentari__Usuar__412EB0B6",
                table: "Comentarios",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Historico__Taref__440B1D61",
                table: "HistoricoAtividades",
                column: "TarefaID",
                principalTable: "Tarefas",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Tarefas__Projeto__3D5E1FD2",
                table: "Tarefas",
                column: "ProjetoID",
                principalTable: "Projetos",
                principalColumn: "ID");
        }
    }
}
