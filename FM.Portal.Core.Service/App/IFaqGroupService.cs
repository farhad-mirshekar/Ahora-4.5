using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Service
{
   public interface IFaqGroupService : IService
    {
        Result<FAQGroup> Get(Guid ID);
        Result<List<FAQGroup>> List();
        Result<FAQGroup> Add(FAQGroup model);
        Result<FAQGroup> Edit(FAQGroup model);
        Result<List<FaqGroupListVM>> ListForWeb();
    }
}
