using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class CommentMapUserDataSource : ICommentMapUserDataSource
    {
        public Result<CommentMapUser> Insert(CommentMapUser model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@CommentID", model.CommentID);
                    param[1] = new SqlParameter("@UserID", model.UserID);


                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "app.spModifyCommentMapUser", param);
                    return Get(model);
                }
            }
            catch
            {
                return Result<CommentMapUser>.Failure();
            }
        }

        public Result<bool> IsUserLike(CommentMapUser model)
        {
            var comment = Get(model);
            if (!comment.Success)
                return Result<bool>.Failure(data: false);
            if(comment.Data.CommentID == Guid.Empty)
                return Result<bool>.Successful(data: true);
            else
                return Result<bool>.Successful(data: false);
        }

        private Result<CommentMapUser> Get(CommentMapUser model)
        {
            try
            {
                CommentMapUser obj = new CommentMapUser();
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@CommentID", model.CommentID);
                param[1] = new SqlParameter("@UserID", model.UserID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetCommentMapUser", param))
                    {
                        while (dr.Read())
                        {
                            obj.CommentID = SQLHelper.CheckGuidNull(dr["CommentID"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.UserID = SQLHelper.CheckGuidNull(dr["UserID"]);
                        }
                    }

                }
                return Result<CommentMapUser>.Successful(data: obj);
            }
            catch (Exception e) { return Result<CommentMapUser>.Failure(); }

        }

    }
}
