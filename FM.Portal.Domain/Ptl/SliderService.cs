using System;
using System.Collections.Generic;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class SliderService : ISliderService
    {
        private readonly ISliderDataSource _dataSource;
        public SliderService(ISliderDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Slider> Add(Slider model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Slider>.Failure(message: validate.Message);

            return _dataSource.Insert(model);
        }

        public Result<int> Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<Slider> Edit(Slider model)
        {
            var validate = ValidationModel(model);
            if (!validate.Success)
                return Result<Slider>.Failure(message: validate.Message);

            return _dataSource.Update(model);
        }

        public Result<Slider> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Slider>> List()
        {
           var table= ConvertDataTableToList.BindList<Slider>(_dataSource.List());
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Slider>>.Successful(data: table);
            return Result<List<Slider>>.Failure();
        }

        public Result<List<Slider>> List(int count)
        {
            var table = ConvertDataTableToList.BindList<Slider>(_dataSource.List(count));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Slider>>.Successful(data: table);
            return Result<List<Slider>>.Failure();
        }
        private Result ValidationModel(Slider model)
        {
            List<string> Errors = new List<string>();
            if (model.Title == string.Empty || model.Title == null || model.Title == "")
                Errors.Add("عنوان اسلایدر را وارد نمایید");

            if (Errors.Any())
                return Result.Failure(message: string.Join("&&", Errors));

            return Result.Successful();
        }
    }
}
