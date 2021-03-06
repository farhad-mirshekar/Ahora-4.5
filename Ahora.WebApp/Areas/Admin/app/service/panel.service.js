﻿(() => {
    var app = angular
        .module('portal');

    app.factory('faqGroupService', faqGroupService);
    faqGroupService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function faqGroupService($http, callbackService, authenticationService) {
        var url = '/api/v1/faqgroup/';
        var service = {
            list: list,
            add: add,
            edit: edit,
            get: get
        };
        return service;

        function list(model) {
            return $http({
                method: 'POST',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('faqService', faqService);
    faqService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function faqService($http, callbackService, authenticationService) {
        var url = '/api/v1/faq/';
        var service = {
            add: add,
            edit: edit,
            list: list
        };
        return service;
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'POST',
                url: url + `list`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `list` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            });
        }
    }

    app.factory('uploadService', uploadService);
    uploadService.$inject = ['$http', '$q', 'callbackService', 'authenticationService'];
    function uploadService($http, $q, callbackService, authenticationService) {
        var url = 'ApiClient/attachment'
        var service = {
            upload: upload

        }
        return service;

        function upload(model) {
            return $http.post(`${url}/upload?type=${model.type}`, model.data,
                {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).then(function (result) {
                    return callbackService.onSuccess({ result: result, request: `${url}/upload?type=${model.type}` });
                }).catch(function () {
                    return callbackService.onError({ result: result });
                });
        }

    }

    app.factory('attachmentService', attachmentService);
    attachmentService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function attachmentService($http, callbackService, authenticationService) {
        var url = 'ApiClient/attachment/';
        var service = {
            add: add,
            list: list,
            remove: remove
        };
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'POST',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Remove`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Remove` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('profileService', profileService);
    profileService.$inject = ['$http', '$q', 'callbackService', 'authenticationService'];
    function profileService($http, $q, callbackService, authenticationService) {
        var url = '/api/v1/user/'
        var service = {
            save: save,
            get: get,
            setPassword: setPassword,
            searchByNationalCode: searchByNationalCode,
            signOut: signOut
        };
        return service;

        function save(model) {
            model.errors = [];

            if (!model.FirstName)
                model.errors.push('نام را وارد نمایید');

            if (!model.LastName)
                model.errors.push('نام خانوادگی را وارد نمایید');

            if (!model.CellPhone)
                model.errors.push('شماره همراه را وارد نمایید');

            if (!model.NationalCode)
                model.errors.push('کد ملی را وارد نمایید');

            if (model.errors, length)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + `Edit`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Edit` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }

        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function setPassword(model) {
            model.errors = [];
            if (!model.OldPassword)
                model.errors.push('کلمه عبور فعلی را وارد کنید.');
            if (!model.NewPassword)
                model.errors.push('کلمه عبور جدید را وارد کنید.');
            if (model.NewPassword.length < 8)
                model.errors.push('کلمه عبور باید حداقل 8 کاراکتر باشد.');
            if (!model.ReNewPassword)
                model.errors.push('تکرار کلمه عبور را وارد کنید.');
            if (model.NewPassword !== model.ReNewPassword)
                model.errors.push('کلمه عبور جدید با تکرار کلمه عبور هم‌خوانی ندارد.');

            if (model.errors.length)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'setpassword',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, url: url + 'setpassword' })
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function searchByNationalCode(model) {
            model.Errors = [];

            if (!model.NationalCode)
                model.Errors.push('کد ملی را وارد نمایید');
            if (model.NationalCode.length < 9)
                model.Errors.push('کد ملی را صحیح وارد نمایید');
            if (model.Errors && model.Errors.length > 0)
                return $q.reject();
            return $http({
                method: 'post',
                url: url + 'searchByNationalCode',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, url: url + 'searchByNationalCode' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function signOut() {
            return $http({
                method: 'POST',
                url: '/account/SignOut',
                data: { type: 'admin' }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: '/account/SignOut' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('commandService', commandService);
    commandService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function commandService($http, callbackService, authenticationService) {
        var url = '/api/v1/command/'
        var service = {
            list: list,
            listByNode: listByNode,
            commandType: commandType,
            get: get,
            listForRole: listForRole,
            add: add,
            edit: edit,
            getPermission: getPermission,
            remove: remove
        };
        return service;

        function list() {
            return $http({
                method: 'POST',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function listForRole(model) {
            return $http({
                method: 'POST',
                url: url + 'ListForRole',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'ListForRole' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function listByNode(model) {
            return $http({
                method: 'POST',
                url: url + 'ListByNode',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'ListByNode' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function commandType() {
            return $http({
                method: 'POST',
                url: url + 'CommandType',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'CommandType' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'Edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function getPermission() {
            return $http({
                method: 'POST',
                url: url + 'GetPermission',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'GetPermission' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('roleService', roleService);
    roleService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function roleService($http, callbackService, authenticationService) {
        var url = '/api/v1/role/'
        var service = {
            list: list,
            add: add,
            edit: edit,
            get: get
        };
        return service;

        function list(model) {
            return $http({
                method: 'POST',
                url: url + 'List',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'Edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }

    }

    app.factory('attributeService', attributeService);
    attributeService.$inject = ['$http', '$q', 'callbackService', 'authenticationService'];
    function attributeService($http, $q, callbackService, authenticationService) {
        var url = '/api/v1/ProductAttribute/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            model.Errors = [];
            if (!model.Name)
                model.Errors.push('نام را وارد نمایید');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            model.Errors = [];
            if (!model.Name)
                model.Errors.push('نام را وارد نمایید');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Get' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('positionService', positionService);
    positionService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function positionService($http, callbackService, authenticationService) {
        var url = '/api/v1/position/'
        var service = {
            add: add,
            list: list,
            setDefault: setDefault,
            get: get,
            edit: edit,
            listByUser: listByUser
        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'POST',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function setDefault(model) {
            return $http({
                method: 'POST',
                url: url + `setDefault/${model.PositionID}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `setDefault/${model.PositionID}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function listByUser(model) {
            return $http({
                method: 'POST',
                url: url + 'listByUser',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'listByUser' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('categoryService', CategoryService);
    CategoryService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function CategoryService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/category/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            model.errors = [];
            if (model.HasDiscountsApplied === true) {
                if (!model.DiscountID || model.DiscountID === '' || model.DiscountID === '00000000-0000-0000-0000-000000000000')
                    model.errors.push('نوع تخفیف را مشخص کنید');
            }
            if (model.errors.length)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(state) {
            return $http({
                method: 'post',
                url: url + 'list/' + state,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List/' + state });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('discountService', discountService);
    discountService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function discountService($http, callbackService, authenticationService) {
        var url = '/api/v1/discount/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }

    }

    app.factory('productService', productService);
    productService.$inject = ['$http', '$q', 'callbackService', 'authenticationService'];
    function productService($http, $q, callbackService, authenticationService) {
        var url = '/api/v1/product/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            model.Errors = [];
            if (!model.Name)
                model.Errors.push('نام را وارد نمایید');
            if (!model.ShortDescription)
                model.Errors.push('توضیحات کوتاه را وارد نمایید');
            if (!model.FullDescription)
                model.Errors.push('بررسی تخصصی محصول را وارد نمایید');
            if (!model.Price || model.Price === 0)
                model.Errors.push('مبلغ را وارد نمایید');
            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError(result);
            })
        }
        function edit(model) {
            model.Errors = [];
            if (!model.Name)
                model.Errors.push('نام را وارد نمایید');
            if (!model.ShortDescription)
                model.Errors.push('توضیحات کوتاه را وارد نمایید');
            if (!model.FullDescription)
                model.Errors.push('بررسی تخصصی محصول را وارد نمایید');
            if (!model.Price || model.Price === 0)
                model.Errors.push('مبلغ را وارد نمایید');
            if (model.ShippingCostID === '-1')
                model.ShippingCostID = null;
            //if (model.HasDiscount === true) {
            //    if (!model.DiscountType)
            //        model.Errors.push('نوع تخفیف را مشخص نمایید');
            //}
            //if (model.DiscountType) {
            //    switch (model.DiscountType) {
            //        case 1:
            //            if (!model.Discount || model.Discount === 0)
            //                model.Errors.push('مبلغ تخفیف را وارد نمایید');
            //            break;
            //        case 2:
            //            if (model.Discount === 0 || model.Discount > 100 )
            //                model.Errors.push('در صد را درست وارد نمایید');
            //            break;
            //    }
            //}
            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('productMapattributeService', productMapattributeService);
    productMapattributeService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function productMapattributeService($http, callbackService, authenticationService) {
        var url = '/api/v1/productMapattribute/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('productVariantAttributeValueService', productVariantAttributeValueService);
    productVariantAttributeValueService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function productVariantAttributeValueService($http, callbackService, authenticationService) {
        var url = '/api/v1/ProductVariantAttributeValue/'
        var service = {
            save: save,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function save(model) {
            if (!model.ID)
                return add(model);
            else
                edit(model);
        }
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('productCommentService', productCommentService);
    productCommentService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function productCommentService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/ProductComment/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            model.Errors = [];
            if (model.CommentType === 0)
                model.Errors.push('لطفا وضعیت نظر را مشخص نمایید');
            if (model.Errors.length)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('categoryPortalService', categoryPortalService);
    categoryPortalService.$inject = ['$http', 'callbackService', 'authenticationService', '$q'];
    function categoryPortalService($http, callbackService, authenticationService, $q) {
        var url = '/api/v1/categoryPortal/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Title)
                model.errors.push('نام را وارد نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Title)
                model.errors.push('نام را وارد نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list() {
            return $http({
                method: 'post',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List/' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('articleService', articleService);
    articleService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function articleService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/article/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');
            if (!model.LanguageID || model.LanguageID === '')
                model.errors.push('انتخاب زبان الزامی می باشد');
            if (!model.ViewStatusType)
                model.errors.push('وضعیت نمایش مقاله را مشخص نمایید');
            if (!model.CommentStatusType)
                model.errors.push('وضعیت نظر مقاله را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');
            if (!model.LanguageID || model.LanguageID === '')
                model.errors.push('انتخاب زبان الزامی می باشد');
            if (!model.ViewStatusType)
                model.errors.push('وضعیت نمایش مقاله را مشخص نمایید');
            if (!model.CommentStatusType)
                model.errors.push('وضعیت نظر مقاله را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `remove/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('categoryMapDiscountService', categoryMapDiscountService);
    categoryMapDiscountService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function categoryMapDiscountService($http, callbackService, authenticationService) {
        var url = '/api/v1/categoryMapDiscount/'
        var service = {
            add: add,

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('newsService', newsService);
    newsService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function newsService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/news/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان خبر الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی خبر الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');
            if (!model.LanguageID || model.LanguageID === '')
                model.errors.push('انتخاب زبان الزامی می باشد');
            if (!model.ViewStatusType)
                model.errors.push('وضعیت نمایش خبر را مشخص نمایید');
            if (!model.CommentStatusType)
                model.errors.push('وضعیت نظر خبر را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان خبر الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی خبر الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');
            if (!model.LanguageID || model.LanguageID === '')
                model.errors.push('انتخاب زبان الزامی می باشد');
            if (!model.ViewStatusType)
                model.errors.push('وضعیت نمایش خبر را مشخص نمایید');
            if (!model.CommentStatusType)
                model.errors.push('وضعیت نظر خبر را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `remove/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('pagesService', pagesService);
    pagesService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function pagesService($http, callbackService, authenticationService) {
        var url = '/api/v1/pages/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list() {
            return $http({
                method: 'post',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('menuService', menuService);
    menuService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function menuService($http, callbackService, authenticationService) {
        var url = '/api/v1/menu/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/ ${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('sliderService', sliderService);
    sliderService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function sliderService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/slider/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان تصویر کشویی الزامی می باشد');

            if (!model.Enabled)
                model.errors.push('وضعیت نمایش تصویر کشویی را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان تصویر کشویی الزامی می باشد');

            if (!model.Enabled)
                model.errors.push('وضعیت نمایش تصویر کشویی را مشخص نمایید');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `remove/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('generalSettingService', generalSettingService);
    generalSettingService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function generalSettingService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/generalSetting/'
        var service = {
            getSetting: getSetting,
            edit: edit

        }
        return service;

        function getSetting() {
            return $http({
                method: 'POST',
                url: url + 'getSetting',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'getSetting' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('eventsService', eventsService);
    eventsService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function eventsService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/events/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان رویداد الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی رویداد الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');

            if (model.errors && model.errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Title || model.Title === '')
                model.errors.push('عنوان رویداد الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.errors.push('متن اصلی رویداد الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.errors.push('انتخاب موضوع الزامی می باشد');

            if (model.errors && model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `remove/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('paymentService', paymentService);
    paymentService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function paymentService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/Payment/'
        var service = {
            get: get,
            list: list,
            getDetail: getDetail,
            getExcel: getExcel

        }
        return service;
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list() {
            return $http({
                method: 'post',
                url: url + `list`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `List` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function getDetail(model) {
            return $http({
                method: 'post',
                url: url + `GetDetail/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `GetDetail/${model}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function getExcel(model) {
            return $http({
                method: 'post',
                url: url + `GetExcel`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `GetExcel` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('notificationService', notificationService);
    notificationService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function notificationService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/notification/'
        var service = {
            list: list,
            readNotification: readNotification,
            get: get,
            getActiveNotification: getActiveNotification
        }
        return service;
        function list(model) {
            return $http({
                method: 'post',
                url: url + `list`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `List` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function readNotification(model) {
            return $http({
                method: 'post',
                url: url + `readNotification/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `readNotification/${model}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'post',
                url: url + `get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function getActiveNotification(model) {
            return $http({
                method: 'post',
                url: url + `getActiveNotification`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `getActiveNotification}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('departmentService', departmentService);
    departmentService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function departmentService($http, callbackService, authenticationService) {
        var url = '/api/v1/department/'
        var service = {
            list: list,
            get: get,
            add: add,
            edit: edit,
            remove: remove,
            listByNode: listByNode
        };
        return service;

        function listByNode(model) {
            return $http({
                method: 'POST',
                url: url + 'ListByNode',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'ListByNode' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list() {
            return $http({
                method: 'POST',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'Edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('userService', userService);
    userService.$inject = ['$http', '$q', 'callbackService', 'authenticationService'];
    function userService($http, $q, callbackService, authenticationService) {
        var url = '/api/v1/user/'
        var service = {
            add: add,
            get: get,
            list: list,
            resetPassword: resetPassword,
            getRefreshToken: getRefreshToken
        };
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + `add`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `add` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }

        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function list(model) {
            return $http({
                method: 'POST',
                url: url + `list`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `list` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function resetPassword(model) {
            return $http({
                method: 'POST',
                url: url + `resetPassword/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `resetPassword/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function getRefreshToken(model) {
            return $http({
                method: 'POST'
                , url: '/account/RefreshToken'
                , data: { RefreshToken: model.RefreshToken }
            }).then((result) => {
                return result.data.authorizationData;
            }).catch((result) => {
                return callbackService.onError({ result: result });
            });
        }
    }

    app.factory('pagesPortalService', pagesPortalService);
    pagesPortalService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function pagesPortalService($http, callbackService, authenticationService) {
        var url = '/api/v1/pagesPortal/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('contactService', contactService);
    contactService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function contactService($http, callbackService, authenticationService) {
        var url = '/api/v1/contact/'
        var service = {
            list: list,
            get: get,
            remove: remove,
        };
        return service;

        function list(model) {
            return $http({
                method: 'POST',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('dynamicPageService', dynamicPageService);
    dynamicPageService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function dynamicPageService($http, callbackService, authenticationService) {
        var url = '/api/v1/DynamicPage/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('linkService', linkService);
    linkService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function linkService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/link/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.Errors = [];
            if (!model.Name)
                model.Errors.push('نام پیوند را وارد نمایید');
            if (!model.Url)
                model.Errors.push('آدرس پیوند را وارد نمایید');
            if (!model.Description)
                model.Errors.push('توضیحات پیوند را وارد نمایید');
            if (model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.Errors = [];

            if (!model.Name)
                model.Errors.push('نام پیوند را وارد نمایید');
            if (!model.Url)
                model.Errors.push('آدرس پیوند را وارد نمایید');
            if (!model.Description)
                model.Errors.push('توضیحات پیوند را وارد نمایید');
            if (model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('staticPageService', staticPageService);
    staticPageService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function staticPageService($http, callbackService, authenticationService) {
        var url = '/api/v1/StaticPage/'
        var service = {
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('bannerService', bannerService);
    bannerService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function bannerService($http, callbackService, authenticationService) {
        var url = '/api/v1/Banner/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('galleryService', galleryService);
    galleryService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function galleryService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/Gallery/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            model.errors = [];
            if (!model.Name)
                model.errors.push('نام آلبوم را وارد نمایید');

            if (!model.Description)
                model.errors.push('توضیحات مربوط به آلبوم را وارد نمایید');

            if (!model.Enabled)
                model.errors.push('نمایش آلبوم را مشخص نمایید');

            if (!model.UrlDesc)
                model.errors.push('توضیحات سئو را وارد نمایید');

            if (!model.MetaKeywords)
                model.errors.push('متاتگ را وارد نمایید');

            if (model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];
            if (!model.Name)
                model.errors.push('نام آلبوم را وارد نمایید');

            if (!model.Description)
                model.errors.push('توضیحات مربوط به آلبوم را وارد نمایید');

            if (!model.Enabled)
                model.errors.push('نمایش آلبوم را مشخص نمایید');

            if (!model.UrlDesc)
                model.errors.push('توضیحات سئو را وارد نمایید');

            if (!model.MetaKeywords)
                model.errors.push('متاتگ را وارد نمایید');

            if (model.errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('shippingCostService', shippingCostService);
    shippingCostService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function shippingCostService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/ShippingCost/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('deliveryDateService', deliveryDateService);
    deliveryDateService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function deliveryDateService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/DeliveryDate/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('relatedProductService', relatedProductService);
    relatedProductService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function relatedProductService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/RelatedProduct/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('documentFlowService', documentFlowService);
    documentFlowService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function documentFlowService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/DocumentFlow/'
        var service = {
            setAsRead: setAsRead
        }
        return service;
        function setAsRead(model) {
            return $http({
                method: 'POST',
                url: url + `SetAsRead/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'setAsRead' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('salesService', salesService);
    salesService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function salesService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/Sales/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            confirm: confirm,
            listFlow: listFlow

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function confirm(model) {
            return $http({
                method: 'POST',
                url: url + 'confirm',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'confirm' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function listFlow(model) {
            return $http({
                method: 'POST',
                url: url + `listFlow/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `listFlow/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('languageService', languageService);
    languageService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function languageService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/Language/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('localeStringResourceService', localeStringResourceService);
    localeStringResourceService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function localeStringResourceService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/LocaleStringResource/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('menuItemService', menuItemService);
    menuItemService.$inject = ['$http', 'callbackService', 'authenticationService'];
    function menuItemService($http, callbackService, authenticationService) {
        var url = '/api/v1/MenuItem/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            remove: remove

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function remove(model) {
            return $http({
                method: 'POST',
                url: url + `Delete/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/ ${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('articleCommentService', articleCommentService);
    articleCommentService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function articleCommentService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/ArticleComment/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];

            if (model.CommentType === 0)
                model.errors.push('لطفا وضعیت نظر را مشخص نمایید');
            if (!model.Body || model.Body === '')
                model.errors.push('متن نظر را وارد نمایید');

            if (model.Errors.length)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('eventsCommentService', eventsCommentService);
    eventsCommentService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function eventsCommentService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/EventsComment/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];

            if (model.CommentType === 0)
                model.errors.push('لطفا وضعیت نظر را مشخص نمایید');
            if (!model.Body || model.Body === '')
                model.errors.push('متن نظر را وارد نمایید');

            if (model.Errors.length)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('newsCommentService', newsCommentService);
    newsCommentService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function newsCommentService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/NewsComment/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list

        }
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            model.errors = [];

            if (model.CommentType === 0)
                model.errors.push('لطفا وضعیت نظر را مشخص نمایید');
            if (!model.Body || model.Body === '')
                model.errors.push('متن نظر را وارد نمایید');

            if (model.errors.length)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
             })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('activityLogService', activityLogService);
    activityLogService.$inject = ['$http', 'callbackService', '$q', 'authenticationService'];
    function activityLogService($http, callbackService, $q, authenticationService) {
        var url = '/api/v1/ActivityLog/'
        var service = {
            get: get,
            list: list

        }
        return service;

        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(model) {
            return $http({
                method: 'post',
                url: url + 'list',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + authenticationService.get('authorizationData').Access_Token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('callbackService', callbackService);
    callbackService.$inject = ['$q', '$http', 'authenticationService'];
    function callbackService($q, $http, authenticationService) {
        var service = {
            onSuccess: onSuccess,
            onError: onError,
            refreshToken: refreshToken
        }
        return service;

        function onSuccess(response) {
            if (!response.result.data || !response.result.data.Success)
                return $q.reject({ result: response.result, request: response.request });

            return response.result.data ? response.result.data.Data : {};
        }
        function onError(error) {
            if (error.result && error.result.result.data === -397)
                return refreshToken().then(error.result).then((result) => {
                    {
                        return onSuccess({ result: error.result, request: error.result.request });
                    }
                });
            else
                return $q.reject(error.result.result.data ? error.result.result.data.Message : 'خطای ناشناخته');
        }
        function refreshToken() {
            let counter = 1;
            return $q.resolve().then(() => {
                if (localStorage.authorizationData) {
                    const token = JSON.parse(localStorage.authorizationData);
                    return $http({
                        method: 'POST'
                        , url: '/Account/RefreshToken'
                        , data: { RefreshToken: token.Refresh_Token }
                    }).then((result) => {
                        authenticationService.setCredentials(result.data.authorizationData);
                        window.location.reload();
                    }).catch(() => {
                        window.location.href = '/account/login';
                    });
                }
            });
        }
    }
})();


