

using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
   public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentDataSource _dataSource;
        public AttachmentService(IAttachmentDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Attachment> Add(Attachment model) => _dataSource.Insert(model);

        public Result<int> Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Attachment> Get(Guid ID) => _dataSource.Get(ID);

        public Result<Attachment> GetVideo(Guid ParentID)
        => _dataSource.GetVideo(ParentID);

        public Result<List<Attachment>> List(Guid ParentID)
        {
          var table = ConvertDataTableToList.BindList<Attachment>(_dataSource.List(ParentID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Attachment>>.Successful(data: table);
            return Result<List<Attachment>>.Failure();
        }
        public Result<Attachment> Update(Attachment model) => _dataSource.Update(model);
    }
}
