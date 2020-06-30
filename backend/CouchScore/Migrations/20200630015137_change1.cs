using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class change1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ScorecardMatchOption");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ScorecardMatchOption",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ScorecardMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Scorecard",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ScorecardMatchOption");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ScorecardMatch");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Scorecard");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ScorecardMatchOption",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
