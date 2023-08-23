using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;

namespace WEB.Pages.Fuel.Commands
{
    public record UpdateFuelCommand(Fuel fuel) : IRequest<Fuel>;

    public class UpdateFuelHandler : IRequestHandler<UpdateFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public UpdateFuelHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Fuel> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel updated = request.fuel;
            _context.Fuels.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}