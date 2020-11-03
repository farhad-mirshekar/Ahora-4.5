using System;
using System.Data;
using System.Data.SqlClient;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using FM.Portal.DataSource;

namespace FM.Portal.Infrastructure.DAL
{
    public class MenuDataSource : IMenuDataSource
    {
        private readonly IRequestInfo _requestInfo;
        public MenuDataSource(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        private Result<Menu> Modify(bool IsNewRecord, Menu model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@ID", model.ID);
                    param[1] = new SqlParameter("@IsNewRecord", IsNewRecord);
                    param[2] = new SqlParameter("@Name", model.Name);
                    param[3] = new SqlParameter("@LanguageID", model.LanguageID);
                    param[4] = new SqlParameter("@UserID", _requestInfo.UserId);

                    var result =  SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spModifyMenu", param);
                    if(result > 0)
                        return Get(model.ID);
                    return Result<Menu>.Failure();
                }
            }
            catch (Exception e) { throw; }
        }
        public Result<Menu> Insert(Menu model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }

        public Result<Menu> Get(Guid ID)
        {
            try
            {
                var obj = new Menu();
                var param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "pbl.spGetMenu", param))
                    {
                        while (dr.Read())
                        {
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.RemoverID = SQLHelper.CheckGuidNull(dr["RemoverID"]);
                            obj.RemoverDate =SQLHelper.CheckDateTimeNull(dr["RemoverDate"]);
                            obj.LanguageID = SQLHelper.CheckGuidNull(dr["LanguageID"]);
                            obj.LanguageName = SQLHelper.CheckStringNull(dr["LanguageName"]);
                            obj.Name = SQLHelper.CheckStringNull(dr["Name"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                        }
                    }

                }
                return Result<Menu>.Successful(data: obj);
            }
            catch
            {
                return Result<Menu>.Failure();
            }
        }

        public DataTable List(MenuListVM listVM)
        {
            try
            {
                var param = new SqlParameter[4];
                param[0] = new SqlParameter("@LanguageID", listVM.LanguageID);
                param[1] = new SqlParameter("@Name", listVM.Name);
                param[2] = new SqlParameter("@PageSize", listVM.PageSize);
                param[3] = new SqlParameter("@PageIndex", listVM.PageIndex);

                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "pbl.spGetsMenu", param);
            }
            catch (Exception e) { throw; }
        }

        public Result<Menu> Update(Menu model)
        {
            return Modify(false, model);
        }

        public Result Delete(Guid ID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ID", ID);
                param[1] = new SqlParameter("@RemoverID", _requestInfo.UserId);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    int i = SQLHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "pbl.spDeleteMenu", param);
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
