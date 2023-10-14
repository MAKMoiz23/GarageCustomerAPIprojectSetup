using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Amenities
    {
        public int AmenitiesID { get; set; }
        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public string? Image { get; set; }
        public int? LocationID { get; set; }
    }
}
