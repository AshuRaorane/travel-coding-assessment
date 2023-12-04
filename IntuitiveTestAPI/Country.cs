using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntuitiveTestAPI
{
    public class Country
    {
        [Key]
        public int GeographyLevel1ID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Airport>? Airports { get; set; }
    }
}
