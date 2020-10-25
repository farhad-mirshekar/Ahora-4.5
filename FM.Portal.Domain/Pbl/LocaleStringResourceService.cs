using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class LocaleStringResourceService : ILocaleStringResourceService
    {
        private readonly ILocaleStringResourceDataSource _dataSource;
        public LocaleStringResourceService(ILocaleStringResourceDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<LocaleStringResource> Add(LocaleStringResource model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<LocaleStringResource>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<LocaleStringResource> Edit(LocaleStringResource model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<LocaleStringResource>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<LocaleStringResource> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<LocaleStringResource>> List(LocaleStringResourceListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<LocaleStringResource>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<LocaleStringResource>>.Successful(data: table);
            return Result<List<LocaleStringResource>>.Failure();
        }

        private Result ValidateModel(LocaleStringResource localeStringResource)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(localeStringResource.ResourceName))
                errors.Add("نام فیلد را وارد نمایید");

            if (localeStringResource.LanguageID == Guid.Empty)
                errors.Add("زبان را انتخاب نمایید");

            if (string.IsNullOrEmpty(localeStringResource.ResourceValue))
                errors.Add("مقدار فیلد را انتخاب نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
