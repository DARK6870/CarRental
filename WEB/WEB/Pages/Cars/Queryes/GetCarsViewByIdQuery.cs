using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Queryes
{
    public record GetCarsViewByIdQuery(int id) : IRequest<Models.Cars>;

    public class GetCarsViewByIdHandler : IRequestHandler<GetCarsViewByIdQuery, Models.Cars>
    {
        private readonly AppDBContext _context;
        public GetCarsViewByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Models.Cars> Handle(GetCarsViewByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Models.Cars>().FromSqlRaw($"SELECT * FROM CarView WHERE car_id = {request.id}").FirstOrDefaultAsync();
            return result;
        }
    }
}
