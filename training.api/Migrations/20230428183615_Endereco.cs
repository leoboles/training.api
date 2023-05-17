using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace training.api.Migrations
{
    /// <inheritdoc />
    public partial class Endereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdCidade",
                table: "Enderecos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCidade",
                table: "Enderecos");
        }
    }
}
