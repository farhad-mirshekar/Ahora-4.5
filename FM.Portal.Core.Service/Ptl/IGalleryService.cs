﻿using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IGalleryService:IService
    {
        Result<Gallery> Add(Gallery model);
        Result<Gallery> Edit(Gallery model);
        Result<Gallery> Get(Guid ID);
        Result<Gallery> Get(string  TrackingCode);
        Result<List<Gallery>> List(GalleryListVM listVM);
        Result Delete(Guid ID);
    }
}
