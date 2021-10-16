using HotelListing.Data.Entities;
using HotelListing.Data.ViewModels;

namespace HotelListing.Data.Extensions
{
    public static class HotelExtension
    {
        public static HotelVM AsVM(this Hotel hotel) => new HotelVM
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Rating = hotel.Rating
        };
    }
}