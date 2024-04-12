using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Assignment1.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string location, string rating)
        {

            var hotels = _context.Hotels.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                hotels = (List<Hotels>)hotels.Where(hotel =>
                    (string.IsNullOrEmpty(searchString) || hotel.Name.Contains(searchString)) ||
                    (string.IsNullOrEmpty(searchString) || hotel.Description.Contains(searchString)) ||
                    (string.IsNullOrEmpty(location) || hotel.Address.Contains(location)) ||
                    (string.IsNullOrEmpty(rating) || hotel.Rating.Contains(rating))

                );
            }

            ViewData["SearchString"] = searchString;
            return View(hotels);
        }


        // play with this search
        public async Task<IActionResult> Search(string searchString, string location, string rating)
        {
            var hotelQuery = from h in _context.Hotels
                             select h;

            bool searchPerformed = !string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(location) || !string.IsNullOrEmpty(rating);

            if (searchPerformed)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    hotelQuery = hotelQuery.Where(hotel => hotel.Name.Contains(searchString) || hotel.Description.Contains(searchString));
                }

                if (!string.IsNullOrEmpty(location))
                {
                    hotelQuery = hotelQuery.Where(hotel => hotel.Address.Contains(location));
                }

                if (!string.IsNullOrEmpty(rating))
                {
                    hotelQuery = hotelQuery.Where(hotel => hotel.Rating.Contains(rating));
                }
            }

            var hotels = await hotelQuery.ToListAsync();

            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;

            return View("Index", hotels);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> Create(Hotels hotel, IFormFile file)
        {

            if(ModelState.IsValid)
            {
                if(file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    hotel.ImagePath = "/images/" + fileName; // Set the file path after the file is saved
                }


                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        public IActionResult Details(int id)
        {
            var hotels = _context.Hotels.FirstOrDefault(h => h.HotelsId == id);

            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var hotels = _context.Hotels.Find(id);

            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
         
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit( int id, [Bind("HotelsId", "Name", "Description", "Type", "Address", "Rating", "Price", "AvailabilityStartDate", "AvailabilityEndDate")] Hotels hotel)
        {
            if (id != hotel.HotelsId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    _context.SaveChanges();
                }
                catch (DBConcurrencyException)
                {
                    if(!HotelExists(hotel.HotelsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        public bool HotelExists (int id)
        {
            return _context.Hotels.Any(h => h.HotelsId == id);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelsId == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteConfirmed(int HotelsId)
        {
            var hotel = _context.Hotels.Find(HotelsId);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
