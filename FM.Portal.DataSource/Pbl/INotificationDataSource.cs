using System.Data;

namespace FM.Portal.DataSource
{
   public interface INotificationDataSource:IDataSource
    {
        DataTable List();
    }
}
