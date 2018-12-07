using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
	public class Startup
	{
		// Built-in DI: This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IGreeter, Greeter>();
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

			app.Run(async (context) =>
			{
				
				// order of settings precedence (?): appsettings.json, env.vars, cli-args
				// Also... you can also pass in from cli: dotnet run Greeting="<some-new-text>" [enter]
				//var greeting = configuration["Greeting"];
				var greeting = greeter.GetMessageOfTheDay(); //configuration["Greeting"];
				await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
			});
		}
	}
}
