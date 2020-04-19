using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
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

        public Result Insert(List<Tags> model,Guid DocumentID)
        => _dataSource.Add(model,DocumentID);

        public Result<int> Delete(Guid DocumentID)
        => _dataSource.Delete(DocumentID);

        public Result<List<TagsSearchListVM>> SearchByName(string Name)
        {
            var table = ConvertDataTableToList.BindList<TagsSearchListVM>(_dataSource.SearchByName(Name.Trim()));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<TagsSearchListVM>>.Successful(data: table);
            return Result<List<TagsSearchListVM>>.Failure();
        }
    }
}
