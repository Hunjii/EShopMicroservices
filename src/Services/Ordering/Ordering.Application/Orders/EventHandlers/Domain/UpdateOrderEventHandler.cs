﻿namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class UpdateOrderEventHandler(ILogger<UpdateOrderEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handler: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
