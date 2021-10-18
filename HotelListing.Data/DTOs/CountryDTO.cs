using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs
{
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength:50,ErrorMessage = "Country name must be under 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength:4,ErrorMessage = "Short country name must be under 4 characters.")]
        public string ShortName { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }

    public class UpdateCountryDTO : CreateCountryDTO
    {
        public IList<int> HotelIds { get; set; }
    }

}