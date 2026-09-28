using Application.Notifications;
using Domain.Notifications;
using Infrastructure.DomainEventsDispatching;
using Infrastructure.Notifications.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Notifications.Core.Configuration;

public static class Services
{
    public static IServiceCollection ConfigureNotificationsServices(this IServiceCollection services)
    {
        var eventHandlerMap = DomainEventsMapBuilder.Build();
        
        services.AddScoped<IPersistNotifications, NotificationRepository>()
            .AddScoped<INotificationUnitOfWork, UnitOfWork>()
            .AddScoped<GetNotifications>()
            .AddScoped<MarkNotificationAsRead>()
            .AddScoped<GetUnreadCount>()
            .AddScoped<CreateTicketPurchaseNotification>()
            .AddSingleton(eventHandlerMap);
        return services;
    }
}