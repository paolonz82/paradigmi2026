using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unicam.Paradigmi.GestoreMusicale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelazioniOk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenere_Albums_AlbumsIdAlbum",
                table: "AlbumGenere");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenere_Generi_GeneriIdGenere",
                table: "AlbumGenere");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artisti_ArtistaIdArtista",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ArtistaIdArtista",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ArtistaIdArtista",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "GeneriIdGenere",
                table: "AlbumGenere",
                newName: "IdGenere");

            migrationBuilder.RenameColumn(
                name: "AlbumsIdAlbum",
                table: "AlbumGenere",
                newName: "IdAlbum");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenere_GeneriIdGenere",
                table: "AlbumGenere",
                newName: "IX_AlbumGenere_IdGenere");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_IdArtista",
                table: "Albums",
                column: "IdArtista");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenere_Albums_IdAlbum",
                table: "AlbumGenere",
                column: "IdAlbum",
                principalTable: "Albums",
                principalColumn: "IdAlbum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenere_Generi_IdGenere",
                table: "AlbumGenere",
                column: "IdGenere",
                principalTable: "Generi",
                principalColumn: "IdGenere",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artisti_IdArtista",
                table: "Albums",
                column: "IdArtista",
                principalTable: "Artisti",
                principalColumn: "IdArtista",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenere_Albums_IdAlbum",
                table: "AlbumGenere");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenere_Generi_IdGenere",
                table: "AlbumGenere");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artisti_IdArtista",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_IdArtista",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "IdGenere",
                table: "AlbumGenere",
                newName: "GeneriIdGenere");

            migrationBuilder.RenameColumn(
                name: "IdAlbum",
                table: "AlbumGenere",
                newName: "AlbumsIdAlbum");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenere_IdGenere",
                table: "AlbumGenere",
                newName: "IX_AlbumGenere_GeneriIdGenere");

            migrationBuilder.AddColumn<int>(
                name: "ArtistaIdArtista",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistaIdArtista",
                table: "Albums",
                column: "ArtistaIdArtista");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenere_Albums_AlbumsIdAlbum",
                table: "AlbumGenere",
                column: "AlbumsIdAlbum",
                principalTable: "Albums",
                principalColumn: "IdAlbum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenere_Generi_GeneriIdGenere",
                table: "AlbumGenere",
                column: "GeneriIdGenere",
                principalTable: "Generi",
                principalColumn: "IdGenere",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artisti_ArtistaIdArtista",
                table: "Albums",
                column: "ArtistaIdArtista",
                principalTable: "Artisti",
                principalColumn: "IdArtista",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
