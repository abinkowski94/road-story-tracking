using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class MarkerDateModificationNullable : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificationDate",
                table: "Markers",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificationDate",
                table: "MarkerImages",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificationDate",
                table: "Markers",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ModificationDate",
                table: "MarkerImages",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}