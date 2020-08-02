using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;

namespace FM.Portal.Domain
{
    public class BaseDocumentService : IBaseDocumentService
    {
        private readonly IBaseDocumentDataSource _dataSource;
        public BaseDocumentService(IBaseDocumentDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<BaseDocument> Add(BaseDocument model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result<BaseDocument> Edit(BaseDocument model)
        => _dataSource.Update(model);

        public Result<BaseDocument> Get(Guid ID)
        => _dataSource.Get(ID);
    }
}
