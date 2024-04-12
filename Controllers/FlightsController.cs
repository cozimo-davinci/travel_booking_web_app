using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
	public class FlightsController : Controller
	{
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var flights = _context.Flights.ToList();

            return View(flights);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
                return NotFound();


            return View(flight);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Flights flight)
        {
            if (!ModelState.IsValid)
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
                return NotFound();


            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId", "Airline", "FlightCode", "Origin", "DepartureTime", "Destination", "ArrivalTime", "TotalFlightTime", "EconomyPrice", "BusinessPrice")] Flights flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    _context.SaveChanges();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        public bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);

            if (flight == null)
                return NotFound();

            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int FlightId)
        {
            var flight = _context.Flights.Find(FlightId);

            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpGet("FlightSearch/{searchOrigin?}/{searchDestination?}")]
        public async Task<IActionResult> FlightSearch(string searchOrigin, string searchDestination)
        {
            var projectsQuery = from p in _context.Flights
                                select p;

            bool searchPerformedOrigin = !string.IsNullOrEmpty(searchOrigin);
            bool searchPerformedDestination = !string.IsNullOrEmpty(searchDestination);
            bool searchPerformedBoth = !string.IsNullOrEmpty(searchDestination) && !string.IsNullOrEmpty(searchOrigin);

            if (searchPerformedBoth)
                projectsQuery = projectsQuery.Where(p => p.Origin.Contains(searchOrigin) && p.Destination.Contains(searchDestination));
            else if (searchPerformedOrigin)
                projectsQuery = projectsQuery.Where(p => p.Origin.Contains(searchOrigin));
            else if (searchPerformedDestination)
                projectsQuery = projectsQuery.Where(p => p.Destination.Contains(searchDestination));

            var flights = await projectsQuery.ToListAsync();
            ViewData["searchPerformedBoth"] = searchPerformedBoth;
            ViewData["searchPerformedOrigin"] = searchPerformedOrigin;
            ViewData["searchPerformedDestination"] = searchPerformedDestination;
            ViewData["searchOrigin"] = searchOrigin;
            ViewData["searchDestination"] = searchDestination;
            return View("Index", flights);
        }


    }
}

