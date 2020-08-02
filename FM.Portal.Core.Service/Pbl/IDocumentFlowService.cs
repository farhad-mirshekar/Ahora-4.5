using FM.Portal.Core.Model;

namespace FM.Portal.Core.Service
{
   public interface IDocumentFlowService:IService
    {
        Result.Result Add(DocumentFlow flow);
    }
}
