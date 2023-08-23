using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.Fuel.Queryes;
using WEB.Pages.Transmission.Queryes;
using WEB.Pages.CarBody.Queryes;
using LazyCache;
using WEB.Pages.CarType.Queryes;
using WEB.Pages.DriveType.Queryes;
using WEB.Pages.Cars.Models;
using WEB.Pages.Cars.Queryes;
using WEB.Pages.Reservation.Queryes;
using WEB.Pages.Cars.Commdns;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Pages.Cars
{
	[Authorize(Roles = "Admin")]
	public class CarsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;
        public CarsController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }


        public async Task<IActionResult> IndexCar()
        {
            var car = await _cache.GetOrAddAsync("car_data", async () =>
            {
                var result = await _mediator.Send(new GetAllCarsQuery());
                return result;
            });
            if (car != null) return View(car);
            else return Problem("ERROR");
        }

        public async Task<IActionResult> Create()
        {
            var model = new NewCar
            {
                Fuels = await _cache.GetOrAddAsync("fuels_data", async () =>
                {
                    var result = await _mediator.Send(new GetAllFuelsQuery());
                    return result;
                }),

                CarType = await _cache.GetOrAddAsync("car_type_data", async () =>
                {
                    var result = await _mediator.Send(new GetAllCarTypesQuery());
                    return result;
                }),

                Transmission = await _cache.GetOrAddAsync("transmissions_data", async () =>
                {
                    var result = await _mediator.Send(new GetAllTransmissionsQuery());
                    return result;
                }),

                Reservation = await _cache.GetOrAddAsync("reservation_data", async () =>
                {
                    var result = await _mediator.Send(new GetReservationStatusQuery());
                    return result;
                }),

                Drive_type = await _cache.GetOrAddAsync("drive_type_data", async () =>
                {
                    var result = await _mediator.Send(new GetAllDriveTypesQuery());
                    return result;
                }),

                BodyType = await _cache.GetOrAddAsync("car_body_data", async () =>
                {
                    var result = await _mediator.Send(new GetAllCarBodyQuery());
                    return result;
                })
            };

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Create(Car car_)
        {
            try
            {
                await _mediator.Send(new AddCarCommand(car_));
                _cache.Remove("car_data");
                _cache.Remove("cars_data");
                return RedirectToAction("IndexCar");
            }
            catch
            {
                return View(car_);
            }
        }




        public async Task<IActionResult> Edit(short id)
        {
            try
            {
                var car = await _mediator.Send(new GetCarByIdQuery(id));

                if (car == null)
                {
                    return NotFound();
                }
                return View(car);
            }
            catch
            {
                return NotFound();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, Car car)
        {
            if (id != car.car_id)
            {
                return NotFound();
            }
            try
            {
                await _mediator.Send(new UpdateCarCommand(car));
                _cache.Remove("car_data");
                _cache.Remove("cars_data");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction("IndexCar");
        }



        public async Task<IActionResult> Delete(short id)
        {
            try
            {
                var car = await _mediator.Send(new GetCarByIdQuery(id));

                if (car == null)
                {
                    return NotFound();
                }

                return View(car);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (await _mediator.Send(new GetAllCarsQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.Car'  is null.");
            }

            var car = await _mediator.Send(new GetCarByIdQuery(id));

            if (car != null)
            {
                await _mediator.Send(new DeleteCarCommand(car));
                _cache.Remove("car_data");
                _cache.Remove("cars_data");
            }

            return RedirectToAction("IndexCar");
        }
    }
}
