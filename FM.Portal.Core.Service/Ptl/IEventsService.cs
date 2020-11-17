using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IEventsService : IService
    {
        Result<Events> Add(Events model);
        Result<Events> Edit(Events model);
        Result<List<Events>> List(EventsListVM listVM);
        Result<Events> Get(Guid ID);
        Result<int> Delete(Guid ID);

    }
}
