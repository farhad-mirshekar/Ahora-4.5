using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class DynamicPageService : IDynamicPageService
    {
        private readonly IDynamicPageDataSource _dataSource;
        private readonly ITagsService _tagsService;
        public DynamicPageService(IDynamicPageDataSource dataSource
                                  , ITagsService tagsService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
        }
        public Result<DynamicPage> Add(DynamicPage model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<DynamicPage>.Failure(message: validateResult.Message);
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
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<DynamicPage> Edit(DynamicPage model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<DynamicPage>.Failure(message: validateResult.Message);
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
            return _dataSource.Update(model);
        }

        public Result<DynamicPage> Get(Guid ID)
        {
            var dynamicPageResult = _dataSource.Get(ID);
            if (dynamicPageResult.Success)
            {
                var resultTag = _tagsService.List(ID);
                if (resultTag.Success)
                {
                    List<string> tags = new List<string>();
                    foreach (var item in resultTag.Data)
                    {
                        tags.Add(item.Name);
                    }
                    dynamicPageResult.Data.Tags = tags;
                }
            }
            return dynamicPageResult;
        }
        public Result<List<DynamicPage>> List(DynamicPageListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<DynamicPage>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<DynamicPage>>.Successful(data: table);
            return Result<List<DynamicPage>>.Failure();
        }

        private Result ValidateModel(DynamicPage model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.Body))
                errors.Add("متن اصلی را وارد نمایید");
            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات کوتاه را وارد نمایید");
            if (string.IsNullOrEmpty(model.UrlDesc))
                errors.Add("متن مرورگر وارد نمایید");
            if (errors.Any())
                return Result.Failure(message:  string.Join("&&",errors));
            return Result.Successful();
        }
    }
}
