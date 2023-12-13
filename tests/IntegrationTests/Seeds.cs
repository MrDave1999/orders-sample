using Application.Shared.Resources;
using Domain;

namespace IntegrationTests;

public static class Seeds
{
    public static async void CreateOrderStatuses(this TestBase testBase)
    {
        await testBase.AddRangeAsync([
            new OrderStatus { Name = Messages.Pending },
            new OrderStatus { Name = Messages.Confirmed },
            new OrderStatus { Name = Messages.InProgress },
            new OrderStatus { Name = Messages.Delivered },
            new OrderStatus { Name = Messages.Cancelled }
        ]);
    }
}
