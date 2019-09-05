using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class material_acabamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Produtos_ProdutoID1",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Restricao_ItemsDeProduto_ItemDeProdutoID",
                table: "Restricao");

            migrationBuilder.DropTable(
                name: "ItemsDeProduto");

            migrationBuilder.DropIndex(
                name: "IX_Restricao_ItemDeProdutoID",
                table: "Restricao");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ProdutoID1",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acabamentos",
                table: "Acabamentos");

            migrationBuilder.DropColumn(
                name: "ItemDeProdutoID",
                table: "Restricao");

            migrationBuilder.DropColumn(
                name: "ProdutoID1",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Acabamentos",
                newName: "Acabamento");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialID",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DimensaoID",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaID",
                table: "Produtos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acabamento",
                table: "Acabamento",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaID",
                table: "Produtos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiais_Acabamento_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                principalTable: "Acabamento",
                principalColumn: "AcabamentoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaID",
                table: "Produtos",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos",
                column: "MaterialID",
                principalTable: "Materiais",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiais_Acabamento_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CategoriaID",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acabamento",
                table: "Acabamento");

            migrationBuilder.DropColumn(
                name: "CategoriaID",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Acabamento",
                newName: "Acabamentos");

            migrationBuilder.AddColumn<int>(
                name: "ItemDeProdutoID",
                table: "Restricao",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialID",
                table: "Produtos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DimensaoID",
                table: "Produtos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProdutoID1",
                table: "Produtos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acabamentos",
                table: "Acabamentos",
                column: "AcabamentoID");

            migrationBuilder.CreateTable(
                name: "ItemsDeProduto",
                columns: table => new
                {
                    ItemDeProdutoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProdutoBaseProdutoID = table.Column<int>(nullable: true),
                    ProdutoParteProdutoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsDeProduto", x => x.ItemDeProdutoID);
                    table.ForeignKey(
                        name: "FK_ItemsDeProduto_Produtos_ProdutoBaseProdutoID",
                        column: x => x.ProdutoBaseProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsDeProduto_Produtos_ProdutoParteProdutoID",
                        column: x => x.ProdutoParteProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ItemDeProdutoID",
                table: "Restricao",
                column: "ItemDeProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos",
                column: "DimensaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProdutoID1",
                table: "Produtos",
                column: "ProdutoID1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsDeProduto_ProdutoBaseProdutoID",
                table: "ItemsDeProduto",
                column: "ProdutoBaseProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsDeProduto_ProdutoParteProdutoID",
                table: "ItemsDeProduto",
                column: "ProdutoParteProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                principalTable: "Acabamentos",
                principalColumn: "AcabamentoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
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
                name: "FK_Restricao_ItemsDeProduto_ItemDeProdutoID",
                table: "Restricao",
                column: "ItemDeProdutoID",
                principalTable: "ItemsDeProduto",
                principalColumn: "ItemDeProdutoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
