using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;

namespace HotelListing.Data.Extensions
{
    public static class CountryExtension
    {
        public static CountryDTO AsVM(this Country country) => new CountryDTO
        {
            Id = country.Id,
            Name = country.Name,
            ShortName = country.ShortName
        };
    }
}