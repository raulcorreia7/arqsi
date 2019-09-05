using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class produtomaterialacabamentointermedio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialAcabamentos_Produtos_ProdutoID",
                table: "MaterialAcabamentos");

            migrationBuilder.DropIndex(
                name: "IX_MaterialAcabamentos_ProdutoID",
                table: "MaterialAcabamentos");

            migrationBuilder.DropColumn(
                name: "ProdutoID",
                table: "MaterialAcabamentos");

            migrationBuilder.CreateTable(
                name: "ProdutoMaterialAcabamentos",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(nullable: false),
                    MaterialAcabamentoID = table.Column<int>(nullable: false),
                    ProdutoID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoMaterialAcabamentos", x => new { x.ProdutoID, x.MaterialAcabamentoID });
                    table.ForeignKey(
                        name: "FK_ProdutoMaterialAcabamentos_MaterialAcabamentos_MaterialAcabamentoID",
                        column: x => x.MaterialAcabamentoID,
                        principalTable: "MaterialAcabamentos",
                        principalColumn: "MaterialAcabamentoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoMaterialAcabamentos_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoMaterialAcabamentos_Produtos_ProdutoID1",
                        column: x => x.ProdutoID1,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoMaterialAcabamentos_MaterialAcabamentoID",
                table: "ProdutoMaterialAcabamentos",
                column: "MaterialAcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoMaterialAcabamentos_ProdutoID1",
                table: "ProdutoMaterialAcabamentos",
                column: "ProdutoID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoMaterialAcabamentos");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoID",
                table: "MaterialAcabamentos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamentos_ProdutoID",
                table: "MaterialAcabamentos",
                column: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialAcabamentos_Produtos_ProdutoID",
                table: "MaterialAcabamentos",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ProdutoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
