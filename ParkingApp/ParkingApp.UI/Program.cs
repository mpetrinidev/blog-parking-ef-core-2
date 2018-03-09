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
            #region "inserts"

            //InsertCategories();
            //InsertMultipleVehicles();
            //InsertParking();

            #endregion

            #region "queries-filters"

            //var categories = _context.Categories.ToList();
            //categories.ForEach(p => Console.WriteLine(p.Name));

            //var oneVehicles = _context.Vehicles.FirstOrDefault(p => p.LicensePlate == "NJS981");
            //var findVehicle = _context.Vehicles.Find(1);
            //var likeVehicle = _context.Vehicles.Where(p => EF.Functions.Like(p.LicensePlate, "%9%")).ToList();

            //var lastVehicle = _context.Vehicles.LastOrDefault(p => p.LicensePlate == "NJS981");
            //var lastVehicleCorrect = _context.Vehicles.OrderBy(p => p.Id).LastOrDefault(p1 => p1.LicensePlate == "NJS981");

            #endregion

            #region "update - delete"

            //UpdateParking();
            //MultipleUpdates();
            //UpdateParkingAnotherInstance();

            //DeleteParking();
            //MultipleDeletes();

            #endregion

            #region "insert related"

            //InsertRelatedVehicles();
            //InsertRelatedVehicleMultipleParkings();
            //InsertRelatedVehicleAnotherInstanceWithError(); //this code throw exception
            //InsertRelatedVehicleAnotherInstance(1);

            #endregion

            Console.ReadLine();
        }

        #region "insert methods"

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

        #endregion

        #region "update methods"

        private static void UpdateParking()
        {
            var existingParking = _context.Parkings.Find(1);
            if (existingParking == null) return;

            existingParking.CheckOut = DateTime.Now;

            _context.SaveChanges();
        }

        private static void MultipleUpdates()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || categories.Count == 0) return;

            var i = 0;
            categories.ForEach(p => p.Name += " " + i++);

            _context.SaveChanges();
        }

        private static void UpdateParkingAnotherInstance()
        {
            var existingParking = _context.Parkings.Find(1);
            if (existingParking == null) return;

            existingParking.CheckOut = DateTime.Now;

            using (var newContext = new ParkingContext())
            {
                newContext.Parkings.Update(existingParking);
                newContext.SaveChanges();
            }
        }

        #endregion

        #region "delete methods"

        private static void DeleteParking()
        {
            var existingParking = _context.Parkings.Find(1);
            if (existingParking == null) return;

            _context.Parkings.Remove(existingParking);
            _context.SaveChanges();
        }

        private static void MultipleDeletes()
        {
            var vehicles = _context.Vehicles.ToList();
            if (vehicles.Count == 0) return;

            _context.Vehicles.RemoveRange(vehicles);
            _context.SaveChanges();
        }

        #endregion

        #region related"

        private static void InsertRelatedVehicles()
        {
            var vehicle = new Vehicle
            {
                CateogoryId = 1,
                LicensePlate = "JNS543",
                Parkings = new List<Parking>
                {
                    new Parking
                    {
                        Ticket = "9485637483",
                        CheckIn = DateTime.Now
                    }
                }
            };

            _context.Add(vehicle);
            _context.SaveChanges();
        }

        private static void InsertRelatedVehicleMultipleParkings()
        {
            var vehicle = new Vehicle
            {
                CateogoryId = 1,
                LicensePlate = "ABC543",
                Parkings = new List<Parking>
                {
                    new Parking
                    {
                        Ticket = "9481637483",
                        CheckIn = DateTime.Now
                    },
                    new Parking
                    {
                        Ticket = "9481639183",
                        CheckIn = DateTime.Now
                    }
                }
            };

            _context.Add(vehicle);
            _context.SaveChanges();
        }

        private static void InsertRelatedVehicleAnotherInstanceWithError()
        {
            var vehicle = _context.Vehicles.Include(p => p.Parkings)
                                           .FirstOrDefault(p => p.Id == 1);

            vehicle?.Parkings.Add(new Parking
            {
                Ticket = "6574837564",
                CheckIn = DateTime.Now
            });

            //another context
            using (var ctx = new ParkingContext())
            {
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();
            }
        }

        private static void InsertRelatedVehicleAnotherInstance(int vehicleId)
        {
            var parking = new Parking
            {
                Ticket = "6574837564",
                CheckIn = DateTime.Now,
                VehicleId = vehicleId
            };

            //another context
            using (var ctx = new ParkingContext())
            {
                ctx.Parkings.Add(parking);
                ctx.SaveChanges();
            }
        }

        #endregion
    }
}
