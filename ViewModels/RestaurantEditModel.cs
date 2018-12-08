using OdeToFood.Models;

namespace OdeToFood.ViewModels
{
	// an input-only view model which only considers the values 
	// which map to the fields you really care about and ignore the rest.
	// these values will generally be provided via form data from client.
	public class RestaurantEditModel
	{
		public string Name { get; set; }
		public CuisineType Cuisine { get; set; }
	}
}
