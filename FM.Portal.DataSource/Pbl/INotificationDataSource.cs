using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface INotificationDataSource:IDataSource
    {
        DataTable List(NotificationListVM listVM);
        Result ReadNotification(Guid ID);
        Result<Notification> Get(Guid ID);
        DataTable GetActiveNotification();
    }
}
