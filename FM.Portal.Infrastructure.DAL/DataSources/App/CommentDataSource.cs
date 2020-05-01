using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
using System.Data;
using System.Collections.Generic;

namespace FM.Portal.Infrastructure.DAL
{
    public class CommentDataSource : ICommentDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public CommentDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;

        }
        private Result<Comment> Modify(bool IsNewRecord, Comment model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Body", model.Body);
                    param[2] = new SqlParameter("@UserID", model.UserID);
                    param[3] = new SqlParameter("@DocumentID", model.DocumentID);
                    param[4] = new SqlParameter("@CommentType", (CommentType)model.CommentType);
                    param[5] = new SqlParameter("@ParentID", model.ParentID);
                    param[6] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[7] = new SqlParameter("@CommentForType", (byte)model.CommentForType);
                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, param, "app.spModifyComment");
                    return Get(model.ID);
                }
            }
            catch (Exception) { throw; }
        }
        public Result<Comment> Get(Guid ID)
        {
            try
            {
                Comment obj = new Comment();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetComment", param))
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
                            obj.DocumentID = SQLHelper.CheckGuidNull(dr["DocumentID"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.CommentForType = (CommentForType)SQLHelper.CheckByteNull(dr["CommentForType"]);
                        }
                    }
                    if (obj.ID != Guid.Empty)
                        return Result<Comment>.Successful(data: obj);
                    else
                        return Result<Comment>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }

        public Result<Comment> Insert(Comment model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<Comment> Update(Comment model)
        {
            return Modify(false, model);
        }

        public DataTable List(CommentListVM listVM)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@DocumentID", listVM.DocumentID);
                param[1] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[2] = new SqlParameter("@PageSize", listVM.PageSize);
                
               return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetComments", param);
                
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(Guid ParentID,Guid DocumentID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ParentID", ParentID);
                param[1] = new SqlParameter("@DocumentID", DocumentID);
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetCommentsByParentID", param);

            }
            catch (Exception e) { throw; }
        }

        public Result<int> Like(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var count = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spLikeComment", param);
                    return Result<int>.Successful(data: count);
                }
            }
            catch { throw; }
        }

        public Result<int> DisLike(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var count = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spDisLikeComment", param);
                    return Result<int>.Successful(data: count);
                }
            }
            catch { throw; }
        }

        public DataTable List(CommentForType commentForType)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                switch (commentForType)
                {
                    case CommentForType.اخبار:
                        {
                            param[0] = new SqlParameter("@commentForType", commentForType);
                            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetCommentsForAdmin", param);
                        }
                    case CommentForType.رویدادها:
                        {
                            param[0] = new SqlParameter("@commentForType", commentForType);
                            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetCommentsForAdmin", param);
                        }
                    case CommentForType.محصولات:
                        {
                            param[0] = new SqlParameter("@commentForType", commentForType);
                            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetCommentsForAdmin", param);
                        }
                    case CommentForType.مقالات:
                        {
                            param[0] = new SqlParameter("@commentForType", commentForType);
                            return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetCommentsForAdmin", param);
                        }
                }
                return null;

            }
            catch (Exception e) { throw; }
        }

        public Result<Comment> CanUserComment(Guid DocumentID, Guid UserID)
        {
            try
            {
                var obj = new Comment();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@DocumentID", DocumentID);
                param[1] = new SqlParameter("@UserID", UserID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spCanUserComment", param))
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
                            obj.DocumentID = SQLHelper.CheckGuidNull(dr["DocumentID"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.CommentForType = (CommentForType)SQLHelper.CheckByteNull(dr["CommentForType"]);
                        }
                    }
                    if (obj.ID != Guid.Empty)
                        return Result<Comment>.Successful(data: obj);
                    else
                        return Result<Comment>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
