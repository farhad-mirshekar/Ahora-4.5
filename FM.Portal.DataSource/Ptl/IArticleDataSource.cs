using FM.Portal.Core.Model;
using FM.Portal.Core;
using System;
using System.Data;

namespace FM.Portal.DataSource
{
    public interface IArticleDataSource : IDataSource
    {
        Result<Article> Insert(Article model);
        Result<Article> Update(Article model);
        DataTable List(ArticleListVM listVM);
        Result<Article> Get(Guid ID);
        Result<int> Delete(Guid ID);
    }
}
