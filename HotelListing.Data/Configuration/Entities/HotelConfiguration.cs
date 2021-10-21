using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Data.Configuration.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "George Town",
                    CountryId = 3,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Grand Palldium",
                    Address = "Nassua",
                    CountryId = 2,
                    Rating = 4
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Test 04",
                    Address = "Test",
                    CountryId = 5,
                    Rating = 4
                },
                new Hotel
                {
                    Id = 5,
                    Name = "Test 05",
                    Address = "Test",
                    CountryId = 5,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 6,
                    Name = "Test 06",
                    Address = "Test",
                    CountryId = 6,
                    Rating = 5
                },
                new Hotel
                {
                    Id = 7,
                    Name = "Test 07",
                    Address = "Test",
                    CountryId = 7,
                    Rating = 3.5
                },
                new Hotel
                {
                    Id = 8,
                    Name = "Test 08",
                    Address = "Test",
                    CountryId = 8,
                    Rating = 4
                },
                new Hotel
                {
                    Id = 9,
                    Name = "Test 09",
                    Address = "Test",
                    CountryId = 9,
                    Rating = 4.8
                },
                new Hotel
                {
                    Id = 10,
                    Name = "Test 10",
                    Address = "Test",
                    CountryId = 10,
                    Rating = 4
                }
            );
        }
    }
}