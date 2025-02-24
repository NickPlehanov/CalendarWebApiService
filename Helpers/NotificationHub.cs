using Microsoft.AspNetCore.SignalR;

namespace CalendarWebApiService.Helpers
{
    public class NotificationHub :Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task SendNotificationToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
