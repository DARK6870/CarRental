using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB.Pages.OrderPage.Models;
using WEB.Pages.Orders;
namespace WEB.Pages.Cars.Models
{
    [Table("Car")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "smallint")]
        public short car_id { get; set; }

        [Required]
        [StringLength(25)]
        [Column(TypeName = "varchar(25)")]
        public string car_name { get; set; }

        [Required]
        [ForeignKey("CarType")]
        public byte type_id { get; set; }

        [Required]
        [ForeignKey("BodyType")]
        public byte body_id { get; set; }

        [Required]
        [ForeignKey("Transmission")]
        public byte transmission_id { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public byte num_seats { get; set; }

        [Required]
        [ForeignKey("Fuel")]
        public byte fuel_id { get; set; }

        [Required]
        [ForeignKey("Drive_type")]
        public byte drive_id { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public short price { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string imagepath { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
