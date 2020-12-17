using System;
using System.Collections.Generic;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Infrastructure;
using FM.Portal.FrameWork.MVC.Helpers.Files;

namespace FM.Portal.Domain
{
    public class SliderService : ISliderService
    {
        private readonly ISliderDataSource _dataSource;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        private readonly IWebHelper _webHelper;
        private readonly IAttachmentService _attachmentService;
        public SliderService(ISliderDataSource dataSource
                            , IActivityLogService activityLogService
                            , ILocaleStringResourceService localeStringResourceService
                            , IWebHelper webHelper
                            , IAttachmentService attachmentService)
        {
            _dataSource = dataSource;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _webHelper = webHelper;
            _attachmentService = attachmentService;
        }
        public Result<Slider> Add(Slider model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Slider>.Failure(message: validate.Message);

            model.ID = Guid.NewGuid();
            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.AddSlider").Data ?? "افزودن تصویر کشویی",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "AddSlider"
            });

            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        {
            var result = _dataSource.Delete(ID);
            if (result.Success)
            {
                var attachmentsResult = _attachmentService.List(ID);
                if (!attachmentsResult.Success)
                    return Result.Failure();

                var attachments = attachmentsResult.Data;

                if (attachments.Count > 0)
                {
                    foreach (var item in attachments)
                    {
                        string path = $"{Enum.GetName(typeof(PathType), item.PathType)}/{item.FileName}";
                        _attachmentService.Delete(item.ID);
                        FileHelper.DeleteFile(path);
                    }
                }

                _activityLogService.Add(new ActivityLog()
                {
                    Comment = _localeStringResourceService.GetResource("ActivityLog.DeleteSlider").Data ?? "حذف تصویر کشویی",
                    EntityID = ID,
                    EntityName = "Slider",
                    IpAddress = _webHelper.GetCurrentIpAddress(),
                    SystemKeyword = "DeleteSlider"
                });
            }

            return result;
        }

        public Result<Slider> Edit(Slider model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Slider>.Failure(message: validate.Message);

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.UpdateSlider").Data ?? "ویرایش تصویر کشویی",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "UpdateSlider"
            });

            return _dataSource.Update(model);
        }

        public Result<Slider> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Slider>> List(SliderListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Slider>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Slider>>.Successful(data: table);
            return Result<List<Slider>>.Failure();
        }
        private Result ValidationModel(Slider model)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(model.Title))
                errors.Add("عنوان تصویر کشویی را وارد نمایید");

            if (model.Enabled == EnableMenuType.نامشخص)
                errors.Add("نحوه نمایش تصویر کشویی را مشخص نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
