﻿using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IBannerService:IService
    {
        Result<Banner> Add(Banner model);
        Result<Banner> Edit(Banner model);
        Result<Banner> Get(Guid ID);
        Result<List<Banner>> List(BannerListVM listVM);
        Result.Result Delete(Guid ID);
    }
}
