using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UwpCommunity.Data.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    AppName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId1",
                table: "Projects",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
