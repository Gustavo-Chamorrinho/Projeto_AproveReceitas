using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testandoBancodDo0.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddnovosCamposReceita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Custo",
                table: "Receita",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dificuldade",
                table: "Receita",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TempoPreparo",
                table: "Receita",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeTempo",
                table: "Receita",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Custo",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "Dificuldade",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "TempoPreparo",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "UnidadeTempo",
                table: "Receita");
        }
    }
}
