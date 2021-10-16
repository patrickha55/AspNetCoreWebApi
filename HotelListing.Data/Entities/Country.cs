using System.Collections;
using System.Collections.Generic;

namespace HotelListing.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual IEnumerable<Hotel> Hotels { get; set; }
    }
}