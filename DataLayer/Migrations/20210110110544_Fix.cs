using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Profiles_FriendReciverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ReciverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserPicture",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "ReciverId",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReciverId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.RenameColumn(
                name: "FriendReciverId",
                table: "FriendRequests",
                newName: "FriendReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_FriendReciverId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_FriendReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Profiles_FriendReceiverId",
                table: "FriendRequests",
                column: "FriendReceiverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Profiles_FriendReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Profiles_ReceiverId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Messages",
                newName: "ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_ReciverId");

            migrationBuilder.RenameColumn(
                name: "FriendReceiverId",
                table: "FriendRequests",
                newName: "FriendReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_FriendReceiverId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_FriendReciverId");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserPicture",
                table: "Profiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Profiles_FriendReciverId",
                table: "FriendRequests",
                column: "FriendReciverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Profiles_ReciverId",
                table: "Messages",
                column: "ReciverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
