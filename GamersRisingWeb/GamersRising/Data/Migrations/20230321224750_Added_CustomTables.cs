using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_CustomTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveTourneys = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentMode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentMode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCatagory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCatagory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCatagory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCatagory_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    TournamentFormatId = table.Column<int>(type: "int", nullable: false),
                    TournamentModeId = table.Column<int>(type: "int", nullable: false),
                    TournamentTypeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournament_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournament_TournamentFormat_TournamentFormatId",
                        column: x => x.TournamentFormatId,
                        principalTable: "TournamentFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournament_TournamentMode_TournamentModeId",
                        column: x => x.TournamentModeId,
                        principalTable: "TournamentMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournament_TournamentType_TournamentTypeId",
                        column: x => x.TournamentTypeId,
                        principalTable: "TournamentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HTMLContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Content_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournament",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_TournamentId",
                table: "Content",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_GameId",
                table: "Tournament",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_TournamentFormatId",
                table: "Tournament",
                column: "TournamentFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_TournamentModeId",
                table: "Tournament",
                column: "TournamentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCatagory_GameId",
                table: "UserCatagory",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCatagory_UserId",
                table: "UserCatagory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "UserCatagory");

            migrationBuilder.DropTable(
                name: "Tournament");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "TournamentFormat");

            migrationBuilder.DropTable(
                name: "TournamentMode");

            migrationBuilder.DropTable(
                name: "TournamentType");
        }
    }
}
