﻿using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service.Ptl
{
   public interface IPagesService:IService
    {
        Result<Pages> Add(Pages model);
        Result<Pages> Edit(Pages model);
        Result<Pages> Get(Guid ID);
        Result<Pages> Get(string TrackingCode);
        Result<List<Pages>> List(PagesListVM listVM);
        Result.Result Delete(Guid ID);
    }
}