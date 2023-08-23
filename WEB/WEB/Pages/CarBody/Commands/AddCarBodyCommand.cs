using MediatR;
using WEB.Models;
using WEB.Pages.CarBody;

namespace WEB.Pages.CarBody.Commands
{
    public record AddCarBodyCommand(BodyType body) : IRequest<BodyType>;

    public class AddCarBodyHandler : IRequestHandler<AddCarBodyCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public AddCarBodyHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<BodyType> Handle(AddCarBodyCommand request, CancellationToken cancellationToken)
        {
            BodyType newbody = request.body;
            await _context.BodyType.AddAsync(newbody);
            await _context.SaveChangesAsync();

            return newbody;
        }
    }
}
