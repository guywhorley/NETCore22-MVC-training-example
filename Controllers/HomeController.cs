using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{	
	public class HomeController : Controller
	{
		private IRestaurantData _restaurantData;

		public HomeController(IRestaurantData restaurantData)
		{
			_restaurantData = restaurantData;
		}

		public IActionResult Index()
		{	
			// get the model
			var model = _restaurantData.GetAll();

			//return new ObjectResult(model);
			return View(model);
		}
	}
}
