using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesTakerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesContributorPs1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "NoteContributors");

            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "NoteContributors");

            migrationBuilder.DropColumn(
                name: "ContributedAt",
                table: "NoteContributors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "NoteContributors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "NoteContributors",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContributedAt",
                table: "NoteContributors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");
        }
    }
}
