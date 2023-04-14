using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace training.api.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdBanco",
                table: "ContaBancarias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancarias_IdBanco",
                table: "ContaBancarias",
                column: "IdBanco");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaBancarias_Bancos_IdBanco",
                table: "ContaBancarias",
                column: "IdBanco",
                principalTable: "Bancos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaBancarias_Bancos_IdBanco",
                table: "ContaBancarias");

            migrationBuilder.DropIndex(
                name: "IX_ContaBancarias_IdBanco",
                table: "ContaBancarias");

            migrationBuilder.DropColumn(
                name: "IdBanco",
                table: "ContaBancarias");
        }
    }
}
