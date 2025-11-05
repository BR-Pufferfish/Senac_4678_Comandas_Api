using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comandas_API.Migrations
{
    /// <inheritdoc />
    public partial class versao1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardapioItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    PossuiPreparo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardapioItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeCliente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    Situacao = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeCliente = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComandaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComandaId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardapioItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComandaItem_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoCozinha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComandaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCozinha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoCozinha_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoCozinhaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PedidoCozinhaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComandaItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCozinhaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoCozinhaItem_ComandaItem_ComandaItemId",
                        column: x => x.ComandaItemId,
                        principalTable: "ComandaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoCozinhaItem_PedidoCozinha_PedidoCozinhaId",
                        column: x => x.PedidoCozinhaId,
                        principalTable: "PedidoCozinha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CardapioItem",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 1, "Salgadinho frito em formato de gota, recheado com frango desfiado e temperado.", false, 5.00m, "Coxinha" },
                    { 2, "Massa fina e crocante, recheada com carne moída, queijo ou outros ingredientes, frita até dourar.", false, 6.50m, "Pastel" },
                    { 3, "Doce feito com leite condensado, chocolate em pó, manteiga e granulado de chocolate.", false, 3.00m, "Brigadeiro" }
                });

            migrationBuilder.InsertData(
                table: "Mesa",
                columns: new[] { "Id", "NumeroMesa", "Situacao" },
                values: new object[,]
                {
                    { 1, 10, 1 },
                    { 2, 11, 2 },
                    { 3, 12, 3 }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Email", "Nome", "Senha" },
                values: new object[] { 1, "admin@admin.com", "Admin", "admin123" });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItem_ComandaId",
                table: "ComandaItem",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCozinha_ComandaId",
                table: "PedidoCozinha",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCozinhaItem_ComandaItemId",
                table: "PedidoCozinhaItem",
                column: "ComandaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCozinhaItem_PedidoCozinhaId",
                table: "PedidoCozinhaItem",
                column: "PedidoCozinhaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardapioItem");

            migrationBuilder.DropTable(
                name: "Mesa");

            migrationBuilder.DropTable(
                name: "PedidoCozinhaItem");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "ComandaItem");

            migrationBuilder.DropTable(
                name: "PedidoCozinha");

            migrationBuilder.DropTable(
                name: "Comanda");
        }
    }
}
