using FM.Portal.Core.Model;
using FM.Portal.Core;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IGeneralSettingDataSource:IDataSource
    {
        DataTable List();
        Result Update(SettingVM model);
    }
}
