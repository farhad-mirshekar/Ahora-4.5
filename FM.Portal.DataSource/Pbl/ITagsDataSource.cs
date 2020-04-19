using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ITagsDataSource:IDataSource
    {
        Result Add(List<Tags> model,Guid DocumentID);
        DataTable List(Guid DocumnetID);
        Result<int> Delete(Guid DocumnetID);
        DataTable SearchByName(string Name);
    }
}
