using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class Corev11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acabamentos",
                columns: table => new
                {
                    AcabamentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acabamentos", x => x.AcabamentoID);
                });

            migrationBuilder.CreateTable(
                name: "Dimensao",
                columns: table => new
                {
                    DimensaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoValorComprimento = table.Column<int>(nullable: false),
                    TipoValorLargura = table.Column<int>(nullable: false),
                    TipoValorAltura = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensao", x => x.DimensaoID);
                });

            migrationBuilder.CreateTable(
                name: "Materiais",
                columns: table => new
                {
                    MaterialID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiais", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "ValorNumerico",
                columns: table => new
                {
                    ValorNumericoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<double>(nullable: false),
                    DimensaoID = table.Column<int>(nullable: true),
                    DimensaoID1 = table.Column<int>(nullable: true),
                    DimensaoID2 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValorNumerico", x => x.ValorNumericoID);
                    table.ForeignKey(
                        name: "FK_ValorNumerico_Dimensao_DimensaoID",
                        column: x => x.DimensaoID,
                        principalTable: "Dimensao",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorNumerico_Dimensao_DimensaoID1",
                        column: x => x.DimensaoID1,
                        principalTable: "Dimensao",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorNumerico_Dimensao_DimensaoID2",
                        column: x => x.DimensaoID2,
                        principalTable: "Dimensao",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialAcabemento",
                columns: table => new
                {
                    MaterialAcabementoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialID = table.Column<int>(nullable: true),
                    AcabamentoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAcabemento", x => x.MaterialAcabementoID);
                    table.ForeignKey(
                        name: "FK_MaterialAcabemento_Acabamentos_AcabamentoID",
                        column: x => x.AcabamentoID,
                        principalTable: "Acabamentos",
                        principalColumn: "AcabamentoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialAcabemento_Materiais_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materiais",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DimensaoID = table.Column<int>(nullable: true),
                    MaterialAcabementoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoID);
                    table.ForeignKey(
                        name: "FK_Produtos_Dimensao_DimensaoID",
                        column: x => x.DimensaoID,
                        principalTable: "Dimensao",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_MaterialAcabemento_MaterialAcabementoID",
                        column: x => x.MaterialAcabementoID,
                        principalTable: "MaterialAcabemento",
                        principalColumn: "MaterialAcabementoID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_ItemsDeProduto_ProdutoBaseProdutoID",
                table: "ItemsDeProduto",
                column: "ProdutoBaseProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsDeProduto_ProdutoParteProdutoID",
                table: "ItemsDeProduto",
                column: "ProdutoParteProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabemento_AcabamentoID",
                table: "MaterialAcabemento",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabemento_MaterialID",
                table: "MaterialAcabemento",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos",
                column: "DimensaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MaterialAcabementoID",
                table: "Produtos",
                column: "MaterialAcabementoID");

            migrationBuilder.CreateIndex(
                name: "IX_ValorNumerico_DimensaoID",
                table: "ValorNumerico",
                column: "DimensaoID");

            migrationBuilder.CreateIndex(
                name: "IX_ValorNumerico_DimensaoID1",
                table: "ValorNumerico",
                column: "DimensaoID1");

            migrationBuilder.CreateIndex(
                name: "IX_ValorNumerico_DimensaoID2",
                table: "ValorNumerico",
                column: "DimensaoID2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsDeProduto");

            migrationBuilder.DropTable(
                name: "ValorNumerico");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Dimensao");

            migrationBuilder.DropTable(
                name: "MaterialAcabemento");

            migrationBuilder.DropTable(
                name: "Acabamentos");

            migrationBuilder.DropTable(
                name: "Materiais");
        }
    }
}
