using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class NewsDataSource : INewsDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public NewsDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }

        public Result<int> Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteNews", param);
                    return Result<int>.Successful(data: result);
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<News> Get(Guid ID)
        {
            try
            {
                var obj = new News();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@TrackingCode", SqlDbType.NVarChar);
                param[1].Value = DBNull.Value;

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetNews", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.CategoryID = SQLHelper.CheckGuidNull(dr["CategoryID"]);
                            obj.CommentStatus = (CommentArticleType)SQLHelper.CheckByteNull(dr["CommentStatus"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.DisLikeCount = SQLHelper.CheckIntNull(dr["DisLikeCount"]);
                            obj.IsShow = (ShowArticleType)SQLHelper.CheckByteNull(dr["IsShow"]);
                            obj.LikeCount = SQLHelper.CheckIntNull(dr["LikeCount"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.ModifiedDate = SQLHelper.CheckDateTimeNull(dr["ModifiedDate"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.ReadingTime = SQLHelper.CheckStringNull(dr["ReadingTime"]);
                        }
                    }

                }
                return Result<News>.Successful(data: obj);
            }
            catch (Exception e) { return Result<News>.Failure(); }
        }

        public Result<News> Get(string TrackingCode)
        {
            try
            {
                var obj = new News();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                param[0].Value = DBNull.Value;
                param[1] = new SqlParameter("@TrackingCode", TrackingCode);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetNews", param))
                    {
                        while (dr.Read())
                        {
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.CategoryID = SQLHelper.CheckGuidNull(dr["CategoryID"]);
                            obj.CommentStatus = (CommentArticleType)SQLHelper.CheckByteNull(dr["CommentStatus"]);
                            obj.Description = SQLHelper.CheckStringNull(dr["Description"]);
                            obj.DisLikeCount = SQLHelper.CheckIntNull(dr["DisLikeCount"]);
                            obj.IsShow = (ShowArticleType)SQLHelper.CheckByteNull(dr["IsShow"]);
                            obj.LikeCount = SQLHelper.CheckIntNull(dr["LikeCount"]);
                            obj.MetaKeywords = SQLHelper.CheckStringNull(dr["MetaKeywords"]);
                            obj.ModifiedDate = SQLHelper.CheckDateTimeNull(dr["ModifiedDate"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.TrackingCode = SQLHelper.CheckStringNull(dr["TrackingCode"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.VisitedCount = SQLHelper.CheckIntNull(dr["VisitedCount"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.CreatorName = SQLHelper.CheckStringNull(dr["CreatorName"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.ReadingTime = SQLHelper.CheckStringNull(dr["ReadingTime"]);
                        }
                    }

                }
                return Result<News>.Successful(data: obj);
            }
            catch (Exception e) { return Result<News>.Failure(); }
        }

        public Result<News> Insert(News model)
            => Modify(true, model);


        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsNews", null);
            }
            catch (Exception e) { throw; }
        }
        public DataTable List(int count)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                count = count == 0 ? 4 : count;
                param[0] = new SqlParameter("@count", count);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsNewsByCount", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<News> Update(News model)
            => Modify(false, model);

        private Result<News> Modify(bool isNewRecord, News model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    lock (con)
                    {
                        SqlParameter[] param = new SqlParameter[13];
                        param[0] = new SqlParameter("@ID", model.ID);

                        param[1] = new SqlParameter("@Body", model.Body);
                        param[2] = new SqlParameter("@CategoryID", model.CategoryID);
                        param[3] = new SqlParameter("@CommentStatus", (byte)model.CommentStatus);
                        param[4] = new SqlParameter("@Description", model.Description);
                        param[5] = new SqlParameter("@isNewRecord", isNewRecord);
                        param[6] = new SqlParameter("@IsShow", (byte)model.IsShow);
                        param[7] = new SqlParameter("@MetaKeywords", model.MetaKeywords);
                        param[8] = new SqlParameter("@Title", model.Title);
                        param[9] = new SqlParameter("@UrlDesc", model.UrlDesc);
                        param[10] = new SqlParameter("@UserID", _requestInfo.UserId);
                        param[11] = new SqlParameter("@TrackingCode", model.TrackingCode);
                        param[12] = new SqlParameter("@ReadingTime", model.ReadingTime);

                        SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyNews", param);

                        return Get(model.ID);
                    }
                }
            }
            catch (Exception e) { throw; }
        }
    }

}
