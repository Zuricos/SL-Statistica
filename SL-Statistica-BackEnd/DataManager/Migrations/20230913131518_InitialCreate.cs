using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Migrations
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOne = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberTwo = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberThree = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberFour = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberFive = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberSix = table.Column<int>(type: "INTEGER", nullable: false),
                    LuckyNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Draws");
        }
    }
}
