using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace COMP2139_Assignment1.Controllers
{
	public class CarsController : Controller
	{

        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Cars.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Cars car, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    car.ImagePath = "/images/" + fileName;
                }

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(car);
        }


        public IActionResult Details(int id)
        {
            var cars = _context.Cars.FirstOrDefault(c => c.CarId == id);

            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cars = _context.Cars.Find(id);

            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Model,RentalCompany,Price,AvailabilityStartDate,AvailabilityEndDate,ImagePath")] Cars car, IFormFile file)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        car.ImagePath = "/images/" + fileName;
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(c => c.CarId == id);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteConfirmed(int CarId)
        {
            var car = _context.Cars.Find(CarId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        [HttpGet("Search/{searchString?}")]

        public async Task<IActionResult> Search(string searchString)
        {
            var carsQuery = from c in _context.Cars
                            select c;
            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            if (searchPerformed)
            {
                carsQuery = carsQuery.Where(c => c.Model.Contains(searchString) || c.RentalCompany.Contains(searchString));

            }

            var cars = await carsQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", cars);
        }

    }
}

