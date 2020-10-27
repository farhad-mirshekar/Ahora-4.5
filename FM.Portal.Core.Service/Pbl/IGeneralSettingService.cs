using FM.Portal.Core.Model;
using System;
using System.Collections.Generic;
namespace FM.Portal.Core.Service
{
   public interface IGeneralSettingService:IService
    {
        Result<SettingVM> GetSetting();
        Result Edit(SettingVM model);
    }
}
