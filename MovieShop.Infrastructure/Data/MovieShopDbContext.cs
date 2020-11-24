using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
	public class MovieShopDbContext : DbContext
	{
		public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) { }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<MovieGenre> MovieGenres { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Trailer> Trailers { get; set; }
		public DbSet<Cast> Casts { get; set; }
		public DbSet<MovieCast> MovieCasts { get; set; }
		public DbSet<Crew> Crews { get; set; }
		public DbSet<MovieCrew> MovieCrews { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Purchase> Purchases { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>(ConfigureMovie);
			modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
			modelBuilder.Entity<Trailer>(ConfigureTrailer);
			modelBuilder.Entity<Cast>(ConfigureCast);
			modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
			modelBuilder.Entity<Crew>(ConfigureCrew);
			modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
			modelBuilder.Entity<User>(ConfigureUser);
			modelBuilder.Entity<Review>(ConfigureReview);
			modelBuilder.Entity<Purchase>(ConfigurePurchase);
		}

		private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
		{
			builder.ToTable("Movie");
			builder.HasKey(m => m.Id);
			builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
			builder.Property(m => m.Overview).HasMaxLength(4096);
			builder.Property(m => m.Tagline).HasMaxLength(512);
			builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
			builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
			builder.Property(m => m.PosterUrl).HasMaxLength(2084);
			builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
			builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
			builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
			builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
		}

		private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
		{
			builder.ToTable("Trailer");
			builder.HasKey(t => t.Id);
			builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
			builder.Property(t => t.Name).HasMaxLength(2084);
		}

		private void ConfigureCast(EntityTypeBuilder<Cast> builder)
		{
			builder.ToTable("Cast");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).HasMaxLength(128);
			builder.Property(c => c.Gender).HasColumnType("nvarchar(max)");
			builder.Property(c => c.TmdbUrl).HasColumnType("nvarchar(max)");
			builder.Property(c => c.ProfilePath).HasMaxLength(2084);
		}

		private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
		{
			builder.ToTable("MovieCast");
			builder.Property(m => m.Character).HasMaxLength(450);
			builder.HasKey(m => new {m.MovieId, m.CastId, m.Character});
		}

		private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
		{
			builder.ToTable("Crew");
			builder.Property(x => x.Name).HasMaxLength(128);
			builder.Property(x => x.ProfilePath).HasMaxLength(2084);
			builder.Property(x => x.Gender).HasColumnType("nvarchar(max)");
			builder.Property(x => x.TmdbUrl).HasColumnType("nvarchar(max)");
		}

		private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
		{
			builder.ToTable("MovieCrew");
			builder.Property(x => x.Department).HasMaxLength(128);
			builder.Property(x => x.Job).HasMaxLength(128);
			builder.HasKey(x => new {x.MovieId, x.CrewId, x.Department, x.Job});
		}

		private void ConfigureUser(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("User");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.FirstName).HasMaxLength(128);
			builder.Property(x => x.LastName).HasMaxLength(128);
			builder.Property(x => x.Email).HasMaxLength(256);
			builder.Property(x => x.HashedPassword).HasMaxLength(1024);
			builder.Property(x => x.Salt).HasMaxLength(1024);
			builder.Property(x => x.PhoneNumber).HasMaxLength(16);
		}

		private void ConfigureReview(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Review");
			builder.HasKey(x => new {x.MovieId, x.UserId});
			builder.Property(x => x.Rating).HasPrecision(3, 2);
			builder.Property(x => x.ReviewText).HasColumnType("nvarchar(max)");
		}

		private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
		{
			builder.ToTable("MovieGenre");
			builder.HasKey(x => new {x.MovieId, x.GenreId});
		}

		private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
		{
			builder.ToTable("Purchase");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.TotalPrice).HasPrecision(18, 2);
		}
	}
}