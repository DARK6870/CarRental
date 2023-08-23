using MediatR;
using WEB.Models;
using WEB.Pages.CarType;

namespace WEB.Pages.CarType.Commands
{
    public record UpdateCarTypeCommand(CarType type) : IRequest<CarType>;

    public class UpdateCarTypeHandler : IRequestHandler<UpdateCarTypeCommand, CarType>
    {
        private readonly AppDBContext _context;
        public UpdateCarTypeHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<CarType> Handle(UpdateCarTypeCommand request, CancellationToken cancellationToken)
        {
            CarType updated = request.type;
            _context.CarType.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
