using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Migrations
{
    public partial class Contacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    RequestedById = table.Column<string>(nullable: false),
                    RequestedToId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_User_RequestedById",
                        column: x => x.RequestedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_User_RequestedToId",
                        column: x => x.RequestedToId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RequestedToId",
                table: "Contacts",
                column: "RequestedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RequestedById_RequestedToId",
                table: "Contacts",
                columns: new[] { "RequestedById", "RequestedToId" },
                unique: true,
                filter: "[RequestedById] IS NOT NULL AND [RequestedToId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    RequestedById = table.Column<string>(nullable: false),
                    RequestedToId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_User_RequestedById",
                        column: x => x.RequestedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friends_User_RequestedToId",
                        column: x => x.RequestedToId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestedToId",
                table: "Friends",
                column: "RequestedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestedById_RequestedToId",
                table: "Friends",
                columns: new[] { "RequestedById", "RequestedToId" },
                unique: true,
                filter: "[RequestedById] IS NOT NULL AND [RequestedToId] IS NOT NULL");
        }
    }
}
