using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UwpCommunity.Data.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Launch",
                columns: table => new
                {
                    LaunchId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launch", x => x.LaunchId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsOwner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

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
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DiscordId = table.Column<string>(nullable: true)
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
                    Description = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    DownloadLink = table.Column<string>(nullable: true),
                    GithubLink = table.Column<string>(nullable: true),
                    ExternalLink = table.Column<string>(nullable: true),
                    AwaitingLaunchApproval = table.Column<bool>(nullable: false),
                    NeedsManualReview = table.Column<bool>(nullable: false),
                    LookingForRoles = table.Column<string>(nullable: true),
                    HeroImage = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaunchProject",
                columns: table => new
                {
                    LaunchId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaunchProject", x => new { x.LaunchId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_LaunchProject_Launch_LaunchId",
                        column: x => x.LaunchId,
                        principalTable: "Launch",
                        principalColumn: "LaunchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaunchProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProject",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    ClientLastUpdated = table.Column<DateTimeOffset>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProject", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_UserProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProject_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_LastUpdated",
                table: "Category",
                column: "LastUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_Launch_LastUpdated",
                table: "Launch",
                column: "LastUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_LaunchProject_ProjectId",
                table: "LaunchProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LastUpdated",
                table: "Projects",
                column: "LastUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_Role_LastUpdated",
                table: "Role",
                column: "LastUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_ProjectId",
                table: "UserProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_RoleId",
                table: "UserProject",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastUpdated",
                table: "Users",
                column: "LastUpdated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaunchProject");

            migrationBuilder.DropTable(
                name: "UserProject");

            migrationBuilder.DropTable(
                name: "Launch");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
