using Microsoft.EntityFrameworkCore.Migrations;

namespace Closify.Migrations
{
    public partial class tipovalor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoRestricao",
                table: "Restricoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoRestricao",
                table: "Restricoes");
        }
    }
}
