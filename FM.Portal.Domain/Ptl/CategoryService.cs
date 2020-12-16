using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.DataSource.Ptl;
using System;
using System.Collections.Generic;
using System.Linq;
using @Services = FM.Portal.Core.Service;
using @Model = FM.Portal.Core.Model;
using FM.Portal.Core.Infrastructure;

namespace FM.Portal.Domain.Ptl
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataSource _dataSource;
        private readonly Services.IActivityLogService _activityLogService;
        private readonly Services.ILocaleStringResourceService _localeStringResourceService;
        private readonly IWebHelper _webHelper;
        public CategoryService(ICategoryDataSource dataSource
                               , Services.IActivityLogService activityLogService
                               , Services.ILocaleStringResourceService localeStringResourceService
                               , IWebHelper webHelper)
        {
            _dataSource = dataSource;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
            _webHelper = webHelper;
        }
        public Result<Category> Add(Category model)
        {
            var validationResult = ValidationModel(model);
            if (!validationResult.Success)
                return Result<Category>.Failure(message: validationResult.Message);

            model.ID = Guid.NewGuid();

            _activityLogService.Add(new Model.ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.AddCategoryPortal").Data ?? "افزودن دسته بندی برای پرتال",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "AddCategoryPortal"
            });

            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        {
            _activityLogService.Add(new Model.ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.DeleteCategoryPortal").Data ?? "حذف دسته بندی برای پرتال",
                EntityID = ID,
                EntityName = "Category",
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "DeleteCategoryPortal"
            });

            return _dataSource.Delete(ID);
        }

        public Result<Category> Edit(Category model)
        {
            var validationResult = ValidationModel(model);
            if (!validationResult.Success)
                return Result<Category>.Failure(message: validationResult.Message);

            _activityLogService.Add(new Model.ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.EditCategoryPortal").Data ?? "ویرایش دسته بندی برای پرتال",
                EntityID = model.ID,
                EntityName = "Category",
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "EditCategoryPortal"
            });

            return _dataSource.Update(model);
        }

        public Result<Category> Get(Guid ID)
            => _dataSource.Get(ID);

        public Result<List<Category>> List()
        {
            var table = ConvertDataTableToList.BindList<Category>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
            {
                if (table.Count > 0)
                {
                    table.ForEach(category =>
                    {
                        category.TitleCrumb = GetFormattedBreadCrumb(category);
                    });
                }

                return Result<List<Category>>.Successful(data: table);
            }
            return Result<List<Category>>.Failure();
        }

        private Result ValidationModel(Category category)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(category.Title))
                errors.Add("نام دسته بندی را وارد نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
        private string GetFormattedBreadCrumb(Category category, string separator = ">>")
        {
            var categoryResult = Get(category.ID);
            if (!categoryResult.Success)
                throw new ArgumentNullException("category");

            category = categoryResult.Data;
            string result = string.Empty;

            var alreadyProcessedCategoryIds = new List<Guid>() { };

            while (category != null && category.ID != Guid.Empty &&  //not null
                 !alreadyProcessedCategoryIds.Contains(category.ID)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Title;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Title, separator, result);
                }

                alreadyProcessedCategoryIds.Add(SQLHelper.CheckGuidNull(category.ID));

                categoryResult = Get(category.ParentID);
                if (!categoryResult.Success)
                    category = null;

                category = categoryResult.Data;

            }
            return result;
        }
    }
}
