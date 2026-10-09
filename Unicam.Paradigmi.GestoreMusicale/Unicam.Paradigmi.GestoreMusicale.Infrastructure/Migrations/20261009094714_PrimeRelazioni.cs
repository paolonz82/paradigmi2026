using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrimeRelazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistaIdArtista",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AlbumGenere",
                columns: table => new
                {
                    AlbumsIdAlbum = table.Column<int>(type: "int", nullable: false),
                    GeneriIdGenere = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumGenere", x => new { x.AlbumsIdAlbum, x.GeneriIdGenere });
                    table.ForeignKey(
                        name: "FK_AlbumGenere_Albums_AlbumsIdAlbum",
                        column: x => x.AlbumsIdAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumGenere_Generi_GeneriIdGenere",
                        column: x => x.GeneriIdGenere,
                        principalTable: "Generi",
                        principalColumn: "IdGenere",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistaIdArtista",
                table: "Albums",
                column: "ArtistaIdArtista");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumGenere_GeneriIdGenere",
                table: "AlbumGenere",
                column: "GeneriIdGenere");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artisti_ArtistaIdArtista",
                table: "Albums",
                column: "ArtistaIdArtista",
                principalTable: "Artisti",
                principalColumn: "IdArtista",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artisti_ArtistaIdArtista",
                table: "Albums");

            migrationBuilder.DropTable(
                name: "AlbumGenere");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ArtistaIdArtista",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ArtistaIdArtista",
                table: "Albums");
        }
    }
}
