using MediatR;
using WEB.Models;
using WEB.Pages.Transmission;

namespace WEB.Pages.Transmission.Commands
{
    public record DeleteTransmissionCommand(Transmission transmission) : IRequest<Transmission>;


    public class DeleteTransmissionHandler : IRequestHandler<DeleteTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public DeleteTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Transmission> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission remove = request.transmission;
            _context.Transmission.Remove(remove);
            await _context.SaveChangesAsync();

            return remove;
        }
    }
}
