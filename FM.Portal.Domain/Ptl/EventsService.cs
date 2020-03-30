using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Globalization;

namespace FM.Portal.Domain
{
    public class EventsService :IEventsService
    {
        private readonly IEventsDataSource _dataSource;
        public EventsService(IEventsDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Events> Add(Events model)
        {
            var dt = DateTime.Now;
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString() +
                                  pc.GetDayOfMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
       => _dataSource.Delete(ID);
        public Result<Events> Edit(Events model)
        => _dataSource.Update(model);

        public Result<Events> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Events> Get(string TrackingCode)
        => _dataSource.Get(TrackingCode);

        public Result<List<Events>> List()
        {
            var table = ConvertDataTableToList.BindList<Events>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<Events>>.Successful(data: table);
            return Result<List<Events>>.Failure();
        }

        public Result<List<EventsListVM>> List(int count)
        {
            var table = ConvertDataTableToList.BindList<EventsListVM>(_dataSource.List(count));
            if (table.Count > 0)
                return Result<List<EventsListVM>>.Successful(data: table);
            return Result<List<EventsListVM>>.Failure();
        }
    }
}
