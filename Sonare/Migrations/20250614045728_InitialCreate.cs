using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sonare.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    MediaUrl = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsFinalMix = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClipCollaborators",
                columns: table => new
                {
                    ClipId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipCollaborators", x => new { x.ClipId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ClipCollaborators_Clips_ClipId",
                        column: x => x.ClipId,
                        principalTable: "Clips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClipCollaborators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OriginalClipId = table.Column<int>(type: "integer", nullable: false),
                    ResponseClipId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborations_Clips_OriginalClipId",
                        column: x => x.OriginalClipId,
                        principalTable: "Clips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborations_Clips_ResponseClipId",
                        column: x => x.ResponseClipId,
                        principalTable: "Clips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClipId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Clips_ClipId",
                        column: x => x.ClipId,
                        principalTable: "Clips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "melody@example.com", "hash1", "melodyMaker" },
                    { 2, new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "beats@example.com", "hash2", "beatSmith" },
                    { 3, new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "synth@example.com", "hash3", "synthQueen" }
                });

            migrationBuilder.InsertData(
                table: "Clips",
                columns: new[] { "Id", "CreatedAt", "Description", "IsFinalMix", "MediaUrl", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "A smooth synthwave track", true, "/media/dreamy.mp3", "Dreamy Synths", 1 },
                    { 2, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Punchy drum loop for hip-hop", false, "/media/hiphop-drums.mp3", "Hip-Hop Drums", 2 },
                    { 3, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Layered ambient textures", true, "/media/ambient.mp3", "Ambient Layers", 3 }
                });

            migrationBuilder.InsertData(
                table: "ClipCollaborators",
                columns: new[] { "ClipId", "UserId", "CreatedAt", "Note", "Role" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polished the final mix", "Mixer" },
                    { 2, 1, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Added percussive elements", "Drummer" },
                    { 3, 2, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helped layer ambient elements", "Producer" }
                });

            migrationBuilder.InsertData(
                table: "Collaborations",
                columns: new[] { "Id", "CreatedAt", "OriginalClipId", "ResponseClipId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 2, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "ClipId", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Love the vibe on this!", 1, new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, "These drums hit hard. 🔥", 2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, "Very cinematic feel, nice job!", 3, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClipCollaborators_UserId",
                table: "ClipCollaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_UserId",
                table: "Clips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_OriginalClipId",
                table: "Collaborations",
                column: "OriginalClipId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_ResponseClipId",
                table: "Collaborations",
                column: "ResponseClipId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ClipId",
                table: "Comments",
                column: "ClipId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClipCollaborators");

            migrationBuilder.DropTable(
                name: "Collaborations");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Clips");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
