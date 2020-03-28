using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IGeneralSettingDataSource:IDataSource
    {
        DataTable List();
        Result Update(SettingVM model);
    }
}
