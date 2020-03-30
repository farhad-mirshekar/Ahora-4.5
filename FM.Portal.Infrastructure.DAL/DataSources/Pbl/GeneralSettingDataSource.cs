using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class GeneralSettingDataSource : IGeneralSettingDataSource
    {
        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsGeneralSetting", null);
            }
            catch (Exception e) { throw; }
        }

        public Result Update(SettingVM model)
        {
            try
            {
                var commands = PreUpdate(model);
                SQLHelper.BatchExcute(commands.ToArray());
                return Result.Successful();

            }
            catch (Exception e) { throw; }
        }

        private List<SqlCommand> PreUpdate(SettingVM model)
        {
            var commands = new List<SqlCommand>();
            var setting = new SettingVM();
            var result = ConvertDataTableToList.BindList<GeneralSetting>(List());
            if (result.Count > 0 || result.Count == 0)
            {
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
            }


            if (setting.Address != model.Address)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "Address");
                param[1] = new SqlParameter("@Value", model.Address);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.CountShowArticle != model.CountShowArticle)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "CountShowArticle");
                param[1] = new SqlParameter("@Value", model.CountShowArticle);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.CountShowNews != model.CountShowNews)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "CountShowNews");
                param[1] = new SqlParameter("@Value", model.CountShowNews);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.CountShowProduct != model.CountShowProduct)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "CountShowProduct");
                param[1] = new SqlParameter("@Value", model.CountShowProduct);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.CountShowSlider != model.CountShowSlider)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "CountShowSlider");
                param[1] = new SqlParameter("@Value", model.CountShowSlider);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.FacebookUrl != model.FacebookUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "FacebookUrl");
                param[1] = new SqlParameter("@Value", model.FacebookUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.Fax != model.Fax)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "Fax");
                param[1] = new SqlParameter("@Value", model.Fax);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.InstagramUrl != model.InstagramUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "InstagramUrl");
                param[1] = new SqlParameter("@Value", model.InstagramUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.Mobile != model.Mobile)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "Mobile");
                param[1] = new SqlParameter("@Value", model.Mobile);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.Phone != model.Phone)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "Phone");
                param[1] = new SqlParameter("@Value", model.Phone);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.SiteDescription != model.SiteDescription)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "SiteDescription");
                param[1] = new SqlParameter("@Value", model.SiteDescription);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.SiteKeyword != model.SiteKeyword)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "SiteKeyword");
                param[1] = new SqlParameter("@Value", model.SiteKeyword);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.SiteMetaTag != model.SiteMetaTag)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "SiteMetaTag");
                param[1] = new SqlParameter("@Value", model.SiteMetaTag);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.SiteName != model.SiteName)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "SiteName");
                param[1] = new SqlParameter("@Value", model.SiteName);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.SiteUrl != model.SiteUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "SiteUrl");
                param[1] = new SqlParameter("@Value", model.SiteUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.TelegramUrl != model.TelegramUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "TelegramUrl");
                param[1] = new SqlParameter("@Value", model.TelegramUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.TwitterUrl != model.TwitterUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "TwitterUrl");
                param[1] = new SqlParameter("@Value", model.TwitterUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.WhatsAppUrl != model.WhatsAppUrl)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "WhatsAppUrl");
                param[1] = new SqlParameter("@Value", model.WhatsAppUrl);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            if (setting.CountShowEvents != model.CountShowEvents)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Name", "CountShowEvents");
                param[1] = new SqlParameter("@Value", model.CountShowEvents);
                commands.Add(SQLHelper.CreateCommand("pbl.spModifyGeneralSetting", CommandType.StoredProcedure, param));
            }
            return commands;
        }
    }
}
