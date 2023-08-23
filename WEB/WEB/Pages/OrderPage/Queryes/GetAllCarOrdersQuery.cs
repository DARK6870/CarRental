using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.OrderPage.Queryes
{
    public record GetAllCarOrdersQuery() : IRequest<List<CarOrder>>;

    public class GetAllCarOrdersHandler : IRequestHandler<GetAllCarOrdersQuery, List<CarOrder>>
    {
        private readonly AppDBContext _context;
        public GetAllCarOrdersHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<CarOrder>> Handle(GetAllCarOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.CarOrders.ToListAsync();
            return orders;
        }
    }
}
