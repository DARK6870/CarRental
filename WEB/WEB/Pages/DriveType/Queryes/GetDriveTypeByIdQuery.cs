using MediatR;
using WEB.Models;
using WEB.Pages.DriveType;

namespace WEB.Pages.DriveType.Queryes
{
    public record GetDriveTypeByIdQuery(byte id) : IRequest<Drive_type>;

    public class GetDriveTypeByIdHandler : IRequestHandler<GetDriveTypeByIdQuery, Drive_type>
    {
        private readonly AppDBContext _context;
        public GetDriveTypeByIdHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Drive_type> Handle(GetDriveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Drive_type.FindAsync(request.id);
            return result;
        }
    }
}
