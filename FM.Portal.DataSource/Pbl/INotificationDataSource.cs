using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface INotificationDataSource:IDataSource
    {
        DataTable List();
        Result ReadNotification(Guid ID);
    }
}
