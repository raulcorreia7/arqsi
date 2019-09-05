using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class corev16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiais_Acabamento_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acabamento",
                table: "Acabamento");

            migrationBuilder.RenameTable(
                name: "Acabamento",
                newName: "Acabamentos");

            migrationBuilder.AlterColumn<int>(
                name: "AcabamentoID",
                table: "Materiais",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acabamentos",
                table: "Acabamentos",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                principalTable: "Acabamentos",
                principalColumn: "AcabamentoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiais_Acabamentos_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acabamentos",
                table: "Acabamentos");

            migrationBuilder.RenameTable(
                name: "Acabamentos",
                newName: "Acabamento");

            migrationBuilder.AlterColumn<int>(
                name: "AcabamentoID",
                table: "Materiais",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acabamento",
                table: "Acabamento",
                column: "AcabamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Materiais_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiais_Acabamento_AcabamentoID",
                table: "Materiais",
                column: "AcabamentoID",
                principalTable: "Acabamento",
                principalColumn: "AcabamentoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
