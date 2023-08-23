using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.Transmission.Queryes;
using WEB.Pages.Transmission.Commands;

namespace WEB.Pages.Transmission
{
    [Authorize(Roles = "Admin")]
    public class TransmissionsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public TransmissionsController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }



        public async Task<IActionResult> IndexTransmission()
        {

            var transmissions = await _cache.GetOrAddAsync("transmissions_data", async () =>
            {
                var result = await _mediator.Send(new GetAllTransmissionsQuery());
                return result;
            });

            if (transmissions != null)
            {
                return View(transmissions);
            }
            else
            {
                return Problem("Entity set 'AppDBContext.Transmission' is null.");
            }
        }


        public IActionResult CreateTransmission()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransmission(Transmission transmission)
        {
            try
            {
                await _mediator.Send(new AddTransmissionCommand(transmission));
                _cache.Remove("transmissions_data");
                return RedirectToAction("IndexTransmission");

            }
            catch
            {
                return View(transmission);
            }
        }



        public async Task<IActionResult> EditTransmission(byte id)
        {
            try
            {
                var transmission = await _mediator.Send(new GetTransmissionByIdQuery(id));

                if (transmission == null)
                {
                    return NotFound();
                }
                return View(transmission);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransmission(byte id, Transmission transmission)
        {
            if (id != transmission.transmission_id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new UpdateTransmissionCommand(transmission));
                _cache.Remove("transmissions_data");
            }

            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction("IndexTransmission");
        }



        public async Task<IActionResult> DeleteTransmission(byte id)
        {
            try
            {
                var transmission = await _mediator.Send(new GetTransmissionByIdQuery(id));

                if (transmission == null)
                {
                    return NotFound();
                }

                return View(transmission);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }


        [HttpPost, ActionName("DeleteTransmission")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (await _mediator.Send(new GetAllTransmissionsQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.Transmission'  is null.");
            }

            var transmission = await _mediator.Send(new GetTransmissionByIdQuery(id));

            if (transmission != null)
            {
                await _mediator.Send(new DeleteTransmissionCommand(transmission));
                _cache.Remove("transmissions_data");
            }

            return RedirectToAction("IndexTransmission");
        }
    }
}
