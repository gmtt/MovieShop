using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repositories;
using MovieShop.Infrastructure.Services;

namespace MovieShop.Web
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
			services.AddControllersWithViews();
			services.AddDbContext<MovieShopDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection")));
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.Cookie.Name = "MovieShopAuthCookie";
				options.ExpireTimeSpan = TimeSpan.FromHours(2);
				options.LoginPath = "/Account/Login";
			});
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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			} else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}