using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using MovieShop.Infrastructure.Services;

namespace MovieShop.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<MovieShopDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo {Title = "MovieShop.API", Version = "v1"});
			});
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<IMovieRepository, MovieRepository>();
			services.AddScoped<IMovieRepository, MovieRepository>();
			services.AddScoped<IPurchaseRepository, PurchaseRepository>();
			services.AddScoped<ICastRepository, CastRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<ICastService, CastService>();
			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IAsyncRepository<Genre>, EFRepository<Genre>>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ICryptoService, CryptoService>();
			services.AddScoped<IAsyncRepository<Favorite>, EFRepository<Favorite>>();
			services.AddScoped<IAsyncRepository<Review>, EFRepository<Review>>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieShop.API v1"));
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}