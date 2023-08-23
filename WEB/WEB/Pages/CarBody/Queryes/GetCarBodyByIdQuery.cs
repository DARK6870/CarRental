using MediatR;
using WEB.Models;
using WEB.Pages.CarBody;

namespace WEB.Pages.CarBody.Queryes
{
    public record GetCarBodyByIdQuery(byte id) : IRequest<BodyType>;

    public class GetCarBodyByIdHandler : IRequestHandler<GetCarBodyByIdQuery, BodyType>
    {
        private readonly AppDBContext _context;
        public GetCarBodyByIdHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<BodyType> Handle(GetCarBodyByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.BodyType.FindAsync(request.id);
            return result;
        }
    }
}
