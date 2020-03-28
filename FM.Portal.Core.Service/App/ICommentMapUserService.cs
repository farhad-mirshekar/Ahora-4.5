using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.Core.Service
{
   public interface ICommentMapUserService : IService
    {
        Result<CommentMapUser> Add(CommentMapUser model);
        Result<bool> IsUserLike(CommentMapUser model);
    }
}
