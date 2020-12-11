using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ITagsService:IService
    {
        Result Add(List<Tags> model);
        Result<List<Tags>> List (Guid DocumentID);
        Result Delete(Guid DocumentID);
        Result<List<TagSearchVM>> List(TagsListVM listVM);
    }
}
