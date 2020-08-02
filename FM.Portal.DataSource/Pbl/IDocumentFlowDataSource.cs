using FM.Portal.Core.Model;
using FM.Portal.Core.Result;

namespace FM.Portal.DataSource
{
   public interface IDocumentFlowDataSource:IDataSource
    {
        Result Insert(DocumentFlow flow);
    }
}
