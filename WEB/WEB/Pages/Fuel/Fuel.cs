using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.Fuel
{
    [Table("Fuel")]
    public class Fuel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte fuel_id { get; set; }

        [Required]
        [StringLength(10), Column(TypeName = "varchar(10)")]
        public string fuel_name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}