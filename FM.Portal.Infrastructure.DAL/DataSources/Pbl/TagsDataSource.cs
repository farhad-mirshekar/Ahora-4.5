using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using System.Data;

namespace FM.Portal.Infrastructure.DAL
{
    public class TagsDataSource : ITagsDataSource
    {
        public Result Add(List<Tags> model,Guid DocumentID)
        {
            try
            {
                Delete(DocumentID);
                var commands = new List<SqlCommand>();
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@ID", Guid.NewGuid());
                        param[1] = new SqlParameter("@IsNewRecord",true);
                        param[2] = new SqlParameter("@Name", item.Name.Trim());
                        param[3] = new SqlParameter("@DocumentID",DocumentID);
                        commands.Add(SQLHelper.CreateCommand("pbl.spModifyTags", CommandType.StoredProcedure, param));
                    }
                }
                SQLHelper.BatchExcute(commands.ToArray());
                return Result.Successful();
            }
            catch(Exception e) { return Result.Failure(); }
        }

        public Result<int> Delete(Guid DocumentID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@DocumentID", DocumentID);
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteTagsByDocumentID", param);
                    return Result<int>.Successful(data: result);
                }

            }
            catch (Exception) { return Result<int>.Failure(message: "خطایی اتفاق افتاده است"); }
        }

        public DataTable List(Guid DocumentID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@DocumentID", DocumentID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetTagsByDocumentID", param);
            }
            catch (Exception e) { throw; }
        }

        public DataTable SearchByName(string Name)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Name", Name);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsTagsByName", param);
            }
            catch (Exception e) { throw; }
        }
    }
}
