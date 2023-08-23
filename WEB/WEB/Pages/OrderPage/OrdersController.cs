using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Pages.OrderPage.Models;
using WEB.Pages.OrderPage.Queryes;
using WEB.Pages.Orders.Commands;

namespace WEB.Pages.OrderPage
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;

        }



        public async Task<IActionResult> IndexOrder()
        {
            return await _mediator.Send(new GetAllCarOrdersQuery()) != null ?
                          View(await _mediator.Send(new GetAllCarOrdersQuery())) :
                          Problem("Entity set 'AppDBContext.Order'  is null.");
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery(id));
                if (order == null)
                {
                    return NotFound();
                }
                ViewData["AppUserId"] = await _mediator.Send(new GetOrderByIdQuery(id));
                return View(order);
            }
            catch
            {
                return Problem("Something went wrong");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(int id, Order order)
        {
            if (id != order.order_id)
            {
                return NotFound();
            }
            try
            {
                await _mediator.Send(new UpdateOrderCommand(order));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.order_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("IndexOrder");
        }

        private bool OrderExists(int id)
        {
            if (_mediator.Send(new GetOrderByIdQuery(id)) is null) return false;
            else return true;
        }

    }
}
