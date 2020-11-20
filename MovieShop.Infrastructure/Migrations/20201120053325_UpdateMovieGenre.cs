using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class UpdateMovieGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genre_GenresId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movie_MoviesId",
                table: "MovieGenre");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MovieGenre",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "MovieGenre",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_MoviesId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_GenreId");

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "MovieGenre",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MovieGenre",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_MoviesId");

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
    }
}
