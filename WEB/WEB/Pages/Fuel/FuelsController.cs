using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.Fuel.Queryes;
using WEB.Pages.Fuel.Commands;

namespace WEB.Pages.Fuel
{
    [Authorize(Roles = "Admin")]
    public class FuelsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public FuelsController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }



        public async Task<IActionResult> IndexFuel()
        {
            var fuel = await _cache.GetOrAddAsync("fuels_data", async () =>
            {
                var result = await _mediator.Send(new GetAllFuelsQuery());
                return result;
            });
            
            if (fuel != null) return View(fuel);
            else return Problem("ERROR");
        }


        public IActionResult CreateFuel()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFuel(Fuel fuels)
        {
            try
            {
                await _mediator.Send(new AddFuelCommand(fuels));
                _cache.Remove("fuels_data");
                return RedirectToAction("IndexFuel");
            }
            catch
            { 
                return View(fuels);
            }
            
        }



        public async Task<IActionResult> EditFuel(byte id)
        {
            try
            {
                var fuel = await _mediator.Send(new GetFuelByIdQuery(id));

                if (fuel == null)
                {
                    return NotFound();
                }
                return View(fuel);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFuel(byte id, Fuel fuels)
        {
            if (id != fuels.fuel_id)
            {
                return NotFound();
            }
            
                try
                {
                    await _mediator.Send(new UpdateFuelCommand(fuels));
                _cache.Remove("fuels_data");
                }
                catch (DbUpdateConcurrencyException)
                {
                return NotFound();
                }
                return RedirectToAction("IndexFuel");
        }



        public async Task<IActionResult> DeleteFuel(byte id)
        {
            try
            {
                var fuel = await _mediator.Send(new GetFuelByIdQuery(id));
                
                if (fuel == null)
                {
                    return NotFound();
                }

                return View(fuel);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost, ActionName("DeleteFuel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (await _mediator.Send(new GetAllFuelsQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.Fuels'  is null.");
            }

            var fuel = await _mediator.Send(new GetFuelByIdQuery(id));

            if (fuel != null)
            {
                await _mediator.Send(new DeleteFuelCommand(fuel));
                _cache.Remove("fuels_data");
            }
            return RedirectToAction("IndexFuel");
        }

    }
}
