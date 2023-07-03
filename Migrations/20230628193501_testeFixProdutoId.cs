using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorEstoque.Migrations
{
    public partial class testeFixProdutoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Produto_ProdutoId1",
                table: "ProdutoEstoque");

            migrationBuilder.RenameColumn(
                name: "ProdutoId1",
                table: "ProdutoEstoque",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoEstoque_ProdutoId1",
                table: "ProdutoEstoque",
                newName: "IX_ProdutoEstoque_ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Produto_ProdutoId",
                table: "ProdutoEstoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Produto_ProdutoId",
                table: "ProdutoEstoque");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "ProdutoEstoque",
                newName: "ProdutoId1");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoEstoque_ProdutoId",
                table: "ProdutoEstoque",
                newName: "IX_ProdutoEstoque_ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Produto_ProdutoId1",
                table: "ProdutoEstoque",
                column: "ProdutoId1",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
