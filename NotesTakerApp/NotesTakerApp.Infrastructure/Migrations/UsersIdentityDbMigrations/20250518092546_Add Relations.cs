using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesTakerApp.Infrastructure.Migrations.UsersIdentityDbMigrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteContributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteContributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteContributors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteContributors_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NoteContributors_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteUser",
                columns: table => new
                {
                    SharedNotesId = table.Column<int>(type: "int", nullable: false),
                    SharedWithId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteUser", x => new { x.SharedNotesId, x.SharedWithId });
                    table.ForeignKey(
                        name: "FK_NoteUser_AspNetUsers_SharedWithId",
                        column: x => x.SharedWithId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteUser_Notes_SharedNotesId",
                        column: x => x.SharedNotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributors_NoteId",
                table: "NoteContributors",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributors_UserId",
                table: "NoteContributors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteContributors_UserId1",
                table: "NoteContributors",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUser_SharedWithId",
                table: "NoteUser",
                column: "SharedWithId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteContributors");

            migrationBuilder.DropTable(
                name: "NoteUser");
        }
    }
}
