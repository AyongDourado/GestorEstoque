using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorEstoque.Migrations
{
    public partial class UpdateProdEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Produto_produtoId",
                table: "ProdutoEstoque");

            migrationBuilder.DropColumn(
                name: "QtdMin",
                table: "ProdutoEstoque");

            migrationBuilder.DropColumn(
                name: "Validade",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "produtoId",
                table: "ProdutoEstoque",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoEstoque_produtoId",
                table: "ProdutoEstoque",
                newName: "IX_ProdutoEstoque_ProdutoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Validade",
                table: "ProdutoEstoque",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtdMin",
                table: "Produto",
                type: "int",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Validade",
                table: "ProdutoEstoque");

            migrationBuilder.DropColumn(
                name: "QtdMin",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "ProdutoEstoque",
                newName: "produtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoEstoque_ProdutoId",
                table: "ProdutoEstoque",
                newName: "IX_ProdutoEstoque_produtoId");

            migrationBuilder.AddColumn<int>(
                name: "QtdMin",
                table: "ProdutoEstoque",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Validade",
                table: "Produto",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Produto_produtoId",
                table: "ProdutoEstoque",
                column: "produtoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
