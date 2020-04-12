using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IAttachmentService : IService
    {
        Result<Attachment> Add(Attachment model);
        Result<Attachment> Update(Attachment model);
        Result<Attachment> Get(Guid ID);
        Result<List<Attachment>> List(Guid ParentID);
        Result<int> Delete(Guid ID);
        Result<Attachment> GetVideo(Guid ParentID);

    }
}
