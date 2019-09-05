using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class migrations13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restricao_Produtos_ProdutoBaseProdutoID",
                table: "Restricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Restricao_Produtos_ProdutoParteProdutoID",
                table: "Restricao");

            migrationBuilder.DropIndex(
                name: "IX_Restricao_ProdutoBaseProdutoID",
                table: "Restricao");

            migrationBuilder.DropIndex(
                name: "IX_Restricao_ProdutoParteProdutoID",
                table: "Restricao");

            migrationBuilder.DropColumn(
                name: "ProdutoBaseProdutoID",
                table: "Restricao");

            migrationBuilder.DropColumn(
                name: "ProdutoParteProdutoID",
                table: "Restricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutoBaseProdutoID",
                table: "Restricao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoParteProdutoID",
                table: "Restricao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ProdutoBaseProdutoID",
                table: "Restricao",
                column: "ProdutoBaseProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ProdutoParteProdutoID",
                table: "Restricao",
                column: "ProdutoParteProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restricao_Produtos_ProdutoBaseProdutoID",
                table: "Restricao",
                column: "ProdutoBaseProdutoID",
                principalTable: "Produtos",
                principalColumn: "ProdutoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restricao_Produtos_ProdutoParteProdutoID",
                table: "Restricao",
                column: "ProdutoParteProdutoID",
                principalTable: "Produtos",
                principalColumn: "ProdutoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
