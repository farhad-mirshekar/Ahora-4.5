using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ISalesDataSource:IDataSource
    {
        Result<Sales> Insert(Sales model);
        Result<Sales> Get(Guid? ID , Guid? PaymentID);
        Result<Sales> Update(Sales model);
        DataTable List(SalesListVM listVM);
    }
}
