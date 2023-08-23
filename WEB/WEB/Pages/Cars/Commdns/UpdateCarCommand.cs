using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Commdns
{
    public record UpdateCarCommand(Car Car) : IRequest<Car>;

    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, Car>
    {
        private readonly AppDBContext _context;
        public UpdateCarHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car updated = request.Car;
            _context.Car.Update(updated);
            await _context.SaveChangesAsync();

            return updated;
        }
    }
}