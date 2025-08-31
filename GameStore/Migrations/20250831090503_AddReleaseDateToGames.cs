using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    /// <inheritdoc />
    public partial class AddReleaseDateToGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Games",
                newName: "Name");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Games",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Games",
                newName: "Title");
        }
    }
}
