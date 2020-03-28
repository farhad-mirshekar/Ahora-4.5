using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ISliderDataSource : IDataSource
    {
        Result<Slider> Insert(Slider model);
        Result<Slider> Update(Slider model);
        DataTable List();
        DataTable List(int count);
        Result<Slider> Get(Guid ID);
        Result<int> Delete(Guid ID);
    }
}
