using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface ICommentMapUserDataSource : IDataSource
    {
        Result<CommentMapUser> Insert(CommentMapUser model);
        Result<bool> IsUserLike(CommentMapUser model);
    }
}
