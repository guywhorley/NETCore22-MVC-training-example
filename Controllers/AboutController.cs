using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
	[Route("company/[controller]/[action]")]
	public class AboutController : Controller
	{
		// This will be the 'default' route.
		public IActionResult Phone()
		{
			return Ok("1+555.555.5555");
		}
		
		public IActionResult Address()
		{
			return Ok("USA");
		}
	}
}
