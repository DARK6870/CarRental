using MediatR;
using WEB.Models;
using WEB.Pages.Transmission;

namespace WEB.Pages.Transmission.Commands
{
    public record UpdateTransmissionCommand(Transmission transmission) : IRequest<Transmission>;

    public class UpdateTransmissionHandler : IRequestHandler<UpdateTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public UpdateTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Transmission> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission update = request.transmission;
            _context.Transmission.Update(update);
            await _context.SaveChangesAsync();

            return update;
        }
    }
}
