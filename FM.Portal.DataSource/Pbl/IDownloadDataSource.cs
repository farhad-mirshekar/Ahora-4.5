using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDownloadDataSource : IDataSource
    {
        Result<Download> Insert(Download model);
        Result<Download> Update(Download model);
        Result<Download> Get(Guid ID);
        DataTable List(Guid PaymentID);
    }
}
