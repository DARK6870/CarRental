using MediatR;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Pages.Fuel.Queryes
{
    public record GetFuelByIdQuery(byte id) : IRequest<Fuel>;

    public class GetFuelByIdHandler : IRequestHandler<GetFuelByIdQuery, Fuel>
    {
        private readonly AppDBContext _context;
        public GetFuelByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Fuel> Handle(GetFuelByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Fuels.FindAsync(request.id);
            return result;
        }
    }
}
