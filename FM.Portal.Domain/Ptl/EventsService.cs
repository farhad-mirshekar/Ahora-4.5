using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Extention.ReadingTime;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using System.Linq;
using FM.Portal.Core.Infrastructure;

namespace FM.Portal.Domain
{
    public class EventsService : IEventsService
    {
        private readonly IEventsDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHelper _webHelper;
        public EventsService(IEventsDataSource dataSource
                           , ITagsService tagsService
                           , IActivityLogService activityLogService
                           , ILocaleStringResourceService localeStringResourceService
                           , IUrlRecordService urlRecordService
                           , IAttachmentService attachmentService
                           , IWebHelper webHelper)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _urlRecordService = urlRecordService;
            _attachmentService = attachmentService;
            _webHelper = webHelper;
        }
        public Result<Events> Add(Events model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Events>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
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

                if (model.Tags != null && model.Tags.Count > 0)
                {
                    var tags = new List<Tags>();
                    foreach (var item in model.Tags)
                    {
                        tags.Add(new Tags { Name = item, DocumentID = model.ID });
                    }
                    _tagsService.Add(tags);
                }
            }

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.AddEvents").Data ?? "افزودن رویداد",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "AddEvents"
            });

            return result;
        }

        public Result Delete(Guid ID)
        {
            //var attachmentsRsult = _attachmentService.List(ID);
            //if (!attachmentsRsult.Success)
            //    return Result.Failure();

            //var attachments = attachmentsRsult.Data;

            //if (attachments.Count > 0)
            //{
            //    _tagsService.Delete(ID);

            //    foreach (var item in attachments)
            //    {
            //        string path = $"{Enum.GetName(typeof(PathType), item.PathType)}/{item.FileName}";
            //        _attachmentService.Delete(item.ID);
            //        FileHelper.DeleteFile(path);
            //    }
            //}

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.DeleteEvents").Data ?? "حذف رویداد",
                EntityID = ID,
                EntityName = "Events",
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "DeleteEvents"
            });

            return _dataSource.Delete(ID);
        }
        public Result<Events> Edit(Events model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<Events>.Failure(message: validateResult.Message);

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

                if (model.Tags != null && model.Tags.Count > 0)
                {
                    var tags = new List<Tags>();
                    foreach (var item in model.Tags)
                    {
                        tags.Add(new Tags { Name = item, DocumentID = model.ID });
                    }
                    _tagsService.Add(tags);
                }
                else
                {
                    _tagsService.Delete(model.ID);
                }
            }

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.UpdateEvents").Data ?? "ویرایش رویداد",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "UpdateEvents"
            });

            return result;
        }

        public Result<Events> Get(Guid ID)
        {
            var Events = _dataSource.Get(ID);
            if (Events.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    Events.Data.Tags = tags;
                }
            }
            return Events;
        }

        public Result<List<Events>> List(EventsListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Events>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Events>>.Successful(data: table);
            return Result<List<Events>>.Failure();
        }

        private Result ValidationModel(Events model)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.Body))
                errors.Add("متن اصلی رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات کوتاه رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(model.Title))
                errors.Add("عنوان رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(model.UrlDesc))
                errors.Add("سئو رویداد الزامی می باشد");

            if (string.IsNullOrEmpty(model.MetaKeywords))
                errors.Add("متاتگ رویداد الزامی می باشد");

            if (model.CategoryID == Guid.Empty)
                errors.Add("انتخاب موضوع رویداد الزامی می باشد");

            if (model.LanguageID == null || model.LanguageID == Guid.Empty)
                errors.Add("انتخاب زبان رویداد الزامی می باشد");


            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
