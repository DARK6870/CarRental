using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Transmission
{
    [Table("Transmission")]
    public class Transmission
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte transmission_id { get; set; }
        [Required]
        [StringLength(10), Column(TypeName = "varchar(10)")]
        public string transmission_name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
