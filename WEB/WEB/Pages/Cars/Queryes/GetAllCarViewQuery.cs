using Microsoft.EntityFrameworkCore;
using WEB.Models;
using MediatR;
using System.Collections;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Queryes
{
    public record GetAllCarViewQuery() : IRequest<List<Models.Cars>>;

    public class GetAllCarViewHandler : IRequestHandler<GetAllCarViewQuery, List<Models.Cars>>
    {
        private readonly AppDBContext _context;
        public GetAllCarViewHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Models.Cars>> Handle(GetAllCarViewQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Set<Models.Cars>().FromSqlRaw("SELECT * FROM CarView");
            return result.ToList();
        }
    }
}
