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
    public class EventsCommentDataSource : IEventsCommentDataSource
    {
        readonly IRequestInfo _requestInfo;
        public EventsCommentDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        #region Events Comment
        public Result<EventsComment> Insert(EventsComment model)
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
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDeleteEventsComment", param);
                    return Result.Successful();
                }

            }
            catch (Exception e) { return Result.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<EventsComment> Update(EventsComment model)
        => Modify(false, model);

        public Result<EventsComment> Get(Guid ID)
        {
            try
            {
                var obj = new EventsComment();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "ptl.spGetEventsComment", param))
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
                            obj.EventsID = SQLHelper.CheckGuidNull(dr["EventsID"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                        }
                    }
                }
                return Result<EventsComment>.Successful(data: obj);
            }
            catch (Exception e) { throw; }
        }

        public DataTable List(EventsCommentListVM listVM)
        {
            try
            {
                var param = new SqlParameter[5];
                param[0] = new SqlParameter("@EventsID", listVM.EventsID);
                param[1] = new SqlParameter("@ParentID", listVM.ParentID);
                param[2] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[3] = new SqlParameter("@PageSize", listVM.PageSize);
                param[4] = new SqlParameter("@CommentType", (byte)listVM.CommentType);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "ptl.spGetsEventsComment", param);

            }
            catch (Exception e) { throw; }
        }

        public Result<EventsComment> Like(Guid ID, Guid UserID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@UserID",UserID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spLikeEventsComment", param);
                    if(result < 0)
                        return Result<EventsComment>.Failure();

                    return Get(ID);
                }

            }
            catch (Exception e) { throw; }
        }

        public Result<EventsComment> DisLike(Guid ID, Guid UserID)
        {
            try
            {
                var param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@UserID", UserID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spDisLikeEventsComment", param);
                    if (result < 0)
                        return Result<EventsComment>.Failure();

                    return Get(ID);
                }

            }
            catch (Exception e) { throw; }
        }
        #endregion

        #region Events Comment Map User
        public Result UserCanLike(EventsCommentMapUser commentMapUser)
        {
            try
            {
                var obj = new Comment();
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[3];
                    param[0] = new SqlParameter("@CommentID", commentMapUser.CommentID);
                    param[1] = new SqlParameter("@UserID", commentMapUser.UserID);
                    param[2] = new SqlParameter("@Count", SqlDbType.Int);
                    param[2].Direction = ParameterDirection.ReturnValue;

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spUserCanLikeEventsComment", param);
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
        private Result<EventsComment> Modify(bool IsNewRecord, EventsComment model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[7];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Body", model.Body);
                    param[2] = new SqlParameter("@UserID", model.UserID);
                    param[3] = new SqlParameter("@EventsID", model.EventsID);
                    param[4] = new SqlParameter("@CommentType", (byte)model.CommentType);
                    param[5] = new SqlParameter("@ParentID", model.ParentID);
                    param[6] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "ptl.spModifyEventsComment", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<EventsComment>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
