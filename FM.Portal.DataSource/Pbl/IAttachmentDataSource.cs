using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
  public interface IAttachmentDataSource : IDataSource
    {
        Result<Attachment> Insert(Attachment model);
        Result<Attachment> Update(Attachment model);
        Result<Attachment> Get(Guid ID);
        DataTable List(Guid ParentID);
        Result<int> Delete(Guid ID);
        Result<Attachment> GetVideo(Guid ParentID);
    }
}
