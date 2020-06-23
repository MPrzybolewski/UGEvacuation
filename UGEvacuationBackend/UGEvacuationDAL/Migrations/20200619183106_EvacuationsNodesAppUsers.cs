using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGEvacuationDAL.Migrations
{
    public partial class EvacuationsNodesAppUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvacuationNodeAppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EvacuationNodeId = table.Column<Guid>(nullable: true),
                    AppUserId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvacuationNodeAppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvacuationNodeAppUser_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvacuationNodeAppUser_EvacuationNode_EvacuationNodeId",
                        column: x => x.EvacuationNodeId,
                        principalTable: "EvacuationNode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvacuationNodeAppUser_AppUserId",
                table: "EvacuationNodeAppUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvacuationNodeAppUser_EvacuationNodeId",
                table: "EvacuationNodeAppUser",
                column: "EvacuationNodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvacuationNodeAppUser");
        }
    }
}
