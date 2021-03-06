﻿using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Caching;

namespace FM.Portal.Domain
{
    public class LocaleStringResourceService : ILocaleStringResourceService
    {
        private readonly ILocaleStringResourceDataSource _dataSource;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        public LocaleStringResourceService(ILocaleStringResourceDataSource dataSource
                                          , IWorkContext workContext
                                          , ICacheManager cacheManager)
        {
            _dataSource = dataSource;
            _workContext = workContext;
            _cacheManager = cacheManager;
        }
        public Result<LocaleStringResource> Add(LocaleStringResource model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<LocaleStringResource>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            var result = _dataSource.Insert(model);
            if (result.Success)
                _cacheManager.Remove(string.Format(CacheParamExtention.Locale_String_Resource_Get_All_Resource_Values, model.LanguageID));
            return result;
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<LocaleStringResource> Edit(LocaleStringResource model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<LocaleStringResource>.Failure(message: validateResult.Message);

            var result = _dataSource.Update(model);
            if (result.Success)
                _cacheManager.Remove(string.Format(CacheParamExtention.Locale_String_Resource_Get_All_Resource_Values, model.LanguageID));
            return result;
        }

        public Result<LocaleStringResource> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<string> GetResource(string format)
        {
            var languageID = Helper.LanguageID;

            if (_workContext.WorkingLanguage != null)
            {
                languageID = _workContext.WorkingLanguage.ID;
            }

            return GetResource(format, languageID);
        }

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
        private Result<string> GetResource(string resourceKey, Guid LanguageID)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            var resourcesResult = GetAllResourceValues(LanguageID);
            if (!resourcesResult.Success)
                return Result<string>.Failure(message: resourcesResult.Message);

            var resources = resourcesResult.Data;
            if (resources.ContainsKey(resourceKey))
            {
                result = resources[resourceKey].Value;
            }
            return Result<string>.Successful(data: result);
        }
        private Result<Dictionary<string, KeyValuePair<Guid, string>>> GetAllResourceValues(Guid LanguageID)
        {
            string key = string.Format(CacheParamExtention.Locale_String_Resource_Get_All_Resource_Values, LanguageID);
            return _cacheManager.Get(key, () =>
            {
                var listResult = List(new LocaleStringResourceListVM() { LanguageID = LanguageID });
                if (!listResult.Success)
                    return Result<Dictionary<string, KeyValuePair<Guid, string>>>.Failure(message: listResult.Message);

                var localeStringResources = listResult.Data;
                var dictionary = new Dictionary<string, KeyValuePair<Guid, string>>();

                foreach (var locale in localeStringResources)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<Guid, string>(locale.ID, locale.ResourceValue));
                }

                return Result<Dictionary<string, KeyValuePair<Guid, string>>>.Successful(data: dictionary);
            });
        }
    }
}
