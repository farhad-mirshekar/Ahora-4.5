using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IFaqDataSource : IDataSource
    {
        Result<FAQ> Insert(FAQ model);
        Result<FAQ> Update(FAQ model);
        Result<FAQ> Get(Guid ID);
        DataTable List(Guid FAQGroupID);
    }
}
