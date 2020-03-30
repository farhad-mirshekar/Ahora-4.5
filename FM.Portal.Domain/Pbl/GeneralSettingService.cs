using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class GeneralSettingService : IGeneralSettingService
    {
        private readonly IGeneralSettingDataSource _dataSource;
        
        public GeneralSettingService(IGeneralSettingDataSource dataSource
                                    )
        {
            _dataSource = dataSource;
           
        }

        public Result Edit(SettingVM model)
        { 
            var result =_dataSource.Update(model);
            //if (result.Success)
            //{
            //    _cacheService.RemoveSiteSettings();
            //    _cacheService.GetSiteSettings();
            //}
            return result;
        }

        public Result<SettingVM> GetSetting()
        {
            var result = ConvertDataTableToList.BindList<GeneralSetting>(_dataSource.List());
            if (result.Count > 0 || result.Count == 0)
            {
                var setting = new SettingVM();
                setting.SiteUrl = result.FirstOrDefault(x => x.Name.Equals("SiteUrl")).Value;
                setting.SiteName = result.FirstOrDefault(x => x.Name.Equals("SiteName")).Value;
                setting.SiteKeyword = result.FirstOrDefault(x => x.Name.Equals("SiteKeyword")).Value;
                setting.SiteDescription = result.FirstOrDefault(x => x.Name.Equals("SiteDescription")).Value;

                setting.Address = result.FirstOrDefault(x => x.Name.Equals("Address")).Value;
                setting.CountShowArticle = result.FirstOrDefault(x => x.Name.Equals("CountShowArticle")).Value;
                setting.CountShowNews = result.FirstOrDefault(x => x.Name.Equals("CountShowNews")).Value;
                setting.CountShowProduct = result.FirstOrDefault(x => x.Name.Equals("CountShowProduct")).Value;
                setting.CountShowEvents = result.FirstOrDefault(x => x.Name.Equals("CountShowEvents")).Value;

                setting.CountShowSlider = result.FirstOrDefault(x => x.Name.Equals("CountShowSlider")).Value;
                setting.FacebookUrl = result.FirstOrDefault(x => x.Name.Equals("FacebookUrl")).Value;
                setting.Fax = result.FirstOrDefault(x => x.Name.Equals("Fax")).Value;
                setting.InstagramUrl = result.FirstOrDefault(x => x.Name.Equals("InstagramUrl")).Value;

                setting.Mobile = result.FirstOrDefault(x => x.Name.Equals("Mobile")).Value;
                setting.Phone = result.FirstOrDefault(x => x.Name.Equals("Phone")).Value;
                setting.SiteMetaTag = result.FirstOrDefault(x => x.Name.Equals("SiteMetaTag")).Value;
                setting.TelegramUrl = result.FirstOrDefault(x => x.Name.Equals("TelegramUrl")).Value;

                setting.TwitterUrl = result.FirstOrDefault(x => x.Name.Equals("TwitterUrl")).Value;
                setting.WhatsAppUrl = result.FirstOrDefault(x => x.Name.Equals("WhatsAppUrl")).Value;
                return Result<SettingVM>.Successful(data: setting);
            }
            return Result<SettingVM>.Failure();
        }
    }
}
