using ParkingApp.Data;
using System;
using System.Linq;

namespace ParkingApp.UI
{
    class Program
    {
        private static ParkingContext _context = new ParkingContext();

        static void Main(string[] args)
        {
            _context.Categories.ToList().ForEach(p => Console.WriteLine(p.Name));

            Console.ReadLine();
        }
    }
}
