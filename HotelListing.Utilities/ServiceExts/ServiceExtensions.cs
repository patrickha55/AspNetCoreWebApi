using HotelListing.Data;
using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelListing.Utilities.ServiceExts
{
    public static class ServiceExtensions
    {
        public const string AllowAll = "AllowAll";

        public static void ConfigureCors(this IServiceCollection services)
        {
            // Config Cross Origin Resource Sharing
            services.AddCors(c =>
            {
                c.AddPolicy(AllowAll, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequiredUniqueChars = 0;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
        }
    }
}