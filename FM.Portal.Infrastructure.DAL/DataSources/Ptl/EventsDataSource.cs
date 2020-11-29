using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource;


namespace FM.Portal.Infrastructure.DAL
{
    public class EventsDataSource : IEventsDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public EventsDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        public Result Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteEvents", param);
                    if (result > 0)
                        return Result.Successful();

                    return Result.Failure();
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<Events> Get(Guid ID)
        {
            try
            {
                var obj = new Events();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetEvents", param))
                    {
                        return ToModel(dr);
                    }
                }
            }
            catch (Exception e) { return Result<Events>.Failure(); }
        }
        public Result<Events> Insert(Events model)
            => Modify(true, model);


        public DataTable List(EventsListVM listVM)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@Title", listVM?.Title);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[3] = new SqlParameter("@LanguageID", listVM.LanguageID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsEvents", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Events> Update(Events model)
            => Modify(false, model);

        private Result<Events> Modify(bool isNewRecord, Events model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    lock (con)
                    {
                        var param = new SqlParameter[13];
                        param[0] = new SqlParameter("@ID", model.ID);

                        param[1] = new SqlParameter("@Body", model.Body);
                        param[2] = new SqlParameter("@CategoryID", model.CategoryID);
                        param[3] = new SqlParameter("@CommentStatusType", (byte)model.CommentStatusType);
                        param[4] = new SqlParameter("@Description", model.Description);
                        param[5] = new SqlParameter("@IsNewRecord", isNewRecord);
                        param[6] = new SqlParameter("@ViewStatusType", (byte)model.ViewStatusType);
                        param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                        param[8] = new SqlParameter("@Title", model.Title);
                        param[9] = new SqlParameter("@UrlDesc", model.UrlDesc);
                        param[10] = new SqlParameter("@UserID", _requestInfo.UserId);
                        param[11] = new SqlParameter("@ReadingTime", model.ReadingTime);
                        param[12] = new SqlParameter("@LanguageID", model.LanguageID);

                       var result= SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyEvents", param);
                        if(result > 0)
                            return Get(model.ID);
                        return Result<Events>.Failure(message:"خطا در درج / ویرایش");
                    }
                }
            }
            catch (Exception e) { throw; }
        }
        private Result<Events> ToModel(SqlDataReader dr)
        {
            try
            {
                var obj = new Events();

                while (dr.Read())
                {
                    obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                    obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                    obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                    obj.CategoryID = SQLHelper.CheckGuidNull(dr["CategoryID"]);
                    obj.CommentStatusType = (CommentStatusType)SQLHelper.CheckByteNull(dr["CommentStatusType"]);
                    obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                    obj.DisLikeCount = SQLHelper.CheckIntNull(dr["DisLikeCount"]);
                    obj.ViewStatusType = (ViewStatusType)SQLHelper.CheckByteNull(dr["ViewStatusType"]);
                    obj.LikeCount = SQLHelper.CheckIntNull(dr["LikeCount"]);
                    obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                    obj.ModifiedDate = SQLHelper.CheckDateTimeNull(dr["ModifiedDate"]);
                    obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                    obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                    obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                    obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                    obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                    obj.CreatorName = SQLHelper.CheckStringNull(dr["CreatorName"]);
                    obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                    obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                    obj.ReadingTime = SQLHelper.CheckStringNull(dr["ReadingTime"]);
                    obj.LanguageID = SQLHelper.CheckGuidNull(dr["LanguageID"]);
                }
                if(obj.ID != Guid.Empty)
                    return Result<Events>.Successful(data: obj);

                return Result<Events>.Successful(data: null);
            }
            catch (Exception e) { throw; }
        }
    }

}
