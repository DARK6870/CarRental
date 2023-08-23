using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.CarType
{
    [Table("CarType")]
    public class CarType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte type_id { get; set; }

        [Required]
        [StringLength(15), Column(TypeName = "varchar(10)")]
        public string car_type_name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
