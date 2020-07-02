using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ScorecardMatch_ScorecardMatchId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ScorecardMatchId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ScorecardMatchId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ScorecardMatchOption");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ScorecardMatch");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ScorecardMatchOption",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ScorecardMatchOption",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ScorecardMatch",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ScorecardMatch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Scorecard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Scorecard",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScorecardLinkedUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScorecardId = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorecardLinkedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorecardLinkedUser_Scorecard_ScorecardId",
                        column: x => x.ScorecardId,
                        principalTable: "Scorecard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScorecardLinkedUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScorecardUserSelection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScorecardMatchOptionId = table.Column<int>(nullable: true),
                    ScorecardLinkedUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorecardUserSelection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorecardUserSelection_ScorecardLinkedUser_ScorecardLinkedUserId",
                        column: x => x.ScorecardLinkedUserId,
                        principalTable: "ScorecardLinkedUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScorecardUserSelection_ScorecardMatchOption_ScorecardMatchOptionId",
                        column: x => x.ScorecardMatchOptionId,
                        principalTable: "ScorecardMatchOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardLinkedUser_ScorecardId",
                table: "ScorecardLinkedUser",
                column: "ScorecardId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardLinkedUser_UserId",
                table: "ScorecardLinkedUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardUserSelection_ScorecardLinkedUserId",
                table: "ScorecardUserSelection",
                column: "ScorecardLinkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardUserSelection_ScorecardMatchOptionId",
                table: "ScorecardUserSelection",
                column: "ScorecardMatchOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScorecardUserSelection");

            migrationBuilder.DropTable(
                name: "ScorecardLinkedUser");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ScorecardMatchOption");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ScorecardMatchOption");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ScorecardMatch");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ScorecardMatch");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Scorecard");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Scorecard");

            migrationBuilder.AddColumn<int>(
                name: "ScorecardMatchId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ScorecardMatchOption",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ScorecardMatch",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_User_ScorecardMatchId",
                table: "User",
                column: "ScorecardMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ScorecardMatch_ScorecardMatchId",
                table: "User",
                column: "ScorecardMatchId",
                principalTable: "ScorecardMatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
