using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface IOrderDetailDataSource : IDataSource
    {
        Result Insert(OrderDetail model);
    }
}
