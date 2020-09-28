using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IContactService:IService
    {
        Result<Contact> Add(Contact model);
        Result<Contact> Get(Guid ID);
        Result.Result Delete (Guid ID);
        Result<List<Contact>> List(ContactListVM listVM);
    }
}
