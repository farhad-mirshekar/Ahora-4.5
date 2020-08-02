using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.Core.Service
{
    public interface IBaseDocumentService : IService
    {
        Result<BaseDocument> Add(BaseDocument model);
        Result<BaseDocument> Edit(BaseDocument model);
        Result<BaseDocument> Get(Guid ID);
    }
}
