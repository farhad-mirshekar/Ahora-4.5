var ListPermission = [];
(() => {
    var app = angular.module('portal');
    app.factory('toolsService', toolsService);
    toolsService.$inject = ['$location', '$window', 'profileService', '$q', 'loadingService', 'authenticationService','$rootScope'];
    function toolsService($location, $window, profileService, $q, loadingService, authenticationService, $rootScope) {
        let service = {
            checkPermission: checkPermission,
            getTreeObject: getTreeObject,
            signOut: signOut,
            arrayEnum: arrayEnum
        };
        return service;
        function checkPermission(input, options) {
            let isAuthorized = false;

            options = options || {};

            if (typeof input === "string")
                isAuthorized =
                    $rootScope.permissions &&
                    $rootScope.permissions.some(e => {
                        return e.FullName === input;
                    });
            else if (Object.prototype.toString.call(input) === "[object Array]") {
                for (let i = 0; i < $rootScope.permissions.length; i++) {
                    const permission = $rootScope.permissions[i];

                    for (let j = 0; j < input.length; j++) {
                        const name = input[j];

                        if (permission.FullName === name) {
                            isAuthorized = true;
                            break;
                        }
                    }

                    if (isAuthorized) break;
                }
            }

            if (!isAuthorized && options.notFound) $location.path("not-found");

            return isAuthorized;
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
                return profileService.signOut();
            }).then((result) => {
                if (result) {
                    authenticationService.clearCredentials();
                    $window.location.href = '/login';
                }
            }).finally(loadingService.hide);
        }
        function arrayEnum(enumObject) {
            let result = [];

            for (let key in enumObject) {
                result.push({ Model: parseInt(key), Name: enumObject[key] });
            }

            return result;
        }

    }
})();