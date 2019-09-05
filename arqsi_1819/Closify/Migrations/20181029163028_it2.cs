using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class it2 : Migration
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
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SuperCatID = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    CategoriaID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaID);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaID1",
                        column: x => x.CategoriaID1,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_SuperCatID",
                        column: x => x.SuperCatID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dimensoes",
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
                    table.PrimaryKey("PK_Dimensoes", x => x.DimensaoID);
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
                name: "Produtos",
                columns: table => new
                {
                    ProdutoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CategoriaID = table.Column<int>(nullable: false),
                    DimensaoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoID);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_Dimensoes_DimensaoID",
                        column: x => x.DimensaoID,
                        principalTable: "Dimensoes",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_ValorNumerico_Dimensoes_DimensaoID",
                        column: x => x.DimensaoID,
                        principalTable: "Dimensoes",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorNumerico_Dimensoes_DimensaoID1",
                        column: x => x.DimensaoID1,
                        principalTable: "Dimensoes",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorNumerico_Dimensoes_DimensaoID2",
                        column: x => x.DimensaoID2,
                        principalTable: "Dimensoes",
                        principalColumn: "DimensaoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agregacoes",
                columns: table => new
                {
                    AgregacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseID = table.Column<int>(nullable: false),
                    ParteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agregacoes", x => x.AgregacaoID);
                    table.ForeignKey(
                        name: "FK_Agregacoes_Produtos_BaseID",
                        column: x => x.BaseID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agregacoes_Produtos_ParteID",
                        column: x => x.ParteID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialAcabamentos",
                columns: table => new
                {
                    MaterialAcabamentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialID = table.Column<int>(nullable: false),
                    AcabamentoID = table.Column<int>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAcabamentos", x => x.MaterialAcabamentoID);
                    table.ForeignKey(
                        name: "FK_MaterialAcabamentos_Acabamentos_AcabamentoID",
                        column: x => x.AcabamentoID,
                        principalTable: "Acabamentos",
                        principalColumn: "AcabamentoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialAcabamentos_Materiais_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materiais",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialAcabamentos_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restricoes",
                columns: table => new
                {
                    RestricaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeRestricao = table.Column<string>(nullable: true),
                    AgregacaoID = table.Column<int>(nullable: true),
                    res_type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restricoes", x => x.RestricaoID);
                    table.ForeignKey(
                        name: "FK_Restricoes_Agregacoes_AgregacaoID",
                        column: x => x.AgregacaoID,
                        principalTable: "Agregacoes",
                        principalColumn: "AgregacaoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agregacoes_BaseID",
                table: "Agregacoes",
                column: "BaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Agregacoes_ParteID",
                table: "Agregacoes",
                column: "ParteID");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_CategoriaID1",
                table: "Categorias",
                column: "CategoriaID1");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_SuperCatID",
                table: "Categorias",
                column: "SuperCatID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamentos_AcabamentoID",
                table: "MaterialAcabamentos",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamentos_MaterialID",
                table: "MaterialAcabamentos",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabamentos_ProdutoID",
                table: "MaterialAcabamentos",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaID",
                table: "Produtos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restricoes_AgregacaoID",
                table: "Restricoes",
                column: "AgregacaoID");

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
                name: "MaterialAcabamentos");

            migrationBuilder.DropTable(
                name: "Restricoes");

            migrationBuilder.DropTable(
                name: "ValorNumerico");

            migrationBuilder.DropTable(
                name: "Acabamentos");

            migrationBuilder.DropTable(
                name: "Materiais");

            migrationBuilder.DropTable(
                name: "Agregacoes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Dimensoes");
        }
    }
}
