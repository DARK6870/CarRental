using MediatR;
using WEB.Models;
using WEB.Pages.DriveType;

namespace WEB.Pages.DriveType.Commands
{
    public record AddDriveTypeCommand(Drive_type drive) : IRequest<Drive_type>;

    public class AddDriveTypeHandler : IRequestHandler<AddDriveTypeCommand, Drive_type>
    {
        private readonly AppDBContext _context;
        public AddDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }


        public async Task<Drive_type> Handle(AddDriveTypeCommand request, CancellationToken cancellationToken)
        {
            Drive_type newtype = request.drive;
            await _context.Drive_type.AddAsync(newtype);
            await _context.SaveChangesAsync();

            return newtype;
        }
    }
}
