using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ITagsService:IService
    {
        Result.Result Insert(List<Tags> model, Guid DocumentID);
        Result<List<Tags>> List (Guid DocumentID);
        Result<int> Delete(Guid DocumentID);
        Result<List<TagsSearchListVM>> SearchByName(string Name);
    }
}
