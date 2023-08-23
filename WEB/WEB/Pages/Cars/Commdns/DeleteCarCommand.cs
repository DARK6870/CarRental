using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Commdns
{
    public record DeleteCarCommand(Car Car) : IRequest<Car>;
    public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, Car>
    {
        private readonly AppDBContext _context;
        public DeleteCarHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Car> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car delete = request.Car;
            _context.Car.Remove(delete);
            await _context.SaveChangesAsync();
            return delete;
        }
    }
}
