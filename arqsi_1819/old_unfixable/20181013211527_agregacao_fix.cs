using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class agregacao_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restricoes_Agregacoes_RestricaoID",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "RestricaoID",
                table: "Agregacoes");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Restricoes",
                newName: "res_type");

            migrationBuilder.AlterColumn<int>(
                name: "RestricaoID",
                table: "Restricoes",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AgregacaoID",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restricoes_AgregacaoID",
                table: "Restricoes",
                column: "AgregacaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restricoes_Agregacoes_AgregacaoID",
                table: "Restricoes",
                column: "AgregacaoID",
                principalTable: "Agregacoes",
                principalColumn: "AgregacaoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restricoes_Agregacoes_AgregacaoID",
                table: "Restricoes");

            migrationBuilder.DropIndex(
                name: "IX_Restricoes_AgregacaoID",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "AgregacaoID",
                table: "Restricoes");

            migrationBuilder.RenameColumn(
                name: "res_type",
                table: "Restricoes",
                newName: "Discriminator");

            migrationBuilder.AlterColumn<int>(
                name: "RestricaoID",
                table: "Restricoes",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "RestricaoID",
                table: "Agregacoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Restricoes_Agregacoes_RestricaoID",
                table: "Restricoes",
                column: "RestricaoID",
                principalTable: "Agregacoes",
                principalColumn: "AgregacaoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
