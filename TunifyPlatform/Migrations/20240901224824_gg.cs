using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TunifyPlatform.Migrations
{
    public partial class gg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing foreign key constraints if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PlaylistSongs_Playlist_PlaylistsId')
                BEGIN
                    ALTER TABLE PlaylistSongs DROP CONSTRAINT FK_PlaylistSongs_Playlist_PlaylistsId
                END
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PlaylistSongs_Song_SongsId')
                BEGIN
                    ALTER TABLE PlaylistSongs DROP CONSTRAINT FK_PlaylistSongs_Song_SongsId
                END
            ");

            // Drop the existing PlaylistSongs table if it exists
            migrationBuilder.DropTable(
                name: "PlaylistSongs");

            // Recreate PlaylistSongs table with the new schema
            migrationBuilder.CreateTable(
                name: "PlaylistSongs",
                columns: table => new
                {
                    PlaylistsId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSongs", x => new { x.PlaylistsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Playlist_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Song_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create a new table for ArtistSongs
            migrationBuilder.CreateTable(
                name: "ArtistSongs",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSongs", x => new { x.ArtistsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_ArtistSongs_Artist_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSongs_Song_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Insert initial data
            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Playlist",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "Chill Vibes", 1 });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, "Pop", "Sunny Day" },
                    { 2, "Jazz", "Rainy Evening" }
                });

            // Create indexes for new tables
            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongs_SongsId",
                table: "PlaylistSongs",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSongs_SongsId",
                table: "ArtistSongs",
                column: "SongsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new tables
            migrationBuilder.DropTable(name: "ArtistSongs");
            migrationBuilder.DropTable(name: "PlaylistSongs");

            // Recreate the PlaylistSongs table with the old schema
            migrationBuilder.CreateTable(
                name: "PlaylistSongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaylistsId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Playlist_PlaylistsId",
                        column: x => x.PlaylistsId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Song_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Song",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Reinsert previous data
            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Playlist",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "Chill Vibes", 1 });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, "Pop", "Sunny Day" },
                    { 2, "Jazz", "Rainy Evening" }
                });

            // Create indexes for the old table
            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongs_SongsId",
                table: "PlaylistSongs",
                column: "SongsId");
        }
    }
}
