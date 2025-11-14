using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comandas_API.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Situacao",
                table: "Mesa",
                newName: "SituacaoMesa");

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "SituacaoMesa",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "SituacaoMesa",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "SituacaoMesa",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SituacaoMesa",
                table: "Mesa",
                newName: "Situacao");

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 1,
                column: "Situacao",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 2,
                column: "Situacao",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Mesa",
                keyColumn: "Id",
                keyValue: 3,
                column: "Situacao",
                value: 3);
        }
    }
}
