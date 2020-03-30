using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IEventsDataSource:IDataSource
    {
        Result<Events> Insert(Events model);
        Result<Events> Update(Events model);
        DataTable List();
        DataTable List(int count);
        Result<Events> Get(Guid ID);
        Result<Events> Get(string TrackingCode);
        Result<int> Delete(Guid ID);
    }
}
