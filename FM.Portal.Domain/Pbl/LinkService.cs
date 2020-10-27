using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class LinkService : ILinkService
    {
        private readonly ILinkDataSource _dataSource;
        public LinkService(ILinkDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Link> Add(Link model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Link>.Failure(message: validateResult.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Link> Edit(Link model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Link>.Failure(message: validateResult.Message);
            return _dataSource.Update(model);
        }

        public Result<Link> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Link>> List(LinkListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Link>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Link>>.Successful(data: table);
            return Result<List<Link>>.Failure();
        }
        private Result ValidateModel(Link model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.Url))
                errors.Add("آدرس را وارد نمایید");
            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات را وارد نمایید");
            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));
            return Result.Successful();
        }
    }
}
