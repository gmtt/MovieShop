namespace MovieShop.Core.Entities
{
	public class Trailer
	{
		public int Id { get; set; }
		public int MovieId { get; set; }
		// Navigation properties, help is navigate to related entities
		public Movie Movie { get; set; }
		public string TrailerUrl { get; set; }
		public string Name { get; set; }
	}
}