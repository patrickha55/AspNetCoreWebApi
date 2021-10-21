using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Data.Configuration.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Afghanistan",
                    ShortName = "AF"
                },
                new Country
                {
                    Id = 2,
                    Name = "Albania",
                    ShortName = "AL"
                },
                new Country
                {
                    Id = 3,
                    Name = "Algeria",
                    ShortName = "DZ"
                },
                new Country
                {
                    Id = 4,
                    Name = "Andorra",
                    ShortName = "AD"
                },
                new Country
                {
                    Id = 5,
                    Name = "Angola",
                    ShortName = "AO"
                },
                new Country
                {
                    Id = 6,
                    Name = "Antigua and Barbuda",
                    ShortName = "AG"
                },
                new Country
                {
                    Id = 7,
                    Name = "Argentina",
                    ShortName = "AR"
                },
                new Country
                {
                    Id = 8,
                    Name = "Armenia",
                    ShortName = "AM"
                },
                new Country
                {
                    Id = 9,
                    Name = "Australia",
                    ShortName = "AU"
                },
                new Country
                {
                    Id = 10,
                    Name = "Austria",
                    ShortName = "AT"
                }
            );
        }
    }
}