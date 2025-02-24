
using CalendarWebApiService.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace CalendarWebApiService.Services
{
    public class MessageService : Hub, IMessage
    {
        private readonly AppSettings? _appSettings;
        public MessageService(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync(_appSettings.HeaderMessage, message);
        }

        public async Task SendMessageForUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync(_appSettings.HeaderMessage, message);
        }
    }
}
 