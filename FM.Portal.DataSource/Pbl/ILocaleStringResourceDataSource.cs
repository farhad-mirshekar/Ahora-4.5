using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ILocaleStringResourceDataSource:IDataSource
    {
        Result<LocaleStringResource> Insert(LocaleStringResource model);
        Result<LocaleStringResource> Update(LocaleStringResource model);
        Result<LocaleStringResource> Get(Guid ID);
        DataTable List(LocaleStringResourceListVM listVM);
        Result Delete(Guid ID);
    }
}
