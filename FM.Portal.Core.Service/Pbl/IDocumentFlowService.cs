using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace FM.Portal.Core.Service
{
   public interface IDocumentFlowService:IService
    {
        Result Add(DocumentFlow flow);
        Result<List<DocumentFlow>> List(DocumentFlowListVM listVM);
        Result SetFlowRead(Guid DocumentID);
        DataTable ListFlow(Guid ID);
    }
}
