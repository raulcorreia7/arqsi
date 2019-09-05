using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class Corev12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restricao",
                columns: table => new
                {
                    RestricaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomeRestricao = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ItemDeProdutoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restricao", x => x.RestricaoID);
                    table.ForeignKey(
                        name: "FK_Restricao_ItemsDeProduto_ItemDeProdutoID",
                        column: x => x.ItemDeProdutoID,
                        principalTable: "ItemsDeProduto",
                        principalColumn: "ItemDeProdutoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restricao_ItemDeProdutoID",
                table: "Restricao",
                column: "ItemDeProdutoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restricao");
        }
    }
}
