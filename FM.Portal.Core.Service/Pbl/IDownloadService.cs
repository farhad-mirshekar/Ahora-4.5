using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDownloadService : IService
    {
        Result<Download> Add(Download model);
        Result<Download> Edit(Download model);
        Result<Download> Get(Guid ID);
        Result<List<Download>> List(Guid PaymentID);
    }
}
