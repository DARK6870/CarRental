using MediatR;
using WEB.Models;
using WEB.Pages.Transmission;

namespace WEB.Pages.Transmission.Queryes
{
    public record GetTransmissionByIdQuery(byte id) : IRequest<Transmission>;

    public class GetTransmissionByIdHandler : IRequestHandler<GetTransmissionByIdQuery, Transmission>
    {
        private readonly AppDBContext _context;
        public GetTransmissionByIdHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Transmission> Handle(GetTransmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Transmission.FindAsync(request.id);
            return result;
        }
    }
}
