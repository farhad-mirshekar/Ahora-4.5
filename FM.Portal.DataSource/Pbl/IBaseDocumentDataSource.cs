using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.DataSource
{
   public interface IBaseDocumentDataSource:IDataSource
    {
        Result<BaseDocument> Insert(BaseDocument model);
        Result<BaseDocument> Update(BaseDocument model);
        Result<BaseDocument> Get(Guid ID);
    }
}
