using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface IPaymentDataSource : IDataSource
    {
        Result FirstStepPayment(Core.Model.Order order, Core.Model.OrderDetail detail, Core.Model.Payment payment);
        Result<Core.Model.Payment> Update(Core.Model.Payment model);
        Result<Core.Model.Payment> Get(System.Guid? ID , System.Guid? OrderID , System.Guid? UserID);

    }
}
