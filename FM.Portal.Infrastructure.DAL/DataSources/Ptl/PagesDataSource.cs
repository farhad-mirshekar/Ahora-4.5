using FM.Portal.Core.Common;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource.Ptl;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using FM.Portal.Core.Extention.ReadingTime;

namespace FM.Portal.Infrastructure.DAL.Ptl
{
    public class PagesDataSource : IPagesDataSource
    {
        private readonly IRequestInfo _requestInfo;
        private readonly DataSource.IUrlRecordDataSource _urlRecordDataSource;
        public PagesDataSource(IRequestInfo requestInfo
                              , DataSource.IUrlRecordDataSource urlRecordDataSource)
        {
            _requestInfo = requestInfo;
            _urlRecordDataSource = urlRecordDataSource;
        }

        private Result<Pages> Modify(bool IsNewRecord, Pages model)
        {
            try
            {
                var commands = new List<SqlCommand>();
                var param = new SqlParameter[7];
                param[0] = new SqlParameter("@ID", model.ID);

                param[1] = new SqlParameter("@Name", model.Name);
                param[2] = new SqlParameter("@PageType", (byte)model.PageType);
                param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                param[4] = new SqlParameter("@UserId", _requestInfo.UserId);
                param[5] = new SqlParameter("@IsNewRecord", IsNewRecord);
                param[6] = new SqlParameter("@Enabled", model.Enabled);
                commands.Add(SQLHelper.CreateCommand("ptl.spModifyPages", CommandType.StoredProcedure, param));

                var urlRecordResult = _urlRecordDataSource.Get(null, model.ID);
                if (urlRecordResult.Success)
                {
                    var urlRecord = urlRecordResult.Data;

                    var urlRecordParam = new SqlParameter[6];
                    if (urlRecord == null)
                        urlRecordParam[0] = new SqlParameter("@ID", Guid.NewGuid());
                    else
                        urlRecordParam[0] = new SqlParameter("@ID", urlRecord.ID);

                    urlRecordParam[1] = new SqlParameter("@EntityID",urlRecord == null ? model.ID : urlRecord.EntityID);
                    if(model.PageType == Core.Model.PageType.داینامیک)
                        urlRecordParam[2] = new SqlParameter("@EntityName", "DynamicPage");
                    else
                        urlRecordParam[2] = new SqlParameter("@EntityName", "StaticPage");
                    urlRecordParam[3] = new SqlParameter("@UrlDesc", CalculateWordsCount.CleanSeoUrl(model.UrlDesc));
                    urlRecordParam[4] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    urlRecordParam[5] = new SqlParameter("@IsNewRecord", IsNewRecord);

                    commands.Add(SQLHelper.CreateCommand("pbl.spModifyUrlRecord", CommandType.StoredProcedure, urlRecordParam));

                    if (SQLHelper.BatchExcute(commands.ToArray()))
                        return Get(model.ID);
                }
                return Result<Pages>.Failure(message: "خطا در درج / ویرایش");
            }
            catch (Exception e) { throw; }
        }
        public Result<Pages> Insert(Pages model)
          => Modify(true, model);

        public Result<Pages> Get(Guid ID)
        {
            try
            {
                var obj = new Pages();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetPage", param))
                    {
                        while (dr.Read())
                        {
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.PageType = (Core.Model.PageType)SQLHelper.CheckByteNull(dr["PageType"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.Enabled = (Core.Model.EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                        }
                    }
                }
                if (obj.ID == Guid.Empty)
                    return Result<Pages>.Failure();

                return Result<Pages>.Successful(data: obj);
            }
            catch
            {
                return Result<Pages>.Failure();
            }
        }

        public DataTable List(PagesListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@PageType", SqlDbType.TinyInt);
                param[0].Value = listVM.PageType != Core.Model.PageType.نامشخص ? listVM.PageType : (object)DBNull.Value;
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsPage", param);
            }
            catch (Exception e) { throw; }
        }
        public Result<Pages> Update(Pages model)
            => Modify(false, model);

        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeletePages", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }

            }
            catch (Exception e) { throw; }
        }
    }
}
