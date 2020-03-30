using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IEventsService : IService
    {
        Result<Events> Add(Events model);
        Result<Events> Edit(Events model);
        Result<List<Events>> List();
        Result<List<EventsListVM>> List(int count);
        Result<Events> Get(Guid ID);
        Result<Events> Get(string TrackingCode);
        Result<int> Delete(Guid ID);

    }
}
