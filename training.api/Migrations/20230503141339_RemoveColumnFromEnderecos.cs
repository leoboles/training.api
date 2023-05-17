using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace training.api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnFromEnderecos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Cidade_CidadeId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_CidadeId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Enderecos");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_IdCidade",
                table: "Enderecos",
                column: "IdCidade");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Cidade_IdCidade",
                table: "Enderecos",
                column: "IdCidade",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Cidade_IdCidade",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_IdCidade",
                table: "Enderecos");

            migrationBuilder.AddColumn<long>(
                name: "CidadeId",
                table: "Enderecos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_CidadeId",
                table: "Enderecos",
                column: "CidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Cidade_CidadeId",
                table: "Enderecos",
                column: "CidadeId",
                principalTable: "Cidade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
