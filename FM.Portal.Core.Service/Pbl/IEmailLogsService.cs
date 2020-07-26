using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IEmailLogsService : IService
    {
        Result<EmailLogs> Add(EmailLogs model);
        Result<EmailLogs> Get(Guid ID);
        //Result.Result Delete(Guid ID);
        Result<List<EmailLogs>> List();
    }
}
