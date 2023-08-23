using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Queryes
{
    public record GetCarByIdQuery(int id) : IRequest<Car>;

    public class GetCarsByIdHandler : IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly AppDBContext _context;
        public GetCarsByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Set<Car>().FromSqlRaw($"SELECT * FROM Car WHERE car_id = {request.id}").FirstOrDefaultAsync();
            return result;
        }
    }
}
