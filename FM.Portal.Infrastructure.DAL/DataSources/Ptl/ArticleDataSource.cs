using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;

namespace FM.Portal.Infrastructure.DAL
{
    public class ArticleDataSource : IArticleDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public ArticleDataSource(IRequestInfo requestInfo)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteArticle", param);
                    return Result<int>.Successful(data: result);
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<Article> Get(Guid ID)
        {
            try
            {
                Article obj = new Article();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetArticle", param))
                    {
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
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.ReadingTime = SQLHelper.CheckStringNull(dr["ReadingTime"]);
                            obj.LanguageID = SQLHelper.CheckGuidNull(dr["LanguageID"]);
                        }
                    }

                }
                return Result<Article>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Article>.Failure(); }
        }

        public Result<Article> Insert(Article model)
            => Modify(true, model);


        public DataTable List(ArticleListVM listVM)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@Title", listVM?.Title);
                param[1] = new SqlParameter("@PageSize", listVM.PageSize);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[3] = new SqlParameter("@LanguageID", listVM.LanguageID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsArticle", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Article> Update(Article model)
            => Modify(false, model);

        private Result<Article> Modify(bool isNewRecord, Article model)
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

                       int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyArticle", param);
                        if (result > 0)
                            return Get(model.ID);
                        else
                            return Result<Article>.Failure(message: "خطا در درج / ویرایش");
                    }
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
