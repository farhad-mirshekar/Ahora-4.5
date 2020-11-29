using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IEventsDataSource:IDataSource
    {
        Result<Events> Insert(Events model);
        Result<Events> Update(Events model);
        DataTable List(EventsListVM listVM);
        Result<Events> Get(Guid ID);
        Result Delete(Guid ID);
    }
}
