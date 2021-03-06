﻿using Microsoft.Extensions.Configuration;

namespace OdeToFood.Services
{
	public interface IGreeter
	{
		string GetMessageOfTheDay();
	}

	// service implementation
	public class Greeter : IGreeter
	{
		private IConfiguration _configuration;

		// built-in DI will automatically pass in the configuration
		public Greeter(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetMessageOfTheDay()
		{
			return _configuration["Greeting"];
		}
	}
}