using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class FriendStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Friends");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Friends",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Friends");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Friends",
                nullable: false,
                defaultValue: false);
        }
    }
}
