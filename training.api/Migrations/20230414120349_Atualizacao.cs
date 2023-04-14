using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace training.api.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Enderecos");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Enderecos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEstado = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    IdLoja = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Lojas_IdLoja",
                        column: x => x.IdLoja,
                        principalTable: "Lojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCidade = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bancos_Cidades_IdCidade",
                        column: x => x.IdCidade,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContaBancarias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saldo = table.Column<float>(type: "real", nullable: false),
                    IdPessoa = table.Column<long>(type: "bigint", nullable: false),
                    IdBanco = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaBancarias_Bancos_IdBanco",
                        column: x => x.IdBanco,
                        principalTable: "Bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContaBancarias_Pessoas_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_IdEstado",
                table: "Enderecos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Bancos_IdCidade",
                table: "Bancos",
                column: "IdCidade");

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_IdEstado",
                table: "Cidades",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancarias_IdBanco",
                table: "ContaBancarias",
                column: "IdBanco");

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancarias_IdPessoa",
                table: "ContaBancarias",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdLoja",
                table: "Produtos",
                column: "IdLoja");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Estados_IdEstado",
                table: "Enderecos",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Estados_IdEstado",
                table: "Enderecos");

            migrationBuilder.DropTable(
                name: "ContaBancarias");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_IdEstado",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Enderecos");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
