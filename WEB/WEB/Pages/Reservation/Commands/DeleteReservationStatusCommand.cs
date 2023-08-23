using MediatR;
using WEB.Models;

namespace WEB.Pages.Reservation.Commands
{
    public record DeleteReservationStatusCommand(Reservation reservation) : IRequest<Reservation>;


    public class DeleteReservationStatusHandler : IRequestHandler<DeleteReservationStatusCommand, Reservation>
    {
        private readonly AppDBContext _context;
        public DeleteReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Reservation> Handle(DeleteReservationStatusCommand request, CancellationToken cancellationToken)
        {
            Reservation remove = request.reservation;
            _context.Reservation.Remove(remove);
            await _context.SaveChangesAsync();

            return remove;
        }
    }
}
