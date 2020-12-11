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
    public class StaticPageService : IStaticPageService
    {
        private readonly IStaticPageDataSource _dataSource;
        private readonly ITagsService _tagsService;
        public StaticPageService(IStaticPageDataSource dataSource
                                ,ITagsService tagsService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
        }
        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<StaticPage> Edit(StaticPage model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<StaticPage>.Failure(message: validateResult.Message);
            if (model.Tags.Count > 0)
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
            return _dataSource.Update(model);
        }

        public Result<StaticPage> Get(Guid ID)
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
        public Result<List<StaticPage>> List(StaticPageListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<StaticPage>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<StaticPage>>.Successful(data: table);
            return Result<List<StaticPage>>.Failure();
        }

        private Result ValidateModel(StaticPage model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.Body))
                errors.Add("متن اصلی را وارد نمایید");
            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات کوتاه را وارد نمایید");
            if (string.IsNullOrEmpty(model.Pages.UrlDesc))
                errors.Add("متن مرورگر وارد نمایید");
            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));
            return Result.Successful();
        }
    }
}
