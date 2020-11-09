using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IEventsCommentService:IService
    {
        Result<EventsComment> Add(EventsComment model);
        Result<EventsComment> Edit(EventsComment model);
        Result<EventsComment> Get(Guid ID);
        Result<List<EventsComment>> List(EventsCommentListVM listVM);
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
