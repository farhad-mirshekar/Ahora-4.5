using System;
using System.Collections.Generic;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System.Linq;
using FM.Portal.Core.Common;

namespace FM.Portal.Domain
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddressDataSource _dataSource;
        public UserAddressService(IUserAddressDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<UserAddress> Add(UserAddress model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<UserAddress>.Failure(message: validate.Message);

            return _dataSource.Insert(model);
        }

        public Result<UserAddress> Edit(UserAddress model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<UserAddress>.Failure(message: validate.Message);

            return _dataSource.Update(model);
        }

        public Result<UserAddress> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<UserAddress>> List(Guid ID)
        {
            var table = ConvertDataTableToList.BindList<UserAddress>(_dataSource.List(ID));
            if (table.Count > 0)
                return Result<List<UserAddress>>.Successful(data: table);
            return Result<List<UserAddress>>.Failure();
        }

        public Result Remove(Guid ID)
        => _dataSource.Delete(ID);

        private Result ValidationModel(UserAddress model)
        {
            List<string> Errors = new List<string>();
            if (model.Address == null || model.Address == "" || model.Address == string.Empty)
                Errors.Add("آدرس را وارد نمایید");

            if (Errors.Any())
                return Result.Failure(message: string.Join("&&", Errors));

            return Result.Successful();
        }
    }
}
