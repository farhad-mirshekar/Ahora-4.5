using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface IContactDataSource : IDataSource
    {
        Result<Contact> Insert(Contact model);
        Result<Contact> Get(Guid ID);
        Result Delete(Guid ID);
        DataTable List(ContactListVM listVM);
    }
}
