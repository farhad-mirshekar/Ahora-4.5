var froalaOption = {
    toolbarButtons: {
        'moreText': {
            'buttons': ['bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', 'fontFamily', 'fontSize', 'textColor', 'backgroundColor', 'inlineClass', 'inlineStyle', 'clearFormatting']
        },
        'moreParagraph': {
            'buttons': ['alignLeft', 'alignCenter', 'formatOLSimple', 'alignRight', 'alignJustify', 'formatOL', 'formatUL', 'paragraphFormat', 'paragraphStyle', 'lineHeight', 'outdent', 'indent', 'quote']
        },
        'moreRich': {
            'buttons': ['insertLink', 'insertImage', 'insertVideo', 'insertTable', 'emoticons', 'fontAwesome', 'specialCharacters', 'embedly', 'insertFile', 'insertHR']
        },
        'moreMisc': {
            'buttons': ['undo', 'redo', 'fullscreen', 'print', 'getPDF', 'spellChecker', 'selectAll', 'html', 'help'],
            'align': 'right',
            'buttonsVisible': 2
        }
    },
    language: 'fa',
    quickInsertEnabled: false,
    charCounterCount: false,
    toolbarSticky: false,
    angularIgnoreAttrs: ['class', 'ng-model', 'id'],
    // Set the video upload URL.
    videoUploadURL: '/attachment/upload?type=7',
    // Set request type.
    videoUploadMethod: 'POST',
    events: {
        'video.beforeUpload': function (videos) {
            // Return false if you want to stop the video upload.
        },
        'video.uploaded': function (response) {

            // Video was uploaded to the server.
        },
    }
};
var froalaOptionComment = {
    toolbarButtons: {
        'moreText': {
            'buttons': ['bold', 'italic', 'underline', 'fontFamily', 'fontSize', 'textColor']
        },
        'moreParagraph': {
            'buttons': ['alignLeft', 'alignCenter', 'alignRight', 'alignJustify']
        }
    },
    language: 'fa'
  
};
(() => {
    var app = angular.module('portal');

    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', '$location'];
    function homeController($scope, $location, toolsService) {
        let home = $scope;
    }
    //-------------------------------------------------------------------------------------------------------
    app.controller('faqGroupController', faqGroupController);
    faqGroupController.$inject = ['$scope', '$routeParams', '$location', '$uibModal', 'toaster', 'loadingService', 'faqGroupService', 'faqService', '$q','$timeout'];
    function faqGroupController($scope, $routeParams, $location, $uibModal, toaster, loadingService, faqGroupService, faqService, $q, $timeout) {
        let faqgroup = $scope;
        faqgroup.state = '';
        faqgroup.Model = {};
        faqgroup.faq = {};
        faqgroup.main = {};
        faqgroup.main.changeState = {
            cartable: cartable,
            edit:edit
        };
        faqgroup.addFaqGroup = addFaqGroup;
        faqgroup.editFaqGroup = editFaqGroup;
        faqgroup.select = select;
        faqgroup.openModalFaq = openModalFaq;
        faqgroup.goToPageAdd = goToPageAdd;
        faqgroup.grid = {
            bindingObject: faqgroup
            , columns: [{ name: 'Title', displayName: 'دسته بندی سوال' }]
            , listService: faqGroupService.list
            , onEdit: faqgroup.main.changeState.edit
            , globalSearch: true
        };
        init();
        function init() {
            loadingService.show();
            switch ($routeParams.state) {
                case 'cartable':
                    cartable();
                    loadingService.hide();
                    break;
                case 'add':
                    faqgroup.state = 'add';
                    loadingService.hide();
                    break;
                case 'edit':
                    faqgroup.state = 'edit';
                    faqGroupService.get($routeParams.id).then((result) => {
                        edit(result);
                    })
                    loadingService.hide();
                    break;
            }
        }
        function goToPageAdd() {
            $location.path('/faq-group/add');
        }
        function addFaqGroup() {
            loadingService.show();
            return faqGroupService.add(faqgroup.Model).then((result) => {
                faqgroup.grid.getlist(false);
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                faqgroup.Model = result;
                $timeout(function () {
                    cartable();
                }, 1000);
            }).finally(loadingService.hide);
        }
        function editFaqGroup() {
            loadingService.show();
            return $q.resolve().then(() => {
                return faqGroupService.edit(faqgroup.Model);
            }).then((result) => {
                faqgroup.grid.getlist(false);
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                faqgroup.Model = result;
                $timeout(function () {
                    cartable();
                }, 1000);
            }).finally(loadingService.hide);
        }
        function cartable() {
            faqgroup.Model = {};
            faqgroup.state = 'cartable';
            $location.path('faq-group/cartable');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return faqService.list(model.ID);
            }).then((result) => {
                faqgroup.faq = result;
                faqgroup.state = 'edit';
                faqgroup.Model = model;
                $location.path(`faq-group/edit/${faqgroup.Model.ID}`);
            }).finally(loadingService.hide);
        }
        function openModalFaq() {

            var modalInstanse = $uibModal.open({
                templateUrl: 'faq.html',
                controller: 'faqController',
                size: 'lg',
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                resolve: {
                    faqgroup: () => { return faqgroup.Model; },
                    faqmodel: () => { return null; }
                }
            })
        }
        function select(model) {
            var modalInstanse = $uibModal.open({
                templateUrl: 'faq.html',
                controller: 'faqController',
                size: 'lg',
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                resolve: {
                    faqgroup: () => { return faqgroup.Model; },
                    faqmodel: () => { return angular.copy(model); }
                }
            })
        }
    }

    //-------------------------------------------------------------------------------------------------------

    app.controller('faqController', faqController);
    faqController.$inject = ['$scope', '$uibModalInstance', '$timeout', 'toaster', 'loadingService', 'faqgroup', 'faqService', '$window', 'faqmodel', '$q'];
    function faqController($scope, $uibModalInstance, $timeout, toaster, loadingService, faqgroup, faqService, $window, faqmodel, $q) {
        let faq = $scope;
        faq.cancel = cancel;
        faq.add = add;
        faq.edit = edit;
        faq.state = 'addd';
        if (faqmodel === null) {
            faq.state = 'add';
            faq.Model = {};
        }
        else {
            faq.Model = faqmodel;
            faq.state = 'edit';
        }

        faq.faqgroup = faqgroup;
        function add() {
            loadingService.show();
            faq.Model.FAQGroupID = faqgroup.ID;
            return $q.resolve().then(() => {
                return faqService.add(faq.Model);
            }).then(() => {
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                $timeout(function () {
                    cancel();
                }, 100);
                $timeout(function () {
                    $window.location.reload();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', "", "مشکلی اتفاق افتاده است");
            }).finally(loadingService.hide);
        }
        function edit() {
            faq.Model.FAQGroupID = faqgroup.ID;
            loadingService.show();
            return $q.resolve().then(() => {
                return faqService.edit(faq.Model);
            }).then(() => {
                toaster.pop('success', '', 'تغییرات با موفقیت انجام گردید');
                $timeout(function () {
                    cancel();
                }, 1000);
                $timeout(function () {
                    $window.location.reload();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', "", "مشکلی اتفاق افتاده است");
            }).finally(loadingService.hide);
        }
        function cancel() {
            $uibModalInstance.dismiss();
        }

    }

    //-------------------------------------------------------------------------------------------------------
    app.controller('profileController', profileController);
    profileController.$inject = ['$scope', 'profileService'];
    function profileController($scope, profileService) {
        let profile = $scope;

        init();

        function init() {
            var id = localStorage.userid;
            return profileService.get(id).then((result) => {
                profile.user = result;
            })
        }
    }
    //------------------------------------------------------------------------------------------------------------
    app.controller('commandController', commandController);
    commandController.$inject = ['$scope', '$q', 'commandService', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout', 'toolsService'];
    function commandController($scope, $q, commandService, loadingService, $routeParams, $location, toaster, $timeout, toolsService) {
        let command = $scope;
        command.Model = {};
        command.list = [];
        command.lists = [];
        command.state = 'cartable';
        command.goToPageAdd = goToPageAdd;
        command.addCommand = addCommand;
        command.addSubCommand = addSubCommand;
        command.editCommand = editCommand;
        command.changeState = {
            cartable: cartable
        }
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        goToPageAdd();
                        loadingService.hide();
                        break;
                    case 'edit':
                        commandService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);


        } // end init

        function cartable() {
            command.tree = {
                data: []
                , colDefs: [
                    { field: 'Name', displayName: 'عنوان انگلیسی' }
                    , { field: 'Title', displayName: 'نام مجوز' }
                    , {
                        field: ''
                        , displayName: ''
                        , cellTemplate: (
                            `<div style='float: left'>
                            <i class='fa fa-plus tgrid-action' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                        , cellTemplateScope: {
                            edit: edit,
                            add: addSubCommand
                        }
                    }
                ]
                , expandingProperty: {
                    field: "Title"
                    , displayName: "عنوان"
                }
            };
            commandService.list().then((result) => {
                setTreeObject(result);
            });
            command.state = 'cartable';
            $location.path('command/cartable');
        }
        function edit(parent) {
            commandService.commandType().then((result) => {
                command.commandType = [].concat(result);
            });
            commandService.get(parent.ID).then((result) => {
                command.Model = result;

                return commandService.listByNode({ Node: result.ParentNode });
            }).then((result) => {
                command.Model.ParentID = result[0].ID;
                $location.path(`/command/edit/${command.Model.ID}`);
                command.state = 'edit';
            })
        }
        function goToPageAdd(parent) {
            parent = parent || {};
            command.state = 'add';
            commandService.commandType().then((result) => {
                command.commandType = [].concat(result);
            })
            command.Model = { ParentID: parent.ID };
            $location.path('/command/add');
        }
        function addCommand() {
            command.Model.Name = command.Model.FullName;
            loadingService.show();
            $q.resolve().then(() => {
                commandService.add(command.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function editCommand() {
            command.Model.Name = command.Model.FullName;
            loadingService.show();
            $q.resolve().then(() => {
                commandService.edit(command.Model).then((result) => {
                    toaster.pop('success', '', 'مجوز جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);
                })
            }).catch((error) => {
                toaster.pop('error', '', 'خطای ناشناخته');
            }).finally(loadingService.hide);
        }
        function addSubCommand(parent) {
            command.state = 'add';
            goToPageAdd(parent);
        }
        function setTreeObject(commands) {
            commands.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            command.tree.data = toolsService.getTreeObject(commands, 'Node', 'ParentNode', '/');
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------
    app.controller('roleController', roleController);
    roleController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'roleService', 'toaster', 'commandService', '$location', 'toolsService'];
    function roleController($scope, $q, loadingService, $routeParams, roleService, toaster, commandService, $location, toolsService) {
        let role = $scope;
        role.Model = {};
        role.main = {};
        role.main.changeState = {
            cartable: cartable,
            edit: edit
        };
        role.Model.Permissions = [];
        role.ListCommand = [];
        role.addRole = addRole;
        role.editRole = editRole;
        role.goToPageAdd = goToPageAdd;
        role.back = back;
        role.grid = {
            bindingObject: role
            , columns: [{ name: 'Name', displayName: 'نام نقش' }]
            , listService: roleService.list
            , onEdit: role.main.changeState.edit
            , globalSearch: true
        };
        init();

        function init() {
            role.state = 'cartable';
            $q.resolve().then(() => {
                loadingService.show();
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        roleService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                    case 'add':
                        add();
                        break;
                }
            }).finally(loadingService.hide);

        } // end init

        function cartable() {
            role.Model = {};
            role.state = 'cartable';
            $location.path('role/cartable');
        }
        function edit(model) {
            loadingService.show;
            return $q.resolve().then(() => {
                role.Model = model;
                return commandService.list();
            }).then((result) => {
                if (result.length > 0) {
                    role.ListCommand = toolsService.getTreeObject(result, 'Node', 'ParentNode', '/');
                }
                return commandService.listForRole({ RoleID: role.Model.ID });
            }).then((result) => {
                if (result.length > 0) {
                    role.Model.Permissions = [];
                    for (var i = 0; i < result.length; i++) {
                        role.Model.Permissions.push(result[i]);
                    }
                }
                role.state = 'edit';
                $location.path(`role/edit/${role.Model.ID}`);
            }).catch(() => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function add() {
            role.state = 'add';
        }

        function editRole() {
            loadingService.show();
            return $q.resolve().then(() => {
                var json = '';
                if (role.Model.Permissions.length === 0)
                    toaster.pop('error', '', 'مجوزی برای نقش انتخاب نکردید');
                for (var i = 0; i < role.Model.Permissions.length; i++) {
                    json += role.Model.Permissions[i].ID + ',';
                }
                role.Model.Json = json;
                return roleService.edit(role.Model);
            }).then((result) => {
                role.Model = result;
                return commandService.listForRole({ RoleID: $routeParams.id });
            }).then((result) => {
                if (result.length > 0) {
                    role.Model.Permissions = [];
                    for (var i = 0; i < result.length; i++) {
                        role.Model.Permissions.push(result[i]);
                    }
                }
                role.grid.getlist(false);
                toaster.pop('success', '', 'ویرایش با موفقیت انجام شد');
            }).catch(() => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function addRole() {
            loadingService.show();
            return $q.resolve().then(() => {
                return roleService.add(role.Model);
            }).then((result) => {
                role.Model = result;
                return edit(role.Model.ID);
            }).catch((error) => {
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function goToPageAdd() {
            $location.path('/role/add');
        }
        function back() {
            $location.path('role/cartable');
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------
    app.controller('positionController', positionController);
    positionController.$inject = ['$scope', '$q', '$timeout', '$routeParams', '$location', 'toaster', '$timeout', 'loadingService', 'positionService', 'toaster', 'profileService', 'roleService'];
    function positionController($scope, $q, $timeout, $routeParams, $location, toaster, $timeout, loadingService, positionService, toaster, profileService, roleService) {
        let position = $scope;
        position.Model = {};
        position.state = '';
        position.ResultSearch = { Enabled: false };
        position.selectCommand = {
            selected: []
        };
        position.goToPageAdd = goToPageAdd;
        position.searchNationalCode = searchNationalCode;
        position.addPosition = addPosition;
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        positionService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            position.state = 'cartable';
            positionService.list().then((result) => {
                label.model = result;
                label.grid = [].concat(result);
            })
            $location.path('/position/cartable');
        }
        function add() {
            position.state = 'add';
            listRole();
            $location.path('/position/add');
        }
        function edit(model) {
            position.state = 'edit';
            position.Model = model;
            $location.path(`/position/edit/${model.ID}`);
        }
        function goToPageAdd() {
            add();
        }
        function searchNationalCode() {
            $q.resolve().then(() => {
                loadingService.show();
                if (!position.Model.NationalCode) {
                    toaster.pop('error', '', 'کد ملی را وارد نمایید');
                }
                else if (position.Model.NationalCode.length < 10) {
                    toaster.pop('error', '', 'کد ملی را صحیح وارد نمایید');
                }
                else {
                    profileService.searchByNationalCode({ NationalCode: position.Model.NationalCode }).then((result) => {
                        if (result.Enabled) {
                            position.ResultSearch = result;
                            position.search = true;
                        }

                    }).catch((error) => {
                        toaster.pop('error', '', 'خطای ناشناخته');
                    })
                }


            }).finally(loadingService.hide);

        }
        function listRole() {
            roleService.list().then((result) => {
                position.listRole = [].concat(result);
            })
        }
        function addPosition() {
            loadingService.show();
            $q.resolve().then(() => {
                if (!position.ResultSearch.Enabled) {
                    toaster.pop('error', '', 'کاربری را جست و جو نکردید');
                    return false;
                } else {
                    position.Model.UserID = position.ResultSearch.ID;
                    return true;
                }
            }).then((state) => {
                if (state) {
                    var json = '';
                    if (position.selectCommand.selected.length === 0) {
                        toaster.pop('error', '', 'نقشی انتخاب نکردید');
                        return false;
                    } else {
                        for (var i = 0; i < position.selectCommand.selected.length; i++) {
                            json += position.selectCommand.selected[i] + ',';
                        }
                        position.Model.Json = json;
                        return true;
                    }
                }
            }).then((state) => {
                if (state) {
                    positionService.add(position.Model).then((result) => {
                        toaster.pop('success', '', 'با موفقیت ذخیره گردید');
                    })
                }
            }).finally(loadingService.hide);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('changePasswordController', changePasswordController);
    changePasswordController.$inject = ['$scope', '$q', '$timeout', '$window', 'toaster', 'loadingService', 'profileService', 'toolsService'];
    function changePasswordController($scope, $q, $timeout, $window, toaster, loadingService, profileService, toolsService) {
        let profile = $scope;
        profile.Model = {};
        profile.changePassword = changePassword;

        function changePassword() {
            loadingService.show();
            $q.resolve().then(() => {
                profile.Model.UserID = toolsService.userID();
                return profileService.setPassword(profile.Model)
            }).then((result) => {
                toaster.pop('success', '', result.Message);
                $timeout(function () {
                    localStorage.clear();
                    $window.location.href('/account/login');
                }, 1000);
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('productController', productController);
    productController.$inject = ['$scope', '$routeParams', 'loadingService', '$q', 'toaster', '$location', 'categoryService', 'productService', 'attachmentService', 'attributeService', 'productMapattributeService', 'productVariantattributeService', 'discountService'];
    function productController($scope, $routeParams, loadingService, $q, toaster, $location, categoryService, productService, attachmentService, attributeService, productMapattributeService, productVariantattributeService, discountService) {
        var product = $scope;
        product.Model = {};
        product.Attribute = {};
        product.ProductVariant = {};
        product.Model.Errors = [];
        product.pic = { type: '1', allowMultiple: true };
        product.pic.list = [];
        product.Model.listPicUploaded = [];
        product.froalaOption = angular.copy(froalaOption);
        product.froalaOptions = angular.copy(froalaOption);
        product.Attribute.Sub = false;
        product.ProductVariant.showGrid = false;
        product.ProductVariant.Parent = true;
        product.addProductMapAttribute = addProductMapAttribute;
        product.addProduct = addProduct;
        product.addProductVarient = addProductVarient;
        product.listProductVarient = listProductVarient;
        product.editProduct = editProduct;
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        productService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function add() {
            product.state = 'add';
            categoryType();
            $location.path('/product/add');
        }
        function edit(model) {
            product.Model = model;
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryType();
            }).then(() => {
                return listAttribute();
            }).then(() => {
                return discountType();
            }).then(() => {
                return listAttributeProduct(product.Model.ID);
            }).then(() => {
                return attachmentService.list({ ParentID: product.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    product.Model.listPicUploaded = [].concat(result);
                product.state = 'edit';
                $location.path(`/product/edit/${product.Model.ID}`);
            }).catch((error) => {
                loadingService.hide();
                toaster.pop('error', '', 'خطا');
            }).finally(loadingService.hide);

        }
        function addProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return productService.add(product.Model);
            }).then((result) => {
                product.Model = result;
                toaster.pop('success', '', 'محصول جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $location.path(`product/edit/${product.Model.ID}`);
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    product.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editProduct() {
            loadingService.show();
            return $q.resolve().then(() => {
                return productService.edit(product.Model);
            }).then((result) => {
                if (product.pic.list.length) {
                    product.pics = [];
                    if (!product.Model.listPicUploaded) {
                        product.pics.push({ ParentID: product.Model.ID, Type: 1, FileName: product.pic.list[0], PathType: product.pic.type });
                    }
                    for (var i = 0; i < product.pic.list.length; i++) {
                        product.pics.push({ ParentID: product.Model.ID, Type: 2, FileName: product.pic.list[i], PathType: product.pic.type });
                    }
                    product.Model = result;
                    return attachmentService.add(product.pics);
                }
            }).then((result) => {
                return attachmentService.list({ ParentID: product.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    product.Model.listPicUploaded = [].concat(result);
                product.pics = [];
                product.pic.list = [];
                toaster.pop('success', '', 'محصول جدید با موفقیت ویرایش گردید');
                loadingService.hide();
            })
                .catch((error) => {
                    if (!error) {
                        $('#content > div').animate({
                            scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        var listError = error.split('&&');
                        product.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#ProductError').offset().top - $('#ProductError').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
        }
        function categoryType() {
            return categoryService.list().then((result) => {
                var list = [].concat(result);
                product.categoryType = [];
                for (var i = 0; i < list.length; i++) {
                    if (list[i].ParentID !== '00000000-0000-0000-0000-000000000000')
                        product.categoryType.push({ Model: list[i].ID, Name: list[i].Title });
                }
            })
        }
        function listAttribute() {
            return attributeService.list().then((result) => {
                var list = [].concat(result);
                product.attributeType = [];
                for (var i = 0; i < list.length; i++) {
                    product.attributeType.push({ Model: list[i].ID, Name: list[i].Name });
                }
            })
        }
        function addProductMapAttribute() {
            product.Attribute.ProductID = product.Model.ID;
            loadingService.show();
            return productMapattributeService.add(product.Attribute).then((result) => {
                loadingService.hide();
            }).catch((error) => {
                loadingService.hide();
            })
        }
        function listAttributeProduct(model) {
            productMapattributeService.list(model).then((result) => {
                product.Attribute.showGrid = true;
                product.ProductVariant.Parent = true;
                product.Attribute.Grid = [].concat(result);
            })
        }
        function addProductVarient() {
            productVariantattributeService.add(product.ProductVariant).then((result) => {
                product.Attribute.Sub = false;
                listAttributeProduct(product.Model.ID);
            })
        }
        function listProductVarient(model) {
            return productVariantattributeService.list(model).then((result) => {
                product.ProductVariant.showGrid = true;
                product.ProductVariant.Grid = [].concat(result);
            })
        }
        function discountType() {
            return $q.resolve().then(() => {
                return discountService.discountType();
            }).then((result) => {
                product.selectDiscountType = result;
            })
        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('ProductCartableController', ProductCartableController);
    ProductCartableController.$inject = ['$scope', 'productService', '$q', '$location'];
    function ProductCartableController($scope, productService, $q, $location) {
        let product = $scope;
        product.grid = {
            bindingObject: product
            , columns: [{ name: 'Name', displayName: 'عنوان آگهی' }]
            , listService: productService.list
            , onEdit: null
            , route: 'product'
            , globalSearch: true
        };
        product.goToPageAdd = goToPageAdd;
        function goToPageAdd() {
            $location.path('/product/add');
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('attributeController', attributeController);
    attributeController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', '$location', 'toaster', '$timeout', 'attributeService'];
    function attributeController($scope, $q, loadingService, $routeParams, $location, toaster, $timeout, attributeService) {
        let attribute = $scope;
        attribute.Model = {};
        attribute.main = {};
        attribute.main.changeState = {
            cartable: cartable,
            edit:edit
        };
        attribute.Model.Errors = [];
        attribute.state = '';
        attribute.goToPageAdd = goToPageAdd;
        attribute.addAttribute = addAttribute;
        attribute.editAttribute = editAttribute;
        attribute.grid = {
            bindingObject: attribute
            , columns: [{ name: 'Name', displayName: 'عنوان' }]
            , listService: attributeService.list
            , onEdit: attribute.main.changeState.edit
            , globalSearch: true
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        attributeService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            attribute.Model = {};
            attribute.state = 'cartable';
            $location.path('/attribute/cartable');
        }
        function add() {
            attribute.state = 'add';
            $location.path('/attribute/add');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                attribute.state = 'edit';
                attribute.Model = model;
                $location.path(`/attribute/edit/${attribute.Model.ID}`);
            }).finally(loadingService.hide);
        }
        function goToPageAdd() {
            add();
        }
        function addAttribute() {
            loadingService.show();
            return $q.resolve().then(() => {
                return attributeService.add(attribute.Model);
            }).then((result) => {
                attribute.Model = result;
                attribute.grid.getlist(false);
                toaster.pop('success', '', 'ویژگی جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editAttribute() {
            loadingService.show();
            return $q.resolve().then(() => {
                return attributeService.edit(attribute.Model);
            }).then((result) => {
                attribute.Model = result;
                attribute.grid.getlist(false);
                toaster.pop('success', '', 'ویژگی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryController', categoryController);
    categoryController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'categoryService', '$location', 'toaster', '$timeout', 'categoryMapDiscountService', 'discountService'];
    function categoryController($scope, $q, loadingService, $routeParams, categoryService, $location, toaster, $timeout, categoryMapDiscountService, discountService) {
        let category = $scope;
        category.Model = {};
        category.main = {};
        category.main.changeState = {
            cartable: cartable,
            edit:edit
        }
        category.state = '';
        category.error = {};
        category.error.show = false;;
        category.goToPageAdd = goToPageAdd;
        category.addCategory = addCategory;
        category.editCategory = editCategory;
        category.grid = {
            bindingObject: category
            , columns: [{ name: 'Title', displayName: 'عنوان' }]
            , listService: categoryService.list
            , onEdit: category.main.changeState.edit
            , route: 'category'
            , globalSearch: true
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        categoryService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function cartable() {
            category.state = 'cartable';
            $location.path('/category/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                return listDiscount();
            }).then(() => {
                category.state = 'add';
                $location.path('/category/add');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryService.get(model.ID);
            }).then((result) => {
                category.Model = result;
                return select();
            }).then(() => {
                return listDiscount();
            }).then(() => {
                category.state = 'edit';
                if (category.Model.ParentID !== "00000000-0000-0000-0000-000000000000") {
                    $('#hassubmenu').prop('checked', true);
                    $("#ParentID").prop("disabled", false);
                } else {
                    $('#hassubmenu').prop('checked', false);
                    $("#ParentID").prop("disabled", true);
                }
                $location.path(`/category/edit/${category.Model.ID}`);;
            }).finally(loadingService.hide);
        }
        function goToPageAdd() {
            add();
        }
        function addCategory() {
            loadingService.show();
            $q.resolve().then(() => {
                return categoryService.add(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    category.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                    }, 'slow');
                }
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');

            })
        }
        function editCategory() {
            loadingService.show();
            $q.resolve().then(() => {
                return categoryService.edit(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                category.error.show = true;
                $('#content > div').animate({
                    scrollTop: $('#categorySection').offset().top - $('#categorySection').offsetParent().offset().top
                }, 'slow');
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');

            }).finally(loadingService.hide);
        }
        function select() {
            categoryService.list().then((result) => {
                category.selectCategory = [];
                for (i = 0; i < result.length; i++) {
                    if (result[i].ParentID === '00000000-0000-0000-0000-000000000000') {
                        category.selectCategory.push({ Model: result[i].ID, Name: result[i].Title });
                    }
                }
            })
        }
        function listDiscount() {
            return $q.resolve().then(() => {
                return discountService.list();
            }).then((result) => {
                category.listDiscount = [];
                for (i = 0; i < result.length; i++) {
                    category.listDiscount.push({ Model: result[i].ID, Name: result[i].Name });
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('discountController', discountController);
    discountController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'discountService', '$location', 'toaster', '$timeout'];
    function discountController($scope, $q, loadingService, $routeParams, discountService, $location, toaster, $timeout) {
        let discount = $scope;
        discount.Model = {};
        discount.main = {};
        discount.main.changeState = {
            cartable: cartable,
            edit:edit
        };
        discount.Errors = [];
        discount.state = '';
        discount.goToPageAdd = goToPageAdd;
        discount.addDiscount = addDiscount;
        discount.editDiscount = editDiscount;
        discount.grid = {
            bindingObject: discount
            , columns: [{ name: 'Name', displayName: 'عنوان تخفیف' }]
            , listService: discountService.list
            , onEdit: discount.main.changeState.edit
            , globalSearch: true
        };
        init();


        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        discountService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            discount.Model = {};
            discount.state = 'cartable';
            $location.path('/discount/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                discount.state = 'add';
                $location.path('/discount/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                discount.state = 'edit';
                discount.Model = model;
                $location.path(`/discount/edit/${discount.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function goToPageAdd() {
            add();
        }
        function addDiscount() {
            loadingService.show();
            return $q.resolve().then(() => {
                return discountService.add(discount.Model)
            }).then((result) => {
                discount.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editDiscount() {
            loadingService.show();
            return $q.resolve().then(() => {
                return discountService.edit(discount.Model)
            }).then((result) => {
                discount.grid.getlist(false);
                discount.Model = result;
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function select() {
            return discountService.discountType().then((result) => {
                discount.selectDiscountType = result;
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentController', commentController);
    commentController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout'];
    function commentController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout) {
        let comment = $scope;
        comment.Model = {};
        comment.state = '';
        comment.main = {};
        comment.froalaOptionComment = froalaOption;
        comment.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        comment.editComment = editComment;
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'NameFamily', displayName: 'نام کاربر' },
                { name: 'ProductName', displayName: 'نام محصول' },
                { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: { 1: 'درحال بررسی', 2: 'تایید', 3: 'عدم تایید' } }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => {
                return 6; }
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        commentService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'cartable';
                $location.path('/comment/cartable');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'edit';
                comment.Model = model;
                commentTypeDropDown();
                $location.path(`/comment/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }
        function commentTypeDropDown() {
            commentService.commentStatusType().then((result) => {
                comment.selectCommentType = result;
            })
        }
        function editComment() {
            loadingService.show();
            return $q.resolve().then(() => {
                if (comment.Model.CommentType === 0) {
                    loadingService.hide();
                    toaster.pop('error', '', 'وضعیت نظر را تعیین نمایید');
                } else {
                    commentService.edit(comment.Model).then((result) => {
                        loadingService.hide();
                        comment.grid.getlist(false);
                        $timeout(function () {
                            toaster.pop('success', '', 'نظر ویرایش گردید');
                            cartable();

                        }, 1000);
                    }).finally(loadingService.hide);
                }
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('categoryPortalController', categoryPortalController);
    categoryPortalController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'categoryPortalService', '$location', 'toaster', '$timeout'];
    function categoryPortalController($scope, $q, loadingService, $routeParams, categoryPortalService, $location, toaster, $timeout) {
        let category = $scope;
        category.Model = {};
        category.main = {};
        category.main.changeState = {
            cartable: cartable,
            edit:edit
        }
        category.state = '';
        category.goToPageAdd = goToPageAdd;
        category.addCategory = addCategory;
        category.editCategory = editCategory;
        category.grid = {
            bindingObject: category
            , columns: [{ name: 'Title', displayName: 'عنوان مقاله' }]
            , listService: categoryPortalService.list
            , onEdit: category.main.changeState.edit
            , globalSearch: true
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        categoryPortalService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            category.state = 'cartable';
            $location.path('/category-portal/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return select();
            }).then(() => {
                category.state = 'add';
                $location.path('/category-portal/add');
            }).finally(loadingService.hide)

        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.get(model.ID);
            }).then((result) => {
                category.Model = result;
                if (category.Model.ParentID !== "00000000-0000-0000-0000-000000000000") {
                    $('#hassubmenu').prop('checked', true);
                    $("#ParentID").prop("disabled", false);
                } else {
                    $('#hassubmenu').prop('checked', false);
                    $("#ParentID").prop("disabled", true);
                }
                return select();
            }).then(() => {
                category.state = 'edit';
                $location.path(`/category-portal/edit/${category.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function goToPageAdd() {
            add();
        }
        function addCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.add(category.Model);
            }).then((result) => {
                category.grid.getlist(false);
                category.Model = result;
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت درج گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editCategory() {
            loadingService.show();
            return $q.resolve().then(() => {
                return categoryPortalService.edit(category.Model);
            }).then((result) => {
                category.Model = result;
                category.grid.getlist(false);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 100);
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function select() {
            return categoryPortalService.list().then((result) => {
                category.selectCategory = [];
                for (i = 0; i < result.length; i++) {
                    if (result[i].ParentID === '00000000-0000-0000-0000-000000000000') {
                        category.selectCategory.push({ Model: result[i].ID, Name: result[i].Title });
                    }
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('articleController', articleController);
    articleController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'articleService', '$location', 'toaster', '$timeout', 'categoryPortalService','attachmentService'];
    function articleController($scope, $q, loadingService, $routeParams, articleService, $location, toaster, $timeout, categoryPortalService, attachmentService) {
        let article = $scope;
        article.Model = {};
        article.Model.Errors = [];
        article.state = '';
        article.Model.listPicUploaded = [];
        article.pic = { type: '4', allowMultiple: false };
        article.pic.list = [];
        article.goToPageAdd = goToPageAdd;
        article.addArticle = addArticle;
        article.editArticle = editArticle;
        init();
        article.main = {};
        article.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        article.grid = {
            bindingObject: article
            , columns: [{ name: 'Title', displayName: 'عنوان مقاله' }]
            , listService: articleService.list
            , deleteService: articleService.remove
            , onAdd: article.main.changeState.add
            , onEdit: article.main.changeState.edit
            , route: 'article'
            , globalSearch: true
            , displayNameFormat: ['Title']
        };
        article.froalaOption = angular.copy(froalaOption);
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        articleService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            article.state = 'cartable';
            $location.path('/article/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropIsShow();
            }).then((result) => {
                return fillDropComment();
            }).then((result) => {
                return fillDropCategory();
            }).then(() => {
                article.state = 'add';
                $location.path('/article/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            return $q.resolve().then(() => {
                article.Model = model;
                if (article.Model.IsShow) {
                    article.Model.IsShow = 1;
                }
                else {
                    article.Model.IsShow = 0;
                }
                if (article.Model.CommentStatus) {
                    article.Model.CommentStatus = 1;
                }
                else {
                    article.Model.CommentStatus = 0;
                }
                return fillDropIsShow();
            }).then(() => {
                return fillDropComment();
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                article.Model.listPicUploaded = [];
                if (result && result.length > 0)
                    article.Model.listPicUploaded = [].concat(result);
                article.state = 'edit';
                $location.path(`/article/edit/${model.ID}`);
            })
        }
        function goToPageAdd() {
            add();
        }
        function addArticle() {
            loadingService.show();
            return $q.resolve().then(() => {
                return articleService.add(article.Model);
            }).then((result) => {
                if (article.pic.list.length) {
                    article.pics = [];
                    if (article.Model.listPicUploaded === 0) {
                        article.pics.push({ ParentID: article.Model.ID, Type: 2, FileName: article.pic.list[0], PathType: article.pic.type });
                    }
                    return attachmentService.add(article.pics);
                }
                article.Model = result;
            }).then(() => {
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    article.Model.listPicUploaded = [].concat(result);
                article.pics = [];
                article.pic.list = [];
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    article.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function editArticle() {
            loadingService.show();
            return $q.resolve().then(() => {
                return articleService.edit(article.Model);
            }).then((result) => {
                if (article.pic.list.length) {
                    article.pics = [];
                    if (article.Model.listPicUploaded.length === 0) {
                        article.pics.push({ ParentID: article.Model.ID, Type:2, FileName: article.pic.list[0], PathType: article.pic.type });
                    }
                    return attachmentService.add(article.pics);
                }
                article.Model = result;
            }).then(() => {
                return attachmentService.list({ ParentID: article.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    article.Model.listPicUploaded = [].concat(result);
                article.pics = [];
                article.pic.list = [];
                article.grid.getlist(false);
                toaster.pop('success', '', 'مقاله با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
            }).catch((error) => {
                if (!error) {
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    var listError = error.split('&&');
                    article.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#articleSection').offset().top - $('#articleSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function fillDropIsShow() {
            return articleService.typeShow().then((result) => {
                article.typeshow = result;
            })
        }
        function fillDropComment() {
            articleService.typeComment().then((result) => {
                article.typecomment = result;
            })
        }
        function fillDropCategory() {
            categoryPortalService.list().then((result) => {
                article.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        article.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('newsController', newsController);
    newsController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'newsService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService'];
    function newsController($scope, $q, loadingService, $routeParams, newsService, $location, toaster, $timeout, categoryPortalService, attachmentService) {
        let news = $scope;
        news.Model = {};
        news.main = {};
        news.main.changeState = {
            cartable: cartable,
            edit:edit
        }
        news.Model.listPicUploaded = [];
        news.Model.Errors = [];
        news.state = '';
        news.pic = { type: '3', allowMultiple: false };
        news.pic.list = [];
        news.froalaOption = angular.copy(froalaOption);
        news.goToPageAdd = goToPageAdd;
        news.addNews = addNews;
        news.editNews = editNews;
        init();
        news.grid = {
            bindingObject: news
            , columns: [{ name: 'Title', displayName: 'عنوان خبر' }]
            , listService: newsService.list
            , deleteService: newsService.remove
            , onEdit: news.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Title']
        };
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        newsService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            news.state = 'cartable';
            $location.path('/news/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropIsShow();
            }).then((result) => {
                return fillDropComment();
            }).then((result) => {
                return fillDropCategory();
            }).then(() => {
                news.state = 'add';
                $location.path('/news/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            return $q.resolve().then(() => {
                news.Model = angular.copy(model);
                if (news.Model.IsShow) {
                    news.Model.IsShow = 1;
                }
                else {
                    news.Model.IsShow = 0;
                }
                if (news.Model.CommentStatus) {
                    news.Model.CommentStatus = 1;
                }
                else {
                    news.Model.CommentStatus = 0;
                }
                return fillDropIsShow();
            }).then(() => {
                return fillDropComment();
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    news.Model.listPicUploaded = [].concat(result);
            }).then(() => {
                news.state = 'edit';
                $location.path(`/news/edit/${model.ID}`);
            })
        }
        function goToPageAdd() {
            add();
        }
        function addNews() {
            loadingService.show();
            return $q.resolve().then(() => {
                newsService.add(news.Model).then((result) => {
                    if (news.pic.list.length) {
                        news.pics = [];
                        if (!news.Model.listPicUploaded) {
                            news.pics.push({ ParentID: news.Model.ID, Type: 1, FileName: news.pic.list[0], PathType: news.pic.type });
                        }
                        return attachmentService.add(news.pics);
                    }
                    news.Model = result;
                }).then((result) => {
                    return attachmentService.list({ ParentID: news.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        news.Model.listPicUploaded = [].concat(result);
                    news.pics = [];
                    news.pic.list = [];
                    news.grid.getlist(false);
                    toaster.pop('success', '', 'خبر جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        news.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function editNews() {
            loadingService.show();
            return $q.resolve().then(() => {
                return newsService.edit(news.Model);
            }).then((result) => {
                if (news.pic.list.length) {
                    news.pics = [];
                    if (!news.Model.listPicUploaded) {
                        news.pics.push({ ParentID: news.Model.ID, Type: 1, FileName: news.pic.list[0], PathType: news.pic.type });
                    }
                    return attachmentService.add(news.pics);
                }
                news.Model = result;
            }).then((result) => {
                return attachmentService.list({ ParentID: news.Model.ID });
            }).then((result) => {
                if (result && result.length > 0)
                    news.Model.listPicUploaded = [].concat(result);
                news.pics = [];
                news.pic.list = [];
                news.grid.getlist(false);
                toaster.pop('success', '', 'خبر جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);//return cartable
            }).catch((error) => {
                if (!error) {
                    var listError = error.split('&&');
                    news.Model.Errors = [].concat(listError);
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                } else {
                    $('#content > div').animate({
                        scrollTop: $('#newsSection').offset().top - $('#newsSection').offsetParent().offset().top
                    }, 'slow');
                }

                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            }).finally(loadingService.hide);
        }
        function fillDropIsShow() {
            return newsService.typeShow().then((result) => {
                news.typeshow = result;
            })
        }
        function fillDropComment() {
            newsService.typeComment().then((result) => {
                news.typecomment = result;
            })
        }
        function fillDropCategory() {
            categoryPortalService.list().then((result) => {
                news.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        news.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('pagesController', pagesController);
    pagesController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'pagesService', '$location', 'toaster', '$timeout', 'toolsService'];
    function pagesController($scope, $q, loadingService, $routeParams, pagesService, $location, toaster, $timeout, toolsService) {
        let pages = $scope;
        pages.Model = {};
        pages.main = {};
        pages.state = 'cartable';
        pages.goToPageAdd = goToPageAdd;
        pages.addPages = addPages;
        pages.editPages = editPages;
        pages.main.changeState = {
            cartable: cartable,
            edit:edit
        }
            //< i class='fa fa-plus tgrid-action' ng-click='cellTemplateScope.add(row.branch)' title ='افزودن'></i >
            //< i class='fa fa-trash tgrid-action' ng-click='cellTemplateScope.remove(row.branch)' title ='حذف'></i >
        pages.tree = {
            data: []
            , colDefs: [
                { field: 'Title', displayName: 'عنوان' }
                , { field: 'RouteUrl', displayName: 'آدرس' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div style='float: left'>
                            <i class='fa fa-pencil tgrid-action' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: view
                        //add: addFirst,
                        //remove:remove,

                    }
                }
            ]
            , expandingProperty: {
                field: "Title"
                , displayName: "عنوان"
            }
        };
        pagesService.list().then((result) => {
            setTreeObject(result);
        });
        init();
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        pagesService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                    case 'view':
                        pagesService.get($routeParams.id).then((result) => {
                            view(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }
        function cartable() {
            pages.Model = {};
            pages.state = 'cartable';
            $location.path('pages/cartable');
        }
        function add(parent) {
            parent = parent || {};
            pages.Model = { ParentID: parent.ID };
            pages.state = 'add';
            $location.path('/pages/add');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.state = 'edit';
                pages.Model = model;
                $location.path(`/pages/edit/${pages.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function addFirst(parent) {
            pages.state = 'add';
            goToPageAdd(parent);
        }
        function goToPageAdd(parent) {
            add(parent);
        }
        function addPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.add(pages.Model)
            }).then((result) => {
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editPages() {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.edit(pages.Model)
            }).then((result) => {
                pages.Model = result;
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function setTreeObject(items) {
            items.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            pages.tree.data = toolsService.getTreeObject(items, 'Node', 'ParentNode', '/');
        }
        function remove(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return pagesService.remove(model.ID);
            }).then((result) => {
                return pagesService.list();
            }).then((result) => {
                setTreeObject(result);
            }).finally(loadingService.hide);
        }
        function view(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                pages.state = 'view';
                pages.Model = model;
                $location.path(`/pages/view/${pages.Model.ID}`);
            }).finally(loadingService.hide);

        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('menuController', menuController);
    menuController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'menuService', '$location', 'toaster', '$timeout', 'toolsService'];
    function menuController($scope, $q, loadingService, $routeParams, menuService, $location, toaster, $timeout, toolsService) {
        let menu = $scope;
        menu.Model = {};
        menu.list = [];
        menu.lists = [];
        menu.state = 'cartable';
        menu.goToPageAdd = goToPageAdd;
        menu.addMenu = addMenu;
        menu.editMenu = editMenu;
        menu.changeState = {
            cartable: cartable,
            edit:edit
        }
        menu.tree = {
            data: []
            , colDefs: [
                { field: 'Name', displayName: 'عنوان' }
                , {
                    field: ''
                    , displayName: ''
                    , cellTemplate: (
                        `<div style='float: left'>
                            <i class='fa fa-plus tgrid-action' ng-click='cellTemplateScope.add(row.branch)' title='افزودن'></i>
                            <i class='fa fa-pencil tgrid-action' ng-click='cellTemplateScope.edit(row.branch)' title='ویرایش'></i>
                            <i class='fa fa-trash tgrid-action' ng-click='cellTemplateScope.remove(row.branch)' title='حذف'></i>
                        </div>`)
                    , cellTemplateScope: {
                        edit: edit,
                        add: addFirst
                    }
                }
            ]
            , expandingProperty: {
                field: "Name"
                , displayName: "عنوان"
            }
        };
        init();


        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        menuService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            menuService.list().then((result) => {
                setTreeObject(result);
            });
            menu.state = 'cartable';
            $location.path('menu/cartable');
        }
        function add(parent) {
            parent = parent || {};
            menu.Model = { ParentID: parent.ID };
            menu.state = 'add';
            $location.path('/menu/add');
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                menu.Model = model;
                return menuService.getbyParentNode({ ParentNode: model.ParentNode });
            }).then((result) => {
                menu.state = 'edit';
                menu.Model.ParentID = result.ID;
                $location.path(`/menu/edit/${menu.Model.ID}`);
            }).finally(loadingService.hide);

        }
        function addFirst(parent) {
            menu.state = 'add';
            goToPageAdd(parent);
        }
        function goToPageAdd(parent) {
            add(parent);
        }
        function addMenu() {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.add(menu.Model)
            }).then((result) => {
                toaster.pop('success', '', 'منو جدید با موفقیت اضافه گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function editMenu() {
            loadingService.show();
            return $q.resolve().then(() => {
                return menuService.edit(menu.Model)
            }).then((result) => {
                menu.Model = result;
                toaster.pop('success', '', 'دسته بندی جدید با موفقیت ویرایش گردید');
                loadingService.hide();
                $timeout(function () {
                    cartable();
                }, 1000);
            }).catch((error) => {
                toaster.pop('error', '', 'خطا');
                loadingService.hide();
            }).finally(loadingService.hide);
        }
        function setTreeObject(items) {
            items.map((item) => {
                if (item.ParentNode === '/')
                    item.expanded = true;
            });
            menu.tree.data = toolsService.getTreeObject(items, 'Node', 'ParentNode', '/');
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('sliderController', sliderController);
    sliderController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'sliderService', '$location', 'toaster', '$timeout', 'attachmentService'];
    function sliderController($scope, $q, loadingService, $routeParams, sliderService, $location, toaster, $timeout, attachmentService) {
        let slider = $scope;
        slider.Model = {};
        slider.Model.listPicUploaded = [];
        slider.Model.Errors = [];
        slider.state = '';
        slider.pic = { type: '5', allowMultiple: false };
        slider.pic.list = [];
        slider.main = {};
        slider.main.changeState = {
            cartable: cartable,
            edit: edit
        }
        slider.grid = {
            bindingObject: slider
            , columns: [{ name: 'Title', displayName: 'عنوان اسلایدر' }]
            , listService: sliderService.list
            , deleteService: sliderService.remove
            , onEdit: edit
            , route: 'slider'
            , globalSearch: true
            , displayNameFormat: ['Title']
        };
        slider.goToPageAdd = goToPageAdd;
        slider.addSlider = addSlider;
        slider.editSlider = editSlider;
        init();
        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        loadingService.hide();
                        break;
                    case 'add':
                        add();
                        loadingService.hide();
                        break;
                    case 'edit':
                        sliderService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        loadingService.hide();
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            slider.state = 'cartable';
            $location.path('/slider/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropTypeEnable();
            }).then(() => {
                slider.state = 'add';
                $location.path('/slider/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            return $q.resolve().then(() => {
                slider.Model = model;
                return fillDropTypeEnable();
            }).then(() => {
                return attachmentService.list({ ParentID: slider.Model.ID });
            }).then((result) => {
                slider.Model.listPicUploaded = [];
                if (result && result.length > 0)
                    slider.Model.listPicUploaded = [].concat(result);
            }).then(() => {
                slider.state = 'edit';
                $location.path(`/slider/edit/${model.ID}`);
            })
        }

        function goToPageAdd() {
            add();
        }
        function fillDropTypeEnable() {
            return sliderService.typeEnable().then((result) => {
                slider.typeEnable = result;
            })
        }
        function addSlider() {
            loadingService.show();
            return $q.resolve().then(() => {
                sliderService.add(slider.Model).then((result) => {
                    slider.Model = result;
                    slider.Model.listPicUploaded = [];
                    if (slider.pic.list.length) {
                        slider.pics = [];
                        if (slider.Model.listPicUploaded.length === 0) {
                            slider.pics.push({ ParentID: slider.Model.ID, Type: 2, FileName: slider.pic.list[0], PathType: slider.pic.type });
                        }
                        return attachmentService.add(slider.pics);
                    }
                }).then((result) => {
                    return attachmentService.list({ ParentID: slider.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        slider.Model.listPicUploaded = [].concat(result);
                    slider.pics = [];
                    slider.pic.list = [];
                    slider.grid.getlist();
                    toaster.pop('success', '', 'اسلایدر جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (error && error.length > 0) {
                        var listError = error.split('&&');
                        slider.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function editSlider() {
            loadingService.show();
            return $q.resolve().then(() => {
                sliderService.edit(slider.Model).then((result) => {
                    if (slider.pic.list.length) {
                        slider.pics = [];
                        if (slider.Model.listPicUploaded.length === 0) {
                            slider.pics.push({ ParentID: slider.Model.ID, Type: 2, FileName: slider.pic.list[0], PathType: slider.pic.type });
                        }
                        return attachmentService.add(slider.pics);
                    }
                    //slider.Model = result;
                }).then((result) => {
                    return attachmentService.list({ ParentID: slider.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        slider.Model.listPicUploaded = [].concat(result);
                    slider.pics = [];
                    slider.pic.list = [];
                    slider.grid.getlist();
                    toaster.pop('success', '', 'اسلایدر جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        slider.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#sliderSection').offset().top - $('#sliderSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------
    app.controller('generalSettingController', generalSettingController);
    generalSettingController.$inject = ['$scope', 'loadingService', 'generalSettingService', 'toaster', '$q'];
    function generalSettingController($scope, loadingService, generalSettingService, toaster, $q) {
        let setting = $scope;
        setting.Model = {};
        setting.editSetting = editSetting;
        init();

        function init() {
            loadingService.show();
            return $q.resolve().then(() => {
                return generalSettingService.getSetting();
            }).then((result) => {
                setting.Model = result;
            }).catch((error) => {
                toaster.pop('error', '', 'خطا در بازیابی اطلاعات');
            }).finally(loadingService.hide);
        }

        function editSetting() {
            loadingService.show();
            return $q.resolve().then(() => {
                return generalSettingService.edit(setting.Model);
            }).then(() => {
                toaster.pop('success', '', 'تنظیمات با موفقیت ذخیره گردید');
                init();
            }).finally(loadingService.hide);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('eventsController', eventsController);
    eventsController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'eventsService', '$location', 'toaster', '$timeout', 'categoryPortalService', 'attachmentService'];
    function eventsController($scope, $q, loadingService, $routeParams, eventsService, $location, toaster, $timeout, categoryPortalService, attachmentService) {
        let events = $scope;
        events.Model = {};
        events.main = {};
        events.Model.listPicUploaded = [];
        events.Model.Errors = [];
        events.state = '';
        events.pic = { type: '8', allowMultiple: false };
        events.pic.list = [];
        events.main.changeState = {
            cartable: cartable,
            edit: edit,
            add:add
        }
        events.froalaOption = angular.copy(froalaOption);
        events.goToPageAdd = goToPageAdd;
        events.addEvents = addEvents;
        events.editEvents = editEvents;
        init();
        events.main = {};
        events.main.changeState = {
            add: add,
            edit: edit,
            cartable: cartable
        }
        events.grid = {
            bindingObject: events
            , columns: [{ name: 'Title', displayName: 'عنوان رویداد' }]
            , listService: eventsService.list
            , deleteService: eventsService.remove
            , onEdit: events.main.changeState.edit
            , globalSearch: true
            , displayNameFormat: ['Title']
        };
        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'add':
                        add();
                        break;
                    case 'edit':
                        eventsService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);
        }
        function cartable() {
            events.state = 'cartable';
            $location.path('/events/cartable');
        }
        function add() {
            loadingService.show();
            return $q.resolve().then(() => {
                return fillDropIsShow();
            }).then((result) => {
                return fillDropComment();
            }).then((result) => {
                return fillDropCategory();
            }).then(() => {
                events.state = 'add';
                $location.path('/events/add');
            }).finally(loadingService.hide);

        }
        function edit(model) {
            return $q.resolve().then(() => {
                events.Model = angular.copy(model);
                if (events.Model.IsShow) {
                    events.Model.IsShow = 1;
                }
                else {
                    events.Model.IsShow = 0;
                }
                if (events.Model.CommentStatus) {
                    events.Model.CommentStatus = 1;
                }
                else {
                    events.Model.CommentStatus = 0;
                }
                return fillDropIsShow();
            }).then(() => {
                return fillDropComment();
            }).then(() => {
                return fillDropCategory();
            }).then(() => {
                return attachmentService.list({ ParentID: events.Model.ID });
            }).then((result) => {
                events.Model.listPicUploaded = [];
                if (result && result.length > 0)
                    events.Model.listPicUploaded = [].concat(result);
            }).then(() => {
                events.state = 'edit';
                $location.path(`/events/edit/${model.ID}`);
            })
        }
        function goToPageAdd() {
            add();
        }
        function addEvents() {
            loadingService.show();
            return $q.resolve().then(() => {
                eventsService.add(events.Model).then((result) => {
                    if (events.pic.list.length) {
                        events.pics = [];
                        if (events.Model.listPicUploaded.length === 0) {
                            events.pics.push({ ParentID: events.Model.ID, Type: 2, FileName: events.pic.list[0], PathType: events.pic.type });
                        }
                        return attachmentService.add(events.pics);
                    }
                    events.Model = result;
                }).then((result) => {
                    return attachmentService.list({ ParentID: events.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        events.Model.listPicUploaded = [].concat(result);
                    events.pics = [];
                    events.pic.list = [];
                    events.grid.getlist(false);
                    toaster.pop('success', '', 'رویداد جدید با موفقیت اضافه گردید');
                    loadingService.hide();
                    $timeout(function () {
                        events.main.changeState.cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        events.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function editEvents() {
            loadingService.show();
            return $q.resolve().then(() => {
                eventsService.edit(events.Model).then((result) => {
                    if (events.pic.list.length) {
                        events.pics = [];
                        if (events.Model.listPicUploaded.length === 0) {
                            events.pics.push({ ParentID: events.Model.ID, Type: 2, FileName: events.pic.list[0], PathType: events.pic.type });
                        }
                        return attachmentService.add(events.pics);
                    }
                    events.Model = result;
                }).then((result) => {
                    return attachmentService.list({ ParentID: events.Model.ID });
                }).then((result) => {
                    if (result && result.length > 0)
                        events.Model.listPicUploaded = [].concat(result);
                    events.pics = [];
                    events.pic.list = [];
                    events.grid.getlist(false);
                    toaster.pop('success', '', 'رویداد جدید با موفقیت ویرایش گردید');
                    loadingService.hide();
                    $timeout(function () {
                        cartable();
                    }, 1000);//return cartable
                }).catch((error) => {
                    if (!error) {
                        var listError = error.split('&&');
                        events.Model.Errors = [].concat(listError);
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    } else {
                        $('#content > div').animate({
                            scrollTop: $('#eventsSection').offset().top - $('#eventsSection').offsetParent().offset().top
                        }, 'slow');
                    }

                    toaster.pop('error', '', 'خطایی اتفاق افتاده است');
                }).finally(loadingService.hide);
            })
        }
        function fillDropIsShow() {
            return eventsService.typeShow().then((result) => {
                events.typeshow = result;
            })
        }
        function fillDropComment() {
            eventsService.typeComment().then((result) => {
                events.typecomment = result;
            })
        }
        function fillDropCategory() {
            categoryPortalService.list().then((result) => {
                events.typecategory = [];
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ParentID !== '00000000-0000-0000-0000-000000000000') {
                        events.typecategory.push({ Name: result[i].Title, Model: result[i].ID });
                    }
                }
            })
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------
    app.controller('commentPortalController', commentPortalController);
    commentPortalController.$inject = ['$scope', '$q', 'loadingService', '$routeParams', 'commentService', '$location', 'toaster', '$timeout'];
    function commentPortalController($scope, $q, loadingService, $routeParams, commentService, $location, toaster, $timeout) {
        let comment = $scope;
        comment.Model = {};
        comment.Search = {};
        comment.state = '';
        comment.main = {};
        comment.froalaOptionComment = froalaOptionComment;
        comment.main.changeState = {
            cartable: cartable,
            edit:edit
        }
        comment.editComment = editComment;
        comment.changeDrop = changeDrop;
        comment.grid = {
            bindingObject: comment
            , columns: [{ name: 'NameFamily', displayName: 'نام کاربر' },
                { name: 'ProductName', displayName: 'عنوان' },
                { name: 'CommentType', displayName: 'وضعیت نظر', type: 'enum', source: {1:'درحال بررسی',2:'تایید',3:'عدم تایید'} }]
            , listService: commentService.list
            , onEdit: comment.main.changeState.edit
            , globalSearch: true
            , showRemove: true
            , options: () => { return comment.Search.CommentForType }
        };
        init();

        function init() {
            loadingService.show();
            $q.resolve().then(() => {
                if (!comment.Search.CommentForType)
                    commentForTypeDropDown();
                comment.Search.CommentForType = 3;
                switch ($routeParams.state) {
                    case 'cartable':
                        cartable();
                        break;
                    case 'edit':
                        commentService.get($routeParams.id).then((result) => {
                            edit(result);
                        })
                        break;
                }
            }).finally(loadingService.hide);

        }

        function cartable() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.state = 'cartable';
                $location.path('/comment-portal/cartable');
            }).finally(loadingService.hide);
        }
        function edit(model) {
            loadingService.show();
            return $q.resolve().then(() => {
                return commentTypeDropDown();
            }).then(() => {
                comment.state = 'edit';
                comment.Model = model;
                $location.path(`/comment-portal/edit/${model.ID}`);
            }).finally(loadingService.hide);
        }
        function commentTypeDropDown() {
            commentService.commentStatusType().then((result) => {
                comment.selectCommentType = result;
            })
        }
        function commentForTypeDropDown() {
            commentService.commentForType().then((result) => {
                comment.selectCommentForType = result;
            })
        }
        function editComment() {
            loadingService.show();
            return $q.resolve().then(() => {
                if (comment.Model.CommentType === 0) {
                    loadingService.hide();
                    toaster.pop('error', '', 'وضعیت نظر را تعیین نمایید');
                } else {
                    commentService.edit(comment.Model).then((result) => {
                        comment.grid.getlist(false);
                        loadingService.hide();
                        $timeout(function () {
                            toaster.pop('success', '', 'نظر ویرایش گردید');
                            cartable();

                        }, 1000);
                    }).finally(loadingService.hide);
                }
            }).catch((error) => {
                toaster.pop('error', '', 'خطایی اتفاق افتاده است');
            })
        }
        function changeDrop() {
            loadingService.show();
            return $q.resolve().then(() => {
                comment.grid.getlist(false);
            }).finally(loadingService.hide);
            
        }
    }
})();