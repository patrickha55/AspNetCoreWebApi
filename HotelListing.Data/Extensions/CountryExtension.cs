using HotelListing.Data.Entities;
using HotelListing.Data.ViewModels;

namespace HotelListing.Data.Extensions
{
    public static class CountryExtension
    {
        public static CountryVM AsVM(this Country country) => new CountryVM
        {
            Id = country.Id,
            Name = country.Name,
            ShortName = country.ShortName
        };
    }
}