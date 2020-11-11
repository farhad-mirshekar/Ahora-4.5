using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IActivityLogTypeService : IService
    {
        Result<ActivityLogType> Add(ActivityLogType model);
        Result<ActivityLogType> Edit(ActivityLogType model);
        Result<ActivityLogType> Get(Guid ID);
        Result Delete(Guid ID);
        Result<List<ActivityLogType>> List(ActivityLogTypeListVM listVM);
    }
}
