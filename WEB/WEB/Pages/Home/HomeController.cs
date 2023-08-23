using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using MediatR;
using LazyCache;
using WEB.Pages.Fuel.Queryes;
using WEB.Pages.Cars.Queryes;

namespace WEB.Pages.Home
{

    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;
        private readonly IAppCache _cache;

        public HomeController(ILogger<HomeController> logger, IMediator mediadtor, IAppCache cache)
        {
            _logger = logger;
            _mediator = mediadtor;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _cache.GetOrAddAsync("cars_data", async () =>
            {
                var result = await _mediator.Send(new GetAllCarViewQuery());
                return result;
            });
            if (cars != null)
            {
                return View(cars);
            }
            else
            {
                return View();
            }
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

        public async Task<IActionResult> Cars()
        {
            try
            {
                var cars = await _mediator.Send(new GetAllCarViewQuery());
                return View(cars);
            }
            catch (Exception ex)
            {
                return View(Index);
            }
        }
    }
}