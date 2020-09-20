(() => {
    angular
        .module('portal')
        .directive('portalGrid', portalGrid);
    portalGrid.$inject = ['$q', '$location', 'loadingService'];
    function portalGrid($q, $location, loadingService) {
        let directive = {
            restrict: 'E'
            , templateUrl: ('./Areas/Admin/app/directive/grid/grid.html')
            , scope: {
                obj: '=obj'
            }
            , link: {
                pre: preLink
            }
        };
        return directive;
        function preLink(scope, element) {
            let grid = scope;
            grid.obj.getlist = getlist;
            grid.obj.options =
                grid.obj.options ||
                function () {
                    return {};
                };
            grid.obj.actions = grid.obj.actions || [
                {
                    title: "ویرایش",
                    class: "fa fa-pencil text-info mr-2 cursor-grid operation-icon",
                    onclick: edit,
                    name: "edit",
                    condinatin: true
                },
                {
                    title: "حذف",
                    class: "fa fa-trash text-danger mr-3 cursor-grid operation-icon",
                    onclick: remove,
                    name: "remove",
                    condination: false
                }
            ];

            grid.obj.pageSize = grid.obj.pageSize || 5;
            grid.obj.pageIndex = grid.obj.pageIndex || 1;
            grid.obj.pageSizeRange = grid.obj.pageSizeRange || [5, 10, 20, 50, 100];
            grid.cellValue = cellValue;
            grid.edit = edit;
            grid.remove = remove;
            grid.obj.confirmRemove = confirmRemove;
            grid.obj.previousPage = previousPage;
            grid.obj.nextPage = nextPage;
            grid.obj.pageSizeChange = pageSizeChange;
            grid.obj.changePageIndex = changePageIndex;

            Object.defineProperty(grid.obj, "total", {
                get: () => {
                    if (
                        grid.obj.items &&
                        grid.obj.items.length > 0 &&
                        grid.obj.items[0].Total
                    )
                        return grid.obj.items[0].Total;
                    else return grid.total;
                }
            });
            Object.defineProperty(grid.obj, "result", {
                set: result => {
                    if (result.Success) {
                        grid.obj.items = result.Data;
                        grid.obj.pageCount = Array.apply(null, {
                            length: result.PageCount + 1
                        }).map(Number.call, Number);
                        grid.obj.pageCount.shift();
                    }
                }
            });

            if (grid.obj === null) {
                console.log('null');
            }
            if (grid.obj.initLoad)
                getlist();
            else
                grid.obj.pageCount = [1];

            grid.pageIndex = grid.obj.pageIndex;

            function getItems() {
                return $q.resolve().then(() => {
                    let options = grid.obj.options();
                    options.PageSize = grid.obj.pageSize;
                    options.PageIndex = grid.obj.pageIndex;

                    return grid.obj.listService(grid.obj.options());
                }).then((result) => {
                    grid.total = 0;
                    grid.obj.items = [].concat(result);
                })
            }
            function cellValue(item, column) {
                let process =
                    column.callback ||
                    function (item) {
                        return item;
                    };
                switch (column.type) {
                    case "enum":
                        return process(column.source[getValue(item, column)], item);
                    default:
                        return process(getValue(item, column), item);
                }
            }
            function getValue(item, column) {
                let keys = column.name.split(".");
                if (keys.length === 1) return item[column.name];
                else {
                    let value = item;
                    for (let i = 0; i < keys.length; i++) {
                        value = value[keys[i]];
                    }
                    return value;
                }
            }
            function edit(item) {
                if (grid.obj.onEdit) {
                    grid.obj.onEdit(item);
                }
                else {
                    $location.path(`${grid.obj.route}/edit/${item.ID}`);
                }
                grid.obj.bindingObject.state = 'edit';

            }
            function remove(item) {
                loadingService.show();
                grid.deleteBuffer = item;
                grid.displayName = "";
                if (typeof grid.obj.displayNameFormat === "function") {
                    grid.displayName = grid.obj.displayNameFormat(item);
                } else {
                    for (let i = 0; i < grid.obj.displayNameFormat.length; i++) {
                        grid.displayName +=
                            grid.deleteBuffer[grid.obj.displayNameFormat[i]] + " ";
                    }
                }
                loadingService.hide();
                element.find(".grid-delete").modal("show");
            }
            function confirmRemove() {
                loadingService.show();
                return $q.resolve().then(() => {
                    return grid.obj.deleteService(grid.deleteBuffer.ID)
                        .then(() => {
                            return getItems();
                        }).then(() => {
                            element.find(".grid-delete").modal("hide");
                            loadingService.hide();
                        })
                }).finally(loadingService.hide);
            }
            function getlist() {
                loadingService.show();
                return $q.resolve().then(() => {
                    return getItems().then(loadingService.hide).catch(() => {
                        loadingService.hide();
                        return $q.reject();
                    });
                }).then(() => {
                    grid.pageIndex = grid.obj.pageIndex;
                    grid.obj.loadingTotal = true;
                    if (grid.obj.getTotalCount)
                        return grid.obj.getTotalCount(grid.obj.options()).then(result => {
                            return result.Total;
                        });
                    else if (grid.obj.items && grid.obj.items.length)
                        return grid.obj.items[0].Total;
                    else return 0;
                }).then(total => {
                    grid.obj.totalPageCount = 1;
                    grid.total = total || 0;
                    if (total)
                        grid.obj.totalPageCount = Math.ceil(total / grid.obj.pageSize);

                    if (grid.obj.totalPageCount <= 100) {
                        grid.obj.pageCount = Array.apply(null, {
                            length: grid.obj.totalPageCount + 1
                        }).map(Number.call, Number);
                        grid.obj.pageCount.shift();
                    }
                    grid.obj.loadingTotal = false;

                    return grid.obj.items;
                });
            }

            function previousPage() {
                if (grid.obj.loadingTotal) return;
                if (grid.obj.pageIndex > 1) {
                    grid.obj.pageIndex--;
                    grid.obj.getlist();
                }
            }
            function nextPage() {
                if (grid.obj.loadingTotal) return;
                if (
                    (grid.obj.pageCount &&
                        grid.obj.pageIndex < grid.obj.pageCount.length) ||
                    grid.obj.pageIndex < grid.obj.totalPageCount
                ) {
                    grid.obj.pageIndex++;
                    grid.obj.getlist();
                }
            }
            function pageSizeChange() {
                if (grid.obj.items.length) {
                    grid.obj.pageIndex = 1;
                    grid.obj.getlist();
                }
            }
            function changePageIndex(event) {
                if (event.keyCode === 13) grid.obj.getlist();
            }
          
        }
    }
})();