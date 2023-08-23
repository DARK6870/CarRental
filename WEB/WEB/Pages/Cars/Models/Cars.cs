using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Pages.Cars.Models
{
    [NotMapped]
    public class Cars
    {
        [Key]
        public short car_id { get; set; }
        public string car_name { get; set; }
        public string body_name { get; set; }
        public string car_type_name { get; set; }
        public string drive_name { get; set; }
        public string fuel_name { get; set; }
        public string transmission_name { get; set; }
        public byte num_seats { get; set; }
        public short price { get; set; }
        public string imagepath { get; set; }
    }
}
