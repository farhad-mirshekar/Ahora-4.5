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
    public class NewsService : INewsService
    {
        private readonly INewsDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHelper _webHelper;
        public NewsService(INewsDataSource dataSource
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
        public Result<News> Add(News model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<News>.Failure(message: validateResult.Message);

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
                Comment = _localeStringResourceService.GetResource("ActivityLog.AddNews").Data ?? "افزودن خبر",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "AddNews"
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
                Comment = _localeStringResourceService.GetResource("ActivityLog.DeleteNews").Data ?? "حذف خبر",
                EntityID = ID,
                EntityName = "News",
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "DeleteNews"
            });

            return _dataSource.Delete(ID);
        }
        public Result<News> Edit(News model)
        {
            var validateResult = ValidationModel(model);
            if (!validateResult.Success)
                return Result<News>.Failure(message: validateResult.Message);

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
                Comment = _localeStringResourceService.GetResource("ActivityLog.UpdateNews").Data ?? "ویرایش خبر",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "UpdateNews"
            });

            return result;
        }

        public Result<News> Get(Guid ID)
        {
            var news = _dataSource.Get(ID);
            if (news.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    news.Data.Tags = tags;
                }
            }
            return news;
        }

        public Result<List<News>> List(NewsListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<News>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<News>>.Successful(data: table);
            return Result<List<News>>.Failure();
        }

        private Result ValidationModel(News news)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(news.Body))
                errors.Add("متن اصلی خبر الزامی می باشد");

            if (string.IsNullOrEmpty(news.Description))
                errors.Add("توضیحات کوتاه خبر الزامی می باشد");

            if (string.IsNullOrEmpty(news.Title))
                errors.Add("عنوان خبر الزامی می باشد");

            if (string.IsNullOrEmpty(news.UrlDesc))
                errors.Add("سئو خبر الزامی می باشد");

            if (string.IsNullOrEmpty(news.MetaKeywords))
                errors.Add("متاتگ خبر الزامی می باشد");

            if (news.CategoryID == Guid.Empty)
                errors.Add("انتخاب موضوع خبر الزامی می باشد");

            if (news.LanguageID == null || news.LanguageID == Guid.Empty)
                errors.Add("انتخاب زبان خبر الزامی می باشد");


            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
