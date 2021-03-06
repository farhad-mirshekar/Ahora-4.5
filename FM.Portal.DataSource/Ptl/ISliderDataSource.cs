﻿using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
   public interface ISliderDataSource : IDataSource
    {
        Result<Slider> Insert(Slider model);
        Result<Slider> Update(Slider model);
        DataTable List(SliderListVM listVM);
        Result<Slider> Get(Guid ID);
        Result Delete(Guid ID);
    }
}
