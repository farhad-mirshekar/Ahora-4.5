﻿using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IPagesService : IService
    {
        Result<Pages> Add(Pages model);
        Result<Pages> Edit(Pages model);
        Result<Pages> Get(Guid ID);
        Result<List<Pages>> List();
        Result Delete(Guid ID);
    }
}
