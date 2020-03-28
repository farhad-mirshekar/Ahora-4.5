using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface ISliderService : IService
    {
        Result<Slider> Add(Slider model);
        Result<Slider> Edit(Slider model);
        Result<List<Slider>> List();
        Result<List<Slider>> List(int count);
        Result<Slider> Get(Guid ID);
        Result<int> Delete(Guid ID);
    }
}
