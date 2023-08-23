using MediatR;
using WEB.Models;
using WEB.Pages.DriveType;

namespace WEB.Pages.DriveType.Queryes
{
    public record GetAllDriveTypesQuery() : IRequest<List<Drive_type>>;

    public class GetAllDriveTypesHandler : IRequestHandler<GetAllDriveTypesQuery, List<Drive_type>>
    {
        private readonly AppDBContext _context;
        public GetAllDriveTypesHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<List<Drive_type>> Handle(GetAllDriveTypesQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Set<Drive_type>();
            return result.ToList();
        }
    }
}
