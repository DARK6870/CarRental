using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;
using WEB.Pages.Transmission;

namespace WEB.Pages.Transmission.Queryes
{
    public record GetAllTransmissionsQuery() : IRequest<List<Transmission>>;

    public class GetAllTransmissionsHandler : IRequestHandler<GetAllTransmissionsQuery, List<Transmission>>
    {
        private readonly AppDBContext _context;
        public GetAllTransmissionsHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Transmission>> Handle(GetAllTransmissionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Transmission.ToListAsync();
            return result;
        }
    }
}
