using Microsoft.EntityFrameworkCore;
using ParkingApp.Data;
using ParkingApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingApp.UI
{
    class Program
    {
        private static ParkingContext _context = new ParkingContext();

        static void Main(string[] args)
        {
            //InsertCategories();
            //InsertMultipleVehicles();
            //InsertParking();

            var categories = _context.Categories.ToList();
            categories.ForEach(p => Console.WriteLine(p.Name));

            var oneVehicles = _context.Vehicles.FirstOrDefault(p => p.LicensePlate == "NJS981");
            var findVehicle = _context.Vehicles.Find(1);
            var likeVehicle = _context.Vehicles.Where(p => EF.Functions.Like(p.LicensePlate, "%9%")).ToList();

            //LastOrDefault
            var lastVehicle = _context.Vehicles.LastOrDefault(p => p.LicensePlate == "NJS981");
            var lastVehicleCorrect = _context.Vehicles.OrderBy(p => p.Id).LastOrDefault(p1 => p1.LicensePlate == "NJS981");

            Console.ReadLine();
        }

        private static void InsertMultipleVehicles()
        {
            var cars = new List<Vehicle>
            {
                new Vehicle { CateogoryId = 1, LicensePlate = "NJS981" },
                new Vehicle { CateogoryId = 2, LicensePlate = "KSJ198" },
                new Vehicle { CateogoryId = 1, LicensePlate = "LSK123" },
                new Vehicle { CateogoryId = 3, LicensePlate = "SHD290" },
            };

            var carsNotExists = cars.Where(p => !_context.Vehicles.Any(x => x.LicensePlate == p.LicensePlate));

            _context.AddRange(carsNotExists);
            _context.SaveChanges();
        }

        private static void InsertCategories()
        {
            var categories = new List<Category>()
            {
                new Category { Name = "Auto"},
                new Category { Name = "Camion" },
                new Category { Name = "Moto" }
            };

            categories
                .ForEach(p =>
                {
                    if (!_context.Categories.Any(x => x.Name == p.Name))
                        _context.Add(p);
                });

            _context.SaveChanges();
        }

        private static void InsertParking()
        {
            var newParking = new Parking
            {
                VehicleId = 1,
                Ticket = "6475846372",
                CheckIn = DateTime.Now
            };

            if (_context.Parkings.Any(p => p.Ticket == newParking.Ticket))
                return;

            _context.Add(newParking);
            _context.SaveChanges();
        }
    }
}
