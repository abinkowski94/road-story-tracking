using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class updatedMarker : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerInvitations_User_InvitedUserId",
                table: "MarkerInvitations");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerInvitations_Markers_MarkerId",
                table: "MarkerInvitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkerInvitations",
                table: "MarkerInvitations");

            migrationBuilder.RenameTable(
                name: "MarkerInvitations",
                newName: "MarkerInvitation");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerInvitations_MarkerId",
                table: "MarkerInvitation",
                newName: "IX_MarkerInvitation_MarkerId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerInvitations_InvitedUserId",
                table: "MarkerInvitation",
                newName: "IX_MarkerInvitation_InvitedUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MarkerId",
                table: "MarkerInvitation",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "MakerId",
                table: "MarkerInvitation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkerInvitation",
                table: "MarkerInvitation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerInvitation_User_InvitedUserId",
                table: "MarkerInvitation",
                column: "InvitedUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerInvitation_Markers_MarkerId",
                table: "MarkerInvitation",
                column: "MarkerId",
                principalTable: "Markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerInvitation_User_InvitedUserId",
                table: "MarkerInvitation");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerInvitation_Markers_MarkerId",
                table: "MarkerInvitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkerInvitation",
                table: "MarkerInvitation");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "MarkerInvitation");

            migrationBuilder.RenameTable(
                name: "MarkerInvitation",
                newName: "MarkerInvitations");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerInvitation_MarkerId",
                table: "MarkerInvitations",
                newName: "IX_MarkerInvitations_MarkerId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerInvitation_InvitedUserId",
                table: "MarkerInvitations",
                newName: "IX_MarkerInvitations_InvitedUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MarkerId",
                table: "MarkerInvitations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkerInvitations",
                table: "MarkerInvitations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerInvitations_User_InvitedUserId",
                table: "MarkerInvitations",
                column: "InvitedUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerInvitations_Markers_MarkerId",
                table: "MarkerInvitations",
                column: "MarkerId",
                principalTable: "Markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}