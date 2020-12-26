using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Profiles_ProfileId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_ProfileId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Profiles");

            migrationBuilder.CreateTable(
                name: "ProfileProfile",
                columns: table => new
                {
                    FriendsWithMeId = table.Column<int>(type: "int", nullable: false),
                    IFriendsWithId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileProfile", x => new { x.FriendsWithMeId, x.IFriendsWithId });
                    table.ForeignKey(
                        name: "FK_ProfileProfile_Profiles_FriendsWithMeId",
                        column: x => x.FriendsWithMeId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileProfile_Profiles_IFriendsWithId",
                        column: x => x.IFriendsWithId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileProfile_IFriendsWithId",
                table: "ProfileProfile",
                column: "IFriendsWithId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileProfile");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ProfileId",
                table: "Profiles",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Profiles_ProfileId",
                table: "Profiles",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
