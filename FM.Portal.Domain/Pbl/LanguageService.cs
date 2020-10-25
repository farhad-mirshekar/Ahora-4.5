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
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageDataSource _dataSource;
        public LanguageService(ILanguageDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Language> Add(Language model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Language>.Failure(message: validateResult.Message);
            
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Language> Edit(Language model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Language>.Failure(message: validateResult.Message);

            return _dataSource.Update(model);
        }

        public Result<Language> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Language>> List(LanguageListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Language>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Language>>.Successful(data: table);
            return Result<List<Language>>.Failure();
        }

        private Result ValidateModel(Language language)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(language.Name))
                errors.Add("نام زبان را وارد نمایید");

            if (language.LanguageCultureType == LanguageCultureType.unknown)
                errors.Add("نوع زبان را انتخاب نمایید");

            if (string.IsNullOrEmpty(language.UniqueSeoCode))
                errors.Add("نام سئو زبان را انتخاب نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
