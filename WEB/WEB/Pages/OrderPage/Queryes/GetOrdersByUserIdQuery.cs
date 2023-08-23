using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using System.Collections;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.OrderPage.Queryes
{
    public record GetOrdersByUserIdQuery(string id) : IRequest<List<CarOrder>>;

    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery, List<CarOrder>>
    {
        private readonly AppDBContext _context;
        public GetOrdersByUserIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<CarOrder>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.CarOrders.Where(x => x.AppUserId == request.id).OrderByDescending(x => x.start_day).ToListAsync();
            return result;
        }
    }
}
