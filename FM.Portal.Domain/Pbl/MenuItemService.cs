using FM.Portal.Core;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemDataSource _dataSource;
        public MenuItemService(IMenuItemDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public Result<MenuItem> Add(MenuItem model)
        {
            if (model.Node == string.Empty || model.Node == "")
                model.Node = null;
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        => _dataSource.Delete(ID);

        public Result<MenuItem> Edit(MenuItem model)
        => _dataSource.Update(model);

        public Result<MenuItem> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<MenuItem>> List(MenuItemListVM listVM)
        {
            var result = ConvertDataTableToList.BindList<MenuItem>(_dataSource.List(listVM));
            var menus = new List<MenuItem>();
            if (result.Count > 0 || result.Count == 0)
            {
                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        var child = ConvertDataTableToList.BindList<MenuItem>(_dataSource.List(new MenuItemListVM() { ParentNode = result[i].Node }));
                        if (child.Count > 0)
                        {
                            menus.Add(new MenuItem
                            {
                                IconText = result[i].IconText,
                                Url = result[i].Url,
                                ID = result[i].ID,
                                Name = result[i].Name,
                                Children = ChildRender(child),
                                Parameters = result[i].Parameters,
                                ForeignLink = result[i].ForeignLink,
                                Node = result[i].Node,
                                ParentNode = result[i].ParentNode,
                                ParentID = result[i].ParentID,
                                CreationDate = result[i].CreationDate,
                                Enabled = result[i].Enabled,
                                MenuID = result[i].MenuID,
                                Priority = result[i].Priority,
                                RemoverDate = result[i].RemoverDate,
                                RemoverID = result[i].RemoverID
                            });
                        }

                        else
                            menus.Add(new MenuItem
                            {
                                IconText = result[i].IconText,
                                Url = result[i].Url,
                                ID = result[i].ID,
                                Name = result[i].Name,
                                Children = null,
                                Parameters = result[i].Parameters,
                                ForeignLink = result[i].ForeignLink,
                                Node = result[i].Node,
                                ParentNode = result[i].ParentNode,
                                ParentID = result[i].ParentID,
                                CreationDate = result[i].CreationDate,
                                Enabled = result[i].Enabled,
                                MenuID = result[i].MenuID,
                                Priority = result[i].Priority,
                                RemoverDate = result[i].RemoverDate,
                                RemoverID = result[i].RemoverID
                            });
                    }
                    return Result<List<MenuItem>>.Successful(data: menus);
                }
                else
                    return Result<List<MenuItem>>.Successful(data: result);
            }
            else
                return Result<List<MenuItem>>.Failure();
        }
        private List<MenuItem> ChildRender(List<MenuItem> child)
        {
            var menus = new List<MenuItem>();
            if (child.Count > 0)
            {
                for (int i = 0; i < child.Count; i++)
                {
                    var subchild = ConvertDataTableToList.BindList<MenuItem>(_dataSource.List(new MenuItemListVM() { ParentNode = child[i].Node }));
                    if (subchild.Count > 0)
                    {
                        menus.Add(new MenuItem
                        {
                            IconText = child[i].IconText,
                            Url = child[i].Url,
                            ID = child[i].ID,
                            Name = child[i].Name,
                            Children = ChildRender(subchild),
                            Parameters = child[i].Parameters,
                            ForeignLink = child[i].ForeignLink,
                            Node = child[i].Node,
                            ParentNode = child[i].ParentNode,
                            ParentID = child[i].ParentID,
                            CreationDate = child[i].CreationDate,
                            Enabled = child[i].Enabled,
                            MenuID = child[i].MenuID,
                            Priority = child[i].Priority,
                            RemoverDate = child[i].RemoverDate,
                            RemoverID = child[i].RemoverID
                        });
                    }
                    else
                    {
                        menus.Add(new MenuItem
                        {
                            IconText = child[i].IconText,
                            Url = child[i].Url,
                            ID = child[i].ID,
                            Name = child[i].Name,
                            Children = null,
                            Parameters = child[i].Parameters,
                            ForeignLink = child[i].ForeignLink,
                            Node = child[i].Node,
                            ParentNode = child[i].ParentNode,
                            ParentID = child[i].ParentID,
                            CreationDate = child[i].CreationDate,
                            Enabled = child[i].Enabled,
                            MenuID = child[i].MenuID,
                            Priority = child[i].Priority,
                            RemoverDate = child[i].RemoverDate,
                            RemoverID = child[i].RemoverID
                        });
                    }
                }
            }
            return menus;
        }
    }
}
