﻿using System;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
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
            return _dataSource.Insert(model);
        }

        public Result<Menu> Edit(Menu model)
        => _dataSource.Update(model);

        public Result<Menu> Get(Guid ID)
        => _dataSource.Get(ID);

        //public Result<List<MenuVM>> GetMenuForWeb(string Node)
        //{
        //    try
        //    {
        //        List<MenuVM> menus = new List<MenuVM>();

        //        var children = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(Node));

        //        if (children.Count > 0)
        //        {
        //            for (int i = 0; i < children.Count; i++)
        //            {
        //                var child = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(children[i].Node));
        //                if (child.Count > 0)
        //                {
        //                    menus.Add(new MenuVM { IconText = children[i].IconText, Url = children[i].Url, ID = children[i].ID, Name = children[i].Name, Children = ChildRender(child), Parameters=children[i].Parameters ,ForeignLink=children[i].ForeignLink});
        //                    //str += ChildRender(child, children[i].ID);
        //                }

        //                else
        //                    menus.Add(new MenuVM { IconText = children[i].IconText, Url = children[i].Url, ID = children[i].ID, Name = children[i].Name, Children = null , Parameters=children[i].Parameters, ForeignLink = children[i].ForeignLink });

        //            }
        //        }
        //        return Result<List<MenuVM>>.Successful(data:menus);

        //    }
        //    catch(Exception e) { return Result<List<MenuVM>>.Failure(); }
           
        //}
        //private List<MenuVM> ChildRender(List<Menu> child)
        //{
        //    List<MenuVM> menus = new List<MenuVM>();
        //    if (child.Count > 0)
        //    {
        //        for (int i = 0; i < child.Count; i++)
        //        {
        //            var subchild = ConvertDataTableToList.BindList<Menu>(_dataSource.GetChildren(child[i].Node));
        //            if (subchild.Count > 0)
        //            {
        //                menus.Add(new MenuVM { IconText = child[i].IconText, Url = child[i].Url, ID = child[i].ID, Name = child[i].Name, Children = ChildRender(subchild) ,Parameters=child[i].Parameters, ForeignLink = child[i].ForeignLink });
        //            }
        //            else
        //            {
        //                menus.Add(new MenuVM { IconText = child[i].IconText, Url =child[i].Url, ID = child[i].ID, Name = child[i].Name, Parameters = child[i].Parameters, ForeignLink = child[i].ForeignLink });
        //            }
        //        }
        //    }
        //    return menus;
        //}
        public Result<List<Menu>> List(MenuListVM listVM)
        {
            var result = ConvertDataTableToList.BindList<Menu>(_dataSource.List(listVM));
            if (result.Count > 0 || result.Count == 0)
                return Result<List<Menu>>.Successful(data: result);
            else
                return Result<List<Menu>>.Failure();
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);
    }
}
