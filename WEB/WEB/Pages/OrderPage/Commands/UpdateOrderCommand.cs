using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.Orders.Commands
{
    public record UpdateOrderCommand(Order order) : IRequest<Order>;


    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly AppDBContext _context;
        public UpdateOrderHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var updated = request.order;
            if (updated == null)
            {
                return new Order();
            }
            _context.Order.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}