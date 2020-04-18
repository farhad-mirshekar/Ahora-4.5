using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.Core.Service
{
   public interface INotificationService : IService
    {
        Result<List<Notification>> List();
        Result.Result ReadNotification(Guid ID);
    }
}
