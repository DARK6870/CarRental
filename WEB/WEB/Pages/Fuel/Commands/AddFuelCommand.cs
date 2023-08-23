using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;

namespace WEB.Pages.Fuel.Commands
{
    public record AddFuelCommand(Fuel fuel) : IRequest<Fuel>;

    public class AddFuelHandler : IRequestHandler<AddFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public AddFuelHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Fuel> Handle(AddFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel newfuel = request.fuel;
            await _context.Fuels.AddAsync(newfuel);
            await _context.SaveChangesAsync();

            return newfuel;
        }
    }
}