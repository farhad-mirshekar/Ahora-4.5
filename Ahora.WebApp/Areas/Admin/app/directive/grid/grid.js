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
            scope.obj.getlist = getlist;
            grid.obj.actions = scope.obj.actions || [
                {
                    title: "ویرایش",
                    class: "fa fa-pencil mr-2",
                    onclick: edit,
                    name: "edit",
                    condinatin: true
                },
                {
                    title: "حذف",
                    class: "fa fa-close mr-2",
                    onclick: remove,
                    name: "remove",
                    condination: false
                }
            ];
            grid.obj.itemsByPage = 1;
            grid.obj.pageSizeRange = [5, 10, 15, 20];
            grid.obj.pageSize = 5;
            grid.cellValue = cellValue;
            grid.edit = edit;
            grid.remove = remove;
            grid.obj.confirmRemove = confirmRemove;

            if (grid.obj === null) {
                console.log('null');
            }
            getItems();
            function getItems() {
                return $q.resolve().then(() => {
                    return grid.obj.listService();
                }).then((result) => {
                    grid.obj.items = [].concat(result);
                })
            }
            function cellValue(item, column) {
                let process =
                    column.callback ||
                    function (item) {
                        return item;
                    };
                return process(getValue(item, column), item);
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
                if (scope.obj.onEdit) {
                    scope.obj.onEdit(item);
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
                    return scope.obj.deleteService(scope.deleteBuffer.ID)
                        .then(() => {
                            return getItems();
                        }).then(() => {
                            element.find(".grid-delete").modal("hide");
                            loadingService.hide();
                        })
                }).finally(loadingService.hide);
            }
            function getlist() {
                return getItems();
            }
        }
    }
})();