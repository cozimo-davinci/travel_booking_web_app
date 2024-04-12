using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace COMP2139_Assignment1.Controllers
{
	public class HomeController : Controller
	{
		

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        
        public IActionResult Search(string query, string filter = "All")
        {
            var viewModel = new Search();

            if (filter == "All" || filter == "Flights")
            {
                viewModel.Flights = _context.Flights
                    .Where(f => f.Airline.Contains(query) ||
                                f.Origin.Contains(query) ||
                                f.Destination.Contains(query)).ToList();


            }

            if (filter == "All" || filter == "Hotels")
            {
                viewModel.Hotels = _context.Hotels
                    .Where(h => h.Name.Contains(query) ||
                                h.Address.Contains(query) ||
                                h.Address.Contains(query))
                    .ToList();

            }

            if (filter == "All" || filter == "CarRentals")
            {
                viewModel.CarRentals = _context.Cars
                    .Where(c => c.Model.Contains(query) || c.RentalCompany.Contains(query) ||
                                c.Price.ToString().Contains(query))
                    .ToList();

            }

            return View("SearchResults", viewModel);
        }
        
    }
}
