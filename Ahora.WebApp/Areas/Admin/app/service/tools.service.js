var ListPermission = [];
(() => {
    var app = angular.module('portal');
    app.factory('toolsService', toolsService);
    toolsService.$inject = ['commandService', '$window', 'profileService', '$q', 'loadingService','authenticationService'];
    function toolsService(commandService, $window, profileService, $q, loadingService, authenticationService) {
        let service = {
            hasuser:hasuser,
            userID:userID,
            getPermission: getPermission,
            checkPermission: checkPermission,
            getTreeObject: getTreeObject,
            signOut: signOut
        };
        return service;
        function hasuser() {
            var data = localStorage.access_token;
            var type = localStorage.type;
            if (type === "1") {
                if (data) {
                    return true;
                }
            }

            return false;
        }
        function userID() {
            var data = localStorage.userid;
            if (data)
                return data;
            return "";
        }
        function getPermission() {
            var userID = localStorage.userid;
            if (!userID) {
            } else {
                return commandService.getPermission().then((result) => {
                    $window.ListPermission = [].concat(result);
                })
            }
        }
        function checkPermission(model) {
            for (var i = 0; i < $window.ListPermission.length; i++) {
                if (model === $window.ListPermission[i].FullName) {
                    return true
                }
            }
            return false;
        }
        function getTreeObject(data, primaryIdName, parentIdName, defaultRoot) {
            if (!data || data.length == 0 || !primaryIdName || !parentIdName) return [];

            let tree = [],
              rootIds = [],
              item = data[0],
              primaryKey = item[primaryIdName],
              treeObjs = {},
              tempChildren = {},
              parentId,
              parent,
              len = data.length,
              i = 0;

            while (i < len) {
                item = data[i++];
                primaryKey = item[primaryIdName];

                if (tempChildren[primaryKey]) {
                    item.children = tempChildren[primaryKey];
                    delete tempChildren[primaryKey];
                }

                treeObjs[primaryKey] = item;
                parentId = item[parentIdName];

                if (parentId && parentId != defaultRoot) {
                    parent = treeObjs[parentId];

                    if (!parent) {
                        let siblings = tempChildren[parentId];
                        if (siblings) siblings.push(item);
                        else tempChildren[parentId] = [item];
                    } else if (parent.children) {
                        parent.children.push(item);
                    } else {
                        parent.children = [item];
                    }
                } else {
                    rootIds.push(primaryKey);
                }
            }

            for (let i = 0; i < rootIds.length; i++) {
                tree.push(treeObjs[rootIds[i]]);
            }

            return tree;
        }
        function signOut() {
            loadingService.show();
            return $q.resolve().then(() => {
                 profileService.signOut();
                authenticationService.clearCredentials();
                $window.location.href = '/account/login';
            }).finally(loadingService.hide);
        }

    }
})();