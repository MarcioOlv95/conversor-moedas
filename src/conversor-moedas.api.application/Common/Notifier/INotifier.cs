using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.application.Common.Notifier
{
    public interface INotifier
    {
        Task NotifyAsync(string message);
        bool HasNotifications();
        Result GetResult();
    }
}