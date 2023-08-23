using MediatR;
using WEB.Models;
using WEB.Pages.CarType;

namespace WEB.Pages.CarType.Commands
{
    public record DeleteCarTypeCommand(CarType type) : IRequest<CarType>;


    public class DeleteCarTypeHandler : IRequestHandler<DeleteCarTypeCommand, CarType>
    {
        private readonly AppDBContext _context;
        public DeleteCarTypeHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<CarType> Handle(DeleteCarTypeCommand request, CancellationToken cancellationToken)
        {
            CarType remove = request.type;
            _context.CarType.Remove(remove);
            await _context.SaveChangesAsync();

            return remove;
        }
    }
}