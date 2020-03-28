using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IFaqService : IService
    {
        Result<FAQ> Add(FAQ model);
        Result<FAQ> Edit(FAQ model);
        Result<List<FAQ>> List(Guid FAQGroupID);
    }
}
