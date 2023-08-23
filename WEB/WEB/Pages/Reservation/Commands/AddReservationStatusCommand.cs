using MediatR;
using WEB.Models;

namespace WEB.Pages.Reservation.Commands
{
    public record AddReservationStatusCommand(Reservation reservation) : IRequest<Reservation>;


    public class AddReservationStatusHandler : IRequestHandler<AddReservationStatusCommand, Reservation>
    {
        private readonly AppDBContext _context;
        public AddReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Reservation> Handle(AddReservationStatusCommand request, CancellationToken cancellationToken)
        {
            Reservation newstatus = request.reservation;
            await _context.Reservation.AddAsync(newstatus);
            await _context.SaveChangesAsync();

            return newstatus;
        }
    }
}
