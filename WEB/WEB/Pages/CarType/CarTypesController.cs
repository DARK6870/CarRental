using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.CarType.Commands;
using WEB.Pages.CarType.Queryes;

namespace WEB.Pages.CarType
{
    [Authorize(Roles = "Admin")]
    public class CarTypesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public CarTypesController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }



        public async Task<IActionResult> IndexCarType()
        {
            var carType = await _cache.GetOrAddAsync("car_type_data", async () =>
            {
                var result = await _mediator.Send(new GetAllCarTypesQuery());
                return result;
            });
            if (carType != null) return View(carType);
            else return Problem("ERROR");
        }

        public IActionResult CreateCarType()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarType(CarType carType)
        {
            try
            {
                await _mediator.Send(new AddCarTypeCommand(carType));
                _cache.Remove("car_type_data");
                return RedirectToAction("IndexCarType");
            }
            catch
            {
                return View(carType);
            }
        }



        public async Task<IActionResult> EditCarType(byte id)
        {
            try
            {
                var carType = await _mediator.Send(new GetCarTypeByIdQuery(id));

                if (carType == null)
                {
                    return NotFound();
                }
                return View(carType);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarType(byte id, CarType carType)
        {
            if (id != carType.type_id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new UpdateCarTypeCommand(carType));
                _cache.Remove("car_type_data");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction("IndexCarType");
        }



        public async Task<IActionResult> DeleteCarType(byte id)
        {
            try
            {
                var carType = await _mediator.Send(new GetCarTypeByIdQuery(id));

                if (carType == null)
                {
                    return NotFound();
                }

                return View(carType);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost, ActionName("DeleteCarType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (await _mediator.Send(new GetAllCarTypesQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.CarType'  is null.");
            }

            var carType = await _mediator.Send(new GetCarTypeByIdQuery(id));

            if (carType != null)
            {
                await _mediator.Send(new DeleteCarTypeCommand(carType));
                _cache.Remove("car_type_data");
            }
            return RedirectToAction("IndexCarType");
        }
    }
}
