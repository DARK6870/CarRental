using MediatR;
using WEB.Models;

namespace WEB.Pages.Reservation.Commands
{
    public record UpdateReservationStatusCommand(Reservation reservation) : IRequest<Reservation>;


    public class UpdateReservationStatusHandler : IRequestHandler<UpdateReservationStatusCommand, Reservation>
    {
        private readonly AppDBContext _context;
        public UpdateReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Reservation> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            Reservation updated = request.reservation;
            _context.Reservation.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
