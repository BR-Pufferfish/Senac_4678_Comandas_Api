using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comandas_API.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaCardapioId",
                table: "CardapioItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriaCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaCardapio", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CardapioItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CardapioItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CardapioItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.InsertData(
                table: "CategoriaCardapio",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, null, "Salgados" },
                    { 2, null, "Doces" },
                    { 3, null, "Bebidas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardapioItem_CategoriaCardapioId",
                table: "CardapioItem",
                column: "CategoriaCardapioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardapioItem_CategoriaCardapio_CategoriaCardapioId",
                table: "CardapioItem",
                column: "CategoriaCardapioId",
                principalTable: "CategoriaCardapio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardapioItem_CategoriaCardapio_CategoriaCardapioId",
                table: "CardapioItem");

            migrationBuilder.DropTable(
                name: "CategoriaCardapio");

            migrationBuilder.DropIndex(
                name: "IX_CardapioItem_CategoriaCardapioId",
                table: "CardapioItem");

            migrationBuilder.DropColumn(
                name: "CategoriaCardapioId",
                table: "CardapioItem");
        }
    }
}
