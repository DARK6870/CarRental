using MediatR;
using WEB.Models;
using WEB.Pages.CarType;

namespace WEB.Pages.CarType.Queryes
{
    public record GetAllCarTypesQuery() : IRequest<List<CarType>>;

    public class GetAllCarTypesHandler : IRequestHandler<GetAllCarTypesQuery, List<CarType>>
    {
        private readonly AppDBContext _context;
        public GetAllCarTypesHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<CarType>> Handle(GetAllCarTypesQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Set<CarType>();
            return result.ToList();
        }
    }
}
