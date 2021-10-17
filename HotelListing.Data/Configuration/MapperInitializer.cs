using AutoMapper;
using HotelListing.Data.DTOs;
using HotelListing.Data.Entities;

namespace HotelListing.Data.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, ManageCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, ManageHotelDTO>().ReverseMap();
        }
    }
}