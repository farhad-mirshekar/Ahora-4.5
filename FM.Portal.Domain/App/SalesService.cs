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
    public class SalesService : ISalesService
    {
        private readonly ISalesDataSource _dataSource;
        private readonly IDepartmentService _departmentService;
        private readonly IPositionService _positionService;
        private readonly IRequestInfo _requestInfo;
        private readonly IDocumentFlowService _documentFlowService;
        public SalesService(ISalesDataSource dataSource
                            , IDepartmentService departmentService
                            , IPositionService positionService
                            , IRequestInfo requestInfo
                            , IDocumentFlowService documentFlowService)
        {
            _dataSource = dataSource;
            _departmentService = departmentService;
            _positionService = positionService;
            _requestInfo = requestInfo;
            _documentFlowService = documentFlowService;
        }
        public Result<Sales> Add(Sales model)
        {
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Confirm(FlowConfirmVM confirmVM)
        {
            try
            {
                var salesResult = Get(confirmVM.DocumentID,null);
                if (!salesResult.Success)
                    return Result.Failure(message: "خطا در بازیابی اطلاعات");
                var sales = salesResult.Data;

                var flow = new SalesFlow()
                {
                    DocumentID = confirmVM.DocumentID
                ,
                    FromUserID = (Guid)_requestInfo.UserId
                ,
                    FromPositionID = (Guid)_requestInfo.PositionId
                ,
                    FromDocState = sales.LastDocState
                ,
                    Comment = confirmVM.Comment
                };
                switch (sales.LastDocState)
                {
                    case SalesDocState.بررسی_و_ارجاع_به_واحد_مالی:
                        var financialReviewResult = FinancialReview(flow);
                        if (!financialReviewResult.Success)
                            return financialReviewResult;
                        break;
                    case SalesDocState.بررسی_و_ارجاع_به_واحد_انبار:
                        var wareHouseReviewResult = WareHouseReview(flow);
                        if (!wareHouseReviewResult.Success)
                            return wareHouseReviewResult;
                        break;

                    case SalesDocState.آماده_بسته_بندی:
                        var readyToPackReview = ReadyToPackReview(flow);
                        if (!readyToPackReview.Success)
                            return readyToPackReview;
                        break;
                }

                return _documentFlowService.Add(MapToFlow(flow));
            }
            catch (Exception e) { throw; }
        }

        public Result<Sales> Edit(Sales model)
        => _dataSource.Update(model);

        public Result<Sales> Get(Guid? ID , Guid? PaymentID)
        => _dataSource.Get(ID,PaymentID);

        public Result<List<Sales>> List(SalesListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Sales>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Sales>>.Successful(data: table);
            return Result<List<Sales>>.Failure();
        }
        public Result<List<SalesFlow>> ListFlow(Guid ID)
        {
            var table = ConvertDataTableToList.BindList<SalesFlow>(_documentFlowService.ListFlow(ID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<SalesFlow>>.Successful(data: table);
            return Result<List<SalesFlow>>.Failure();
        }

        #region Private
        private Result FinancialReview(SalesFlow flow)
        {
            try
            {
                var departmentsResult = _departmentService.List();
                if (!departmentsResult.Success)
                    return Result.Failure(message: departmentsResult.Message);
                var departments = departmentsResult.Data;

                var financialDepartment = departments.Where(x => x.Type == DepartmentType.واحد_انبار_و_لجستیک).FirstOrDefault();
                if (financialDepartment != null)
                {
                    var positionResult = _positionService.List(new PositionListVM() { DepartmentID = financialDepartment.ID });
                    if (!positionResult.Success)
                        return Result.Failure(message: positionResult.Message);
                    var positionBoos = positionResult.Data.Where(x => x.Type == PositionType.رییس_امور).FirstOrDefault();
                    if (positionBoos != null)
                    {
                        flow.ToPositionID = positionBoos.ID;
                        flow.ToDocState = SalesDocState.بررسی_و_ارجاع_به_واحد_انبار;
                        flow.SendType = SendDocumentType.تایید_ارسال;
                    }
                }
                if (flow.ToPositionID == Guid.Empty)
                    return Result.Failure();

                return Result.Successful();
            }
            catch (Exception e) { return Result.Failure(); }
        }
        private Result WareHouseReview(SalesFlow flow)
        {
            try
            {
                var departmentsResult = _departmentService.List();
                if (!departmentsResult.Success)
                    return Result.Failure(message: departmentsResult.Message);
                var departments = departmentsResult.Data;

                var financialDepartment = departments.Where(x => x.Type == DepartmentType.واحد_انبار_و_لجستیک).FirstOrDefault();
                if (financialDepartment != null)
                {
                    var positionResult = _positionService.List(new PositionListVM() { DepartmentID = financialDepartment.ID });
                    if (!positionResult.Success)
                        return Result.Failure(message: positionResult.Message);
                    var positionBoos = positionResult.Data.Where(x => x.Type == PositionType.رییس_امور).FirstOrDefault();
                    if (positionBoos != null)
                    {
                        flow.ToPositionID = positionBoos.ID;
                        flow.ToDocState = SalesDocState.آماده_بسته_بندی;
                        flow.SendType = SendDocumentType.تایید_ارسال;
                    }
                }
                if (flow.ToPositionID == Guid.Empty)
                    return Result.Failure();

                return Result.Successful();
            }
            catch (Exception e) { return Result.Failure(); }
        }
        private Result ReadyToPackReview(SalesFlow flow)
        {
            try
            {
                flow.ToPositionID = Guid.Empty;
                flow.ToDocState = SalesDocState.ارسال_محصول;
                flow.SendType = SendDocumentType.تایید_نهایی;

                return Result.Successful();
            }
            catch (Exception e) { return Result.Failure(); }
        }
        private DocumentFlow MapToFlow(SalesFlow flow)
        {
            var DocumentFlow = new DocumentFlow()
            {
                Comment = flow.Comment,
                DocumentID = flow.DocumentID,
                FromDocState = (DocState)flow.FromDocState,
                FromPositionID = flow.FromPositionID,
                ToDocState = (DocState)flow.ToDocState,
                ToPositionID = flow.ToPositionID,
                SendType = flow.SendType
            };
            return DocumentFlow;
        }
        #endregion
    }
}
