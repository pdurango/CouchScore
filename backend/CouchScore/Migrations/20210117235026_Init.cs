using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scorecard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scorecard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScorecardMatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ScorecardId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorecardMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorecardMatch_Scorecard_ScorecardId",
                        column: x => x.ScorecardId,
                        principalTable: "Scorecard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "ScorecardMatchOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ScorecardMatchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorecardMatchOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorecardMatchOption_ScorecardMatch_ScorecardMatchId",
                        column: x => x.ScorecardMatchId,
                        principalTable: "ScorecardMatch",
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
                name: "IX_ScorecardMatch_ScorecardId",
                table: "ScorecardMatch",
                column: "ScorecardId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardMatchOption_ScorecardMatchId",
                table: "ScorecardMatchOption",
                column: "ScorecardMatchId");

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

            migrationBuilder.DropTable(
                name: "ScorecardMatchOption");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ScorecardMatch");

            migrationBuilder.DropTable(
                name: "Scorecard");
        }
    }
}
