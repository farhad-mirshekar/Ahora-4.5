using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class BankService : IBankService
    {
        private readonly IBankDataSource _dataSource;

        public BankService(IBankDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Core.Model.Bank> GetActiveBank()
         => _dataSource.GetActiveBank();
    }
}
