using Microsoft.EntityFrameworkCore.Migrations;

namespace CouchScore.Migrations
{
    public partial class UserSelectionAdded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
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

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
