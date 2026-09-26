using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventsManagement.Core.Configuration;

public static class Database
{
    public static IServiceCollection ConfigureEventsManagementDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EventManagementDbContext>(options =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
        return services;
    }
}