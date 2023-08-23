using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;

namespace WEB.Pages.Fuel.Commands
{
    public record DeleteFuelCommand(Fuel fuel) : IRequest<Fuel>;


    public class DeleteFuelHandler : IRequestHandler<DeleteFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public DeleteFuelHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Fuel> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel removefuel = request.fuel;
            _context.Fuels.Remove(removefuel);
            await _context.SaveChangesAsync();

            return removefuel;
        }
    }
}