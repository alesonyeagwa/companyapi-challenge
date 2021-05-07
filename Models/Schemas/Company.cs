using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Models.Schemas
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Exchange { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Ticker { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}\d+$")]
        public string ISIN { get; set; }
        [RegularExpression(@"^(https?:\/\/)?([\w\d-_]+)\.([\w\d-_\.]+)\/?\??([^#\n\r]*)?#?([^\n\r]*)$")]
        public string Website { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
    }
}
