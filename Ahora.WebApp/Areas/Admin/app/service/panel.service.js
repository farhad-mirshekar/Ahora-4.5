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
           return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
            });
        }
    }

    app.factory('uploadService', uploadService);
    uploadService.$inject = ['$http', '$q', 'callbackService'];
    function uploadService($http, $q, callbackService) {
        var url = '/attachment/'
        var service = {
            upload: upload

        }
        return service;

        function upload(model) {
            var defer = $q.defer();
            return $http.post("/attachment/upload", model,
                {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                })
            .then(function (d) {
                return d;// defer.resolve(d);
            })
            .catch(function () {
                return defer.reject("File Upload Failed!");
            });

            //return defer.promise;
        }

    }

    app.factory('attachmentService', attachmentService);
    attachmentService.$inject = ['$http', 'callbackService'];
    function attachmentService($http, callbackService) {
        var url = '/attachment/';
        var service = {
            add: add,
            list: list,
            remove:remove
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                callbackService.onSuccess({result:result , url:url+'setpassword'})
            })
               .catch(function (result) {
                   return callbackService.onError({result:result});
               })
        }
        function searchByNationalCode(model) {
            return $http({
                method: 'post',
                url: url + 'searchByNationalCode',
                data: model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({result:result , url:url+'searchByNationalCode'});
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function signOut() {
            return $http({
                method: 'POST',
                url: '/account/SignOut',
                data: { type:'admin'}
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
            remove:remove
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                 return callbackService.onError({result:result});
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
               return callbackService.onError({result:result});
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
                  return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
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
                return callbackService.onError({result:result});
            })
        }
    }

    app.factory('positionService', positionService);
    positionService.$inject = ['$http', 'callbackService'];
    function positionService($http, callbackService) {
        var url = '/api/v1/position/'
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
                return callbackService.onSuccess({ result: result, request: url + 'add' });
            })
               .catch(function (result) {
                   return callbackService.onError({result:result});
               })
        }
    }

    app.factory('categoryService', CategoryService);
    CategoryService.$inject = ['$http', 'callbackService','$q'];
    function CategoryService($http, callbackService,$q) {
        var url = '/api/v1/category/'
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
                   callbackService.onError({result:result});
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
                   return callbackService.onError({result:result});
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

    app.factory('discountService', discountService);
    discountService.$inject = ['$http', 'callbackService'];
    function discountService($http, callbackService) {
        var url = '/api/v1/discount/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            discountType: discountType

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
                   return callbackService.onError({result:result});
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
        function discountType() {
            return $http({
                method: 'post',
                url: url + 'DiscountType',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'DiscountType' });
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
                   return callbackService.onError({result:result});
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
                   return callbackService.onError({result:result});
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
    }

    app.factory('productVariantattributeService', productVariantattributeService);
    productVariantattributeService.$inject = ['$http', 'callbackService'];
    function productVariantattributeService($http, callbackService) {
        var url = '/api/v1/ProductVariantAttribute/'
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
    }

    app.factory('commentService', commentService);
    commentService.$inject = ['$http', 'callbackService'];
    function commentService($http, callbackService) {
        var url = '/api/v1/comment/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            commentStatusType: commentStatusType,
            commentForType: commentForType

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
                return callbackService.onSuccess({ result: result, request: url + `list/${model}` });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function commentStatusType() {
            return $http({
                method: 'post',
                url: url + 'CommentStatusType',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'commentStatusType' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function commentForType() {
            return $http({
                method: 'post',
                url: url + 'commentForType',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'commentForType' });
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
    }

    app.factory('articleService', articleService);
    articleService.$inject = ['$http', 'callbackService','$q'];
    function articleService($http, callbackService,$q) {
        var url = '/api/v1/article/'
        var service = {
            add: add,
            edit: edit,
            get: get,
            list: list,
            typeShow: typeShow,
            typeComment: typeComment,
            remove:remove

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
            if (!model.CategoryID  || model.CategoryID === '')
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
        function typeShow() {
            return $http({
                method: 'post',
                url: url + 'TypeShowArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeShowArticle' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function typeComment() {
            return $http({
                method: 'post',
                url: url + 'TypeCommentArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeCommentArticle' });
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
            typeShow: typeShow,
            typeComment: typeComment,
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
        function typeShow() {
            return $http({
                method: 'post',
                url: url + 'TypeShowArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeShowArticle' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function typeComment() {
            return $http({
                method: 'post',
                url: url + 'TypeCommentArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeCommentArticle' });
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
            remove:remove

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
            getbyParentNode: getbyParentNode,
            remove:remove

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
        function getbyParentNode(model) {
            return $http({
                method: 'POST',
                url: url + `GetByParentNode`,
                data:model,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then(function (result) {
                return callbackService.onSuccess({ result: result, request: url + `GetByParentNode` });
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
                return callbackService.onSuccess({ result: result, request: url + `Delete/ ${ model }` });
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
            typeEnable: typeEnable,
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
        function typeEnable() {
            return $http({
                method: 'post',
                url: url + 'TypeEnable',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeEnable' });
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
            typeShow: typeShow,
            typeComment: typeComment,
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
        function typeShow() {
            return $http({
                method: 'post',
                url: url + 'TypeShowArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeShowArticle' });
            }).catch((result) => {
                return callbackService.onError({ result: result });
            })
        }
        function typeComment() {
            return $http({
                method: 'post',
                url: url + 'TypeCommentArticle',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.access_token
                }
            }).then((result) => {
                return callbackService.onSuccess({ result: result, request: url + 'TypeCommentArticle' });
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

    app.factory('callbackService', callbackService);
    callbackService.$inject = ['$q', '$http', 'authenticationService'];
    function callbackService($q, $http, authenticationService) {
        var service = {
            onSuccess: onSuccess,
            onError:onError,
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
                return refreshToken().then(error.request).then((result) => {
                    {
                        return onSuccess({ result: result, request: error.request });
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
                        , data: { RefreshToken:Refresh_Token }
                    }).then((result) => {
                        authenticationService.setCredentials(result.data);
                    }).catch((error) => {
                        window.location.href = '/account/login';
                        //counter++;
                        //if (counter < 10)
                        //    authenticationService.setCredentials({status:0});
                        //else
                        //    window.location.reload();
                    });
                }
            });
        }
    }
})();


