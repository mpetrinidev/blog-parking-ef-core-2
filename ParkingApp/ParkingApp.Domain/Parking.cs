using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingApp.Domain
{
    [Table("Parking")]
    public class Parking
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }

        [Required, StringLength(10)]
        public string Ticket { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
