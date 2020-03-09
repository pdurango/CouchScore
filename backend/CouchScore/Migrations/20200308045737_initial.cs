using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scorecard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scorecard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScorecardMatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
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
                name: "ScorecardMatchOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ScorecardMatchId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_ScorecardMatch_ScorecardMatchId",
                        column: x => x.ScorecardMatchId,
                        principalTable: "ScorecardMatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardMatch_ScorecardId",
                table: "ScorecardMatch",
                column: "ScorecardId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorecardMatchOption_ScorecardMatchId",
                table: "ScorecardMatchOption",
                column: "ScorecardMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ScorecardMatchId",
                table: "User",
                column: "ScorecardMatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
