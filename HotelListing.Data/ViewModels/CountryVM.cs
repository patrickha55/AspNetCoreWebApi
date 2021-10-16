using System.Collections.Generic;

namespace HotelListing.Data.ViewModels
{
    public class CountryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public class ManageCountryVM
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}