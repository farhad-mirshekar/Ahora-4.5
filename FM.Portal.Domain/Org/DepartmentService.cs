using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentDataSource _dataSource;
        public DepartmentService(IDepartmentDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Result<Department> Add(Department model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Department>.Failure(message: validate.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Department> Edit(Department model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Department>.Failure(message: validate.Message);
            return _dataSource.Update(model);
        }

        public Result<Department> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Department>> List()
        {
            var table = ConvertDataTableToList.BindList<Department>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Department>>.Successful(data: table);
            return Result<List<Department>>.Failure();
        }

        public Result<List<Department>> ListByNode(string Node)
        {
            var table = ConvertDataTableToList.BindList<Department>(_dataSource.ListByNode(Node));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Department>>.Successful(data: table);
            return Result<List<Department>>.Failure();
        }

        private Result ValidationModel(Department model)
        {
            List<string> Errors = new List<string>();
            if (model.Name == null || model.Name == "" || model.Name == string.Empty)
                Errors.Add("نام را وارد نمایید. ");
            if (model.Address == null || model.Address == "" || model.Address == string.Empty)
                Errors.Add("آدرس را وارد نمایید. ");
            if (model.CodePhone == null || model.CodePhone == "" || model.CodePhone == string.Empty)
                Errors.Add("کد تلفن را وارد نمایید. ");
            if (model.Phone == null || model.Phone == "" || model.Phone == string.Empty)
                Errors.Add("شماره تلفن را وارد نمایید. ");
            if (model.PostalCode == null || model.PostalCode == "" || model.PostalCode == string.Empty)
                Errors.Add("کد پستی را وارد نمایید. ");


            if (Errors.Any())
                return Result.Failure(message: string.Join("&&", Errors));

            return Result.Successful();
        }
    }
}
