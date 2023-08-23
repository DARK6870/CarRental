using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WEB.Models;
using WEB.Pages.Reservation.Commands;
using WEB.Pages.Reservation.Queryes;

namespace WEB.Pages.Reservation
{
    [Authorize(Roles = "Admin")]
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public ReservationsController(IMediator mediator, IAppCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }


        public async Task<IActionResult> IndexRes()
        {
            var reservation = await _cache.GetOrAddAsync("reservation_data", async () =>
        {
            var result = await _mediator.Send(new GetReservationStatusQuery());
            return result;
        });
            if (reservation != null)
            {
                return View(reservation);
            }
            else
            {
                return Problem("Entity set 'AppDBContext.Reservation'  is null.");
            }
        }

        public IActionResult CreateRes()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRes(Reservation reservation)
        {
            try
            {
                await _mediator.Send(new AddReservationStatusCommand(reservation));
                _cache.Remove("reservation_data");
                return RedirectToAction("IndexRes");
            }
            catch
            {
                return View(reservation);
            }

        }



        public async Task<IActionResult> EditRes(byte id)
        {
            try
            {
                var reservation = await _mediator.Send(new GetReservationStatusByIdQuery(id));
                return View(reservation);
            }

            catch
            {
                return NotFound();
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRes(byte id, Reservation reservation)
        {
            if (id != reservation.status_id)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new UpdateReservationStatusCommand(reservation));
                _cache.Remove("reservation_data");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction("IndexRes");
        }



        public async Task<IActionResult> DeleteRes(byte id)
        {
            try
            {
                var reservation = await _mediator.Send(new GetReservationStatusByIdQuery(id));
                return View(reservation);
            }
            catch
            {
                return Problem("Sonething went wrong");
            }


        }



        [HttpPost, ActionName("DeleteRes")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                var reservationStatuses = await _mediator.Send(new GetReservationStatusQuery());

                if (reservationStatuses == null)
                {
                    return Problem("Entity set 'AppDBContext.Reservation'  is null.");
                }

                var reservation = await _mediator.Send(new GetReservationStatusByIdQuery(id));

                if (reservation != null)
                {
                    await _mediator.Send(new DeleteReservationStatusCommand(reservation));
                    _cache.Remove("reservation_data");
                }

                return RedirectToAction("IndexRes");
            }
            catch
            {
                return Problem("Sonething went wrong");
            }
        }
    }
}
