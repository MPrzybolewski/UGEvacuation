using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGEvacuationDAL.Migrations
{
    public partial class EvacuationsNodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvacuationNode",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NodeId = table.Column<int>(nullable: false),
                    EvacuationId = table.Column<Guid>(nullable: false),
                    Density = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvacuationNode", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvacuationNode");
        }
    }
}
