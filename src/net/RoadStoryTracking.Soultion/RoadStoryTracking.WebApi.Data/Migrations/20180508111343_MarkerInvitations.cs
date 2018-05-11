using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class MarkerInvitations : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkerInvitation");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarkerInvitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InvitationStatuses = table.Column<int>(nullable: false),
                    InvitedUserId = table.Column<string>(nullable: true),
                    MakerId = table.Column<Guid>(nullable: false),
                    MarkerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkerInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkerInvitation_User_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarkerInvitation_Markers_MarkerId",
                        column: x => x.MarkerId,
                        principalTable: "Markers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarkerInvitation_InvitedUserId",
                table: "MarkerInvitation",
                column: "InvitedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkerInvitation_MarkerId",
                table: "MarkerInvitation",
                column: "MarkerId");
        }
    }
}