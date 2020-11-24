using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FM.Portal.Infrastructure.DAL
{
    public class StaticPageDataSource : IStaticPageDataSource
    {
        private readonly IRequestInfo _requestInfo;
        private readonly DataSource.Ptl.IPagesDataSource _pagesDataSource;
        public StaticPageDataSource(IRequestInfo requestInfo
                                    , DataSource.Ptl.IPagesDataSource pagesDataSource)
        {
            _requestInfo = requestInfo;
            _pagesDataSource = pagesDataSource;
        }

        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteStaticPage", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<StaticPage> Get(Guid ID)
        {
            try
            {
                var obj = new StaticPage();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetStaticPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.AttachmentID = SQLHelper.CheckGuidNull(dr["AttachmentID"]);
                            obj.BannerShow = (EnableMenuType)SQLHelper.CheckByteNull(dr["BannerShow"]);
                        }
                    }
                }
                if (obj.ID != Guid.Empty)
                {
                    var pages = new Core.Model.Ptl.Pages();
                    var paramPage = new SqlParameter[1];
                    paramPage[0] = new SqlParameter("@ID", ID);

                    using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                    {
                        using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetPage", paramPage))
                        {
                            while (dr.Read())
                            {
                                pages.Name = SQLHelper.CheckStringNull(dr["Name"]);
                                pages.PageType = (PageType)SQLHelper.CheckByteNull(dr["PageType"]);
                                pages.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                                pages.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                                pages.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                                pages.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                                pages.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            }
                        }
                        if (pages.ID != Guid.Empty)
                            obj.Pages = pages;
                    }
                }

                if (obj.ID != Guid.Empty)
                    return Result<StaticPage>.Successful(data: obj);
                else
                    return Result<StaticPage>.Failure();
            }
            catch(Exception e)
            {
                return Result<StaticPage>.Failure();
            }
        }

        public DataTable List(StaticPageListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@Name", listVM.Name);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsStaticPage", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<StaticPage> Update(StaticPage model)
       => Modify(false, model);

        private Result<StaticPage> Modify(bool IsNewRecord, StaticPage model)
        {
            try
            {
                var commands = new List<SqlCommand>();

                #region Static Page
                var paramStaticPage = new SqlParameter[7];
                paramStaticPage[0] = new SqlParameter("@ID", model.ID);

                paramStaticPage[1] = new SqlParameter("@Body", model.Body);
                paramStaticPage[2] = new SqlParameter("@Description", model.Description);
                paramStaticPage[3] = new SqlParameter("@IsNewRecord", IsNewRecord);
                paramStaticPage[4] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                paramStaticPage[5] = new SqlParameter("@AttachmentID", model.AttachmentID);
                paramStaticPage[6] = new SqlParameter("@BannerShow", model.BannerShow);

                commands.Add(SQLHelper.CreateCommand("ptl.spModifyStaticPage", CommandType.StoredProcedure, paramStaticPage));
                #endregion

                #region Page
                var paramsPage = new SqlParameter[7];
                paramsPage[0] = new SqlParameter("@ID", model.ID);

                paramsPage[1] = new SqlParameter("@Name", model.Pages.Name);
                paramsPage[2] = new SqlParameter("@UrlDesc", model.Pages.UrlDesc);
                paramsPage[3] = new SqlParameter("@UserId", _requestInfo.UserId);
                paramsPage[4] = new SqlParameter("@Enabled", (byte)model.Pages.Enabled);
                paramsPage[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                paramsPage[6] = new SqlParameter("@PageType", (byte)model.Pages.PageType);

                commands.Add(SQLHelper.CreateCommand("ptl.spModifyPages", CommandType.StoredProcedure, paramsPage));
                #endregion

                if (SQLHelper.BatchExcute(commands.ToArray()))
                    return Get(model.ID);

                return Result<StaticPage>.Failure(message: "خطا در درج / ویرایش");
            }
            catch (Exception e) { return Result<StaticPage>.Failure(message: "خطا در درج / ویرایش"); }
        }
    }
}
