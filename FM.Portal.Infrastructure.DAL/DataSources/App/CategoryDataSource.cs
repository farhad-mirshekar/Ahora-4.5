using System;
using System.Data;
using FM.Portal.Core.Model;
using FM.Portal.DataSource;
using FM.Portal.Core.Common;
using System.Data.SqlClient;
using FM.Portal.Core.Result;
using System.Collections.Generic;

namespace FM.Portal.Infrastructure.DAL
{
    public class CategoryDataSource : ICategoryDataSource
    {
        public DataTable List()
        {
            try
            {
                return SQLHelper.GetDataTable(CommandType.StoredProcedure, "app.spGetsCategory", null);
            }
            catch (Exception e) { throw; }

        }
        private Result<Category> Modify(bool isNewrecord, Category model)
        {
            try
            {
                var commands = new List<SqlCommand>();

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ID", model.ID);

                param[1] = new SqlParameter("@IncludeInTopMenu", model.IncludeInTopMenu);
                param[2] = new SqlParameter("@ParentID", model.ParentID);
                param[3] = new SqlParameter("@IncludeInLeftMenu", model.IncludeInLeftMenu);
                param[4] = new SqlParameter("@Title", model.Title);
                param[5] = new SqlParameter("@isNewRecord", isNewrecord);
                param[6] = new SqlParameter("@HasDiscountsApplied", model.HasDiscountsApplied);
                commands.Add(SQLHelper.CreateCommand("app.spModifyCategory", CommandType.StoredProcedure, param));

                if (model.HasDiscountsApplied)
                {
                    SqlParameter[] paramDiscount = new SqlParameter[4];
                    paramDiscount[0] = new SqlParameter("@ID", Guid.NewGuid());

                    paramDiscount[1] = new SqlParameter("@CategoryID", model.ID);
                    paramDiscount[2] = new SqlParameter("@DiscountID", model.DiscountID);
                    paramDiscount[3] = new SqlParameter("@IsNewRecord", true);
                    commands.Add(SQLHelper.CreateCommand("app.spModifyCategoryMapDiscount", CommandType.StoredProcedure, paramDiscount));
                }
                SQLHelper.BatchExcute(commands.ToArray());

                return Get(model.ID);
            }
            catch (Exception e) { throw; }

        }

        public Result<Category> Insert(Category model)
        {
            model.ID = Guid.NewGuid();
            return Modify(true, model);
        }
        public Result<Category> Update(Category model)
        {
            return Modify(false, model);
        }

        public Result<Category> Get(Guid ID)
        {
            try
            {
                Category obj = new Category();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetCategory", param))
                    {
                        while (dr.Read())
                        {
                            obj.IncludeInTopMenu = SQLHelper.CheckBoolNull(dr["IncludeInTopMenu"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]); ;
                            obj.IncludeInLeftMenu = SQLHelper.CheckBoolNull(dr["IncludeInLeftMenu"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.HasDiscountsApplied = SQLHelper.CheckBoolNull(dr["HasDiscountsApplied"]);
                            obj.DiscountID = SQLHelper.CheckGuidNull(dr["DiscountID"]);
                        }
                    }

                }
                return Result<Category>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Category>.Failure(); }

        }

        public Result<Category> GetByParent(Guid ID)
        {
            try
            {
                Category obj = new Category();
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ParentID", ID);

                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnectionString()))
                {
                    using (SqlDataReader dr = SQLHelper.ExecuteReader(con, CommandType.StoredProcedure, "app.spGetByParentCategory", param))
                    {
                        while (dr.Read())
                        {
                            obj.IncludeInTopMenu = SQLHelper.CheckBoolNull(dr["IncludeInTopMenu"]);
                            obj.CreationDate = SQLHelper.CheckDateTimeNull(dr["CreationDate"]);
                            obj.ID = SQLHelper.CheckGuidNull(dr["ID"]);
                            obj.IncludeInLeftMenu = SQLHelper.CheckBoolNull(dr["IncludeInLeftMenu"]);
                            obj.HasDiscountsApplied = SQLHelper.CheckBoolNull(dr["HasDiscountsApplied"]);
                            obj.ParentID = SQLHelper.CheckGuidNull(dr["ParentID"]);
                            obj.Title = SQLHelper.CheckStringNull(dr["Title"]);
                            obj.DiscountID = SQLHelper.CheckGuidNull(dr["DiscountID"]);
                        }
                    }

                }
                return Result<Category>.Successful(data: obj);
            }
            catch (Exception e) { return Result<Category>.Failure(); }
        }
    }
}
