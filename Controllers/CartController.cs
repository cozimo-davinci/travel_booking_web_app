using COMP2139_Assignment1.Data;
using COMP2139_Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace COMP2139_Assignment1.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var cartWithItems = _context.CartItems
                    .Include(cart => cart.FlightCarts)
                    .ThenInclude(flightCart => flightCart.Flight)
                    .Include(cart => cart.Hotels) // Include hotels
                    .Include(cart => cart.Cars) // Include car rentals
                    .FirstOrDefault();

                return View(cartWithItems);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                ViewBag.ErrorMessage = "Something went wrong while processing your request. Please try again.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult AddFlightToCart(int id, string origin, string destination)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var flight = _context.Flights.Find(id);
                    if (flight != null)
                    {
                        cart.FlightCarts.Add(new FlightCart { FlightID = id, CartID = cart.CartID });
                        _context.SaveChanges();
                        TempData["Notification"] = "Flight is added to cart successfully!";
                    }
                    else
                    {
                        TempData["Notification"] = "Flight is already in your cart";
                    }
                }
                return RedirectToAction("Index", "Flights", new { origin, destination });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return RedirectToAction("Index", "Flights", new { origin, destination });
            }
        }

        [HttpPost]
        public IActionResult RemoveFlightFromCart(int FlightID)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var flightCart = cart.FlightCarts.FirstOrDefault(fc => fc.FlightID == FlightID);
                    if (flightCart != null)
                    {
                        _context.FlightCarts.Remove(flightCart);
                        _context.SaveChanges();
                        TempData["Notification"] = "Flight removed successfully!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle exceptions 
                TempData["Notification"] = "Something went wrong while removing the flight. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult AddHotelToCart(int id, string searchString)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var hotel = _context.Hotels.Find(id);
                    if (hotel != null)
                    {
                        // Assuming each hotel can only be added to the cart once
                        var hotelInCart = cart.Hotels.FirstOrDefault(h => h.HotelsId == id);
                        if (hotelInCart == null)
                        {
                            cart.Hotels.Add(hotel);
                            _context.SaveChanges();
                            TempData["Notification"] = "Hotel is added to cart successfully!";
                        }
                        else
                        {
                            TempData["Notification"] = "Hotel is already in the cart!";
                        }
                    }
                }
                // Redirect to the Hotel Index page with the search string
                return RedirectToAction("Index", "Hotels", new { searchString });
            }
            catch (Exception)
            {
                // Handle exceptions 
                TempData["Notification"] = "Something went wrong while adding the hotel to the cart. Please try again.";
                // Redirect to the Hotel Index page with the search string
                return RedirectToAction("Index", "Hotels", new { searchString });
            }
        }


        [HttpPost]
        public IActionResult RemoveHotelFromCart(int HotelsId)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var hotel = _context.Hotels.Find(HotelsId);
                    if (hotel != null)
                    {
                        // Assuming each hotel can only be added to the cart once
                        var hotelInCart = cart.Hotels.FirstOrDefault(h => h.HotelsId == HotelsId);
                        if (hotelInCart != null)
                        {
                            cart.Hotels.Remove(hotelInCart);
                            _context.SaveChanges();
                            TempData["Notification"] = "Hotel removed successfully!";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Handle exceptions gracefully
                TempData["Notification"] = "Something went wrong while removing the hotel. Please try again.";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult AddRentalToCart(int id, string searchString)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var rental = _context.Cars.Find(id);
                    if (rental != null)
                    {
                        // Assuming each rental can only be added to the cart once
                        var carRentalInCart = cart.Cars.FirstOrDefault(r => r.CarId == id);
                        if (carRentalInCart == null)
                        {
                            cart.Cars.Add(rental);
                            _context.SaveChanges();
                            TempData["Notification"] = "Car Rental added to cart successfully!";
                        }
                        else
                        {
                            TempData["Notification"] = " Car Rental is already in the cart!";
                        }
                    }
                }
                return RedirectToAction("Index", "Cars", new { searchString });
            }
            catch (Exception)
            {
                // Handle exceptions gracefully
                TempData["Notification"] = "An error occurred while adding the rental to the cart.";
                return RedirectToAction("Index", "Cars", new { searchString });
            }
        }


        [HttpPost]
        public IActionResult RemoveRentalFromCart(int CarId)
        {
            try
            {
                var cart = RetrieveOrCreateCart();
                if (cart != null)
                {
                    var rental = _context.Cars.Find(CarId);
                    if (rental != null)
                    {
                        // Assuming each rental can only be added to the cart once
                        var rentalInCart = cart.Cars.FirstOrDefault(r => r.CarId == CarId);
                        if (rentalInCart != null)
                        {
                            cart.Cars.Remove(rentalInCart);
                            _context.SaveChanges();
                            TempData["Notification"] = "Rental removed successfully!";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Handle exceptions gracefully
                TempData["Notification"] = "An error occurred while removing the rental.";
                return RedirectToAction("Index");
            }
        }

        private Cart RetrieveOrCreateCart()
        {
            var cart = _context.CartItems
                .Include(c => c.FlightCarts)
                .Include(c => c.Hotels) // Include hotels 
                .Include(c => c.Cars) // Include car rentals
                .FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart();
                _context.CartItems.Add(cart);
                _context.SaveChanges();
            }
            return cart;
        }
    }
}
