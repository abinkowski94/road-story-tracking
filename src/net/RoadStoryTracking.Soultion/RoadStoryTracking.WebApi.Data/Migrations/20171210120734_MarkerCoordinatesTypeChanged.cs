﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class MarkerCoordinatesTypeChanged : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}