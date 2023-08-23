using Microsoft.EntityFrameworkCore;
using WEB.Models;
using MediatR;
using System.Collections;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Queryes
{
    public record GetAllCarsQuery() : IRequest<List<Car>>;

    public class GetAllCarsHandler : IRequestHandler<GetAllCarsQuery, List<Car>>
    {
        private readonly AppDBContext _context;
        public GetAllCarsHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Car>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Set<Car>();
            return result.ToList();
        }
    }
}
