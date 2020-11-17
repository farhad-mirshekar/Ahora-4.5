using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IUrlRecordDataSource : IDataSource
    {
        Result<UrlRecord> Insert(UrlRecord model);
        Result<UrlRecord> Update(UrlRecord model);
        Result<UrlRecord> Get(string UrlDesc);
        Result<UrlRecord> Get(Guid? ID , Guid? EntityID);
        DataTable List(UrlRecordListVM listVM);
    }
}
