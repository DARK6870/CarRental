using MediatR;
using WEB.Models;
using WEB.Pages.CarBody;

namespace WEB.Pages.CarBody.Queryes
{
    public record GetAllCarBodyQuery() : IRequest<List<BodyType>>;

    public class GetAllCarBodyHandler : IRequestHandler<GetAllCarBodyQuery, List<BodyType>>
    {
        private readonly AppDBContext _context;
        public GetAllCarBodyHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<BodyType>> Handle(GetAllCarBodyQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Set<BodyType>();
            return result.ToList();
        }
    }
}
