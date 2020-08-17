using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IDocumentFlowService:IService
    {
        Result.Result Add(DocumentFlow flow);
        Result.Result Confirm(FlowConfirmVM confirmVM);
        Result<List<DocumentFlow>> List(DocumentFlowListVM listVM);
        Result.Result SetFlowRead(Guid DocumentID);
    }
}
