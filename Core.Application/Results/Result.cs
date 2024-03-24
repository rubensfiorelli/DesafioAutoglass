using Core.Domain.Notifications;

namespace Core.Application.Results
{
    public class Result : IResultBase
    {
        private List<Notification> _notifications;

        public Result(int resultCode, string message, bool success, object data)
        {
            ResultCode = resultCode;
            Message = message;
            Success = success;
            Data = data;

            _notifications = new List<Notification>();
        }

        public int ResultCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void SetNotifications(List<Notification> notifications)
        {
            _notifications = notifications;
        }

        public void SetData(object data)
        {
            Data = data;
        }
    }
}
