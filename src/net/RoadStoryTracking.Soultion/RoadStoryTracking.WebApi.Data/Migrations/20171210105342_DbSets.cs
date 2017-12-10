using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class DbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marker_User_ApplicationUserId",
                table: "Marker");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkerImage_Marker_MarkerId",
                table: "MarkerImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkerImage",
                table: "MarkerImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marker",
                table: "Marker");

            migrationBuilder.RenameTable(
                name: "MarkerImage",
                newName: "MarkerImages");

            migrationBuilder.RenameTable(
                name: "Marker",
                newName: "Markers");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerImage_MarkerId",
                table: "MarkerImages",
                newName: "IX_MarkerImages_MarkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Marker_ApplicationUserId",
                table: "Markers",
                newName: "IX_Markers_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkerImages",
                table: "MarkerImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markers",
                table: "Markers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerImages_Markers_MarkerId",
                table: "MarkerImages",
                column: "MarkerId",
                principalTable: "Markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_User_ApplicationUserId",
                table: "Markers",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkerImages_Markers_MarkerId",
                table: "MarkerImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Markers_User_ApplicationUserId",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markers",
                table: "Markers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkerImages",
                table: "MarkerImages");

            migrationBuilder.RenameTable(
                name: "Markers",
                newName: "Marker");

            migrationBuilder.RenameTable(
                name: "MarkerImages",
                newName: "MarkerImage");

            migrationBuilder.RenameIndex(
                name: "IX_Markers_ApplicationUserId",
                table: "Marker",
                newName: "IX_Marker_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MarkerImages_MarkerId",
                table: "MarkerImage",
                newName: "IX_MarkerImage_MarkerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marker",
                table: "Marker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkerImage",
                table: "MarkerImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marker_User_ApplicationUserId",
                table: "Marker",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkerImage_Marker_MarkerId",
                table: "MarkerImage",
                column: "MarkerId",
                principalTable: "Marker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
