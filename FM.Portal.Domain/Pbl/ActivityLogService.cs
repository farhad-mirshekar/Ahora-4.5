using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogDataSource _dataSource;
        private readonly IActivityLogTypeService _activityLogTypeService;
        private readonly IRequestInfo _requestInfo;
        public ActivityLogService(IActivityLogDataSource dataSource
                                 , IActivityLogTypeService activityLogTypeService
                                 , IRequestInfo requestInfo)
        {
            _dataSource = dataSource;
            _activityLogTypeService = activityLogTypeService;
            _requestInfo = requestInfo;
        }
        public Result<ActivityLog> Add(ActivityLog model)
        {
            var activityLogTypeResult = _activityLogTypeService.List(new ActivityLogTypeListVM() {SystemKeyword = model.SystemKeyword });
            if (!activityLogTypeResult.Success)
                return Result<ActivityLog>.Failure(message: activityLogTypeResult.Message);
            
            var activityLogType = activityLogTypeResult.Data.First();
            model.ID = Guid.NewGuid();
            model.ActivityLogTypeID = activityLogType.ID;
            if (model.UserID == null)
                model.UserID = _requestInfo.UserId;
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<ActivityLog> Edit(ActivityLog model)
        => _dataSource.Update(model);

        public Result<ActivityLog> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<ActivityLog>> List(ActivityLogListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<ActivityLog>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<ActivityLog>>.Successful(data: table);
            return Result<List<ActivityLog>>.Failure();
        }
    }
}
