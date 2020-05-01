using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;

namespace FM.Portal.Infrastructure.DAL
{
    public class AttachmentDataSource : IAttachmentDataSource
    {
        public Result<int> Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteAttachment", param);
                    return Result<int>.Successful(data: result);
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public Result<Attachment> Get(Guid ID)
        {
            try
            {
                Attachment obj = new Attachment();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetAttachment", param))
                    {
                        while (dr.Read())
                        {
                            obj.Comment = SQLHelper.CheckStringNull(dr["Comment"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Type = (AttachmentType)SQLHelper.CheckByteNull(dr["Type"]);
                            obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                            obj.Data = (byte[])dr["Data"];
                        }
                    }

                }
                return Result<Attachment>.Successful(data: obj);
            }
            catch
            {
                return Result<Attachment>.Failure();
            }
        }

        public Result<Attachment> GetVideo(Guid ParentID)
        {
            try
            {
                Attachment obj = new Attachment();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ParentID", ParentID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetAttachmentVideo", param))
                    {
                        while (dr.Read())
                        {
                            obj.Comment = SQLHelper.CheckStringNull(dr["Comment"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.FileName = SQLHelper.CheckStringNull(dr["FileName"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Type = (AttachmentType)SQLHelper.CheckByteNull(dr["Type"]);
                            obj.PathType = (PathType)SQLHelper.CheckByteNull(dr["PathType"]);
                        }
                    }

                }
                return Result<Attachment>.Successful(data: obj);
            }
            catch
            {
                return Result<Attachment>.Failure();
            }
        }

        public Result<Attachment> Insert(Attachment model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public DataTable List(Guid ParentID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ParentID", ParentID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetAttachmentsByParentIDs", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Attachment> Update(Attachment model)
        {
            return Modify(false, model);
        }

        private Result<Attachment> Modify(bool isNewRecord, Attachment model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@ID", model.ID);

                    param[1] = new SqlParameter("@Type", (byte)model.Type);
                    param[2] = new SqlParameter("@ParentID", model.ParentID);
                    param[3] = new SqlParameter("@FileName", model.FileName);
                    param[4] = new SqlParameter("@Comment", model.Comment);
                    param[5] = new SqlParameter("@isNewRecord", isNewRecord);
                    param[6] = new SqlParameter("@Data", model.Data);
                    param[7] = new SqlParameter("@PathType", (byte)model.PathType);

                    SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyAttachment", param);
                    return Get(model.ID);
                }
            }
            catch (Exception e) { return Result<Attachment>.Failure(); }

        }
    }
}
