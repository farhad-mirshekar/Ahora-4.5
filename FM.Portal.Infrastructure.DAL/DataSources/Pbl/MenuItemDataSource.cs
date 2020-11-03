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
   public class MenuItemDataSource: IMenuItemDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public MenuItemDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        private Result<MenuItem> Modify(bool IsNewRecord, MenuItem model)
        {
            try
            {
                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    var param = new SqlParameter[12];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@Enabled", (byte)model.Enabled);
                    param[2] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[3] = new SqlParameter("@Name", model.Name);
                    param[4] = new SqlParameter("@ParentID", model.ParentID);
                    param[5] = new SqlParameter("@Node", model.Node);
                    param[6] = new SqlParameter("@Url", model.Url);
                    param[7] = new SqlParameter("@IconText", model.IconText);
                    param[8] = new SqlParameter("@Priority", model.Priority);
                    param[9] = new SqlParameter("@Parameters", model.Parameters);
                    param[10] = new SqlParameter("@ForeignLink", (byte)model.ForeignLink);
                    param[11] = new SqlParameter("@MenuID", model.MenuID);

                    var result = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyMenuItem", param);
                    if (result > 0)
                        return Get(model.ID);
                    return Result<MenuItem>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<MenuItem> Insert(MenuItem model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<MenuItem> Get(Guid ID)
        {
            try
            {
                var obj = new MenuItem();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetMenuItem", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.Enabled = (EnableMenuType)SQLHelper.CheckByteNull(dr["Enabled"]);
                            obj.Node = SQLHelper.CheckStringNull(dr["Node"]);
                            obj.ParentNode = SQLHelper.CheckStringNull(dr["ParentNode"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.Url = SQLHelper.CheckStringNull(dr["Url"]);
                            obj.IconText = SQLHelper.CheckStringNull(dr["IconText"]);
                            obj.Priority = SQLHelper.CheckIntNull(dr["Priority"]);
                            obj.Parameters = SQLHelper.CheckStringNull(dr["Parameters"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.ForeignLink = (EnableMenuType)SQLHelper.CheckByteNull(dr["ForeignLink"]);
                            obj.MenuID = SQLHelper.CheckGuidNull(dr["MenuID"]);
                        }
                    }

                }
                return Result<MenuItem>.Successful(data: obj);
            }
            catch
            {
                return Result<MenuItem>.Failure();
            }
        }

        public DataTable List(MenuItemListVM listVM)
        {
            try
            {
                var param = new SqlParameter[3];
                param[0] = new SqlParameter("@MenuID", listVM.MenuID);
                param[1] = new SqlParameter("@ParentNode", listVM.ParentNode);
                param[2] = new SqlParameter("@LanguageID", listVM.LanguageID);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsMenuItem", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<MenuItem> Update(MenuItem model)
        {
            return Modify(false, model);
        }
        public Result Delete(Guid ID)
        {
            try
            {
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (var con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteMenuItem", param);
                    if (i > 0)
                        return Result.Successful();
                    else
                        return Result.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
    }
}
