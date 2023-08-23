using MediatR;
using WEB.Models;
using WEB.Pages.CarBody;

namespace WEB.Pages.CarBody.Commands
{
    public record DeleteCarBodyCommand(BodyType body) : IRequest<BodyType>;

    public class DeleteCarBodyHandler : IRequestHandler<DeleteCarBodyCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public DeleteCarBodyHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<BodyType> Handle(DeleteCarBodyCommand request, CancellationToken cancellationToken)
        {
            BodyType remove = request.body;
            _context.BodyType.Remove(remove);
            await _context.SaveChangesAsync();

            return remove;
        }
    }
}
