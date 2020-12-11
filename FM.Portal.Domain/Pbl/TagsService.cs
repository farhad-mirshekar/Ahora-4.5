using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class TagsService : ITagsService
    {
        private readonly ITagsDataSource _dataSource;
        public TagsService(ITagsDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<List<Tags>> List(Guid DocumentID)
        {
            var table = ConvertDataTableToList.BindList<Tags>(_dataSource.List(DocumentID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Tags>>.Successful(data: table);
            return Result<List<Tags>>.Failure();
        }

        public Result Add(List<Tags> model)
        => _dataSource.Insert(model);

        public Result Delete(Guid DocumentID)
        => _dataSource.Delete(DocumentID);

        public Result<List<TagSearchVM>> List(TagsListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<TagSearchVM>(_dataSource.SearchByName(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<TagSearchVM>>.Successful(data: table);
            return Result<List<TagSearchVM>>.Failure();
        }
    }
}
