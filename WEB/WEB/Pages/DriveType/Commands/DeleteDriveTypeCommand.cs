using MediatR;
using WEB.Models;
using WEB.Pages.DriveType;

namespace WEB.Pages.DriveType.Commands
{
    public record DeleteDriveTypeCommand(Drive_type drive) : IRequest<Drive_type>;

    public class DeleteDriveTypeHandler : IRequestHandler<DeleteDriveTypeCommand, Drive_type>
    {
        private readonly AppDBContext _context;
        public DeleteDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Drive_type> Handle(DeleteDriveTypeCommand request, CancellationToken cancellationToken)
        {
            Drive_type remove = request.drive;
            _context.Drive_type.Remove(remove);
            await _context.SaveChangesAsync();

            return remove;
        }
    }
}
