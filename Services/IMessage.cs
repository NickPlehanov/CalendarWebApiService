namespace CalendarWebApiService.Services
{
    public interface IMessage
    {
        public Task SendMessageForUser(string userId, string message);
        public Task SendMessage(string message);
    }
}
