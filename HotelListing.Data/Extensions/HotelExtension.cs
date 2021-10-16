using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;

namespace HotelListing.Data.Extensions
{
    public static class HotelExtension
    {
        public static HotelDTO AsVM(this Hotel hotel) => new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Rating = hotel.Rating
        };
    }
}