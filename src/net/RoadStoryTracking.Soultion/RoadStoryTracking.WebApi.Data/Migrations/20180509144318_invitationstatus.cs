using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class invitationstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationStatuses",
                table: "MarkerInvitations");

            migrationBuilder.AddColumn<int>(
                name: "InvitationStatus",
                table: "MarkerInvitations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationStatus",
                table: "MarkerInvitations");

            migrationBuilder.AddColumn<int>(
                name: "InvitationStatuses",
                table: "MarkerInvitations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
