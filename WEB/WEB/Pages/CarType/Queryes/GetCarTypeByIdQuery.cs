using MediatR;
using WEB.Models;
using WEB.Pages.CarType;

namespace WEB.Pages.CarType.Queryes
{
    public record GetCarTypeByIdQuery(byte id) : IRequest<CarType>;

    public class GetCarTypeByIdHandler : IRequestHandler<GetCarTypeByIdQuery, CarType>
    {
        private readonly AppDBContext _context;
        public GetCarTypeByIdHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<CarType> Handle(GetCarTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.CarType.FindAsync(request.id);
            return result;
        }
    }
}
