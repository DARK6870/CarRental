using MediatR;
using WEB.Models;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.OrderPage.Queryes
{
    public record GetOrderByIdQuery(int id) : IRequest<Order>;

    public class GetCarOrdersByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly AppDBContext _context;
        public GetCarOrdersByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Order.FindAsync(request.id);
            return result;
        }
    }
}
