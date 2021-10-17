using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs
{
    public class ManageHotelDTO
    {
        [Required]
        [StringLength(maximumLength:100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength:100, MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [Range(1,5)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
    public class HotelDTO : ManageHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}