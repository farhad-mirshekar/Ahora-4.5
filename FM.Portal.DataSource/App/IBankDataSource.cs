using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface IBankDataSource :IDataSource
    {
        Result<Core.Model.Bank> GetActiveBank();
    }
}
