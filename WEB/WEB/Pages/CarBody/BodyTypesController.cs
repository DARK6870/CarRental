using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.CarBody.Commands;
using WEB.Pages.CarBody.Queryes;

namespace WEB.Pages.CarBody
{
    [Authorize(Roles = "Admin")]
    public class BodyTypesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public BodyTypesController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }


        public async Task<IActionResult> IndexCarBody()
        {
            var bodyType = await _cache.GetOrAddAsync("body_type_data", async () =>
            {
                var result = await _mediator.Send(new GetAllCarBodyQuery());
                return result;
            });
            if (bodyType != null) return View(bodyType);
            else return Problem("ERROR");
        }



        public IActionResult CreateCarBody()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCarBody(BodyType bodyType)
        {
            try
            {
                await _mediator.Send(new AddCarBodyCommand(bodyType));
                _cache.Remove("body_type_data");
                return RedirectToAction(nameof(IndexCarBody));
            }
            catch
            {
                return View(bodyType);
            }
        }

        public async Task<IActionResult> EditCarBody(byte id)
        {
            try
            {
                var bodyType = await _mediator.Send(new GetCarBodyByIdQuery(id));

                if (bodyType == null)
                {
                    return NotFound();
                }
                return View(bodyType);
            }
            catch
            {
                return Problem("Something went wrogn");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarBody(byte id, BodyType bodyType)
        {
            if (id != bodyType.body_id)
            {
                return NotFound();
            }
            try
            {
                await _mediator.Send(new UpdateCarBodyCommand(bodyType));
                _cache.Remove("body_type_data");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();

            }
            return RedirectToAction(nameof(IndexCarBody));
        }



        public async Task<IActionResult> DeleteCarBody(byte id)
        {
            try
            {
                var bodyType = await _mediator.Send(new GetCarBodyByIdQuery(id));

                if (bodyType == null)
                {
                    return NotFound();
                }

                return View(bodyType);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost, ActionName("DeleteCarBody")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (await _mediator.Send(new GetAllCarBodyQuery()) == null)
            {
                return Problem("Entity set 'AppDBContext.BodyType'  is null.");
            }

            var bodyType = await _mediator.Send(new GetCarBodyByIdQuery(id));

            if (bodyType != null)
            {
                await _mediator.Send(new DeleteCarBodyCommand(bodyType));
                _cache.Remove("body_type_data");
            }

            return RedirectToAction("IndexCarBody");
        }
    }
}
