

using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class CommentMapUserService : ICommentMapUserService
    {
        private readonly ICommentMapUserDataSource _dataSource;
        public CommentMapUserService(ICommentMapUserDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<CommentMapUser> Add(CommentMapUser model)
        {
            return _dataSource.Insert(model);
        }

        public Result<bool> IsUserLike(CommentMapUser model)
        {
            return _dataSource.IsUserLike(model);
        }
    }
}
