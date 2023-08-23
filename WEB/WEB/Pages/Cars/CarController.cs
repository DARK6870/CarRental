using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.Cars.Queryes;
using WEB.Pages.OrderPage.Commands;
using WEB.Pages.OrderPage.Models;
using System.Net;
using System.Net.Mail;

namespace WEB.Pages.Cars
{
    public class CarController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;
        private readonly UserManager<IdentityUser> _userManager;
        public CarController(IMediator mediator, IAppCache cache, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _cache = cache;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Car()
        {
            return View();
        }

        public async Task<IActionResult> Cars(List<string> selectedGearBoxes, List<string> selectedEngineTypes, List<string> selectedBrands, string sorting = "")
        {
            ViewBag.selectedEngineTypes = selectedEngineTypes;
            ViewBag.selectedGearBoxes = selectedGearBoxes;
            ViewBag.selectedBrands = selectedBrands;
            ViewBag.sorting = sorting;
            TempData["Sorted"] = sorting;

            var cars = await _cache.GetOrAddAsync("cars_data", async () =>
            {
                var result = await _mediator.Send(new GetAllCarViewQuery());
                return result;
            });

            if (selectedGearBoxes.Count > 0)
                cars = cars.Where(x => selectedGearBoxes.Contains(x.transmission_name)).ToList();
            if (selectedEngineTypes.Count > 0)
                cars = cars.Where(x => selectedEngineTypes.Contains(x.fuel_name)).ToList();
            if (selectedBrands.Count > 0)
                cars = cars.Where(x => selectedBrands.Contains(x.body_name)).ToList();


            if (sorting == "cheapest")
            {
                cars = cars.OrderBy(x => x.price).ToList();
            }
            else if (sorting == "expensive")
            {
                cars = cars.OrderByDescending(x => x.price).ToList();
            }

            return View(cars);
        }


        public async Task<IActionResult> Rent(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var rent = await _mediator.Send(new GetCarsViewByIdQuery(id));

            return View(rent);
        }


        public async Task<IActionResult> PlaceOrder(DateTime startDateTime, DateTime endDateTime, short carId, short price)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }



            var user = await _userManager.GetUserAsync(User);

            var order = new Order
            {
                status_id = 1,
                AppUserId = user.Id,
                Phone = user.PhoneNumber,
                start_day = startDateTime,
                end_day = endDateTime,
                car_id = carId,
                total_price = (endDateTime.Date - startDateTime.Date).Days * price
            };
                
                await _mediator.Send(new AddOrderCommand(order));

                return RedirectToAction("Profile", "Account");
            }

    }
}
