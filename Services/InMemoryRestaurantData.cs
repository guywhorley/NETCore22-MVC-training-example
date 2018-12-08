using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
	public class InMemoryRestaurantData : IRestaurantData
	{
		// A way to generate in-memory data which could be replaced by db later on.
		private List<Restaurant> _restaurants = new List<Restaurant>
		{
			new Restaurant {Id = 1, Name = "Scott's Pizza Place"},
			new Restaurant {Id = 2, Name = "Flying Dutchman Pies"},
			new Restaurant {Id = 3, Name = "King's Place"}
		};

		public Restaurant Add(Restaurant restaurant)
		{
			restaurant.Id = _restaurants.Max(r => r.Id) + 1;
			_restaurants.Add(restaurant);
			return restaurant;
		}

		public Restaurant Get(int id)
		{
			return _restaurants.FirstOrDefault(r => r.Id == id);
		}

		public IEnumerable<Restaurant> GetAll()
		{
			return _restaurants.OrderBy(r => r.Name);
		}
	}
}
