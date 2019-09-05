using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class restricoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AlturaMax",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AlturaMin",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ComprimentoMax",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ComprimentoMin",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LarguraMax",
                table: "Restricoes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LarguraMin",
                table: "Restricoes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlturaMax",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "AlturaMin",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "ComprimentoMax",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "ComprimentoMin",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "LarguraMax",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "LarguraMin",
                table: "Restricoes");
        }
    }
}
