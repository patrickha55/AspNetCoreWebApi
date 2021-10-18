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
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, ManageHotelDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}