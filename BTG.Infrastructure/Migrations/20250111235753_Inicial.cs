using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CodigoCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CodigoCliente);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    CodigoProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Produto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.CodigoProduto);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    CodigoPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.CodigoPedido);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_CodigoCliente",
                        column: x => x.CodigoCliente,
                        principalTable: "Cliente",
                        principalColumn: "CodigoCliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalhesPedido",
                columns: table => new
                {
                    CodigoPedido = table.Column<int>(type: "int", nullable: false),
                    CodigoProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesPedido", x => new { x.CodigoPedido, x.CodigoProduto });
                    table.ForeignKey(
                        name: "FK_DetalhesPedido_Pedido_CodigoPedido",
                        column: x => x.CodigoPedido,
                        principalTable: "Pedido",
                        principalColumn: "CodigoPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesPedido_Produto_CodigoProduto",
                        column: x => x.CodigoProduto,
                        principalTable: "Produto",
                        principalColumn: "CodigoProduto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedido_CodigoProduto",
                table: "DetalhesPedido",
                column: "CodigoProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_CodigoCliente",
                table: "Pedido",
                column: "CodigoCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesPedido");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
