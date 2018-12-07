using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{	
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			Restaurant model = new Restaurant
			{
				Id = 1,
				Name = "Arbys"
			};
			// is serialized as json
			return new ObjectResult(model);
		}
	}
}
