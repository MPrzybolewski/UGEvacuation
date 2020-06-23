using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGEvacuationDAL.Migrations
{
    public partial class Evacuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evacuation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlockedEdges = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evacuation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evacuation");
        }
    }
}
