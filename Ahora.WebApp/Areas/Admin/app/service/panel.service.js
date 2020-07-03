(() => {
    var app = angular
        .module('portal');

    app.factory('faqGroupService', faqGroupService);
    faqGroupService.$inject = ['$http', 'callbackService'];
    function faqGroupService($http, callbackService) {
        var url = '/api/v1/faqgroup/';
        var service = {
            list: list,
            add: add,
            edit: edit,
            get: get
        };
        return service;

        function list() {
            return $http({
                method: 'POST',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('faqService', faqService);
    faqService.$inject = ['$http', 'callbackService'];
    function faqService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `list/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            });
        }
    }

    app.factory('uploadService', uploadService);
    uploadService.$inject = ['$http', '$q', 'callbackService'];
    function uploadService($http, $q, callbackService) {
        var url = '/attachment'
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
                })
                .then(function (result) {
                    return callbackService.onSuccess({ result: result, request: `${url}/upload?type=${model.type}` });
                })
                .catch(function () {
                    return callbackService.onError({ result: result });
                });
        }

    }

    app.factory('attachmentService', attachmentService);
    attachmentService.$inject = ['$http', 'callbackService'];
    function attachmentService($http, callbackService) {
        var url = '/attachment/';
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Remove` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('profileService', profileService);
    profileService.$inject = ['$http', '$q', 'callbackService'];
    function profileService($http, $q, callbackService) {
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

        }

        function get(model) {
            return $http({
                method: 'POST',
                url: url + `get/${model}`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                callbackService.onSuccess({ result: result, url: url + 'setpassword' })
            })
                .catch(function (result) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    commandService.$inject = ['$http', 'callbackService'];
    function commandService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function getPermission() {
            return $http({
                method: 'POST',
                url: url + 'GetPermission',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'GetPermission' });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('roleService', roleService);
    roleService.$inject = ['$http', 'callbackService'];
    function roleService($http, callbackService) {
        var url = '/api/v1/role/'
        var service = {
            list: list,
            add: add,
            edit: edit,
            get: get
        };
        return service;

        function list() {
            return $http({
                method: 'POST',
                url: url + 'List',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }

    }

    app.factory('attributeService', attributeService);
    attributeService.$inject = ['$http', '$q', 'callbackService'];
    function attributeService($http, $q, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return result;
            })
                .catch(function (result) {
                    return result;
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Get' });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('positionService', positionService);
    positionService.$inject = ['$http', 'callbackService'];
    function positionService($http, callbackService) {
        var url = '/api/v1/position/'
        var service = {
            add: add,
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'list' });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('categoryService', CategoryService);
    CategoryService.$inject = ['$http', 'callbackService', '$q'];
    function CategoryService($http, callbackService, $q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('discountService', discountService);
    discountService.$inject = ['$http', 'callbackService'];
    function discountService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }

    }

    app.factory('productService', productService);
    productService.$inject = ['$http', '$q', 'callbackService'];
    function productService($http, $q, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Add' });
            })
                .catch(function (result) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('productMapattributeService', productMapattributeService);
    productMapattributeService.$inject = ['$http', 'callbackService'];
    function productMapattributeService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('productVariantattributeService', productVariantattributeService);
    productVariantattributeService.$inject = ['$http', 'callbackService'];
    function productVariantattributeService($http, callbackService) {
        var url = '/api/v1/ProductVariantAttribute/'
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `delete/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
    }

    app.factory('commentService', commentService);
    commentService.$inject = ['$http', 'callbackService', '$q'];
    function commentService($http, callbackService, $q) {
        var url = '/api/v1/comment/'
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                url: url + `list/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `list/${model}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('categoryPortalService', categoryPortalService);
    categoryPortalService.$inject = ['$http', 'callbackService'];
    function categoryPortalService($http, callbackService) {
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
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Get/${model}` });
            })
                .catch(function (result) {
                    return callbackService.onError({ result: result });
                })
        }
        function list(state) {
            return $http({
                method: 'post',
                url: url + 'list/'+state,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List/'+state });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('articleService', articleService);
    articleService.$inject = ['$http', 'callbackService', '$q'];
    function articleService($http, callbackService, $q) {
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
            model.Errors = [];
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('categoryMapDiscountService', categoryMapDiscountService);
    categoryMapDiscountService.$inject = ['$http', 'callbackService'];
    function categoryMapDiscountService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    newsService.$inject = ['$http', 'callbackService', '$q'];
    function newsService($http, callbackService, $q) {
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
            model.Errors = [];
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان مقاله الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی مقاله الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            if (!model.CategoryID || model.CategoryID === '')
                model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('pagesService', pagesService);
    pagesService.$inject = ['$http', 'callbackService'];
    function pagesService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    menuService.$inject = ['$http', 'callbackService'];
    function menuService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    sliderService.$inject = ['$http', 'callbackService', '$q'];
    function sliderService($http, callbackService, $q) {
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
            model.Errors = [];
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان اسلایدر الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان اسلایدر الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('generalSettingService', generalSettingService);
    generalSettingService.$inject = ['$http', 'callbackService', '$q'];
    function generalSettingService($http, callbackService, $q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
    }

    app.factory('eventsService', eventsService);
    eventsService.$inject = ['$http', 'callbackService', '$q'];
    function eventsService($http, callbackService, $q) {
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
            model.Errors = [];
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان رویداد الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی رویداد الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            //if (!model.CategoryID || model.CategoryID === '')
            //    model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();
            return $http({
                method: 'POST',
                url: url + 'Add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
            if (!model.Title || model.Title === '')
                model.Errors.push('عنوان رویداد الزامی می باشد');
            if (!model.Description || model.Description === '')
                model.Errors.push('توضیحات کوتاه الزامی می باشد');
            if (!model.Body || model.Body === '')
                model.Errors.push('متن اصلی رویداد الزامی می باشد');
            if (!model.MetaKeywords || model.MetaKeywords === '')
                model.Errors.push('متاتگ الزامی می باشد');
            if (!model.UrlDesc || model.UrlDesc === '')
                model.Errors.push('سئو الزامی می باشد');
            //if (!model.CategoryID || model.CategoryID === '')
            //    model.Errors.push('انتخاب موضوع الزامی می باشد');

            if (model.Errors && model.Errors.length > 0)
                return $q.reject();

            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `remove/${model}` });
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'List' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('paymentService', paymentService);
    paymentService.$inject = ['$http', 'callbackService', '$q'];
    function paymentService($http, callbackService, $q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `GetExcel` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('notificationService', notificationService);
    notificationService.$inject = ['$http', 'callbackService', '$q'];
    function notificationService($http, callbackService, $q) {
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
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + `getActiveNotification}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('departmentService', departmentService);
    departmentService.$inject = ['$http', 'callbackService'];
    function departmentService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('userService', userService);
    userService.$inject = ['$http', '$q', 'callbackService'];
    function userService($http, $q, callbackService) {
        var url = '/api/v1/user/'
        var service = {
            add: add,
            get: get,
            list: list,
            resetPassword: resetPassword
        };
        return service;

        function add(model) {
            return $http({
                method: 'POST',
                url: url + `add`,
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `resetPassword/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('pagesPortalService', pagesPortalService);
    pagesPortalService.$inject = ['$http', 'callbackService'];
    function pagesPortalService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    contactService.$inject = ['$http', 'callbackService'];
    function contactService($http, callbackService) {
        var url = '/api/v1/contact/'
        var service = {
            list: list,
            get: get,
            remove: remove,
        };
        return service;

        function list() {
            return $http({
                method: 'POST',
                url: url + 'list',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            }).catch(function (result) {
                return callbackService.onError({ result: result });
            })
        }
    }

    app.factory('dynamicPageService', dynamicPageService);
    dynamicPageService.$inject = ['$http', 'callbackService'];
    function dynamicPageService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    linkService.$inject = ['$http', 'callbackService','$q'];
    function linkService($http, callbackService,$q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    staticPageService.$inject = ['$http', 'callbackService'];
    function staticPageService($http, callbackService) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    bannerService.$inject = ['$http', 'callbackService'];
    function bannerService($http, callbackService) {
        var url = '/api/v1/Banner/'
        var service = {
            add:add,
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                data:model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    galleryService.$inject = ['$http', 'callbackService','$q'];
    function galleryService($http, callbackService,$q) {
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
            return $http({
                method: 'POST',
                url: url + 'add',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    shippingCostService.$inject = ['$http', 'callbackService', '$q'];
    function shippingCostService($http, callbackService, $q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                data:model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
    deliveryDateService.$inject = ['$http', 'callbackService', '$q'];
    function deliveryDateService($http, callbackService, $q) {
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function edit(model) {
            return $http({
                method: 'POST',
                url: url + 'edit',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + 'Edit' });
            })
                .catch(function (result) {
                    callbackService.onError({ result: result });
                })
        }
        function get(model) {
            return $http({
                method: 'POST',
                url: url + `Get/${model}`,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
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
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `Delete/${model}` });
            })
                .catch(function (result) {
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
                if (localStorage.refresh_token) {
                    const Refresh_Token = localStorage.refresh_token;
                    return $http({
                        method: 'POST'
                        , url: '/account/RefreshToken'
                        , data: { RefreshToken: Refresh_Token }
                    }).then((result) => {
                        authenticationService.setCredentials(result.data);
                    }).catch((error) => {
                        window.location.href = '/account/login';
                        counter++;
                        if (counter < 10)
                            authenticationService.setCredentials({ status: 0 });
                        else
                            window.location.reload();
                    });
                }
            });
        }
    }
})();


