using MediatR;
using WEB.Models;
using WEB.Pages.Transmission;

namespace WEB.Pages.Transmission.Commands
{
    public record AddTransmissionCommand(Transmission transmission) : IRequest<Transmission>;


    public class AddTransmissionHandler : IRequestHandler<AddTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public AddTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Transmission> Handle(AddTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission newtransmission = request.transmission;
            await _context.Transmission.AddAsync(newtransmission);
            await _context.SaveChangesAsync();

            return newtransmission;
        }
    }
}