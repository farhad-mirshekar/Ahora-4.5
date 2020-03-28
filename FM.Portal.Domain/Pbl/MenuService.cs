using System;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;

namespace FM.Portal.Domain
{
    public class MenuService : IMenuService
    {
        private readonly IMenuDataSource _dataSource;
        public MenuService(IMenuDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<Menu> Add(Menu model)
        {
            if (model.Node == string.Empty || model.Node == "")
                model.Node = null;
            return _dataSource.Create(model);
        }

        public Result<Menu> Edit(Menu model)
        => _dataSource.Update(model);

        public Result<Menu> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Menu> Get(string ParentNode)
        => _dataSource.Get(ParentNode);

        public string GetMenuForWeb(string Node)
        {
            string str = "";
            var children = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(Node));

            if (children.Count > 0)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    var child = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(children[i].Node));
                    if (child.Count > 0)
                    {
                        str += $"<li class='nav-item dropdown'><a class='nav-link dropdown-toggle' href='{children[i].Url}' id='{children[i].ID}' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'><i class='{children[i].IconText}'></i> {children[i].Name}</a>";
                        str += ChildRender(child, children[i].ID);
                        str += "</li>";
                    }

                    else
                        str += $"<li class='nav-item'><a class='nav-link' href='{children[i].Url}'><i class='{children[i].IconText}'></i> {children[i].Name}</a></li>";

                }
            }
            return str;
        }
        private string ChildRender(List<Menu> child, Guid Parent)
        {
            string str = $"<ul class='dropdown-menu' aria-labelledby='{Parent}'>";
            if (child.Count > 0)
            {
                for (int i = 0; i < child.Count; i++)
                {
                    var subchild = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(child[i].Node));
                    if (subchild.Count > 0)
                    {
                        str += $"<li class='dropdown-submenu'><a class='dropdown-item dropdown-toggle' href='{child[i].Url}'><i class='{child[i].IconText}'></i> {child[i].Name}</a>";
                        str += ChildRender(subchild, child[i].ID);
                        str += "</li>";
                    }
                    else
                    {
                        str += $"<li><a class='dropdown-item' href='{child[i].Url}'><i class='{child[i].IconText}'></i> {child[i].Name}</a></li>";
                    }
                }
            }
            str += "</ul>";
            return str;
        }
        public Result<List<Menu>> List()
        {
            var result = ConvertDataTableToList.BindList<Menu>(_dataSource.List());
            if (result.Count > 0 || result.Count == 0)
                return Result<List<Menu>>.Successful(data: result);
            else
                return Result<List<Menu>>.Failure();
        }
    }
}
