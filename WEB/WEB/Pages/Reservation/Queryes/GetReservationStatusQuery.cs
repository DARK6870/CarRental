using MediatR;
using WEB.Models;

namespace WEB.Pages.Reservation.Queryes
{
    public record GetReservationStatusByIdQuery(byte id) : IRequest<Reservation>;

    public class GetReservationStatusByIdHandler : IRequestHandler<GetReservationStatusByIdQuery, Reservation>
    {
        private readonly AppDBContext _context;
        public GetReservationStatusByIdHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Reservation> Handle(GetReservationStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Reservation.FindAsync(request.id);
            return result;
        }
    }
}
