using MediatR;
using WEB.Models;
using WEB.Pages.CarBody;

namespace WEB.Pages.CarBody.Commands
{
    public record UpdateCarBodyCommand(BodyType body) : IRequest<BodyType>;

    public class UpdateCarBodyHandler : IRequestHandler<UpdateCarBodyCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public UpdateCarBodyHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<BodyType> Handle(UpdateCarBodyCommand request, CancellationToken cancellationToken)
        {
            BodyType updated = request.body;
            _context.BodyType.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
