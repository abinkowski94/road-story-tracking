using Microsoft.EntityFrameworkCore.Migrations;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class endDateRenamed : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Markers",
                newName: "ValidOnMapTo");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidOnMapTo",
                table: "Markers",
                newName: "EndDate");
        }
    }
}