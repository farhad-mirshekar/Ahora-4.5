using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface ILanguageDataSource:IDataSource
    {
        Result<Language> Insert(Language model);
        Result<Language> Update(Language model);
        Result<Language> Get(Guid ID);
        DataTable List(LanguageListVM listVM);
        Result Delete(Guid ID);
    }
}
