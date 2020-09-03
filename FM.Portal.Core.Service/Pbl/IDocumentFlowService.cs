using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.Core.Service
{
   public interface IDocumentFlowService:IService
    {
        Result.Result Add(DocumentFlow flow);
        Result<List<DocumentFlow>> List(DocumentFlowListVM listVM);
        Result.Result SetFlowRead(Guid DocumentID);
        DataTable ListFlow(Guid ID);
    }
}
