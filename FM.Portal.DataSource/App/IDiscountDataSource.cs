using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IDiscountDataSource : IDataSource
    {
        Result<Discount> Insert(Discount model);
        Result<Discount> Update(Discount model);
        DataTable List(DiscountListVM listVM);
        Result<Discount> Get(Guid ID);
    }
}
