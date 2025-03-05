using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedWinner.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContestId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    SeedTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SeedRandomPart = table.Column<string>(type: "TEXT", nullable: false),
                    SeedAdditionalEntropy = table.Column<string>(type: "TEXT", nullable: false),
                    Results = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditableResults",
                columns: table => new
                {
                    DrawId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditableResults", x => x.DrawId);
                    table.ForeignKey(
                        name: "FK_AuditableResults_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_ContestId_Title",
                table: "Draws",
                columns: new[] { "ContestId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditableResults");

            migrationBuilder.DropTable(
                name: "Draws");
        }
    }
}
