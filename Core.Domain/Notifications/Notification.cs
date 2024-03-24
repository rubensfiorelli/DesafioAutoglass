using Core.Domain.Contracts;

namespace Core.Domain.Notifications
{
    public sealed class Notification : INotification
    {
        public Notification(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }

        public string Message { get; private set; }
        public string PropertyName { get; private set; }
    }
}
