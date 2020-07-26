using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IEmailLogsDataSource:IDataSource
    {
        Result<EmailLogs> Insert(EmailLogs model);
        Result<EmailLogs> Get(Guid ID);
        //Result.Result Delete(Guid ID);
        DataTable List();
    }
}
