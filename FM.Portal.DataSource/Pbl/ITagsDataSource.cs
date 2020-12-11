using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ITagsDataSource:IDataSource
    {
        Result Insert(List<Tags> model);
        DataTable List(Guid DocumnetID);
        Result Delete(Guid DocumnetID);
        DataTable SearchByName(TagsListVM listVM);
    }
}
