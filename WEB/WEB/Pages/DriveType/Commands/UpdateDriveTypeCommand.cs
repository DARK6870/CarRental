using MediatR;
using WEB.Models;
using WEB.Pages.DriveType;

namespace WEB.Pages.DriveType.Commands
{
    public record UpdateDriveTypeCommand(Drive_type drive) : IRequest<Drive_type>;

    public class UpdateDriveTypeHandler : IRequestHandler<UpdateDriveTypeCommand, Drive_type>
    {
        private readonly AppDBContext _context;
        public UpdateDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Drive_type> Handle(UpdateDriveTypeCommand request, CancellationToken cancellationToken)
        {
            Drive_type updated = request.drive;
            _context.Drive_type.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
