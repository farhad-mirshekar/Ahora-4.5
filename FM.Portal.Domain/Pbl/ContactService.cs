using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class ContactService : IContactService
    {
        private readonly IContactDataSource _dataSource;
        public ContactService(IContactDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Contact> Add(Contact model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Contact> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Contact>> List(ContactListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Contact>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Contact>>.Successful(data: table);
            return Result<List<Contact>>.Failure();
        }
    }
}
