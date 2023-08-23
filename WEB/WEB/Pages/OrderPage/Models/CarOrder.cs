using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace WEB.Pages.OrderPage.Models
{
    [NotMapped]
    public class CarOrder
    {
        [Key]
        public int order_id { get; set; }
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


        public byte status_id { get; set; }
        public string status_name { get; set; }
        public string AppUserId { get; set; }
        public string Phone { get; set; }
        public DateTime start_day { get; set; }
        public DateTime end_day { get; set; }
        public int total_price { get; set; }
    }
}

