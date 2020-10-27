using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IEmailLogsDataSource:IDataSource
    {
        Result<EmailLogs> Insert(EmailLogs model);
        Result<EmailLogs> Get(Guid ID);
        //Result Delete(Guid ID);
        DataTable List();
    }
}
