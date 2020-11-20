using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class RenameMovieGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genre_GenresId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movie_MoviesId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.RenameTable(
                name: "GenreMovie",
                newName: "MovieGenre");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                columns: new[] { "GenresId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Genre_GenresId",
                table: "MovieGenre",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Movie_MoviesId",
                table: "MovieGenre",
                column: "MoviesId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genre_GenresId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movie_MoviesId",
                table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.RenameTable(
                name: "MovieGenre",
                newName: "GenreMovie");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_MoviesId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "GenresId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genre_GenresId",
                table: "GenreMovie",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
