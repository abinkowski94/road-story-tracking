using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class UniqueIdsForFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_RequestedById",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestedById_RequestedToId",
                table: "Friends",
                columns: new[] { "RequestedById", "RequestedToId" },
                unique: true,
                filter: "[RequestedById] IS NOT NULL AND [RequestedToId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_RequestedById_RequestedToId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestedById",
                table: "Friends",
                column: "RequestedById");
        }
    }
}
