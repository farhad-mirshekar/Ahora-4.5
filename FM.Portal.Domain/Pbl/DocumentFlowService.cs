using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
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

        public Result Confirm(FlowConfirmVM confirmVM)
        {
            try
            {
                confirmVM.DocumentID = SQLHelper.CheckGuidNull("7C02B460-03BC-4DFB-A4AA-627B51405555");
                var listFlowResult = List(new DocumentFlowListVM {DocumentID = confirmVM.DocumentID });
                if (!listFlowResult.Success)
                    return Result.Failure(message: listFlowResult.Message);
                var listFlows = listFlowResult.Data;

                switch (confirmVM.SendType)
                {
                    case SendDocumentType.تایید_ارسال:
                        {
                            switch (_requestInfo.PositionType)
                            {
                                case PositionType.رییس_امور:
                                    {
                                        var lastFlow = listFlows.OrderByDescending(x => x.Date).Select(x => x)?.First();
                                    }
                                    break;
                            }
                        }
                        break;
                }
                return null;
            }
            catch(Exception e) { throw; }
        }

        public Result<List<DocumentFlow>> List(DocumentFlowListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<DocumentFlow>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<DocumentFlow>>.Successful(data: table);
            return Result<List<DocumentFlow>>.Failure();
        }
    }
}
