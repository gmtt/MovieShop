﻿namespace MovieShop.Core.Models.Request
{
	public class ReviewRequestModel
	{
		public int UserId { get; set; }
		public int MovieId { get; set; }
		public string ReviewText { get; set; }
		public double Rating { get; set; }
	}
}