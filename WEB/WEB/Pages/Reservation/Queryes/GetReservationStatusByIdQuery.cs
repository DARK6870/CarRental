using MediatR;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Pages.Reservation.Queryes
{
    public record GetReservationStatusQuery() : IRequest<List<Reservation>>;

    public class GetReservationStatusHandler : IRequestHandler<GetReservationStatusQuery, List<Reservation>>
    {
        private readonly AppDBContext _context;
        public GetReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Reservation>> Handle(GetReservationStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Reservation.ToListAsync();
            return result;
        }
    }
}
