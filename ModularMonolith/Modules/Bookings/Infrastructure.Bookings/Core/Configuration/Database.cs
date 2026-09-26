using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheDatabase = Infrastructure.Queries.Database;

namespace Infrastructure.Bookings.Core.Configuration;

public static class Database
{
    public static IServiceCollection ConfigureBookingsDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BookingDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
        services.AddScoped<TheDatabase>(_ => new TheDatabase(connectionString));
        return services;
    }
}