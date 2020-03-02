using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScorecardMatches_Scorecards_ScorecardId",
                table: "ScorecardMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_ScorecardMatchOptions_ScorecardMatches_ScorecardMatchId",
                table: "ScorecardMatchOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ScorecardMatches_ScorecardMatchId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scorecards",
                table: "Scorecards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScorecardMatchOptions",
                table: "ScorecardMatchOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScorecardMatches",
                table: "ScorecardMatches");

            migrationBuilder.RenameTable(
                name: "Scorecards",
                newName: "Scorecard");

            migrationBuilder.RenameTable(
                name: "ScorecardMatchOptions",
                newName: "ScorecardMatchOption");

            migrationBuilder.RenameTable(
                name: "ScorecardMatches",
                newName: "ScorecardMatch");

            migrationBuilder.RenameIndex(
                name: "IX_ScorecardMatchOptions_ScorecardMatchId",
                table: "ScorecardMatchOption",
                newName: "IX_ScorecardMatchOption_ScorecardMatchId");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "ScorecardMatch",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_ScorecardMatches_ScorecardId",
                table: "ScorecardMatch",
                newName: "IX_ScorecardMatch_ScorecardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scorecard",
                table: "Scorecard",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScorecardMatchOption",
                table: "ScorecardMatchOption",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScorecardMatch",
                table: "ScorecardMatch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScorecardMatch_Scorecard_ScorecardId",
                table: "ScorecardMatch",
                column: "ScorecardId",
                principalTable: "Scorecard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScorecardMatchOption_ScorecardMatch_ScorecardMatchId",
                table: "ScorecardMatchOption",
                column: "ScorecardMatchId",
                principalTable: "ScorecardMatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ScorecardMatch_ScorecardMatchId",
                table: "User",
                column: "ScorecardMatchId",
                principalTable: "ScorecardMatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScorecardMatch_Scorecard_ScorecardId",
                table: "ScorecardMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_ScorecardMatchOption_ScorecardMatch_ScorecardMatchId",
                table: "ScorecardMatchOption");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ScorecardMatch_ScorecardMatchId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScorecardMatchOption",
                table: "ScorecardMatchOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScorecardMatch",
                table: "ScorecardMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scorecard",
                table: "Scorecard");

            migrationBuilder.RenameTable(
                name: "ScorecardMatchOption",
                newName: "ScorecardMatchOptions");

            migrationBuilder.RenameTable(
                name: "ScorecardMatch",
                newName: "ScorecardMatches");

            migrationBuilder.RenameTable(
                name: "Scorecard",
                newName: "Scorecards");

            migrationBuilder.RenameIndex(
                name: "IX_ScorecardMatchOption_ScorecardMatchId",
                table: "ScorecardMatchOptions",
                newName: "IX_ScorecardMatchOptions_ScorecardMatchId");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ScorecardMatches",
                newName: "created");

            migrationBuilder.RenameIndex(
                name: "IX_ScorecardMatch_ScorecardId",
                table: "ScorecardMatches",
                newName: "IX_ScorecardMatches_ScorecardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScorecardMatchOptions",
                table: "ScorecardMatchOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScorecardMatches",
                table: "ScorecardMatches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scorecards",
                table: "Scorecards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScorecardMatches_Scorecards_ScorecardId",
                table: "ScorecardMatches",
                column: "ScorecardId",
                principalTable: "Scorecards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScorecardMatchOptions_ScorecardMatches_ScorecardMatchId",
                table: "ScorecardMatchOptions",
                column: "ScorecardMatchId",
                principalTable: "ScorecardMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ScorecardMatches_ScorecardMatchId",
                table: "User",
                column: "ScorecardMatchId",
                principalTable: "ScorecardMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
