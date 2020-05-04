using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Globalization;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using FM.Portal.Core.Extention.ReadingTime;

namespace FM.Portal.Domain
{
    public class EventsService :IEventsService
    {
        private readonly IEventsDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IAttachmentService _attachmentService;
        public EventsService(IEventsDataSource dataSource
                            , ITagsService tagsService
                            , IAttachmentService attachmentService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _attachmentService = attachmentService;
        }
        public Result<Events> Add(Events model)
        {
            var dt = DateTime.Now;
            var pc = new PersianCalendar();
            string trackingCode = pc.GetYear(dt).ToString().Substring(2, 2) +
                                  pc.GetMonth(dt).ToString() +
                                  pc.GetDayOfMonth(dt).ToString();
            model.TrackingCode = trackingCode;
            model.ID = Guid.NewGuid();
            if (model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
        {
            var attachment = _attachmentService.List(ID);
            _tagsService.Delete(ID);
            if(attachment.Data.Count > 0)
            {
                foreach (var item in attachment.Data)
                {
                    string path = $"{Enum.GetName(typeof(PathType), item.PathType)}/{item.FileName}";
                    _attachmentService.Delete(item.ID);
                    FileHelper.DeleteFile(path);
                }
            }
           return _dataSource.Delete(ID);
        }
        public Result<Events> Edit(Events model)
        {
            if (model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            else
            {
                _tagsService.Delete(model.ID);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            return _dataSource.Update(model);
        }

        public Result<Events> Get(Guid ID)
        {
            var events = _dataSource.Get(ID);
            if (events.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    events.Data.Tags = tags;
                }
            }
            return events;
        }

        public Result<Events> Get(string TrackingCode)
        {
            var events = _dataSource.Get(TrackingCode);
            if (events.Success)
            {
                var resultTag = _tagsService.List(events.Data.ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    events.Data.Tags = tags;
                }
            }
            return events;
        }

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
            if (table.Count > 0 || table.Count == 0)
                return Result<List<EventsListVM>>.Successful(data: table);
            return Result<List<EventsListVM>>.Failure();
        }
    }
}
