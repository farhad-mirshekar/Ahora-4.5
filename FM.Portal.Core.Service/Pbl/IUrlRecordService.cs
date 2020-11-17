using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
    public interface IUrlRecordService:IService
    {
        Result<UrlRecord> Add(UrlRecord model);
        Result<UrlRecord> Edit(UrlRecord model);
        Result<UrlRecord> Get(string UrlDesc);
        Result<UrlRecord> Get(Guid? ID,Guid? EntityID);
        Result<List<UrlRecord>> List(UrlRecordListVM listVM);
    }
}
