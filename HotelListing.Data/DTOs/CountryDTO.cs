using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs
{
    public class ManageCountryDTO
    {
        [Required]
        [StringLength(maximumLength:50,ErrorMessage = "Country name must be under 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength:4,ErrorMessage = "Short country name must be under 4 characters.")]
        public string ShortName { get; set; }
    }
    public class CountryDTO : ManageCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }

}