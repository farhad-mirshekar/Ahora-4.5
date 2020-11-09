using FM.Portal.Core;
using FM.Portal.Core.Model;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IEventsCommentDataSource:IDataSource
    {
        Result<EventsComment> Insert(EventsComment model);
        Result<EventsComment> Update(EventsComment model);
        Result<EventsComment> Get(Guid ID);
        DataTable List(EventsCommentListVM listVM);
        Result Delete(Guid ID);
        Result<EventsComment> Like(Guid ID, Guid UserID);
        Result<EventsComment> DisLike(Guid ID, Guid UserID);

        /// <summary>
        /// For Table EventsComment_User_Mapping
        /// </summary>
        /// <param name="commentMapUser"></param>
        /// <returns></returns>
        Result UserCanLike(EventsCommentMapUser commentMapUser);
    }
}
