using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGEvacuationDAL.Migrations
{
    public partial class EvacuationsNodesAppUsersUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationNodeAppUser_AppUser_AppUserId",
                table: "EvacuationNodeAppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationNodeAppUser_EvacuationNode_EvacuationNodeId",
                table: "EvacuationNodeAppUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvacuationNodeId",
                table: "EvacuationNodeAppUser",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "EvacuationNodeAppUser",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationNodeAppUser_AppUser_AppUserId",
                table: "EvacuationNodeAppUser",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationNodeAppUser_EvacuationNode_EvacuationNodeId",
                table: "EvacuationNodeAppUser",
                column: "EvacuationNodeId",
                principalTable: "EvacuationNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationNodeAppUser_AppUser_AppUserId",
                table: "EvacuationNodeAppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationNodeAppUser_EvacuationNode_EvacuationNodeId",
                table: "EvacuationNodeAppUser");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvacuationNodeId",
                table: "EvacuationNodeAppUser",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "EvacuationNodeAppUser",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationNodeAppUser_AppUser_AppUserId",
                table: "EvacuationNodeAppUser",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationNodeAppUser_EvacuationNode_EvacuationNodeId",
                table: "EvacuationNodeAppUser",
                column: "EvacuationNodeId",
                principalTable: "EvacuationNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
