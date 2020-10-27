using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;

namespace FM.Portal.Core.Service
{
   public interface INotificationService : IService
    {
        Result<List<Notification>> List(NotificationListVM listVM);
        Result<Notification> Get(Guid ID);
        Result ReadNotification(Guid ID);
        Result<List<Notification>> GetActiveNotification();
    }
}
