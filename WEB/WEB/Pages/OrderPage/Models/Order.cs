using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Pages.OrderPage.Models
{
    [Table("Orders")]
    public class Order
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "int")]
        public int order_id { get; set; }

        [Required, ForeignKey("Reservation"), Column(TypeName = "tinyint")]
        public byte status_id { get; set; }

        [Required, ForeignKey("AppUser"), Column(TypeName = "nvarchar(450)")]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime start_day { get; set; }
        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime end_day { get; set; }
        [Required]
        [ForeignKey("Car")]
        public short car_id { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int total_price { get; set; }
    }
}
