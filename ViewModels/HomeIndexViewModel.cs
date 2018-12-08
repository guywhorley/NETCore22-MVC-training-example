using OdeToFood.Models;
using System.Collections.Generic;


namespace OdeToFood.ViewModels
{
	// This is the Data Transfer Object which contains everything that
	// the view will need to render the page, include the actual entity
	// model data.
	public class HomeIndexViewModel
	{
		public IEnumerable<Restaurant> Restaurants { get; set; }
		public string CurrentMessage { get; set; }
	}
}
