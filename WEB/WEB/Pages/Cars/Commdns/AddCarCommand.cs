using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WEB.Models;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Cars.Commdns
{
    public record AddCarCommand(Car Car) : IRequest<Car>;


    public class AddCarHandler : IRequestHandler<AddCarCommand, Car>
    {
        private readonly AppDBContext _context;
        public AddCarHandler(AppDBContext context)
        {
            _context = context;
        }



        public async Task<Car> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            Car newCar = request.Car;
            await _context.Car.AddAsync(newCar);
            await _context.SaveChangesAsync();

            return newCar;
        }
    }
}