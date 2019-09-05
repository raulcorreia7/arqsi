using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class agregacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Restricao");

            migrationBuilder.CreateTable(
                name: "Agregacoes",
                columns: table => new
                {
                    AgregacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseID = table.Column<int>(nullable: false),
                    ParteID = table.Column<int>(nullable: false),
                    RestricaoID = table.Column<int>(nullable: false)
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
                name: "Restricoes",
                columns: table => new
                {
                    RestricaoID = table.Column<int>(nullable: false),
                    NomeRestricao = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restricoes", x => x.RestricaoID);
                    table.ForeignKey(
                        name: "FK_Restricoes_Agregacoes_RestricaoID",
                        column: x => x.RestricaoID,
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

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos",
                column: "MaterialID",
                principalTable: "Materiais",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Restricoes");

            migrationBuilder.DropTable(
                name: "Agregacoes");

            migrationBuilder.CreateTable(
                name: "Restricao",
                columns: table => new
                {
                    RestricaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    NomeRestricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restricao", x => x.RestricaoID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Materiais_MaterialID",
                table: "Produtos",
                column: "MaterialID",
                principalTable: "Materiais",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
