using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WEB.Pages.Cars.Models;

namespace WEB.Pages.DriveType
{
    [Table("Drive_type")]
    public class Drive_type
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(TypeName = "tinyint")]
        public byte drive_id { get; set; }

        [Required]
        [StringLength(5), Column(TypeName = "varchar(5)")]
        public string drive_name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
