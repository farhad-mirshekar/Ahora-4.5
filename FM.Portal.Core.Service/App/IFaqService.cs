using FM.Portal.Core.Model;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IFaqService : IService
    {
        Result<FAQ> Add(FAQ model);
        Result<FAQ> Edit(FAQ model);
        Result<List<FAQ>> List(FaqListVM listVM);
    }
}
