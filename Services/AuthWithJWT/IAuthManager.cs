using System.Threading.Tasks;
using HotelListing.Data.DTOs;

namespace Services.AuthWithJWT
{
    public interface IAuthManager
    {
        Task<bool> ValidateUserAsync(SignInDTO request);
        Task<string> CreateTokenAsync();
    }
}