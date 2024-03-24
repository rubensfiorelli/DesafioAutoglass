using Core.Domain.Notifications;

namespace Core.Domain.Validations
{
    public partial class ContractValidations<T>
    {
        public ContractValidations<T> ValidManufactureIsOk(DateTime Manufacturing, DateTime Validate, string message, string propertyName)
        {
            if (Manufacturing >= Validate)
                AddNotification(new Notification(message, propertyName));

            return this;

        }

    }
  
}
