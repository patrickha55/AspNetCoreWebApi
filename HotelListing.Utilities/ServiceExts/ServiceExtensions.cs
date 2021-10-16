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
    }
}