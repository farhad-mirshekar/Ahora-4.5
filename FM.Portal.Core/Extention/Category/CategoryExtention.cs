using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using System;
using System.Collections.Generic;

namespace FM.Portal.Core.Extention.Category
{
    public static class CategoryExtention
    {
        public static string GetFormattedBreadCrumb(Core.Model.Category category,
            ICategoryService categoryService,
            string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<Guid>() { };

            while (category.ID != Guid.Empty &&  //not null
                !alreadyProcessedCategoryIds.Contains(SQLHelper.CheckGuidNull(category.ID))) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Title;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Title, separator, result);
                }

                alreadyProcessedCategoryIds.Add(SQLHelper.CheckGuidNull(category.ID));

                var data = categoryService.Get(category.ParentID);
                category = data.Data;

            }
            return result;
        }
    }
}
