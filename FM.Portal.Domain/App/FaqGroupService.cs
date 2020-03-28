using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;


namespace FM.Portal.Domain
{
    public class FaqGroupService: IFaqGroupService
    {
        private readonly IFaqGroupDataSource _dataSource;
        public FaqGroupService(IFaqGroupDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<List<FAQGroup>> List()
        {
            var table= ConvertDataTableToList.BindList<FAQGroup>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<FAQGroup>>.Successful(data: table);
            return Result<List<FAQGroup>>.Failure();
        }
        public Result<FAQGroup> Add(FAQGroup model) => _dataSource.Insert(model);
        public Result<FAQGroup> Edit(FAQGroup model) => _dataSource.Update(model);
        public Result<FAQGroup> Get(Guid ID) => _dataSource.Get(ID);

        public Result<List<FaqGroupListVM>> ListForWeb()
        {
            var table = ConvertDataTableToList.BindList<FaqGroupListVM>(_dataSource.ListForWeb());
            if (table.Count > 0)
                return Result<List<FaqGroupListVM>>.Successful(data: table);
            return Result<List<FaqGroupListVM>>.Failure();

        }
    }
}
