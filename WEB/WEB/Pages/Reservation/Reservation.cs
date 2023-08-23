using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB.Pages.OrderPage.Models;

namespace WEB.Pages.Reservation
{
    [Table("Res_status")]
    public class Reservation
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte status_id { get; set; }

        [Required]
        [StringLength(15), Column(TypeName = "varchar(20)")]
        public string status_name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
