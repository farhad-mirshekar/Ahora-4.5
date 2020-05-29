using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface ILinkDataSource : IDataSource
    {
        Result<Link> Insert(Link model);
        Result<Link> Update(Link model);
        Result<Link> Get(Guid ID);
        DataTable List(LinkListVM listVM);
        Result Delete(Guid ID);

    }
}
