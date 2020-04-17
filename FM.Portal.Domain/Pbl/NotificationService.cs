using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class NotificationService : INotificationService
    {
        readonly INotificationDataSource _dataSource;
        public NotificationService(INotificationDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<List<Notification>> List()
        {
            var table = ConvertDataTableToList.BindList<Notification>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Notification>>.Successful(data: table);
            return Result<List<Notification>>.Failure();
        }
    }
}
