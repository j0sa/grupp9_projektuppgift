using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class friend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    FriendRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendSenderId = table.Column<int>(type: "int", nullable: false),
                    FriendReciverId = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.FriendRequestId);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Profiles_FriendReciverId",
                        column: x => x.FriendReciverId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Profiles_FriendSenderId",
                        column: x => x.FriendSenderId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendReciverId",
                table: "FriendRequests",
                column: "FriendReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_FriendSenderId",
                table: "FriendRequests",
                column: "FriendSenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequests");
        }
    }
}