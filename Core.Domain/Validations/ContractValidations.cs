using Core.Domain.Contracts;
using Core.Domain.Notifications;

namespace Core.Domain.Validations
{
    public partial class ContractValidations<T> where T : IContract
    {
        private List<Notification> _notifications;

        public ContractValidations() => _notifications = new();
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public void AddNotification(Notification notification) => _notifications.Add(notification);
        public bool IsValid() => _notifications.Count == 0;
    }
}
