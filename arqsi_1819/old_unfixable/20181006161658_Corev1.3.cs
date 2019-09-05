using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class Corev13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_MaterialAcabemento_MaterialAcabementoID",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "MaterialAcabemento");

            migrationBuilder.RenameColumn(
                name: "MaterialAcabementoID",
                table: "Produtos",
                newName: "MaterialAcabamentoID");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_MaterialAcabementoID",
                table: "Produtos",
                newName: "IX_Produtos_MaterialAcabamentoID");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Produtos",
                nullable: true);

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
                name: "MaterialAcabamento",
                columns: table => new
                {
                    MaterialAcabamentoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialID = table.Column<int>(nullable: false),
                    AcabamentoID = table.Column<int>(nullable: false)
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
                name: "IX_Categorias_CategoriaID1",
                table: "Categorias",
                column: "CategoriaID1");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_SuperCatID",
                table: "Categorias",
                column: "SuperCatID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_MaterialAcabamento_MaterialAcabamentoID",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "MaterialAcabamento");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "MaterialAcabamentoID",
                table: "Produtos",
                newName: "MaterialAcabementoID");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_MaterialAcabamentoID",
                table: "Produtos",
                newName: "IX_Produtos_MaterialAcabementoID");

            migrationBuilder.CreateTable(
                name: "MaterialAcabemento",
                columns: table => new
                {
                    MaterialAcabementoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcabamentoID = table.Column<int>(nullable: true),
                    MaterialID = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabemento_AcabamentoID",
                table: "MaterialAcabemento",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAcabemento_MaterialID",
                table: "MaterialAcabemento",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_MaterialAcabemento_MaterialAcabementoID",
                table: "Produtos",
                column: "MaterialAcabementoID",
                principalTable: "MaterialAcabemento",
                principalColumn: "MaterialAcabementoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
