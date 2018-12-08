using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{	
	public class HomeController : Controller
	{
		private IRestaurantData _restaurantData;
		private IGreeter _greeter;

		public HomeController(IRestaurantData restaurantData,
			                  IGreeter greeter)
		{
			_restaurantData = restaurantData;
			_greeter = greeter;
		}

		public IActionResult Index()
		{
			// Entity-Model
			//var model = _restaurantData.GetAll();

			// set the view-model
			HomeIndexViewModel model = new HomeIndexViewModel();
			model.Restaurants = _restaurantData.GetAll();
			model.CurrentMessage = _greeter.GetMessageOfTheDay();

			return View(model);
		}

		public IActionResult Details(int id)
		{
			var model = _restaurantData.Get(id);
			if (model == null)
				//return NotFound();
				return RedirectToAction(nameof(Index));
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] // always have this on posts for webapps
		public IActionResult Create(RestaurantEditModel model)
		{
			// always check if IsValid
			if (ModelState.IsValid)
			{
				// mapp form data to the new restaurant
				var newRestaurant = new Restaurant();
				newRestaurant.Name = model.Name;
				newRestaurant.Cuisine = model.Cuisine;

				newRestaurant = _restaurantData.Add(newRestaurant);

				// Post-Redirect-Get pattern
				// without this approach, the user could refresh the page and repost.
				return RedirectToAction(nameof(Details), new {id = newRestaurant.Id});
			} 
			else
			{
				// try to create again
				return View();
			}
		}
	}
}
