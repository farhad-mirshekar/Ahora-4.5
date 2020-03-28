using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;
using System.Collections.Generic;
namespace FM.Portal.Core.Service
{
   public interface IGeneralSettingService:IService
    {
        Result<SettingVM> GetSetting();
        Result.Result Edit(SettingVM model);
    }
}
