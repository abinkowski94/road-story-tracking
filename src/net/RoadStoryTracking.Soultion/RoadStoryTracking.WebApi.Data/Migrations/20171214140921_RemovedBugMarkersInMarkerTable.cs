using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class RemovedBugMarkersInMarkerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Markers_MarkerId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_MarkerId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "MarkerId",
                table: "Markers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MarkerId",
                table: "Markers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Markers_MarkerId",
                table: "Markers",
                column: "MarkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Markers_MarkerId",
                table: "Markers",
                column: "MarkerId",
                principalTable: "Markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
