using MediatR;
using WEB.Models;
using WEB.Pages.CarType;

namespace WEB.Pages.CarType.Commands
{
    public record AddCarTypeCommand(CarType type) : IRequest<CarType>;


    public class AddCarTypeHandler : IRequestHandler<AddCarTypeCommand, CarType>
    {
        private readonly AppDBContext _context;
        public AddCarTypeHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<CarType> Handle(AddCarTypeCommand request, CancellationToken cancellationToken)
        {
            CarType newtype = request.type;
            await _context.CarType.AddAsync(newtype);
            await _context.SaveChangesAsync();

            return newtype;
        }
    }
}