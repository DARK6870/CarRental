using MediatR;
using WEB.Models;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.OrderPage.Commands
{
    public record AddOrderCommand(Models.Order order) : IRequest<Models.Order>;

    public class AddOrderHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly AppDBContext _context;
        public AddOrderHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Order neworder = request.order;
            await _context.Order.AddAsync(neworder);
            await _context.SaveChangesAsync();

            return neworder;
        }
    }
}
