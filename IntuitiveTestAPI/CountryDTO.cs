using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntuitiveTestAPI
{
    public class CountryDTO
    {
        public string Name { get; set; } = string.Empty;

    }
}
