using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.ManagerTask.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableTarefas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "Tarefas");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Tarefas",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tarefas",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "Prazo",
                table: "Tarefas",
                type: "datetime",
                nullable: true);
        }
    }
}
