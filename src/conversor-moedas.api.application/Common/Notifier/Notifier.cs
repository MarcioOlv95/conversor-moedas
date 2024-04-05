using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.application.Common.Notifier
{
    public class Notifier : INotifier
    {
        public Result Result = new();

        public async Task NotifyAsync(string message)
        {
            Result.Errors.Add(message);
        }

        public bool HasNotifications()
        {
            return Result.Errors.Any();
        }

        public Result GetResult()
        {
            return Result;
        }
    }
}
