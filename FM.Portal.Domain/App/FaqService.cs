using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class FaqService : IFaqService
    {
        public FaqService(IFaqDataSource dataSource) { _dataSource = dataSource; }
        private readonly IFaqDataSource _dataSource;
        public Result<FAQ> Add(FAQ model) => _dataSource.Insert(model);

        public Result<FAQ> Edit(FAQ model) => _dataSource.Update(model);
        public Result<List<FAQ>> List(Guid FAQGroupID)
        {
          var table =  ConvertDataTableToList.BindList<FAQ>(_dataSource.List(FAQGroupID));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<FAQ>>.Successful(data: table);
            return Result<List<FAQ>>.Failure();
        }
    }
}
