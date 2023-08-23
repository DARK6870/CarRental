using MediatR;
using WEB.Models;

namespace WEB.Pages.Fuel.Queryes
{
    public record GetAllFuelsQuery() : IRequest<List<Fuel>>;

    public class GetAllFuelsHandler : IRequestHandler<GetAllFuelsQuery, List<Fuel>>
    {
        private readonly AppDBContext _context;
        public GetAllFuelsHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Fuel>> Handle(GetAllFuelsQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Fuels;
            return result.ToList();
        }
    }
}
