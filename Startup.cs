using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Data;
using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
	public class Startup
	{
		private IConfiguration _configuration;

		// In order to use configuration in configure services for EF, you must inject configuration into the ctor of Startup
		public Startup(IConfiguration configuration)
		{			
			_configuration = configuration;
		}

		// Built-in DI: This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IGreeter, Greeter>();
			services.AddDbContext<OdeToFoodDbContext>(
				options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
			services.AddScoped<IRestaurantData, SqlRestaurantData>();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		// Optionally, you can pass IConfiguration into Configure(). But you also have to manually add
		// appsetting.json file to the root of the project. Is run once. Configures middleware.
		public void Configure(IApplicationBuilder app,
							  IHostingEnvironment env,
							  //IConfiguration configuration)
							  IGreeter greeter,
							  ILogger<Startup> logger)
		{
			// useful for unhandled exceptions
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Convention-based routing
			//MapRouteRouteBuilderExtensions.MapRoute("Default", "{controller=Home}/{action=Index/{id?}");

			#region inline
			// in-line middleware components
			//app.Use(next => { 
			//	return async context =>
			//	{
			//		logger.LogInformation("Request incoming");
			//		if (context.Request.Path.StartsWithSegments("/mym"))
			//		{
			//			await context.Response.WriteAsync("Hit!!");
			//			logger.LogInformation("Request handled");
			//		}
			//		else
			//		{
			//			await next(context);
			//			logger.LogInformation("Response outgoing");
			//		}
			//	};
			//});

			// baked-in WelcomePage middleware that only responds to /wp in url
			//app.UseWelcomePage(new WelcomePageOptions
			//{
			//	Path="/wp"
			//});
			#endregion

			app.UseStaticFiles();

			//app.UseMvcWithDefaultRoute();
			//app.UseMvc(); // no routes configured
			app.UseMvc(ConfigureRoutes);

			app.Run(async (context) =>
			{
				
				// order of settings precedence (?): appsettings.json, env.vars, cli-args
				// Also... you can also pass in from cli: dotnet run Greeting="<some-new-text>" [enter]
				//var greeting = configuration["Greeting"];
				var greeting = greeter.GetMessageOfTheDay(); //configuration["Greeting"];
				context.Response.ContentType = "text/plain";
				//await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
				await context.Response.WriteAsync($"Not found : {env.EnvironmentName}");
			});
		}

		private void ConfigureRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute("Default", "{controller=Home}/{action=index}/{id?}");
		}
	}
}
