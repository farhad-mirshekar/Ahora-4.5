using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class EmailLogsService : IEmailLogsService
    {
        private readonly IEmailLogsDataSource _dataSource;
        public EmailLogsService(IEmailLogsDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<EmailLogs> Add(EmailLogs model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result<EmailLogs> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<EmailLogs>> List()
        {
            var table = ConvertDataTableToList.BindList<EmailLogs>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<EmailLogs>>.Successful(data: table);
            return Result<List<EmailLogs>>.Failure();
        }
    }
}
