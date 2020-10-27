using FM.Portal.Core;

namespace FM.Portal.DataSource
{
   public interface IBankDataSource :IDataSource
    {
        Result<Core.Model.Bank> GetActiveBank();
    }
}
