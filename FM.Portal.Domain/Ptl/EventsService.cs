using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using FM.Portal.Core.Extention.ReadingTime;
using System.Linq;

namespace FM.Portal.Domain
{
    public class EventsService :IEventsService
    {
        private readonly IEventsDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IAttachmentService _attachmentService;
        private readonly IUrlRecordService _urlRecordService;
        public EventsService(IEventsDataSource dataSource
                            , ITagsService tagsService
                            , IAttachmentService attachmentService
                            , IUrlRecordService urlRecordService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _attachmentService = attachmentService;
            _urlRecordService = urlRecordService;
        }
        public Result<Events> Add(Events model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Events>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            if (model.Tags != null && model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            model.ReadingTime = CalculateReadingTime.MinReadTime(model.Body);
            var result = _dataSource.Insert(model);
            if (result.Success)
            {
                _urlRecordService.Add(new UrlRecord()
                {
                    UrlDesc = model.UrlDesc,
                    EntityID = model.ID,
                    EntityName = model.GetType().Name,
                    Enabled = EnableMenuType.فعال
                });
            }
            return result;
        }

        public Result Delete(Guid ID)
        {
            var attachmentsRsult = _attachmentService.List(ID);
            if (!attachmentsRsult.Success)
                return Result.Failure();

            var attachments = attachmentsRsult.Data;

            if (attachments.Count > 0)
            {
                _tagsService.Delete(ID);

                foreach (var item in attachments)
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
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Events>.Failure(message: validateResult.Message);

            if (model.Tags != null && model.Tags.Count > 0)
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
            var result = _dataSource.Update(model);
            if (result.Success)
            {
                var urlRecordResult = _urlRecordService.Get(null, model.ID);
                if (urlRecordResult.Success)
                {
                    urlRecordResult.Data.UrlDesc = model.UrlDesc;
                    _urlRecordService.Edit(urlRecordResult.Data);
                }
            }
            return result;
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

        public Result<List<Events>> List(EventsListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Events>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Events>>.Successful(data: table);
            return Result<List<Events>>.Failure();
        }

        private Result ValidationModel(Events events)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(events.Body))
                errors.Add("متن اصلی رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(events.Description))
                errors.Add("توضیحات کوتاه رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(events.Title))
                errors.Add("عنوان رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(events.UrlDesc))
                errors.Add("سئو رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(events.MetaKeywords))
                errors.Add("متاتگ رویداد الزامی می باشد");

            if (events.CategoryID == Guid.Empty)
                errors.Add("انتخاب موضوع رویداد الزامی می باشد");

            if (events.LanguageID == null || events.LanguageID == Guid.Empty)
                errors.Add("انتخاب زبان رویداد الزامی می باشد");


            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
