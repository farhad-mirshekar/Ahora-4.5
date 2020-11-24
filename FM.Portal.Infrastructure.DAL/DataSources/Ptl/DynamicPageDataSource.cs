using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using FM.Portal.Core.Extention.ReadingTime;

namespace FM.Portal.Infrastructure.DAL
{
    public class DynamicPageDataSource : IDynamicPageDataSource
    {
        private readonly IRequestInfo _requestInfo;
        private readonly IUrlRecordDataSource _urlRecordDataSource;
        public DynamicPageDataSource(IRequestInfo requestInfo
                                    , IUrlRecordDataSource urlRecordDataSource)
        {
            _requestInfo = requestInfo;
            _urlRecordDataSource = urlRecordDataSource;
        }
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteDynamicPage", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<DynamicPage> Get(Guid ID)
        {
            try
            {
                var obj = new DynamicPage();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetDynamicPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.IsShow = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["IsShow"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.PageID = SQLHelper.CheckGuidNull(dr["PageID"]);
                            obj.PageName = SQLHelper.CheckStringNull(dr["PageName"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                        }
                    }
                }
                if (obj.ID != Guid.Empty)
                    return Result<DynamicPage>.Successful(data: obj);

                return Result<DynamicPage>.Failure();

            }
            catch
            {
                return Result<DynamicPage>.Failure();
            }
        }

        public Result<DynamicPage> Insert(DynamicPage model)
        => Modify(true, model);

        public DataTable List(DynamicPageListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@PageID", listVM.PageID);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsDynamicPage", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<DynamicPage> Update(DynamicPage model)
       => Modify(false, model);
        private Result<DynamicPage> Modify(bool IsNewRecord, DynamicPage model)
        {
            try
            {
                var commands = new List<SqlCommand>();
                var param = new SqlParameter[10];
                param[0] = new SqlParameter("@ID", model.ID);

                param[1] = new SqlParameter("@Body", model.Body);
                param[2] = new SqlParameter("@Description", model.Description);
                param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                param[4] = new SqlParameter("@UserId", _requestInfo.UserId);
                param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                param[6] = new SqlParameter("@IsShow", (byte)model.IsShow);
                param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                param[8] = new SqlParameter("@PageID", model.PageID);
                param[9] = new SqlParameter("@Name", model.Name);
                commands.Add(SQLHelper.CreateCommand("ptl.spModifyDynamicPage", CommandType.StoredProcedure, param));

                var urlRecordResult = _urlRecordDataSource.Get(null, model.ID);
                if (urlRecordResult.Success)
                {
                    var urlRecord = urlRecordResult.Data;

                    var urlRecordParam = new SqlParameter[6];
                    if (urlRecord == null)
                        urlRecordParam[0] = new SqlParameter("@ID", Guid.NewGuid());
                    else
                        urlRecordParam[0] = new SqlParameter("@ID", urlRecord.ID);

                    urlRecordParam[1] = new SqlParameter("@EntityID", urlRecord == null ? model.ID : urlRecord.EntityID);
                    urlRecordParam[2] = new SqlParameter("@EntityName", "DynamicPageDetail");
                    urlRecordParam[3] = new SqlParameter("@UrlDesc", CalculateWordsCount.CleanSeoUrl(model.UrlDesc));
                    urlRecordParam[4] = new SqlParameter("@Enabled", (byte)model.IsShow);
                    urlRecordParam[5] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    commands.Add(SQLHelper.CreateCommand("pbl.spModifyUrlRecord", CommandType.StoredProcedure, urlRecordParam));

                    if (SQLHelper.BatchExcute(commands.ToArray()))
                        return Get(model.ID);
                }
                return Result<DynamicPage>.Failure(message: "خطا در درج / ویرایش");

            }
            catch (Exception e) { throw; }
        }
    }
}
