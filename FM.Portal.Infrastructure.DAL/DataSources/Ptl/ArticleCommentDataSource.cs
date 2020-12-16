using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class ArticleCommentDataSource : IArticleCommentDataSource
    {
        readonly IRequestInfo _requestInfo;
        public ArticleCommentDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        #region Article Comment
        public Result<ArticleComment> Insert(ArticleComment model)
        => Modify(true, model);

        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@CommentID", ID);
                param[1] = new SqlParameter("@RemoverID", _requestInfo.UserId);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteArticleComment", param);
                    if(result > 0)
                        return Result.Successful();
                    
                    return Result.Failure();
                }

            }
            catch (Exception e) { return Result.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<ArticleComment> Update(ArticleComment model)
        => Modify(false, model);

        public Result<ArticleComment> Get(Guid ID)
        {
            try
            {
                var obj = new ArticleComment();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetArticleComment", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                            obj.Body = SQLHelper.CheckStringNull(dr["Body"]);
                            obj.DisLikeCount = SQLHelper.CheckIntNull(dr["DisLikeCount"]);
                            obj.LikeCount = SQLHelper.CheckIntNull(dr["LikeCount"]);
                            obj.CommentType = (CommentType)SQLHelper.CheckByteNull(dr["CommentType"]);
                            obj.ArticleID = SQLHelper.CheckGuidNull(dr["ArticleID"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.ArticleTitle = SQLHelper.CheckStringNull(dr["ArticleTitle"]);
                        }
                    }
                }
                if(obj.ID != Guid.Empty)
                return Result<ArticleComment>.Successful(data: obj);

                return Result<ArticleComment>.Successful(data: null);
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(ArticleCommentListVM listVM)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@ArticleID", listVM.ArticleID);
                param[1] = new SqlParameter("@ParentID", listVM.ParentID);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[3] = new SqlParameter("@PageSize", listVM.PageSize);
                param[4] = new SqlParameter("@CommentType", (byte)listVM.CommentType);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsArticleComment", param);

            }
            catch (Exception e) { throw; }
        }

        public Result<ArticleComment> Like(Guid ID, Guid UserID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@UserID",UserID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spLikeArticleComment", param);
                    if(result < 0)
                        return Result<ArticleComment>.Failure();

                    return Get(ID);
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<ArticleComment> DisLike(Guid ID, Guid UserID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@UserID", UserID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDisLikeArticleComment", param);
                    if (result < 0)
                        return Result<ArticleComment>.Failure();

                    return Get(ID);
                }

            }
            catch (Exception e) { throw; }
        }
        #endregion

        #region Article Comment Map User
        public Result UserCanLike(ArticleCommentMapUser commentMapUser)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[3];
                    param[0] = new SqlParameter("@CommentID", commentMapUser.CommentID);
                    param[1] = new SqlParameter("@UserID", commentMapUser.UserID);
                    param[2] = new SqlParameter("@Count", SqlDbType.Int);
                    param[2].Direction = ParameterDirection.ReturnValue;

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spUserCanLikeArticleComment", param);
                    var result = param[2].Value;
                    if ((int)result <= 0)
                        return Result.Successful();

                    return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
        #endregion

        #region Private
        private Result<ArticleComment> Modify(bool IsNewRecord, ArticleComment model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Body", model.Body);
                    param[2] = new SqlParameter("@UserID", model.UserID);
                    param[3] = new SqlParameter("@ArticleID", model.ArticleID);
                    param[4] = new SqlParameter("@CommentType", (byte)model.CommentType);
                    param[5] = new SqlParameter("@ParentID", model.ParentID);
                    param[6] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyArticleComment", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<ArticleComment>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
