using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class dimensaotest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID",
                table: "ValorNumerico");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID1",
                table: "ValorNumerico");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID2",
                table: "ValorNumerico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dimensao",
                table: "Dimensao");

            migrationBuilder.RenameTable(
                name: "Dimensao",
                newName: "Dimensoes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dimensoes",
                table: "Dimensoes",
                column: "DimensaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Dimensoes_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                principalTable: "Dimensoes",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID",
                table: "ValorNumerico",
                column: "DimensaoID",
                principalTable: "Dimensoes",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID1",
                table: "ValorNumerico",
                column: "DimensaoID1",
                principalTable: "Dimensoes",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID2",
                table: "ValorNumerico",
                column: "DimensaoID2",
                principalTable: "Dimensoes",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Dimensoes_DimensaoID",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID",
                table: "ValorNumerico");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID1",
                table: "ValorNumerico");

            migrationBuilder.DropForeignKey(
                name: "FK_ValorNumerico_Dimensoes_DimensaoID2",
                table: "ValorNumerico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dimensoes",
                table: "Dimensoes");

            migrationBuilder.RenameTable(
                name: "Dimensoes",
                newName: "Dimensao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dimensao",
                table: "Dimensao",
                column: "DimensaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Dimensao_DimensaoID",
                table: "Produtos",
                column: "DimensaoID",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID",
                table: "ValorNumerico",
                column: "DimensaoID",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID1",
                table: "ValorNumerico",
                column: "DimensaoID1",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValorNumerico_Dimensao_DimensaoID2",
                table: "ValorNumerico",
                column: "DimensaoID2",
                principalTable: "Dimensao",
                principalColumn: "DimensaoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
