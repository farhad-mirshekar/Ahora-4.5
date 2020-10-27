namespace FM.Portal.Core.Service
{
   public interface IBankService : IService
    {
        Result<Core.Model.Bank> GetActiveBank();
    }
}
