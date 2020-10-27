using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FM.Portal.Domain
{
    public class DocumentFlowService : IDocumentFlowService
    {
        private readonly IDocumentFlowDataSource _dataSource;
        private readonly IRequestInfo _requestInfo;
        public DocumentFlowService(IDocumentFlowDataSource dataSource
                                   , IRequestInfo requestInfo)
        {
            _dataSource = dataSource;
            _requestInfo = requestInfo;
        }
        public Result Add(DocumentFlow flow)
        => _dataSource.Insert(flow);

        public Result<List<DocumentFlow>> List(DocumentFlowListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<DocumentFlow>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<DocumentFlow>>.Successful(data: table);
            return Result<List<DocumentFlow>>.Failure();
        }

        public DataTable ListFlow(Guid ID)
        => _dataSource.ListFlow(ID);

        public Result SetFlowRead(Guid DocumentID)
        => _dataSource.SetAsRead(DocumentID);
    }
}
