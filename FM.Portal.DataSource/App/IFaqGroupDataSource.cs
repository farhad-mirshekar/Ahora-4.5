using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IFaqGroupDataSource : IDataSource
    {
        Result<FAQGroup> Get(Guid ID);
        DataTable List(FaqGroupListVM listVM);
        Result<FAQGroup> Insert(FAQGroup model);
        Result<FAQGroup> Update(FAQGroup model);
    }
}
