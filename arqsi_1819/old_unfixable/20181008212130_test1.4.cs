using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class test14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_MaterialAcabamento_MaterialAcabamentoID",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "MaterialAcabamento");

            migrationBuilder.RenameColumn(
                name: "MaterialAcabamentoID",
                table: "Produtos",
                newName: "ProdutoID1");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_MaterialAcabamentoID",
                table: "Produtos",
                newName: "IX_Produtos_ProdutoID1");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoBaseProdutoID",
                table: "Restricao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoParteProdutoID",
                table: "Restricao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialID",
                table: "Produtos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcabamentoID",
                table: "Materiais",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ProdutoBaseProdutoID",
                table: "Restricao",
                column: "ProdutoBaseProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ProdutoParteProdutoID",
                table: "Restricao",
                column: "ProdutoParteProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MaterialID",
                table: "Produtos",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                principalTable: "Acabamentos",
                principalColumn: "AcabamentoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos",
                column: "MaterialID",
                principalTable: "Materiais",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Produtos_ProdutoID1",
                table: "Produtos",
                column: "ProdutoID1",
                principalTable: "Produtos",
                principalColumn: "ProdutoID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Produtos_ProdutoID1",
                table: "Produtos");

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

            migrationBuilder.DropIndex(
                name: "IX_Produtos_MaterialID",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropColumn(
                name: "ProdutoBaseProdutoID",
                table: "Restricao");

            migrationBuilder.DropColumn(
                name: "ProdutoParteProdutoID",
                table: "Restricao");

            migrationBuilder.DropColumn(
                name: "MaterialID",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AcabamentoID",
                table: "Materiais");

            migrationBuilder.RenameColumn(
                name: "ProdutoID1",
                table: "Produtos",
                newName: "MaterialAcabamentoID");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_ProdutoID1",
                table: "Produtos",
                newName: "IX_Produtos_MaterialAcabamentoID");

            migrationBuilder.CreateTable(
                name: "MaterialAcabamento",
                columns: table => new
                {
                    MaterialAcabamentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcabamentoID = table.Column<int>(nullable: false),
                    MaterialID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAcabamento", x => x.MaterialAcabamentoID);
                    table.ForeignKey(
                        name: "FK_MaterialAcabamento_Acabamentos_AcabamentoID",
                        column: x => x.AcabamentoID,
                        principalTable: "Acabamentos",
                        principalColumn: "AcabamentoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialAcabamento_Materiais_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materiais",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamento_AcabamentoID",
                table: "MaterialAcabamento",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamento_MaterialID",
                table: "MaterialAcabamento",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_MaterialAcabamento_MaterialAcabamentoID",
                table: "Produtos",
                column: "MaterialAcabamentoID",
                principalTable: "MaterialAcabamento",
                principalColumn: "MaterialAcabamentoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
