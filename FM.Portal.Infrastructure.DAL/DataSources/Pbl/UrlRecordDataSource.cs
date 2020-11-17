using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FM.Portal.Infrastructure.DAL
{
    public class UrlRecordDataSource : IUrlRecordDataSource
    {
        public Result<UrlRecord> Get(string UrlDesc)
        => _Get(new UrlRecordVM() { UrlDesc = UrlDesc });

        public Result<UrlRecord> Get(Guid? ID, Guid? EntityID)
        => _Get(new UrlRecordVM() {ID = ID , EntityID = EntityID});

        public Result<UrlRecord> Insert(UrlRecord model)
        => Modify(true, model);

        public DataTable List(UrlRecordListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@PageSize", listVM.PageSize);
                param[1] = new SqlParameter("@PageIndex", listVM.PageIndex);
                param[2] = new SqlParameter("@UrlDesc", listVM.UrlDesc);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsUrlRecord", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<UrlRecord> Update(UrlRecord model)
        => Modify(false, model);

        private Result<UrlRecord> Modify(bool IsNewrecord, UrlRecord model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[6];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewrecord);
                    param[2] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[3] = new SqlParameter("@UrlDesc", model.UrlDesc);
                    param[4] = new SqlParameter("@EntityID", model.EntityID);
                    param[5] = new SqlParameter("@EntityName", model.EntityName);

                    int result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyUrlRecord", param);
                    if (result > 0)
                        return Get(model.UrlDesc);
                    return Result<UrlRecord>.Failure(message: "خطا در درج / ویرایش");
                }
            }
            catch (Exception e) { throw; }

        }
        private Result<UrlRecord> _Get(UrlRecordVM urlRecord)
        {
            try
            {
                var obj = new UrlRecord();
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@UrlDesc", urlRecord?.UrlDesc);
                param[1] = new SqlParameter("@ID", urlRecord.ID);
                param[2] = new SqlParameter("@EntityID", urlRecord.EntityID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (var dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetUrlRecord", param))
                    {
                        while (dr.Read())
                        {
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.UrlDesc = SQLHelper.CheckStringNull(dr["UrlDesc"]);
                            obj.EntityID = SQLHelper.CheckGuidNull(dr["EntityID"]);
                            obj.EntityName = SQLHelper.CheckStringNull(dr["EntityName"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                        }
                    }

                }
                return Result<UrlRecord>.Successful(data: obj);
            }
            catch (Exception e) { return Result<UrlRecord>.Failure(); }
        }
    }
    public class UrlRecordVM
    {
        public Guid? ID { get; set; }
        public Guid? EntityID { get; set; }
        public string UrlDesc { get; set; }
    }
}
