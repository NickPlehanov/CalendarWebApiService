
using CalendarWebApiService.Models;
using CalendarWebApiService.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace CalendarWebApiService.Helpers
{
    public class BackgroundService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public BackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void Dispose()
        {
            StopAsync(new CancellationToken(true));
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _appSettings = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<AppSettings>>();
                _timer = new Timer(Worker, null, TimeSpan.Zero, TimeSpan.FromSeconds(_appSettings.Value.Period));
            }
            return Task.CompletedTask;
        }
        private async void Worker(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var messageService = scope.ServiceProvider.GetRequiredService<IMessage>();
                var notesService = scope.ServiceProvider.GetRequiredService<INotesService>();
                await foreach (var item in notesService.GetNotesReadyToAlarm())
                {
                    await Call(messageService, item);
                }
            }
        }
        private async Task Call(IMessage? svc, Notes note)
        {
            await svc?.SendMessage($"Заголовок: {note.Title} {Environment.NewLine} {note.Text}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
