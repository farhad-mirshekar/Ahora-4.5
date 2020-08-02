using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class DocumentFlowService : IDocumentFlowService
    {
        private readonly IDocumentFlowDataSource _dataSource;
        public DocumentFlowService(IDocumentFlowDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result Add(DocumentFlow flow)
        => _dataSource.Insert(flow);
    }
}
