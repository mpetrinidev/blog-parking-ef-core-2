using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingApp.Domain
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int CateogoryId { get; set; }

        [Required, StringLength(10)]
        public string LicensePlate { get; set; }

        public Category Category { get; set; }
        public List<Parking> Parkings { get; set; }
    }
}
